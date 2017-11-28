using System;
using System.IO;
using Commons.Interfaces;

namespace Commons.DB
{
  /// <summary>
  /// DBインスタンス生成クラス
  /// </summary>
  public class DatabaseFactory
  {
    /// <summary>
    /// DBインスタンス取得
    /// </summary>
    /// <returns>IDatabasインスタンス</returns>
    public static IDatabase Create()
    {

      // TODO 設定ファイルによるインスタンス生成
      var resourcePath = AppContext.BaseDirectory;
      resourcePath = Path.Combine(resourcePath, "Resource/Test.db");

      return new SQLiteDB("Data Source=" + resourcePath);
    }
  }
}