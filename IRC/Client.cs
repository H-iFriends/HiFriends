namespace IRC;

using System.Net;
using System.Net.Sockets;
using System.Text;
using utils;

public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
	
public delegate void MotdReceivedEventHandler(object sender, MotdReceivedEventArgs e);

public class Client {
	
	public event MessageReceivedEventHandler EventMessageReceived;
	
	public event MotdReceivedEventHandler EventMotdReceived;

	private readonly IPEndPoint remoteEndPoint;

	private readonly Socket socket;

	private string incompleteMessage;

	private string nick;

	private string user;
	
	private bool loggedIn;
	
	private const int BUFFER_SIZE = 1024;
	
	private byte[] buffer = new byte[BUFFER_SIZE];
	
	private StringBuilder motd = new StringBuilder();

	public bool LoggedIn => this.loggedIn;

	public Client(string hostName, int port = 6667) {
		var ipAddressFunc = () => {
			if (IPAddress.TryParse(hostName, out var ip)) {
				return ip;
			}
			var host = Dns.GetHostEntry(hostName);
			return host.AddressList[0];
		};
		
		var ipAddress = ipAddressFunc();
		
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
		this.socket.BeginReceive(this.buffer, 0, BUFFER_SIZE, SocketFlags.None, this.ReceiveCallback, null);

		return true;
	}

	public void Disconnect() {
		if (!this.socket.Connected)
			return;
		this.socket.Disconnect(false);
	}

	public bool Login(string nick, string user, string realName = "", string? password = null) {
		if (!this.socket.Connected || string.IsNullOrWhiteSpace(nick) || string.IsNullOrWhiteSpace(user))
			return false;

		if (string.IsNullOrWhiteSpace(realName))
			realName = "HiFriends IRC Client User";
		
		this.nick = nick;
		this.user = user;
		
		if (!this.SendMessage("HELLO"))
			return false;

		if (!string.IsNullOrWhiteSpace(password) && !this.SendMessage("PASS", password))
			return false;
		
		return this.SendMessage("NICK", nick) && this.SendMessage("USER", user, "0", "*", ":" + realName);
	}

	public bool SendMessage(string command, params string[] parameters) {
		if (!this.socket.Connected)
			return false;
		var message = new StringBuilder();
		message.Append(command);
		foreach (var parameter in parameters)
			message.Append(' ').Append(parameter);
		message.Append("\r\n");
		return this.Send(message.ToString());
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

		var receivedData = this.buffer[..received];
		
		Console.WriteLine(HexDump.Get(receivedData, 0, received));
		
		var message = Encoding.UTF8.GetString(receivedData);
		
		

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
					this.ParseMessage(msg[..^1]);
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
		this.socket.BeginReceive(this.buffer, 0, BUFFER_SIZE, SocketFlags.None, this.ReceiveCallback, null);
	}

	private void ParseMessage(string message) {
		if (string.IsNullOrWhiteSpace(message))
			return;
		var messageObj = Message.parse(message);
		if (messageObj == null)
			return;
		
		this.HandleMessage(messageObj);
	}

	private void HandleMessage(Message message) {
		Console.WriteLine("Received: " + message.ToString());

		switch (message.Command) {
			case MessageType.RPL_MOTDSTART:
				HandleMotdStart(message);
				break;
			case MessageType.RPL_MOTD:
				HandleMotd(message);
				break;
			case MessageType.RPL_ENDOFMOTD:
				HandleEndOfMotd(message);
				break;
			
		}
	}
	
	private void HandleMotdStart(Message message) {
		this.motd.Clear();
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
	}
	
	private void HandleMotd(Message message) {
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
	}
	
	private void HandleEndOfMotd(Message message) {
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
		this.EventMotdReceived(this, new MotdReceivedEventArgs(this.motd.ToString()));
	}
}