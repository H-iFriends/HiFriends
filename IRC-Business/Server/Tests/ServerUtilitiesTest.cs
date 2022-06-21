using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IRC_Business.Server.Tests;

[TestClass]
public class ServerUtilitiesTest
{
    public ServerUtilities ServerUtilities;

    [TestInitialize]
    public void Init()
    {
        this.ServerUtilities = new ServerUtilities(new List<string>(){"a", "b", "aa"});
    }
    
    [TestMethod]
    public void AddServerTest()
    {
        try
        {
            ServerUtilities.AddServer(" ");
        }
        catch (ArgumentException e)
        {
            Assert.AreEqual("Server name cannot be empty or consist of only white-space.", e.Message);
        }
        ServerUtilities.AddServer("a");
        CollectionAssert.AreEquivalent(new List<string>(){"a", "b", "aa"}, this.ServerUtilities.Servers);
        ServerUtilities.AddServer("c");
        CollectionAssert.AreEquivalent(new List<string>(){"a", "b", "aa", "c"}, this.ServerUtilities.Servers);

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
        ServerUtilities.RemoveServer("NotExist");
        CollectionAssert.AreEquivalent(new List<string>(){"a", "b", "aa"}, this.ServerUtilities.Servers);

        ServerUtilities.RemoveServer("a");
        CollectionAssert.AreEquivalent(new List<string>(){"b", "aa"}, this.ServerUtilities.Servers);
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
        CollectionAssert.AreEquivalent(new List<string>(){"a", "aa"}, servers);
    }
}