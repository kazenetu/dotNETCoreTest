﻿using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebAPI;

namespace WebApiSample21
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
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // トークン発行
      services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

      // session
      services.AddSession(options =>
      {
        options.Cookie.Name = Statics.SessionCookieName;
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
    {
      if (env.IsDevelopment())
      {
        //app.UseDeveloperExceptionPage();
      }
      else
      {
        //app.UseHsts();
      }

      // httpsを利用しない
      //app.UseHttpsRedirection();

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
