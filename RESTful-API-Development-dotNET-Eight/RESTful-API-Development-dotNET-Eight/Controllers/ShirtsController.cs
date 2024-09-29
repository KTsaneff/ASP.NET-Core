using Microsoft.AspNetCore.Mvc;
using RESTful_API_Development_dotNET_Eight.Models;
using RESTful_API_Development_dotNET_Eight.Models.Repositories;

namespace RESTful_API_Development_dotNET_Eight.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        

        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok("Controller Routing: Reading all the shirts...");
        }

        [HttpGet("{id}")]   
        public IActionResult GetShirtById(int id)
        {
            if(id <= 0)
            {
                return BadRequest(/*"Invalid shirt ID."*/);
            }

            var shirt = ShirtRepository.GetShirtById(id);

            if(shirt == null)
            {
                return NotFound();
            }

            return Ok(shirt);
        }

        [HttpPost]
        public IActionResult CreateShirt([FromBody]Shirt shirt)
        {
            return Ok("Controller Routing: Creating a new shirt...");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShirt(int id)
        {
            return Ok($"Controller Routing: Updating shirt with ID: {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            return Ok($"Controller Routing: Deleting shirt with ID: {id}");
        }
    }
}
