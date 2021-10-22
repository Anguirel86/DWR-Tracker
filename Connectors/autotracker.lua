--
-- This lua script runs in the FCEUX emulator and allows 
-- the autotracker to query the emulator for memory values.
--
-- The script and autotracker connect via a socket.  The
-- autotracker is the server and this script is the client.
--
-- This script is based on the QUsb2Snes luabridge.lua script.
--
local socket = require("socket.core")

local socketConnection
local connected = false
local disconnectRequested = false
local address = '127.0.0.1'
local port = 4242

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
    local adr = tonumber(parts[2])
    local length = tonumber(parts[3])
    byteRange = memory.readbyterange(adr, length)
    dataTable = {string.byte(byteRange, 1, #byteRange)}
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
    
    local returnCode, errorMessage = socketConnection:connect(address, port)
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


-- Main loop
while true do
  handleSocket()
  emu.frameadvance();
end -- End main loop