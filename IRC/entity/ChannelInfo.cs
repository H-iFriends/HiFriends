namespace IRC.entity; 

public class ChannelInfo {
	public readonly string name;
	
	public readonly string topic;
	
	public readonly int userCount;

	public ChannelInfo(string name, string topic, int userCount) {
		this.name = name;
		this.topic = topic;
		this.userCount = userCount;
	}
}