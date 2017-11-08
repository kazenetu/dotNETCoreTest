using System;
using System.IO;
using System.Text;
using ConsoleApp.DB;
using ConsoleApp.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;

      var hellow = new Hellow("C#");
      Console.WriteLine(hellow.GetFormatName());

      // SQL発行:SQLite
      Console.Write("SQLite >");
      Console.WriteLine(selectSqlBySQLite());

      // SQL発行:Postgres
      Console.Write("Postgres >");
      Console.WriteLine(selectSqlByPostgres());

      // LogTest
      logTest();
    }

    private static void logTest()
    {
      var loggerFactory = new LoggerFactory();
      loggerFactory .AddProvider(new AppLoggerProvider());

      
      var logger = loggerFactory.CreateLogger("test");
      logger.LogWarning(new EventId(999),"test!");
      logger.LogError(new EventId(999),"test! {0}",new Exception("例外エラーテスト"));

    }

    /// <summary>
    /// SelectSQL発行サンプル：SQLite
    /// </summary>
    /// <returns>ユーザー名</returns>
    private static string selectSqlBySQLite()
    {
      var resourcePath = AppContext.BaseDirectory;
      resourcePath = Path.Combine(resourcePath, "Resource/Test.db");

      // SQL実行
      using (var db = new SQLiteDB("Data Source=" + resourcePath))
      {
        return  getUserName(db);
      }
    }

    /// <summary>
    /// SelectSQL発行サンプル：Postgres
    /// </summary>
    /// <returns>ユーザー名</returns>
    private static string selectSqlByPostgres()
    {
        // 接続文字列作成
        var connectionString = string.Format("Server={0};Port=5432;User Id={1};Password={2};Database={3}",
            "localhost", "test", "test","test");

      // SQL実行
      using (var db = new PostgreSQLDB(connectionString))
      {
        return  getUserName(db);
      }
    }

    /// <summary>
    /// ユーザー名取得用Select発行
    /// </summary>
    /// <param name="db">databaseインスタンス</param>
    /// <returns>ユーザー名</returns>
    private static string getUserName(IDatabase db)
    {
        var sql = new StringBuilder();
        sql.AppendLine("select");
        sql.AppendLine("  USER_NAME");
        sql.AppendLine("from");
        sql.AppendLine("  MT_USER");
        sql.AppendLine("limit 1");

        var result = db.Fill(sql.ToString());
        if (result.Rows.Count <= 0)
        {
          return string.Empty;
        }

        return result.Rows[0]["USER_NAME"].ToString();
    }
  }
}
