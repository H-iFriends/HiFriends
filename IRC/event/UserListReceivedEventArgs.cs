namespace IRC;

public class UserListReceivedEventArgs {
	public readonly string Channel;

	public readonly string[] Users;

	public UserListReceivedEventArgs(string channel, string[] users) {
		this.Channel = channel;
		this.Users = users;
	}
}