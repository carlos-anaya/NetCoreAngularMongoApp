using Microsoft.AspNetCore.Mvc;
using ProductsStore.Models;
using System.Threading.Tasks;

namespace ProductsStore.Apis
{
    [Route("api/messages")]
    public class MessagesController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(Message), 200)]
        public async Task<ActionResult> Messages()
        {
            //Simulate async process
            return await Task.Run(() =>
            {
                var msg = new Message { Data = "Hello World" };
                return Ok(msg);
            });
        }
    }
}