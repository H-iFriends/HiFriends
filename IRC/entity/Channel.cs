using System.Collections.ObjectModel;

namespace IRC.entity;

public class Channel
{
    public ChannelInfo? ChannelInfo;
    // public string hostName;
    // public int hostPort;
    public Client Client;
    public string ChatHistory = "";
    public string Topic = "";
    public int Activity = 0;
    public Channel(Client client, ChannelInfo? channelInfo = null)
    {
        ChannelInfo = channelInfo;
        // this.hostName = hostName;
        // this.hostPort = hostPort;
        this.Client = client;
    }

    public override string ToString()
    {
        if (this.ChannelInfo != null)
            return this.ChannelInfo.name;
        else return "NULL";
    }
}