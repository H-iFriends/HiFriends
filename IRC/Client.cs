using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IRC;

public class Client {
	
	private IPEndPoint remoteEndPoint;
	
	private Socket socket;
	
	public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
	
	private string incompleteMessage;

	public string ServerAddress { get; }
	
	public bool Connected => socket.Connected;

	public Client(string hostName, int port = 6667) {
		var host = Dns.GetHostEntry(hostName);
		var ipAddress = host.AddressList[0];
		remoteEndPoint = new IPEndPoint(ipAddress, port);

		socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		ServerAddress = hostName + ":" + port;
	}
	
	public bool Connect() {
		if (socket.Connected)
			return true;
		try {
			socket.Connect(remoteEndPoint);
		} catch (SocketException) {
			return false;
		}
		
		// Receive data asynchronously
		socket.BeginReceive(new byte[1024], 0, 1024, SocketFlags.None, ReceiveCallback, null);
		
		return true;
	}
	
	public void Disconnect() {
		if (!socket.Connected)
			return;
		socket.Disconnect(false);
	}
	
	public bool Send(string message) {
		if (!socket.Connected)
			return false;
		var data = Encoding.UTF8.GetBytes(message);
		var sent = socket.Send(data);
		return sent == data.Length;
	}
	
	public string Receive() {
		if (!socket.Connected)
			return null;
		var buffer = new byte[1024];
		var received = socket.Receive(buffer);
		return Encoding.UTF8.GetString(buffer, 0, received);
	}
	
	private void ReceiveCallback(IAsyncResult ar) {
		var received = socket.EndReceive(ar);
		if (received == 0)
			return;
		var message = Encoding.UTF8.GetString(new byte[received]);
		
		// If the message is incomplete, store it and wait for the next message
		message = incompleteMessage + message;
		incompleteMessage = "";
		
		// If the message is complete, process it
		if (message.Contains("\r\n")) {
			var messages = message.Split('\n');
			foreach (var msg in messages) {
				if (msg.Length == 0)
					continue;
				if (msg.EndsWith('\r'))
					HandleMessage(msg[..^1]);
				else {
					// A complete message contains a \r\n, so the last message is incomplete
					// Store it and wait for the next message
					incompleteMessage = msg;
					break;
				}
			}
		}
		
		// Receive data asynchronously
		socket.BeginReceive(new byte[1024], 0, 1024, SocketFlags.None, ReceiveCallback, null);
	}
	
	private void HandleMessage(string message) {
		
	}

}