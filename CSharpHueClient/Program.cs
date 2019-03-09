using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace CSharpHueClient
{
    class Program
    {

        public static HubConnection connection;

        static void Main(string[] args)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:9090/huelights")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<int, string, string>("ControlLight", (id, color, mode) => {
                Console.WriteLine(string.Format("{0} {1} {2}", id, color, mode));
            });
            Task.Run(async () => {
                await connection.StartAsync();
            });
            Console.ReadKey();
        }
    }
}
