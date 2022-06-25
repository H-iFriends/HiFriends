namespace IRC.entity;

public class UserInfo
{
    public string User;
    public string Nick;
    public string RealName;
    public string? Password;

    public UserInfo(string user, string nick, string realName, string? password)
    {
        this.User = user;
        this.Nick = nick;
        this.RealName = realName;
        this.Password = password;
    }
}