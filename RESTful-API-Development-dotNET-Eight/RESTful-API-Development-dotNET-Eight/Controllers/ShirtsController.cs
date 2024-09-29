using Microsoft.AspNetCore.Mvc;
using RESTful_API_Development_dotNET_Eight.Filters;
using RESTful_API_Development_dotNET_Eight.Filters.ActionFilters;
using RESTful_API_Development_dotNET_Eight.Filters.ExceptionFilters;
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
            return Ok(ShirtRepository.GetShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        [Shirt_ValidateCreateShirtFilter]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById),
                                   new { id = shirt.ShirtId },
                                   shirt);
        }

        [HttpPut("{id}")]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_UpdateShirtFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            ShirtRepository.UpdateShirt(shirt);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var shirt = ShirtRepository.GetShirtById(id);
            ShirtRepository.DeleteShirt(id);

            return Ok(shirt);
        }
    }
}
