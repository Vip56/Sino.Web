namespace WebApp
{
    //public class DefaultAuthenticationEvents : CookieAuthenticationEvents
    //{
    //    public override Task RedirectToAccessDenied(CookieRedirectContext context)
    //    {
    //        context.RedirectUri = null;
    //        context.Response.StatusCode = 200;
    //        context.Response.ContentType = "application/json";
    //        context.Response.WriteAsync(JsonConvert.SerializeObject(new BaseResponse
    //        {
    //            errorCode = "100002",
    //            errorMessage = "无权限",
    //            success = false
    //        }));
    //        return Task.CompletedTask;
    //    }

    //    public override Task RedirectToLogout(CookieRedirectContext context)
    //    {
    //        return base.RedirectToLogout(context);
    //    }

    //    public override Task RedirectToLogin(CookieRedirectContext context)
    //    {
    //        context.RedirectUri = null;
    //        context.Response.StatusCode = 200;
    //        context.Response.ContentType = "application/json";
    //        context.Response.WriteAsync(JsonConvert.SerializeObject(new BaseResponse
    //        {
    //            errorCode = "100001",
    //            errorMessage = "未登录",
    //            success = false
    //        }));
    //        return Task.CompletedTask;
    //    }

    //    public override Task RedirectToReturnUrl(CookieRedirectContext context)
    //    {
    //        return base.RedirectToReturnUrl(context);
    //    }
    //}
}
