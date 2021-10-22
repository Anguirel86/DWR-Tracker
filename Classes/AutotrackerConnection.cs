using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DWR_Tracker.Classes
{


  public enum ReadType
  {
    BATTLE_STATUS,
    HERO_AND_MAP_DATA,
  }

  /// <summary>
  /// This class represents a block of memory read from the emulator.  It contains functions
  /// to assist in querying specific values based on NES addressing.
  /// </summary>
  public class MemoryBlock
  {
    private int start;
    private int length;
    private byte[] memData;
    private ReadType type;

    public MemoryBlock(int start, int length, ReadType type)
    {
      this.start = start;
      this.length = length;
      this.type = type;
    }

    public ReadType GetReadType()
    {
      return type;
    }

    /// <summary>
    /// Set the byte array that represents the data of this memory block.
    /// </summary>
    /// <param name="data">Byte array containing NES memory data</param>
    public void SetMemoryBlockData(byte[] data)
    {
      if (data.Length != length)
      {
        throw new IndexOutOfRangeException("Invalid data size for this memory block.");
      }

      this.memData = data;
    }

    /// <summary>
    /// Read an unsigned byte from the memory block. Address is specified as a NES address.
    /// </summary>
    /// <param name="address">NES memory address to read</param>
    /// <returns></returns>
    public byte ReadU8(int address)
    {
      int index = address - start;

      // Verify the requested address is in the range of this memory block.
      if (index < 0 || index > (length - 1))
      {
        throw new IndexOutOfRangeException(String.Format(
          "Address {0} is out of range. Start: {1} - End: {2}", 
          address, start, (start + length - 1)));
      }

      return memData[index];
    }
  }

  /// <summary>
  /// This interface is used by classes that want to be notified when their requested
  /// memory reads have finished.
  /// </summary>
  public interface IMemoryReadListener
  {
    public void HandleMemoryRead(MemoryBlock memData);
  }


  /// <summary>
  /// This class represents a command to be sent to the emulator.
  /// </summary>
  class Command
  {
    public String command;
    public IMemoryReadListener callback;
    public MemoryBlock memory;
  }


  class JsonReadData
  {
    public IList<byte> data { get; set; }
  }

  /// <summary>
  /// This class manages the connection to the autotracker lua script
  /// running on the emulator.
  /// </summary>
  public class AutotrackerConnection
  {
    private Socket emulatorClient;
    private readonly BlockingCollection<Command> commandQueue = new BlockingCollection<Command>();
    private Command currentCommand = null;
    private readonly ManualResetEvent messageProcessing = new ManualResetEvent(false);
    private readonly ManualResetEvent SendLoopEvent = new ManualResetEvent(false);
    private Thread sendThread = null, recThread = null;

    /// <summary>
    /// Add a command to the send queue.
    /// </summary>
    /// <param name="command"></param>
    private void SendCommand(Command command)
    {
      // Only add commands if we are connected.
      if (emulatorClient != null && emulatorClient.Connected)
      {
        commandQueue.Add(command);
      }
    }

    /// <summary>
    /// Send a command to the emulator to read the specified memory segment.
    /// When the read finishes the provided IMemoryReadListener will be notified.
    /// </summary>
    /// <param name="address">Starting memory address</param>
    /// <param name="length">Number of bytes to read</param>
    /// <param name="callback">Listener to be notified when read finishes</param>
    public void ReadMemory(int address, int length, ReadType readType, IMemoryReadListener callback)
    {
      SendCommand(new Command()
      {
        callback = callback,
        command = "Read|" + address + "|" + length + "\n",
        memory = new MemoryBlock(address, length, readType)
      });
    }

    /// <summary>
    /// Start the autotracker. This allows the autotracker to accept
    /// connections from emulator clients and starts the send/receive threads.
    /// </summary>
    public void StartAutotracker()
    {
      if (sendThread == null && recThread == null)
      {
        recThread = new Thread(new ThreadStart(this.ReceiveLoop));
        sendThread = new Thread(new ThreadStart(this.SendLoop));
        sendThread.Start();
        recThread.Start();
      }
    }

    /// <summary>
    /// Check whether or not there is an emulator connected to the autotracker.
    /// </summary>
    /// <returns>True if connected to an emulator, false if not</returns>
    public bool IsConnected()
    {
      return emulatorClient != null && emulatorClient.Connected;
    }

    /// <summary>
    /// Process a message from the autotracker script running on the emulator.
    /// This message is in json format.
    /// </summary>
    /// <param name="message">json message string</param>
    private void ProcessMessage(String message)
    {
      JsonReadData json = JsonConvert.DeserializeObject<JsonReadData>(message);

      byte[] data = new byte[json.data.Count];
      json.data.CopyTo(data, 0);
      MemoryBlock memory = currentCommand.memory;
      memory.SetMemoryBlockData(data);

      currentCommand.callback.HandleMemoryRead(memory);
      messageProcessing.Set(); // Signal the send loop to send the next command
    }

    /// <summary>
    /// The main send loop of the autotracker.  Commands to the emulator are queued
    /// and sent sequentially to the emulator by this loop.
    /// </summary>
    private void SendLoop()
    {
      while (true)
      {
        if (emulatorClient != null && emulatorClient.Connected)
        {
          messageProcessing.Reset();
          currentCommand = commandQueue.Take();
          emulatorClient.Send(Encoding.ASCII.GetBytes(currentCommand.command));
         
          messageProcessing.WaitOne();
        } else
        {
          // We're waiting on a connection.
          Console.WriteLine("Send loop waiting");
          SendLoopEvent.WaitOne();
          Console.WriteLine("Send loop sending");
        }
      }
    }

    /// <summary>
    /// The main receive loop of the autotracker. This function handles a client connection from
    /// the emulator and listens for incoming messages from the socket.  Only one client can
    /// be connected to the autotracker at a time.
    /// </summary>
    private void ReceiveLoop()
    {
      // Set up the server socket
      IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
      IPAddress ipAddress = null;
      // Find the IPv4 localhost address
      foreach (IPAddress addr in ipHostInfo.AddressList)
      {
        if (addr.AddressFamily == AddressFamily.InterNetwork)
        {
          ipAddress = addr;
          break;
        }
      }
      IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 4242);
      Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

      // Buffer for data coming in over the socket
      byte[] buffer = new byte[1024];

      try
      {
        listener.Bind(localEndPoint);
        listener.Listen(10);
        StringBuilder messageBuffer = new StringBuilder();

        // Listen forever for new connections.  
        while (true)
        {
          emulatorClient = listener.Accept();
          Console.WriteLine("Emulator connected");
          SendLoopEvent.Set();

          try
          {
            while (true)
            {
              // Build up the message string until we get a full message (newline at the end of the json string)
              int bytesReceived = emulatorClient.Receive(buffer);
              messageBuffer.Append(Encoding.ASCII.GetString(buffer, 0, bytesReceived));

              int index = -1;
              for (int i = 0; i < messageBuffer.Length; i++)
              {
                if (messageBuffer[i] == '\n')
                {
                  index = i;
                  break;
                }
              }

              if (index > -1)
              {
                // We found a newline, we have a complete message to process.
                string message = messageBuffer.ToString(0, index + 1);
                ProcessMessage(message);
                messageBuffer.Remove(0, index + 1);
              }
            }
          } catch (SocketException)
          {
            // If we get any excpetion on the socket just close the connection and go back
            // to waiting for a new connection.  This can happen if the client side 
            // unexpectedly closes while we are waiting for a response.

            // Close the connection, clean up, and wait for a new connection.
            emulatorClient.Close();
            SendLoopEvent.Reset();
            messageBuffer.Clear();
            while (commandQueue.Count > 0)
            {
              commandQueue.Take();
            }
          }
        }

      } catch (Exception e)
      {
        Console.WriteLine(e);
      }

    }
  } 
}
