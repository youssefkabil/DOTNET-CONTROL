using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DOTNET_Control.Models;

public class AdminOnlyAttribute : ActionFilterAttribute
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminOnlyAttribute(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = await _userManager.GetUserAsync(context.HttpContext.User);

        if (user == null || !user.isAdmin)
        {
            context.Result = new ForbidResult(); // Block access if the user is not an admin
            return;
        }

        await next();
    }
}
