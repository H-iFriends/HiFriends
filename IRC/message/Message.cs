namespace IRC;

using System.Text;

public class Message {
	public Message(MessageType command, string[] parameters, Prefix? prefix) {
		this.Command = command;
		this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
		this.Prefix = prefix;
	}

	public MessageType Command { get; }

	public string[] Parameters { get; }

	public Prefix? Prefix { get; }

	public static Message? parse(string message) {
		// Make a "string" message into a Message object
		var copy = message;

		// filter empty / whitespace lines
		if (string.IsNullOrWhiteSpace(copy))
			return null;

		// Get the prefix, if any
		Prefix prefix = null;
		if (':' == message[0]) {
			// has a prefix
			var prefixEnd = message.IndexOf(' ');
			prefix = Prefix.of(message[1..prefixEnd]);
			message = message[(prefixEnd + 1)..];
		}

		var commandStr = message[..message.IndexOf(' ')].ToUpper();
		// This works, verified!
		var command = (MessageType)Enum.Parse(typeof(MessageType), commandStr);
		message = message[(commandStr.Length + 1)..];

		// Get the parameters
		var parameters = new List<string>();
		while (!string.IsNullOrWhiteSpace(message)) {
			message = message.Trim();
			if (message[0] == ':') {
				parameters.Add(message[1..]);
				break;
			}
			var parameterEnd = message.IndexOf(' ');
			parameters.Add(message[..parameterEnd]);
			message = message[(parameterEnd + 1)..];
		}

		// Create the message
		var messageObj = new Message(command, parameters.ToArray(), prefix);
		return messageObj;
	}

	public override string ToString() {
		var sb = new StringBuilder();
		if (this.Prefix != null)
			sb.Append(this.Prefix);
		sb.Append(' ');
		sb.Append(this.Command.ToString());
		foreach (var parameter in this.Parameters)
			sb.Append(' ').Append(parameter);
		return sb.ToString();
	}
}