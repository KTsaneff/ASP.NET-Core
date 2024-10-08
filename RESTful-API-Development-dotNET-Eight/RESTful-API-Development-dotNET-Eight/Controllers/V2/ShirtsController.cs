﻿using Microsoft.AspNetCore.Mvc;
using RESTful_API_Development_dotNET_Eight.Attributes;
using RESTful_API_Development_dotNET_Eight.Data;
using RESTful_API_Development_dotNET_Eight.Filters.ActionFilters;
using RESTful_API_Development_dotNET_Eight.Filters.ActionFilters.V2;
using RESTful_API_Development_dotNET_Eight.Filters.AuthFilters;
using RESTful_API_Development_dotNET_Eight.Filters.ExceptionFilters;
using RESTful_API_Development_dotNET_Eight.Models;

namespace RESTful_API_Development_dotNET_Eight.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("/api/v{v:apiVersion}/[controller]")]
    [JwtTokenAuthFilter]
    public class ShirtsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public ShirtsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [RequiredClaim("read", "true")]
        public IActionResult GetShirts()
        {
            return Ok(db.Shirts.ToList());
        }

        [HttpGet("{id}")]
        [RequiredClaim("read", "true")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult GetShirtById(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }

        [HttpPost]
        [TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
        [Shirt_EnsureDescriptionIsPresentFilter]
        [RequiredClaim("write", "true")]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            this.db.Shirts.Add(shirt);
            this.db.SaveChanges();

            return CreatedAtAction(nameof(GetShirtById),
                                   new { id = shirt.ShirtId },
                                   shirt);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [Shirt_ValidateUpdateShirtFilter]
        [TypeFilter(typeof(Shirt_HandleUpdateExceptionsFilterAttribute))]
        [Shirt_EnsureDescriptionIsPresentFilter]
        [RequiredClaim("write", "true")]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            var shirtToUpdate = HttpContext.Items["shirt"] as Shirt;

            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Description = shirt.Description;

            this.db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        [RequiredClaim("delete", "true")]
        public IActionResult DeleteShirt(int id)
        {
            var shirtToDelete = HttpContext.Items["shirt"] as Shirt;
            
            this.db.Shirts.Remove(shirtToDelete);
            this.db.SaveChanges();

            return Ok(shirtToDelete);
        }
    }
}
