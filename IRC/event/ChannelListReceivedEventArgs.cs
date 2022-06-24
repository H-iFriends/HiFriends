namespace IRC;

using entity;

public class ChannelListReceivedEventArgs {
	public readonly ChannelInfo[] channels;
	
	public ChannelListReceivedEventArgs(ChannelInfo[] channels) {
		this.channels = channels;
	}
}