namespace IRCTest;

using System.Text;
using IRC;

[TestClass]
public class ClientTest {
	
	[TestMethod]
	public void TestLogin() {
		var client = new Client("8.130.102.137");
		if (!client.Connect()) {
			Assert.Fail("Could not connect to server");
		}
		
		if (!client.Login("test", "test")) {
			Assert.Fail("Could not login");
		}
	}

	[TestMethod]
	public void TestHex() {
		var b = Encoding.UTF8.GetBytes("ikosujfgbjikegbjikerbgjikbguiebfguibefguiberuigbeuirgbui");
		Console.WriteLine(IRC.utils.HexDump.Get(b, 0, b.Length));
	}

}