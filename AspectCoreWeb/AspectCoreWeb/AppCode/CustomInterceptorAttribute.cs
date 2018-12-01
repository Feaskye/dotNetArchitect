using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Abstractions;

namespace AspectCoreWeb.AppCode
{
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {

        [FromContainer]
        public ILogger<CustomInterceptorAttribute> Logger { get; set; }


        private readonly string _name;
        public CustomInterceptorAttribute(string name)
        {
            _name = name;
        }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Logger = context.ServiceProvider.Resolve<ILogger<CustomInterceptorAttribute>>();

                Logger.LogInformation("call interceptor");

                Console.WriteLine("Before service call");
                await next(context);
            }
            catch (Exception)
            {
                Console.WriteLine("Service threw an exception!");
                throw;
            }
            finally
            {
                Console.WriteLine("After service call");
            }
        }
    }
}
