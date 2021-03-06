namespace IRC;

using entity;

public partial class Client {
	private void HandleRplMotdStart(Message message) {
		this.motd.Clear();
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
	}

	private void HandleRplMotd(Message message) {
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
	}

	private void HandleRplEndOfMotd(Message message) {
		this.motd.Append(message.Parameters[1]);
		this.motd.AppendLine();
		this.LoggedIn = true;
		this.EventMotdReceived?.Invoke(this, new MotdReceivedEventArgs(this.motd.ToString()));
	}

	private void HandlePrivMsg(Message message) {
		var sender = message.Prefix?.GetNick()!;
		var target = message.Parameters[0];
		var messageText = message.Parameters[1];
		this.EventMessageReceived?.Invoke(this, new MessageReceivedEventArgs(sender, target, messageText));
	}

	private void HandleJoin(Message message) {
		var nick = message.Prefix?.GetNick()!;
		var user = message.Prefix?.GetUser()!;
		var host = message.Prefix?.GetHost()!;
		var joinedChannel = message.Parameters[0];
		this.EventJoinedChannel?.Invoke(this, new JoinedChannelEventArgs(nick, user, host, joinedChannel));
	}

	private void HandleRplNamReply(Message message) {
		var channel = message.Parameters[2];
		if (!this.userListBuffer.ContainsKey(channel))
			this.userListBuffer.Add(channel, "");
		this.userListBuffer[channel] += message.Parameters[3].TrimEnd(' ') + " ";
	}

	private void HandleRplEndOfNames(Message message) {
		var channel = message.Parameters[1];
		var userList = this.userListBuffer[channel];
		this.EventUserListReceived?.Invoke(this, new UserListReceivedEventArgs(channel, userList.TrimEnd(' ').Split(' ')));
		this.userListBuffer.Remove(channel);
	}

	private void HandleRplList(Message message) {
		var channel = message.Parameters[1];
		var userCountStr = message.Parameters[2];
		var topic = message.Parameters.Length >= 4 ? message.Parameters[3] : "";

		if (!int.TryParse(userCountStr, out var userCount)) // Looks invalid
			return;

		this.channelListBuffer.Add(new ChannelInfo(channel, topic, userCount));
	}

	private void HandleRplListEnd(Message message) {
		this.EventChannelListReceived?.Invoke(this, new ChannelListReceivedEventArgs(this.channelListBuffer.ToArray()));
	}

	private void HandlePing(Message message) {
		this.SendMessage("PONG", message.Parameters[0]);
	}
	
	private void HandleNick(Message message) {
		var oldNick = message.Prefix?.GetNick()!;
		var newNick = message.Parameters[0];
		this.EventNickChanged?.Invoke(this, new NickChangedEventArgs(oldNick, newNick));
	}

	private void HandleErrNicknameInUse(Message message) {
		this.EventNicknameInUse?.Invoke(this, new NicknameInUseEventArgs(message.Parameters[1]));
	}
	
	private void HandleErrNoSuchNick(Message message) {
		this.EventNoSuchNick?.Invoke(this, new NoSuchNickEventArgs(message.Parameters[1]));
	}
	
	private void HandleErrNotOnChannel(Message message) {
		this.EventNotOnChannel?.Invoke(this, new NotOnChannelEventArgs(message.Parameters[1]));
	}
	
	private void HandlePart(Message message) {
		var nick = message.Prefix?.GetNick()!;
		var channel = message.Parameters[0];
		this.EventPartedChannel?.Invoke(this, new PartedChannelEventArgs(channel, nick));
	}
	
	private void HandleCannotJoinChannel(Message message) {
		string m;
		switch (message.Command) {
			case MessageType.ERR_CHANNELISFULL:
				m = "Channel is full";
				break;
			case MessageType.ERR_BANNEDFROMCHAN:
				m = "You are banned from this channel";
				break;
			case MessageType.ERR_INVITEONLYCHAN:
				m = "This channel is invite only";
				break;
			case MessageType.ERR_BADCHANNELKEY:
				m = "Bad channel key";
				break;
			case MessageType.ERR_NOSUCHCHANNEL:
				m = "No such channel";
				break;
			default:
				m = "Cannot join channel";
				break;
		}
		
		this.EventCannotJoinChannel?.Invoke(this, new CannotJoinChannelEventArgs(message.Parameters[1], m));
	}
	
}