using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductsStore.Infraestructure
{
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, max-age=0";
            context.HttpContext.Response.Headers["Pragma"] = "no-cache";
            context.HttpContext.Response.Headers["Expires"] = "-1";

            base.OnActionExecuting(context);
        }
    }
}