using System.Text;
using Commons.ConfigModel;
using Commons.DB;
using Commons.Interfaces;
using Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
  public class UserRepository : IUserRepository
  {
    private IDatabase db;

    public UserRepository(IOptions<DatabaseConfigModel> config)
    {
      db = DatabaseFactory.Create(config.Value);
    }

    public void Dispose()
    {
      db.Dispose();
    }

    /// <summary>
    /// ユーザー名取得用Select発行
    /// </summary>
    /// <param name="userId">ユーザーID</param>
    /// <returns>ユーザー名</returns>
    public string GetUserName(string userId)
    {
      var sql = new StringBuilder();
      sql.AppendLine("select");
      sql.AppendLine("  USER_NAME");
      sql.AppendLine("from");
      sql.AppendLine("  MT_USER");
      sql.AppendLine("where ");
      sql.AppendLine("  USER_ID = @USER_ID");

      // Param設定
      db.ClearParam();
      db.AddParam("@USER_ID", userId);

      var result = db.Fill(sql.ToString());
      if (result.Rows.Count <= 0)
      {
        return string.Empty;
      }

      return result.Rows[0]["USER_NAME"].ToString();
    }

  }
}