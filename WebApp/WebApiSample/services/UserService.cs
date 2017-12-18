
using Interfaces;
using Models;

namespace service
{
  /// <summary>
  /// ユーザー情報
  /// </summary>
  public class UserService : IUserService
  {
    private readonly IUserRepository repository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="repository">ユーザーリポジトリ</param>    
    public UserService(IUserRepository repository)
    {
      this.repository = repository;
    }

    /// <summary>
    /// ユーザー名取得
    /// </summary>
    /// <param name="user">ユーザーパラメータ</param>
    /// <returns>"Hello! ユーザー名"</returns>
    public string GetUserName(User user)
    {
      return string.Format("Hello! {0}", repository.getUserName(user.UserId));
    }
  }
}