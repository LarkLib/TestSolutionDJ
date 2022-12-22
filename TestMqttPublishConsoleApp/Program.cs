// See https://aka.ms/new-console-template for more information
using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Protocol;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // Create TCP based options using the builder.
        var client = new MqttFactory().CreateMqttClient();
        var clientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("127.0.0.1", 1883)
            .WithCleanSession(false)
            .WithClientId("d66449e061d6492f24c5c3e80597a3f2")
            .Build();
        var result = await client.ConnectAsync(clientOptions);
        for (int i = 0; i < 100; i++)
        {
            var result2 = await client.PublishStringAsync("testevent", $"test string {i}", MqttQualityOfServiceLevel.AtLeastOnce);
            Console.WriteLine($"test string {i}");
            Thread.Sleep(500);
        }
    }
}