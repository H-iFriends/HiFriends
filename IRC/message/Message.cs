namespace IRC;

public class Message {
	public Message(MessageType command, string[] parameters, Prefix? prefix) {
		this.Command = command;
		this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
		this.Prefix = prefix;
	}

	public MessageType Command { get; }

	public string[] Parameters { get; }

	public Prefix? Prefix { get; }
}