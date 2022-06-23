namespace IRC;

using System.Net;
using System.Net.Sockets;
using System.Text;

public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
	
public delegate void MotdReceivedEventHandler(object sender, MotdReceivedEventArgs e);

public partial class Client {
	
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

}
