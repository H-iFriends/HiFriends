namespace IRC;

public class CannotJoinChannelEventArgs {
	public readonly string Channel;

	public readonly string Message;

	public CannotJoinChannelEventArgs(string channel, string message) {
		this.Channel = channel;
		this.Message = message;
	}
}