namespace IRC;

public class NicknameInUseEventArgs {
	public readonly string Nick;

	public NicknameInUseEventArgs(string nick) => this.Nick = nick;
}