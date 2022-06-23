// See https://aka.ms/new-console-template for more information

using IRC_Business.Server;

ServerUtilities serverUtilities = new ServerUtilities(new List<Server>()
    {new("a", 1), new("b", 2), new("aa", 3)});
serverUtilities.ExportServer();
