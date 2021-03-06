// See https://aka.ms/new-console-template for more information

using IRC;

Console.WriteLine("Hello, World!");

var client = new Client("8.130.102.137");

client.EventMotdReceived += (sender, args) => {
	var c = sender as Client;
	Console.WriteLine($"\n\nReceived MOTD: from server {c.ServerAddress}\n" + args.motd);
};

client.EventMessageReceived += (sender, args) => {
	var c = sender as Client;
	Console.WriteLine($"\n\nReceived message: from server {c.ServerAddress}\nTarget: [{args.target}]\nMessage: {args.message}");
};

client.EventJoinedChannel += (sender, eventArgs) => {
	var c = sender as Client;
	Console.WriteLine($"\n\nJoined channel: from server {c.ServerAddress}\nChannel: {eventArgs.channel}, User: {eventArgs.user}");
};

client.EventUserListReceived += (sender, eventArgs) => {
	var c = sender as Client;
	Console.WriteLine($"\n\nReceived user list: from server {c.ServerAddress}\nChannel: {eventArgs.Channel}");
	foreach (var user in eventArgs.Users) {
		Console.WriteLine($"User: {user}");
	}
};

Console.WriteLine("Connecting to server...");

if (!client.Connect()) Console.WriteLine("Could not connect to server");

Console.WriteLine("Connected to server");

Thread.Sleep(10000);

Console.WriteLine("Logging in...");

if (!client.Login("test", "test")) Console.WriteLine("Could not login");

Console.WriteLine("Logged in");

Thread.Sleep(3000);

Console.WriteLine("Joining channel...");

if (!client.Join("#FEFE")) Console.WriteLine("Could not join channel");

Console.WriteLine("Joined channel");

Thread.Sleep(3000);

if (!client.Privmsg("#FEFE", "Hello, World!")) Console.WriteLine("Could not send message");

if (!client.Privmsg("#FEFE", DateTime.Now.ToString())) Console.WriteLine("Could not send message");

if (!client.Privmsg("gugu", "Hello, gugu!")) Console.WriteLine("Could not send message");

Console.WriteLine("Sent message");

Thread.Sleep(3000);

if (!client.Privmsg("#FEFE", "I am leaving!")) Console.WriteLine("Could not send message");

if (!client.Part("#FEFE")) Console.WriteLine("Could not part channel");

Thread.Sleep(3000);

if (!client.Join("#FEFE")) Console.WriteLine("Could not join channel");

Thread.Sleep(3000);

if (!client.List()) Console.WriteLine("Could not list channels");

if (!client.Privmsg("#FEFE", "I am back!")) Console.WriteLine("Could not send message");

Thread.Sleep(30000);

client.Disconnect();