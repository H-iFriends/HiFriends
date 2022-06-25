namespace IRC;

using System.Net;
using System.Net.Sockets;
using System.Text;
using entity;

public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

public delegate void MotdReceivedEventHandler(object sender, MotdReceivedEventArgs e);

public delegate void JoinedChannelEventHandler(object sender, JoinedChannelEventArgs e);

public delegate void UserListReceivedEventHandler(object sender, UserListReceivedEventArgs e);

public delegate void ChannelListReceivedEventHandler(object sender, ChannelListReceivedEventArgs e);

public delegate void NickChangedEventHandler(object sender, NickChangedEventArgs e);

public delegate void NicknameInUseEventHandler(object sender, NicknameInUseEventArgs e);

public delegate void CannotJoinChannelEventHandler(object sender, CannotJoinChannelEventArgs e);

public delegate void NoSuchNickEventHandler(object sender, NoSuchNickEventArgs e);

public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);

public delegate void NotOnChannelEventHandler(object sender, NotOnChannelEventArgs e);

public partial class Client {
	private const int BUFFER_SIZE = 1024;

	private readonly byte[] buffer = new byte[BUFFER_SIZE];

	private readonly List<ChannelInfo> channelListBuffer = new();

	private readonly StringBuilder motd = new();

	private readonly IPEndPoint remoteEndPoint;

	private readonly Socket socket;

	private readonly Dictionary<string, string> userListBuffer = new();

	private string incompleteMessage;

	private string nick;

	private string user;

	public bool LoggedIn { get; private set; }

	public event MessageReceivedEventHandler EventMessageReceived;

	public event MotdReceivedEventHandler EventMotdReceived;

	public event JoinedChannelEventHandler EventJoinedChannel;

	public event UserListReceivedEventHandler EventUserListReceived;

	public event ChannelListReceivedEventHandler EventChannelListReceived;

	public event NickChangedEventHandler EventNickChanged;
	
	public event NicknameInUseEventHandler EventNicknameInUse;
	
	public event CannotJoinChannelEventHandler EventCannotJoinChannel;
	
	public event NoSuchNickEventHandler EventNoSuchNick;
	
	public event ErrorEventHandler EventError;
	
	public event NotOnChannelEventHandler EventNotOnChannel;
	
}