namespace IRC;

using System.Net;
using System.Net.Sockets;
using System.Text;

public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

public delegate void MotdReceivedEventHandler(object sender, MotdReceivedEventArgs e);

public partial class Client {
	private const int BUFFER_SIZE = 1024;

	private readonly IPEndPoint remoteEndPoint;

	private readonly Socket socket;

	private readonly byte[] buffer = new byte[BUFFER_SIZE];

	private string incompleteMessage;

	private readonly StringBuilder motd = new();

	private string nick;

	private string user;

	public bool LoggedIn {
		get;
		private set;
	}

	public event MessageReceivedEventHandler EventMessageReceived;

	public event MotdReceivedEventHandler EventMotdReceived;
}