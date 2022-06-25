namespace IRC;

public class NotOnChannelEventArgs {
	public readonly string Channel;

	public NotOnChannelEventArgs(string channel) => this.Channel = channel;
}