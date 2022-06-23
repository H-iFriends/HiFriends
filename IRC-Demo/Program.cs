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

Console.WriteLine("Connecting to server...");

if (!client.Connect()) Console.WriteLine("Could not connect to server");

Console.WriteLine("Connected to server");

Thread.Sleep(10000);

Console.WriteLine("Logging in...");

if (!client.Login("test", "test")) Console.WriteLine("Could not login");

Console.WriteLine("Logged in");

Thread.Sleep(10000);

Console.WriteLine("Joining channel...");

if (!client.Join("#FEFE")) Console.WriteLine("Could not join channel");

Console.WriteLine("Joined channel");

Thread.Sleep(30000);

client.Disconnect();