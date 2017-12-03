using System;

namespace Models
{
  public class User : ModelBase<User>
  {
    public string UserId { set; get; }
    public string UserName { set; get; }
    public string PassWord { set; get; }
    public DateTime DataDate { set; get; }
    public DateTime TimeDate { set; get; }
    public DateTime TsDate { set; get; }
  }
}
