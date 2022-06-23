namespace IRC; 

public partial class Client {
	
	private void HandleMotdStart(Message message) {
		this.motd.Clear();
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
	}
	
	private void HandleMotd(Message message) {
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
	}
	
	private void HandleEndOfMotd(Message message) {
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
		this.EventMotdReceived(this, new MotdReceivedEventArgs(this.motd.ToString()));
	}

	private void HandlePrivMsg(Message message) {
		var sender = message.Prefix?.GetNick()!;
		var target = message.Parameters[0];
		var messageText = message.Parameters[1];
		this.EventMessageReceived(this, new MessageReceivedEventArgs(sender, target, messageText));
	}
	
}