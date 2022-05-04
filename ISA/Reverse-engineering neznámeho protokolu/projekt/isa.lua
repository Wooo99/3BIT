--set for protocol
isa_protocol = Proto("ISA",  "ISA protocol")
--set what we will add after
data_length = ProtoField.int32("isaProtocol.mesasge_length", "Message length", base.dec)
source = ProtoField.string("isaProtocol.source", "Source", base.ACII)
destination = ProtoField.string("isaProtocol.destination", "Destination", base.ASCII)
status = ProtoField.string("isaProtocol.status", "Status", base.ACII)
message = ProtoField.string("isaProtocol.message", "Message", base.ASCII)


-- set that fields
isa_protocol.fields = { data_length, source, destination, message, status }

function isa_protocol.dissector(buffer, pinfo, tree)
  length = buffer:len() --let of our payload

  pinfo.cols.protocol = isa_protocol.name
  local subtree = tree:add(isa_protocol, buffer(), "ISA data") -- subtree where we will print our informations for customer
  
  subtree:add(data_length, length) --print length
  if   tostring(buffer(1,2)) == "6f6b" or tostring(buffer(1,3)) == "657272" then -- check start of message, "ok" or "err"
	subtree:add(source,"Server") 
	subtree:add(destination,"Client")
	if   tostring(buffer(1,2)) == "6f6b" then
		subtree:add(status,"ok") -- status of message
		i = 0
		while  tostring(buffer(i+4,1)) == "28"
		do i = i + 1 end
		local message_data = buffer(i+4,length-2*i-5)
		subtree:add(message,message_data) -- print message
	else subtree:add(status,"error")
		i = 0
		while  tostring(buffer(i+5,1)) == "28"
		do i = i + 1 end
		local message_data = buffer(i+5,length-2*i-6)
		subtree:add(message,message_data)
	end
  else 	
	subtree:add(source,"Client")
	subtree:add(destination,"Server")
	subtree:add(status,"ok")
	i = 0
	while tostring(buffer(i,1)) == "28" -- delete "(" and ")"
	do
		i = i + 1
	end
	if i > 2 then
		message_data = buffer(i,length-i)
		subtree:add(message,message_data)
	else
		message_data = buffer(i,length-i-1)
		subtree:add(message,message_data)
  end
end



--rename our communication 
local tcp_port = DissectorTable.get("tcp.port")
tcp_port:add(32323, isa_protocol)