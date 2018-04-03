using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using NLog.Web;
using System.Reflection;

namespace WebApiSample
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

      // NLog: setup the logger first to catch all errors
      var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      var configPath = Path.Combine(path,"NLog.config");
      var logger = NLogBuilder.ConfigureNLog(configPath).GetCurrentClassLogger();
      try
      {
        logger.Debug("init main");
        BuildWebHost(args).Run();
      }
      catch (Exception e)
      {
        //NLog: catch setup errors
        logger.Error(e, "Stopped program because of exception");
        throw;
      }
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseNLog()
            .Build();
  }
}
