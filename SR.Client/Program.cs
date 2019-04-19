using Microsoft.AspNetCore.SignalR.Client;
using SR.core;
using System;
using System.Threading.Tasks;

namespace SR.Client
{
    class Program
    {
        static HubConnection connection;
        static void Main(string[] args)
        {

            connection = new HubConnectionBuilder()
              .WithUrl("http://localhost:60427/trainhub")
              .Build();
  
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            registerHubHandlers();


            Task.Run(async ()   => 
            {
                try
                {
                    await connection.StartAsync();
                    await connection.SendAsync("JoinRoom", "A");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            });


            Console.ReadLine();

        }

        



        private static void registerHubHandlers ()
        {

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                Console.WriteLine(newMessage);
            });
            connection.On<MaterieelVirtueel>("ReceiveTrain", (train) =>
            {
                var newMessage = $"id: {train.id}: area :{train.emplacement_id }";
                Console.WriteLine(newMessage);
            });


        }

    }
}
