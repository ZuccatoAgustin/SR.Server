using System;

namespace SR.Server
{
    class Program
    {
        HubConnection connection;
        static void Main(string[] args)
        {

            connection = new HubConnectionBuilder()
              .WithUrl("http://localhost:53353/ChatHub")
              .Build();

            Console.WriteLine("Hello World!");
        }
    }
}
