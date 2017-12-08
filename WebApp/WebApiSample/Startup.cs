using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.ConfigModel;
using Infrastructure;
using Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using service;

namespace WebApiSample
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
            services.AddMvc();

            // DIのテスト
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserService ,UserService>();

            // Configを専用Modelに設定
            services.Configure<DatabaseConfigModel>(this.Configuration.GetSection("DB"));  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            // 静的ファイルのデフォルト設定を有効にする
            app.UseDefaultFiles();

            // 静的ファイルを使用する
            app.UseStaticFiles();
        }
    }
}
