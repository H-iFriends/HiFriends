namespace IRC;

public class MessageReceivedEventArgs {
	public readonly string target;
	
	public readonly string message;
	
	public MessageReceivedEventArgs(string target, string message) {
		this.target = target;
		this.message = message;
	}
}