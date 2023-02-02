// See https://aka.ms/new-console-template for more information
using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using MQTTnet.Packets;
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        // Create TCP based options using the builder.
        var client = new MqttFactory().CreateMqttClient();
        var clientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("192.168.1.220", 1883)
            .WithCredentials("djadmin","dj123456")
            .WithCleanSession(false)
            .WithClientId("d66449e061d6492f24c5c3e80597a3f1")
            .Build();
        var result = await client.ConnectAsync(clientOptions);

        client.ApplicationMessageReceivedAsync += e =>
        {
            Console.WriteLine($"{DateTime.Now} Received application message.");
            Console.WriteLine($"Topic:{e.ApplicationMessage.Topic}, Message:{e.ApplicationMessage.ConvertPayloadToString()}");
            return Task.CompletedTask;
        };

        //var mqttSubscribeOptions = new MqttFactory().CreateSubscribeOptionsBuilder()
        //    .WithTopicFilter(
        //        f =>
        //        {
        //            f.WithTopic("testevent");
        //        })
        //    .Build();

        //await client.SubscribeAsync(mqttSubscribeOptions);
        //await client.SubscribeAsync("testevent", MqttQualityOfServiceLevel.AtLeastOnce);


        //var mqttSubscribeOptions = new MqttClientSubscribeOptionsBuilder().WithTopicFilter("a")
        //            .WithTopicFilter("b", MqttQualityOfServiceLevel.AtLeastOnce)
        //            .WithTopicFilter("c", MqttQualityOfServiceLevel.ExactlyOnce)
        //            .WithTopicFilter("d")
        //            .Build();

        var mqttSubscribeOptions = new MqttFactory().CreateSubscribeOptionsBuilder()
            .WithTopicFilter(
                f =>
                {
                    f.WithTopic("testevent");
                    f.WithTopic("testevent1");
                    f.WithTopic("aaa/+");
                })
            .Build();

        await client.SubscribeAsync(mqttSubscribeOptions);

        Console.WriteLine("MQTT client subscribed to topic.");

        Console.WriteLine("Press enter to exit.");
        Console.ReadLine();
    }
}
