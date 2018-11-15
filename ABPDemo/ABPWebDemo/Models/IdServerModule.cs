using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABPWebDemo.Models
{
    public class IdServerModule: AbpModule
    {

        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public IdServerModule(IHostingEnvironment env)
        {
            _env = env;
            //_appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IdServerModule).GetAssembly());
        }


    }
}
