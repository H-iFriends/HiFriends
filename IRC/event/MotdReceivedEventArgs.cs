namespace IRC; 

public class MotdReceivedEventArgs {
	public readonly string motd;

	public MotdReceivedEventArgs(string motd) => this.motd = motd ?? throw new ArgumentNullException(nameof(motd));
}