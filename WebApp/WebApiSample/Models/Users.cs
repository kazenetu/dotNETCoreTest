using System.Text;
using Commons.Interfaces;

namespace Models
{
  public class Users
  {
    #region パブリックメソッド

    /// <summary>
    /// ユーザー名取得用Select発行
    /// </summary>
    /// <param name="db">databaseインスタンス</param>
    /// <param name="userId">ユーザーID</param>
    /// <returns>ユーザー名</returns>
    public static string getUserName(IDatabase db, string userId)
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

    #endregion
  }
}
