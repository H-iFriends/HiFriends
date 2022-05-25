namespace IRC;

public class Prefix {
	private readonly string host;
	private readonly string nick;
	private readonly string prefix;
	private readonly string user;

	public Prefix(string prefix = null, string nick = null, string user = null, string host = null) {
		this.prefix = prefix;
		this.nick = nick;
		this.user = user;
		this.host = host;
	}

	public string getPrefix() => this.prefix;

	public string getNick() => this.nick;

	public string getUser() => this.user;

	public string getHost() => this.host;

	public static Prefix of(string p) {
		if (string.IsNullOrWhiteSpace(p))
			return new Prefix();

		p = p[1..];
		string nick = null, user = null, host = null;

		if (p.Contains('@')) {
			var parts = p.Split('@');
			nick = parts[0];
			host = parts[1];
		}

		if (string.IsNullOrWhiteSpace(nick) || !nick.Contains('!'))
			return new Prefix(p, nick, user, host);

		var parts_ = nick.Split('!');
		nick = parts_[0];
		user = parts_[1];

		return new Prefix(p, nick, user, host);
	}
}