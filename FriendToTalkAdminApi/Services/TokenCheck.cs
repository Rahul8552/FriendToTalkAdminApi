using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.Net;
using FriendToTalkAdminBAL;

namespace FriendToTalkAdminApi.Services
{
    public class TokenCheck : ActionFilterAttribute
    {
        private readonly LoginBAL _loginBAL;

        public TokenCheck(LoginBAL loginBAL)
        {
            _loginBAL = loginBAL;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve values from request headers
            string userId = context.HttpContext.Request.Headers["userId"];
            string loginToken = context.HttpContext.Request.Headers["LoginToken"];
            string siteCode = context.HttpContext.Request.Headers["siteCode"];

            //Retrieve sitecode from action arguments or request body
            //if (context.ActionArguments.ContainsKey("sitecode"))
            //{
            //    siteCode = context.ActionArguments["sitecode"]?.ToString() ?? "";
            //}
            //else
            //{
                // Attempt to parse sitecode from request body if not found in action arguments
                //using (StreamReader reader = new StreamReader(context.HttpContext.Request.Body))
                //{
                //    string requestBody = reader.ReadToEnd();
                //    if (!string.IsNullOrEmpty(requestBody))
                //    {
                //        JObject json = JObject.Parse(requestBody);
                //        siteCode = json.GetValue("sitecode")?.ToString() ?? "";
                //
                //   }
                //}

            //}

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(loginToken) || string.IsNullOrEmpty(siteCode))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "Unauthorized Request"
                };
            }
            else
            {
                var response = _loginBAL.CheckToken(userId, loginToken, siteCode);

                if (response.ErrorCode != "1")
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
}