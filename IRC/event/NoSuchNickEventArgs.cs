namespace IRC;

public class NoSuchNickEventArgs {
	public readonly string Nick;

	public NoSuchNickEventArgs(string nick) => this.Nick = nick;
}