using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebProject.Filters
{
    public class MemberStatusFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            //if (context.HttpContext.Session.GetString("Manager") == null)
            //{
            //    context.Result = new RedirectToActionResult("Login", "Login", null);
            //}
            var Manager = context.HttpContext.Session.GetString("Manager");
            var Admin = context.HttpContext.Session.GetString("Admin");
            if (context.HttpContext.Session.GetString("Admin") == null && controller == "ADProducts")
            {
                context.Result = new RedirectToActionResult("Index", "Products", null);
            }
            else if ((Admin == null) && controller == "Members" && action == "Index")
            {
                context.Result = new RedirectToActionResult("UserDetails", "Members", null);
            }
        }
    }
}
