using AspectCore.Lite.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class DefaultInterceptorAttribute : InterceptorAttribute
    {
        public override async Task Invoke(IAspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("Before service call");
                await next(context);
            }
            catch (Exception)
            {
                Console.WriteLine("Service throw an exception");
                throw;
            }
        }
    }
}
