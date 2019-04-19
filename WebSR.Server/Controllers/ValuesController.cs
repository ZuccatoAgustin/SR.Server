using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<string>>> Get(int count = 2, string area = "A")
        {
            var r = Areas.GetAreas().Single(e => e.Nombre == area).Nombre;

            for (int i = 0; i < count; i++)
            {
                var a = new SR.core.MaterieelVirtueel();
                a.id = new Random().Next(0, int.MaxValue);
                a.spoortak = new Random().Next(0, int.MaxValue);
                a.lintnaam = (new Random().Next(0, int.MaxValue)).ToString();
                a.kmwaarde = new Random().Next(0, int.MaxValue);
                a.spoor = new Random().Next(0, 5);
                a.meters = new Random().Next(0, 5);
                a.nummer = new Random().Next(0, 5).ToString();
                a.emplacement_id = new Random().Next(0, 5);
                //  HubContext.Clients.All.SendMaterieelvirtueel(a);

               // await HubContext.Clients.All.SendAsync("ReceiveMessage", "paso", DateTime.Now.ToShortDateString());

                // await HubContext.Clients.Group(r).SendAsync("ReceiveTrain", a);
                await HubContext.Clients.Group(r).SendAsync("ReceiveTrain", a);
                
            }

            return new string[] { count.ToString() };
        } 
    }
}
