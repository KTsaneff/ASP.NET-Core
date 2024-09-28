using Microsoft.AspNetCore.Mvc;
using RESTful_API_Development_dotNET_Eight.Models;

namespace RESTful_API_Development_dotNET_Eight.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public string GetShirts()
        {
            return "Controller Routing: Reading all the shirts...";
        }

        [HttpGet("{id}")]   
        public string GetShirtById(int id)
        {
            return $"Controller Routing: Reading shirt with ID: {id}";
        }

        [HttpPost]
        public string CreateShirt([FromBody]Shirt shirt)
        {
            return "Controller Routing: Creating a new shirt...";
        }

        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Controller Routing: Updating shirt with ID: {id}";
        }

        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Controller Routing: Deleting shirt with ID: {id}";
        }
    }
}
