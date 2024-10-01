using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RESTful_API_Development_dotNET_Eight.Models;

namespace RESTful_API_Development_dotNET_Eight.Filters.ActionFilters.V2
{
    public class Shirt_EnsureDescriptionIsPresentFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var shirt = context.ActionArguments["shirt"] as Shirt;
            if(shirt != null && !shirt.ValidateDescription())
            {
                context.ModelState.AddModelError("Shirt", "Description is required.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
