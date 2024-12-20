using System.Net;
using FriendToTalkAdminBAL;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FriendToTalkAdminApi.Services;

public class RoleCheck : ActionFilterAttribute
{
    private readonly LoginBAL _loginBAL;

    public RoleCheck(LoginBAL loginBAL)
    {
        _loginBAL = loginBAL;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.HttpContext.Request.Headers["userId"];
        var sitepath = context.HttpContext.Request.Headers["sitepath"];

        if ((string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId)) && (string.IsNullOrEmpty(sitepath) || string.IsNullOrWhiteSpace(sitepath)))
        {
            context.Result = new Microsoft.AspNetCore.Mvc.ContentResult()
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Content = "Unauthorized Request"
            };
        }
        else
        {
                
            var Response = _loginBAL.CheckRole(userId,
                sitepath);
            if (Response.ErrorCode != "1")
            {

                context.Result = new Microsoft.AspNetCore.Mvc.ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "Unauthorized Request"
                };
            }
        }
        base.OnActionExecuting(context);
    }

    
}