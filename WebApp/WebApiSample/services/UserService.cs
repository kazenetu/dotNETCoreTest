
using Interfaces;
using Models;

namespace service
{
  public class UserService : IUserService
  {
    private readonly IUserRepository repository;

    public UserService(IUserRepository repository)
    {
      this.repository = repository;
    }

    public string getUserName(User user)
    {
      return string.Format("Hello! {0}", repository.getUserName(user.UserId));
    }
  }
}