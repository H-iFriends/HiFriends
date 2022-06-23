namespace IRC_Business.Server;

public class Server: IEquatable<Server>
{
    public string ServerName;
    public int ServerPort;

    public Server(string serverName, int serverPort)
    {
        this.ServerName = serverName;
        this.ServerPort = serverPort;
    }

    public bool Equals(Server? other)
    {
        if (other == null)
            return false;
        return this.ServerName.Equals(other.ServerName) && this.ServerPort == other.ServerPort;
    }

    public override bool Equals(object? obj)
    {
        return this.Equals(obj as Server);
    }

    public override int GetHashCode()
    {
        return this.ServerPort.GetHashCode() + this.ServerName.GetHashCode();
    }
}