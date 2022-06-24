namespace IRC;

public class NickChangedEventArgs {
	public readonly string NewNick;

	public readonly string OldNick;

	public NickChangedEventArgs(string oldNick, string newNick) {
		this.OldNick = oldNick;
		this.NewNick = newNick;
	}
}