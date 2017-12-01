using Interfaces;

namespace Models
{
  public class Users
  {
    #region パブリックメソッド

    public static string getUserName(IUserRepository repository, string userId)
    {
      return string.Format("Hello! {0}", repository.getUserName(userId));
    }

    #endregion
  }
}
