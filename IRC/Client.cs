namespace IRC;

using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client {
	public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

	private string incompleteMessage;

	private readonly IPEndPoint remoteEndPoint;

	private readonly Socket socket;

	public Client(string hostName, int port = 6667) {
		var host = Dns.GetHostEntry(hostName);
		var ipAddress = host.AddressList[0];
		this.remoteEndPoint = new IPEndPoint(ipAddress, port);

		this.socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		this.ServerAddress = hostName + ":" + port;
	}

	public string ServerAddress { get; }

	public bool Connected => this.socket.Connected;

	public bool Connect() {
		if (this.socket.Connected)
			return true;
		try {
			this.socket.Connect(this.remoteEndPoint);
		} catch (SocketException) {
			return false;
		}

		// Receive data asynchronously
		this.socket.BeginReceive(new byte[1024], 0, 1024, SocketFlags.None, this.ReceiveCallback, this.ServerAddress);

		return true;
	}

	public void Disconnect() {
		if (!this.socket.Connected)
			return;
		this.socket.Disconnect(false);
	}

	public bool Send(string message) {
		if (!this.socket.Connected)
			return false;
		var data = Encoding.UTF8.GetBytes(message);
		var sent = this.socket.Send(data);
		return sent == data.Length;
	}

	public string Receive() {
		if (!this.socket.Connected)
			return "";
		var buffer = new byte[1024];
		var received = this.socket.Receive(buffer);
		return Encoding.UTF8.GetString(buffer, 0, received);
	}

	private void ReceiveCallback(IAsyncResult ar) {
		var received = this.socket.EndReceive(ar);
		if (received == 0)
			return;
		var message = Encoding.UTF8.GetString(new byte[received]);

		// If the message is incomplete, store it and wait for the next message
		message = this.incompleteMessage + message;
		this.incompleteMessage = "";

		// If the message is complete, process it
		if (message.Contains("\r\n")) {
			var messages = message.Split('\n');
			foreach (var msg in messages) {
				if (msg.Length == 0)
					continue;
				if (msg.EndsWith('\r')) {
					this.HandleMessage(msg[..^1]);
				} else {
					// (msg.EndsWith('\r'))
					// A complete message contains a \r\n, so the last message is incomplete
					// Store it and wait for the next message
					this.incompleteMessage = msg;
					break;
				}
			}
		}

		// Receive data asynchronously
		this.socket.BeginReceive(new byte[1024], 0, 1024, SocketFlags.None, this.ReceiveCallback, null);
	}

	private void HandleMessage(string message) {
		// Make a "string" message into a Message object
		
	}
}