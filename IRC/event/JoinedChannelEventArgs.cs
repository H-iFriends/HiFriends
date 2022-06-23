namespace IRC; 

public class JoinedChannelEventArgs {
	
	public readonly string nick;
	
	public readonly string user;
	
	public readonly string host;
	
	public readonly string channel;

	public JoinedChannelEventArgs(string nick, string user, string host, string channel) {
		this.nick = nick;
		this.user = user;
		this.host = host;
		this.channel = channel;
	}
}