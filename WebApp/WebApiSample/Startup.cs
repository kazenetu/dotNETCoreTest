using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.ConfigModel;
using DinkToPdf;
using DinkToPdf.Contracts;
using Infrastructure;
using Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

      services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

      // DIのテスト
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserService, UserService>();

      // Configを専用Modelに設定
      services.Configure<DatabaseConfigModel>(this.Configuration.GetSection("DB"));

      // Adds a default in-memory implementation of IDistributedCache.
      services.AddDistributedMemoryCache();

      // session
      //services.AddSession();
      services.AddSession(options =>
      {
          // Set a short timeout for easy testing.
          // options.IdleTimeout = TimeSpan.FromSeconds(10);
          // options.Cookie.HttpOnly = true;
          options.Cookie.Name = Static.SessionName;
      });      

       services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
    {
      if (env.IsDevelopment())
      {
        //app.UseDeveloperExceptionPage();
      }

      // エラーハンドリング
      app.UseStatusCodePagesWithRedirects("/error/error.html");

      // session設定
      app.UseSession();

      app.UseMvc();

      // ルートアクセス時にトークン発行
      app.Use(next => context =>
      {
        if (
          string.Equals(context.Request.Path.Value, "/", StringComparison.OrdinalIgnoreCase) ||
          string.Equals(context.Request.Path.Value, "/index.html", StringComparison.OrdinalIgnoreCase))
        {
          // We can send the request token as a JavaScript-readable cookie, and Angular will use it by default.
          var tokens = antiforgery.GetAndStoreTokens(context);
          context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
            new CookieOptions() { HttpOnly = false });
        }

        return next(context);
      });

      // 静的ファイルのデフォルト設定を有効にする
      app.UseDefaultFiles();

      // 静的ファイルを使用する
      app.UseStaticFiles();
    }
  }
}
