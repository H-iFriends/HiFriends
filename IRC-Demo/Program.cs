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

if (!client.Connect()) Console.WriteLine("Could not connect to server");

if (!client.Login("test", "test")) Console.WriteLine("Could not login");

Thread.Sleep(15000);

if (!client.Join("#FEFE")) Console.WriteLine("Could not join channel");

Thread.Sleep(30000);

client.Disconnect();