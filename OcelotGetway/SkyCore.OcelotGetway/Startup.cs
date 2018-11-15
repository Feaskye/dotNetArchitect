using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using SkyCore.AhphOcelot.Middleware;
using Ocelot.Administration;

namespace SkyCore.OcelotGetway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            Action<IdentityServerAuthenticationOptions> isOptions = o =>
            {
                o.Authority = "http://localhost:6611"; //IdentityServer��ַ
                o.RequireHttpsMetadata = false;
                o.ApiName = "gateway_admin"; //���ع�������ƣ���Ӧ��Ϊ�ͻ�����Ȩ��scope
            };
            services.AddOcelot().AddAhphOcelot(option =>
            {
                //option.DbConnectionStrings = "Server=localhost;Database=Ctr_AuthPlatform;User ID=root;Password=bl123456;";
                option.DbConnectionStrings = "Server=.;Database=Ctr_AuthPlatform;User ID=sa;Password=bl123456;";
                option.RedisConnectionStrings = new List<string>() {         "192.168.1.111:6379,password=bl123456,defaultDatabase=0,poolsize=50,ssl=false,writeBuffer=10240,connectTimeout=1000,connectRetry=1;"
                };
                //option.EnableTimer = true;//���ö�ʱ����
                //option.TimerDelay = 10 * 000;//����10��
            })
            //.UseMySql()
            .AddAdministration("/SkyOcelot", isOptions);

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAhphOcelot().Wait();


            //app.UseStaticFiles();
            //app.UseCookiePolicy();

            //app.UseMvc();



        }
    }
}
