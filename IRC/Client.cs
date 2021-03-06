namespace IRC;

using System.Net;
using System.Net.Sockets;
using System.Text;
using utils;

public partial class Client
{
	public string HostName;
	public int Port;
	public Client(string hostName, int port = 6667) {
		var ipAddressFunc = () => {
			if (IPAddress.TryParse(hostName, out var ip)) return ip;
			var host = Dns.GetHostEntry(hostName);
			return host.AddressList[0];
		};

		var ipAddress = ipAddressFunc();

		this.remoteEndPoint = new IPEndPoint(ipAddress, port);

		this.socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		this.ServerAddress = hostName + ":" + port;
		this.HostName = hostName;
		this.Port = port;
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
		this.LoggedIn = false;
		this.socket.Disconnect(false);
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
		try {
			var messageObj = Message.parse(message);
			if (messageObj == null)
				return;
			
			this.HandleMessage(messageObj);
		} catch (Exception e) {
			Console.WriteLine(e);
			this.LoggedIn = false;
			this.socket.Disconnect(true);
			this.EventError?.Invoke(this, new ErrorEventArgs(e));
		}
	}

	private void HandleMessage(Message message) {
		Console.WriteLine("Received: " + message);

		switch (message.Command) {
			case MessageType.PING:
				this.HandlePing(message);
				break;
			case MessageType.RPL_MOTDSTART:
				this.HandleRplMotdStart(message);
				break;
			case MessageType.RPL_MOTD:
				this.HandleRplMotd(message);
				break;
			case MessageType.RPL_ENDOFMOTD:
				this.HandleRplEndOfMotd(message);
				break;
			case MessageType.PRIVMSG:
				this.HandlePrivMsg(message);
				break;
			case MessageType.JOIN:
				this.HandleJoin(message);
				break;
			case MessageType.RPL_NAMREPLY:
				this.HandleRplNamReply(message);
				break;
			case MessageType.RPL_ENDOFNAMES:
				this.HandleRplEndOfNames(message);
				break;
			case MessageType.RPL_LIST:
				this.HandleRplList(message);
				break;
			case MessageType.RPL_LISTEND:
				this.HandleRplListEnd(message);
				break;
			case MessageType.NICK:
				this.HandleNick(message);
				break;
			case MessageType.ERR_NICKNAMEINUSE:
				this.HandleErrNicknameInUse(message);
				break;
			case MessageType.ERR_CHANNELISFULL:
			case MessageType.ERR_BANNEDFROMCHAN:
			case MessageType.ERR_INVITEONLYCHAN:
			case MessageType.ERR_BADCHANNELKEY:
			case MessageType.ERR_NOSUCHCHANNEL:
				this.HandleCannotJoinChannel(message);
				break;
			case MessageType.ERR_NOSUCHNICK:
				this.HandleErrNoSuchNick(message);
				break;
			case MessageType.ERR_NOTONCHANNEL:
				this.HandleErrNotOnChannel(message);
				break;
			case MessageType.PART:
				this.HandlePart(message);
				break;
		}
	}
}