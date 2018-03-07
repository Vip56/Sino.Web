using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Sino.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sino.Web.Filter
{
    public class CheckSignatureFilter : ActionFilterAttribute
    {
        public static string Token { get; set; }

        /// <summary>
        /// signature查询字符串名称
        /// </summary>
        public static string SignatureQueryName { get; set; } = "signature";

        /// <summary>
        /// 时间戳查询字符串名称
        /// </summary>
        public static string TimeStampQueryName { get; set; } = "timestamp";

        /// <summary>
        /// 随机数查询字符串名称
        /// </summary>
        public static string NonceQueryName { get; set; } = "nonce";

        /// <summary>
        /// 接口校验失败提示
        /// </summary>
        public static string ErrorMessage { get; set; } = "接口校验失败";

        /// <summary>
        /// 基于校验中的时间戳进行判断，单位s
        /// </summary>
        public static int TimeOut { get; set; } = 120;

        /// <summary>
        /// 接口校验失败代码
        /// </summary>
        public static int ErrorCode { get; set; } = -2;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                if (!descriptor.MethodInfo.CustomAttributes.Any(x => x.AttributeType == typeof(CheckSignatureAttribute)))
                {
                    bool isNotPass = true;
                    var queryName = new string[] { SignatureQueryName, TimeStampQueryName, NonceQueryName };
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    foreach (var queryValue in context.HttpContext.Request.Headers)
                    {
                        if (queryName.Any(x => x.ToLower() == queryValue.Key.ToLower()))
                        {
                            if (queryValue.Value.Count >= 1 && !string.IsNullOrEmpty(queryValue.Value.FirstOrDefault()))
                                values.Add(queryValue.Key.ToLower(), queryValue.Value.FirstOrDefault());
                        }
                    }

                    if (values.Count == 3)
                    {
                        string timestamp = values[TimeStampQueryName.ToLower()];
                        var hash = MD5Hash(Token + timestamp + values[NonceQueryName.ToLower()]);
                        if (!IsTimeStampOutTime(timestamp) && string.Compare(hash, values[SignatureQueryName.ToLower()], true) == 0)
                        {
                            isNotPass = false;
                        }
                    }

                    if(isNotPass)
                    {
                        context.Result = new ObjectResult(new BaseResponse
                        {
                            success = false,
                            errorCode = ErrorCode.ToString(),
                            errorMessage = ErrorMessage
                        });
                        return;
                    }
                }
            }
            base.OnActionExecuting(context);
        }

        public static string MD5Hash(string value)
        {
            byte[] bytes;
            using (var md5 = MD5.Create())
            {
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
            var result = new StringBuilder();
            foreach (byte t in bytes)
            {
                result.Append(t.ToString("X2"));
            }
            return result.ToString();
        }

        public static bool IsTimeStampOutTime(string timestamp)
        {
            try
            {
                long origin = Convert.ToInt64(timestamp);
                long dest = GetTimeStamp();
                return origin + TimeOut < dest ? true : false;
            }
            catch(Exception)
            {
                return true;
            }
        }

        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}
