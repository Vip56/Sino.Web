using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using Sino.Web.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sino.Web.Filter
{
    /// <summary>
    /// 输入/输出 日志记录
    /// </summary>
    public class InputOutputLogFilter : ActionFilterAttribute
    {
        public static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Info(new LogInfo()
            {
                Method = context.ActionDescriptor.RouteValues["controller"],
                Argument = new
                {
                    RequestId = context.HttpContext.TraceIdentifier,
                    Arguments = context.ActionArguments,
                    Host = GetClientUserIp(context)
                },
                Description = "入参记录"
            });
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            OutputLogAttribute outputlogattribute = (OutputLogAttribute)((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttribute(typeof(OutputLogAttribute), false);
            //判断是否需要开启出参日志记录
            if (outputlogattribute!=null)
            {
                _logger.Info(new LogInfo()
                {
                    Method = context.ActionDescriptor.RouteValues["controller"],
                    Argument = new
                    {
                        RequestId = context.HttpContext.TraceIdentifier,
                        context.Result,
                        Host = GetClientUserIp(context)
                    },
                    Description = "出参记录"
                });
            }
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 获取客户Ip
        /// </summary>
        private string GetClientUserIp(ActionContext context)
        {
            var ip = context.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
                ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip;
        }
    }
}
