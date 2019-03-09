using BotCore.Models.Handler;
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
                .WithUrl("http://www.stud-informatik.de:9090/huelights")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<int, string, string>("ControlLight", (id, color, mode) => {
                HueHandler.ControlLight(id, color, mode);
            });
            Task.Run(async () => {
                try
                {
                    await connection.StartAsync();
                    Console.WriteLine("connection established");
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
            Console.ReadKey();
        }
    }
}
