namespace IRC; 

public class PartedChannelEventArgs {
	
	public readonly string Channel;

	public readonly string Nick;
	
	public PartedChannelEventArgs(string channel, string nick) {
		Channel = channel;
		Nick = nick;
	}

}