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

	public string GetPrefix() => this.prefix;

	public string GetNick() => this.nick;

	public string GetUser() => this.user;

	public string GetHost() => this.host;

	public static Prefix of(string p) {
		if (string.IsNullOrWhiteSpace(p))
			return new Prefix();

		p = p[1..];
		string nick = null, user = null, host = null;

		if (p.Contains('@')) {
			var parts = p.Split('@');
			nick = parts[0];
			host = parts[1];
		} else if (p.Contains('.')) { // Seems to be a hostname
			return new Prefix(p, "", "", p);
		} else { // Nickname only
			return new Prefix(p, p, "", "");
		}

		if (string.IsNullOrWhiteSpace(nick) || !nick.Contains('!'))
			return new Prefix(p, nick, user, host);

		var parts_ = nick.Split('!');
		nick = parts_[0];
		user = parts_[1];

		return new Prefix(p, nick, user, host);
	}

	public override string ToString() {
		return $"Prefix: [{this.prefix}], Nick: [{this.nick}], User: [{this.user}], Host: [{this.host}]";
	}
}