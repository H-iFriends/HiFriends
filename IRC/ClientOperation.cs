namespace IRC; 

public partial class Client {
	
	public bool Login(string nick, string user, string realName = "", string? password = null) {
		if (!this.socket.Connected || string.IsNullOrWhiteSpace(nick) || string.IsNullOrWhiteSpace(user))
			return false;

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
		return this.SendMessage("JOIN", channel);
	}
	
}