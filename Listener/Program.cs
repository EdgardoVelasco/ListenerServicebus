using System;
using System.Threading.Tasks;
using Listener.bus;
using Azure.Messaging.ServiceBus;
namespace Listener
{
    class Program
    {
         static void Main(string[] args)
        {
            Console.WriteLine(".--.Recibiendo mensajes desde Service bus-.-.");
            var cliente = Busclient.Cliente;
            ReceiveMessagesFromSubscriptionAsync(cliente, "tienda", "app").Wait();
            Console.WriteLine();
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body} ");

            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
        static async Task ReceiveMessagesFromSubscriptionAsync(ServiceBusClient client, string topicName, string subscriptionName)
        {
            
                // create a processor that we can use to process the messages
                ServiceBusProcessor processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());

                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            
        }
    }
}
