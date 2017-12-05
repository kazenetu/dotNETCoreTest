using System;
using Models;

namespace Interfaces
{
  /// <summary>
  /// ユーザー情報インターフェース
  /// </summary>
  public interface IUserService
  {
    /// <summary>
    /// ユーザー名取得
    /// </summary>
    /// <param name="user">ユーザーパラメータ</param>
    /// <returns>ユーザー名</returns>
    string getUserName(User user);
  }
}