using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace IRC_Business.Server.Tests;

[TestClass]
public class ServerUtilitiesTest
{
    public ServerUtilities ServerUtilities;

    [TestInitialize]
    public void Init()
    {
        this.ServerUtilities = new ServerUtilities(new ObservableCollection<Server>()
            {new("a", 1), new("b", 2), new("aa", 3)});
    }
    
    [TestMethod]
    public void AddServerTest()
    {
        try
        {
            ServerUtilities.AddServer(" ", 10);
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Server name cannot be empty or consist of only white-space.", e.Message);
        }
        ServerUtilities.AddServer("a", 1);

        var list1 = new List<Server>()
            {new("a", 1), new("b", 2), new("aa", 3)};
        
        Assert.IsTrue(ServerUtilities.Servers[0].Equals(new("a", 1)));
        Assert.IsTrue((list1.Count == this.ServerUtilities.Servers.Count) && !list1.Except(this.ServerUtilities.Servers).Any());
        
        ServerUtilities.AddServer("c", 4);
        list1.Add(new("c", 4));
        Assert.IsTrue((list1.Count == this.ServerUtilities.Servers.Count) && !list1.Except(this.ServerUtilities.Servers).Any());

    }

    [TestMethod]
    public void RemoveServerTest()
    {
        try
        {
            ServerUtilities.RemoveServer(" ");
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Server name cannot be empty or consist of only white-space.", e.Message);
        }
        
        var list1 = new List<Server>()
            {new("a", 1), new("b", 2), new("aa", 3)};
        ServerUtilities.RemoveServer("NotExist");
        Assert.IsTrue((list1.Count == this.ServerUtilities.Servers.Count) && !list1.Except(this.ServerUtilities.Servers).Any());

        ServerUtilities.RemoveServer("a");
        list1.Remove(new("a", 1));
        Assert.IsTrue((list1.Count == this.ServerUtilities.Servers.Count) && !list1.Except(this.ServerUtilities.Servers).Any());
    }

    [TestMethod]
    public void FindServerTest()
    {
        try
        {
            ServerUtilities.FindServer(" ");
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Server name cannot be empty or consist of only white-space.", e.Message);
        }

        var servers = ServerUtilities.FindServer("a");
        var list1 = new List<Server>() {new("a", 1), new("aa", 3)};
        Assert.IsTrue((list1.Count == servers.Count) && !list1.Except(servers).Any());

    }

    [TestMethod]
    public void ExportAndImportServerTest()
    {
        ServerUtilities.ExportServer();
        var importServers = ServerUtilities.ImportServer();
        
        CollectionAssert.AreEquivalent(ServerUtilities.Servers, importServers);
        Assert.IsTrue((ServerUtilities.Servers.Count == importServers.Count) && !ServerUtilities.Servers.Except(importServers).Any());

    }
}