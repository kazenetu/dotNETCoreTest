using System;

namespace Interfaces
{
  /// <summary>
  /// ユーザー情報
  /// </summary>
  public interface IUserRepository:IDisposable
  {
      string getUserName(string userId);
  }
}