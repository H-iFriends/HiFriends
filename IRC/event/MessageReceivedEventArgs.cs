namespace IRC;

public class MessageReceivedEventArgs {
	public readonly string sender;
	
	public readonly string target;
	
	public readonly string message;
	
	public MessageReceivedEventArgs(string sender, string target, string message) {
		this.sender = sender;
		this.target = target;
		this.message = message;
	}
}