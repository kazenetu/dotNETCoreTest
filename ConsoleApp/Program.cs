using System;
using System.IO;
using System.Text;
using ConsoleApp.DB;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding =System.Text.Encoding.UTF8;

            var hellow = new Hellow("C#");
            Console.WriteLine(hellow.GetFormatName());

            // SQL発行
            Console.WriteLine(selectSql());
        }

        private static string selectSql()
        {
            var resourcePath = AppContext.BaseDirectory;
            resourcePath = Path.Combine(resourcePath,"Resource/Test.db");

            // SQL実行
            using(var db = new SQLiteDB("Data Source=" + resourcePath))
            {
                var sql = new StringBuilder();
                sql.AppendLine("select");
                sql.AppendLine("  USER_NAME");
                sql.AppendLine("from");
                sql.AppendLine("  MT_USER");

                var result = db.Fill(sql.ToString());
                if (result.Rows.Count <= 0)
                {
                    return string.Empty;
                }

                return result.Rows[0]["USER_NAME"].ToString();
            }
        }
    }
}
