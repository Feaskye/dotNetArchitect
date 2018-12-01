using AspectCore.DynamicProxy;
using AspectCoreWeb.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspectCoreWeb.Services
{

    [NonAspect]
    public interface ICustomService
    {

        [ServiceInterceptor(typeof(CustomInterceptorAttribute))]
        void Call();

    }
}
