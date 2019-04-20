using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SR.core;

namespace WebSR.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
 
        private IHubContext<TrainHub> HubContext
        {
            get;
            set;
        }

        public ValuesController(IHubContext<TrainHub> hubcontext)
        {
            HubContext = hubcontext;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get(int count = 1 )
        {

            var id = 0;
            //var tasks = new List<Task>();


            //for (int i = 0; i < 70; i++)
            //{
            //    tasks.Add(SendTrain(id++)); 

            //}


            //await Task.WhenAll(tasks);

            for (int i = 0; i < count; i++)
            {
                Thread thread1 = new Thread(SendTrain);
                thread1.Start();
                Thread.Sleep(1000);

            }

          




            return new string[] { "ok" };
        }

        public void  SendTrain()
        { 
            var r = Area.GetRamdom();
            var  numero = new Random().Next(0, int.MaxValue).ToString();
            while (true)
            {

                var a = new SR.core.Train();
                a.id = new Guid();
                a.Field1 = new Random().Next(0, int.MaxValue);
                a.Field2 = (new Random().Next(0, int.MaxValue)).ToString();
                a.Field3 = new Random().Next(0, int.MaxValue);
                a.Field4 = new Random().Next(0, 5);
                a.Field5 = new Random().Next(0, 5);
                a.Number = numero;
                a.area_id = r.Id;
                //  HubContext.Clients.All.SendMaterieelvirtueel(a);
                // await HubContext.Clients.All.SendAsync("ReceiveMessage", "paso", DateTime.Now.ToShortDateString());
                // await HubContext.Clients.Group(r).SendAsync("ReceiveTrain", a);


                Thread.Sleep(10 * 1000);
                Task.Run(async () =>
                {
                    await HubContext.Clients.Group(r.Nombre).SendAsync("ReceiveTrain", a);
                });
            }
        }
    }
}
