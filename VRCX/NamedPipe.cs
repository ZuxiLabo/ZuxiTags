using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuxiTags.VRCX
{
    [Zuxi.SDK.DoNotObfuscate]
    internal class IPCConnection
    {
        private const string SendPipeName = "vrcx-ipc";
        private const string ReceivePipeName = "vrcx-ipc";

        internal static async Task Main1()
        {
            IPCConnection example = new IPCConnection();

            // Start the receiver server
            Task receiveTask = example.StartReceivePipeServer();

            // Send a message after the server is running
            await Task.Delay(1000); // Small delay to ensure the receiver is listening
           // example.SendMessage("Hello from the sending pipe!");

            // Wait for the receiver task to complete (in a real scenario, use a condition to close the pipes)
            await receiveTask;
        }

        // Method to call on message reception
        internal void ProcessReceivedMessage(string message)
        {
            LogManager.Log("Received message: " + message);
            // Handle the received message here
        }

        // Sending Pipe (Client)
        internal void SendMessage(string message)
        {
            using (var client = new NamedPipeClientStream(SendPipeName))
            {
                client.Connect();
                using (var writer = new StreamWriter(client))
                {
                    writer.WriteLine(message);
                    writer.Flush();
                }
            }
        }

        // Receiving Pipe (Server)
        internal async Task StartReceivePipeServer()
        {
            while (true)
            {
                using (var server = new NamedPipeServerStream(ReceivePipeName, PipeDirection.In))
                {
                    LogManager.Log("Waiting for client connection...");
                    await server.WaitForConnectionAsync();

                    using (var reader = new StreamReader(server))
                    {
                        string? message;
                        while ((message = await reader.ReadLineAsync()) != null)
                        {
                            ProcessReceivedMessage(message); // Call the method on message reception
                        }
                    }
                }
                // Reaches here if the client disconnects; loop starts a new connection
            }
        }
    }
}
