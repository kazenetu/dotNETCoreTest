using System;
using Models;

namespace Interfaces
{
  /// <summary>
  /// ユーザー情報
  /// </summary>
  public interface IUserService
  {
      string getUserName(User user);
  }
}