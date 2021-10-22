--
-- This lua script runs in the FCEUX emulator and allows 
-- the autotracker to query the emulator for memory values.
--
-- The script and autotracker connect via a socket.  The
-- autotracker is the server and this script is the client.
--
-- This script is based on the QUsb2Snes luabridge.lua script.
--

--
-- Enum of the supported emulators.
--
Emulators = {
  BIZHAWK = 1,
  FCEUX = 2,
  MESEN = 3,
  UNKNOWN = 4,
}

-- Script variables
local socket = require("socket.core")
local socketConnection
local connected = false
local disconnectRequested = false
local emuAddress = '127.0.0.1'
local port = 4242
local emulator = Emulators.UNKNOWN


--
-- Try to determine which emulator this script is running in.
--
function determineEmulator()
  -- Bizhawk has the event library.
  -- FCEUX has the FCEU library.
  -- Mesen seems to put all functionality in the emu package, so
  -- use that as the default for now. 
  -- Need to find a better way to detect Mesen.
  if event then
    return Emulators.BIZHAWK
  elseif FCEU then
    return Emulators.FCEUX
  else
    return Emulators.MESEN
  end
  
end

--
-- Handle memory reads on FCEUX and Bizhawk.
--
function readFceux(address, length)
  byteRange = memory.readbyterange(address, length)
  return {string.byte(byteRange, 1, #byteRange)}
end

--
-- Handle memory reads on Bizhawk
--
function readBizhawk(address, length)
  return memory.readbyterange(address-1, length+1)
end

--
-- Handle memory reads on Mesen.
-- Mesen does not have a bulk read function, so 
-- build up the table byte by byte.
--
function readMesen(address, length)
  byteRange = {}
  for i = 0, length-1 do
    table.insert(byteRange, emu.read(address + i, emu.memType.cpuDebug))
  end
  --return {string.byte(byteRange, 1, #byteRange)}
  return byteRange
end

-- Handle incoming commands from the socket
local function onMessage(message)
  -- Break the command string up into component parts.
  -- Parts are separated by pipe characters '|'
  --
  -- The first part is the command, subsequent parts are
  -- arguments to the command.
  --   ie: Read|188|4
  --       Will read four bytes from NES RAM starting at 0xBC (188).
  local parts = {}
  for part in string.gmatch(message, '([^|]+)') do
    parts[#parts + 1] = part
  end

  if parts[1] == "Read" then
    local address = tonumber(parts[2])
    local length = tonumber(parts[3])
    local dataTable
    if emulator == Emulators.MESEN then
      dataTable = readMesen(address, length)
    elseif emulator == Emulators.FCEUX then
      dataTable = readFceux(address, length)
    elseif emulator == Emulators.BIZHAWK then
      dataTable = readBizhawk(address, length)
    end
    -- Send back a json formatted message with the requested memory data
    socketConnection:send("{\"data\": [" .. table.concat(dataTable, ",") .. "]}\n")
  elseif parts[1] == "Exit" then
    disconnectRequested = true
  end

end

-- Handle connecting and receiving data over the socket
-- connection to the autotracker.
handleSocket = function()
  -- If we're not connected, try to connect to the autotracker server
  if not connected then
    socketConnection, err = socket:tcp()
    if err ~= nil then
      emu.print(err)
      return
    end
    
    local returnCode, errorMessage = socketConnection:connect(emuAddress, port)
    if (returnCode == nil) then
      print("Error while connecting: " .. errorMessage)
      connected = false
      return
    end
    
    socketConnection:settimeout(0)
    connected = true
    print('Autotracker Connected')
    return
  end
  
  -- Handle incoming data on the socket
  local s, status = socketConnection:receive('*l')
  if s then
    onMessage(s)
  end
  
  -- Handle disconnecting
  if disconnectRequested or status == 'closed' then
    print('Autotracker disconnected')
    socketConnection:close()
    connected = false
    disconnectRequested = false
    return
  end
end

emulator = determineEmulator()
-- Main loop
if emulator == Emulators.MESEN then
  emu.addEventCallback(handleSocket, emu.eventType.endFrame)
else
  while true do
    handleSocket()
    emu.frameadvance();
  end
end