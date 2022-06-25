namespace IRC;

public partial class Client {
	public bool Login(string nick, string user, string realName = "", string? password = null) {
		if (!this.socket.Connected || string.IsNullOrWhiteSpace(nick) || string.IsNullOrWhiteSpace(user))
			return false;

		if (this.LoggedIn)
			return true;

		if (string.IsNullOrWhiteSpace(realName))
			realName = "HiFriends IRC Client User";

		this.nick = nick;
		this.user = user;

		if (!this.SendMessage("HELLO"))
			return false;

		if (!string.IsNullOrWhiteSpace(password) && !this.SendMessage("PASS", password))
			return false;

		return this.SendMessage("NICK", nick) && this.SendMessage("USER", user, "0", "*", ":" + realName);
	}

	public bool Join(string channel) {
		if (!this.LoggedIn)
			return false;
		if (string.IsNullOrWhiteSpace(channel))
			return false;

		return this.SendMessage("JOIN", channel);
	}
	
	public bool Privmsg(string target, string message) {
		if (!this.LoggedIn)
			return false;
		if (string.IsNullOrWhiteSpace(message))
			return true;
		
		return this.SendMessage("PRIVMSG", target, ":" + message);
	}
	
	public bool Part(string channel) {
		if (!this.LoggedIn)
			return false;
		if (string.IsNullOrWhiteSpace(channel))
			return true;
		
		return this.SendMessage("PART", channel);
	}
	
	public bool List(params string[] channels) {
		if (!this.LoggedIn)
			return false;
		if (channels.Length == 0)
			return this.SendMessage("LIST");
		else
			return this.SendMessage("LIST", string.Join(",", channels));
	}
}