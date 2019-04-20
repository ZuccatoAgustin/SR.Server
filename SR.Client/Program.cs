using Microsoft.AspNetCore.SignalR.Client;
using SR.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SR.Client
{
    class Program
    {
        static HubConnection connection;
        static void Main(string[] args)
        {
            List<string> areas = new List<string>();
            if (args != null && args.Any())
            {
                areas = args.ToList();

                areas.ForEach(e =>
                {
                    Console.WriteLine(e);
                }

                    );
                

            }
            else {

                Console.WriteLine("area random , o escriba A,B,C :");

                var s = Console.ReadLine();


                if (!string.IsNullOrEmpty(s))
                {
                    areas = s.Split(",").ToList();
                }
                else
                {
                    areas = Area.GetAreas().Select(e => e.Nombre).ToList();
                }
            }
             

           

            connection = new HubConnectionBuilder()
.WithUrl("http://localhost:60427/trainhub")
.Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            registerHubHandlers();

            Task.Run(async () =>
            {
                try
                {
                    await connection.StartAsync();

                    if (areas.Any())
                    {
                        areas.ForEach(async f =>
                        {
                            await connection.SendAsync("JoinRoom", f);
                        });
                    }
                    else
                    {
                        await connection.SendAsync("JoinRoom", Area.GetRamdom().Nombre);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            });


            Console.ReadLine();

        }





        private static void registerHubHandlers()
        {

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                Console.WriteLine(newMessage);
            });
            connection.On<Train>("ReceiveTrain", (train) =>
            {
                Console.ForegroundColor = Area.GetArea(train.area_id).Color;
                var newMessage = $"id: {train.id}: area :{train.area_id } numero: {train.Number }";
                Console.WriteLine(newMessage);
            });


        }

    }
}
