namespace IRCTest;

using System.Text;
using IRC;

[TestClass]
public class ClientTest {
	
	[TestMethod]
	public void TestLogin() {
		var client = new Client("8.130.102.137");

		client.EventMotdReceived += (sender, args) => {
			var c = sender as Client;
			Console.WriteLine($"\n\nReceived MOTD: from server {c.ServerAddress}\n" + args.motd);
		};

		client.EventMessageReceived += (sender, args) => {
			var c = sender as Client;
			Console.WriteLine($"\n\nReceived message: from server {c.ServerAddress}\nTarget: [{args.target}]\nMessage: {args.message}");
		};

		if (!client.Connect()) {
			Assert.Fail("Could not connect to server");
		}
		
		if (!client.Login("test", "test")) {
			Assert.Fail("Could not login");
		}

		if (!client.Join("#FEFE")) {
			Assert.Fail("Could not join channel");
		}
		
		Thread.Sleep(20000);
		
		client.Disconnect();
	}

	[TestMethod]
	public void TestHex() {
		var b = Encoding.UTF8.GetBytes("ikosujfgbjikegbjikerbgjikbguiebfguibefguiberuigbeuirgbui");
		Console.WriteLine(IRC.utils.HexDump.Get(b, 0, b.Length));
	}

}