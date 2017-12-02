using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Commons.Interfaces;
using Commons.DB;
using Microsoft.AspNetCore.Mvc;
using Models;
using Interfaces;

namespace WebApiSample.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly IUserService service;

    public UsersController(IUserService service){
      this.service = service;
    }

    // POST api/Users
    [HttpPost]
    public IActionResult  Post([FromBody]Dictionary<string, object> param)
    {
      var data = new Dictionary<string, object>();

      var paramNameUserId = "userID";

      // 入力チェック
      if(!param.ContainsKey(paramNameUserId)){
        return BadRequest();
      }

      // パラメータ取得
      var user = Models.User.Create();
      user.UserId = param["userID"].ToString();

      // サービスのメソッドを呼び出し
      var userName = service.getUserName(user);
      data.Add(nameof(userName), userName);

      var result = new Dictionary<string, Dictionary<string, object>>();
      result.Add("value", data);

      return Json(result);
    }

  }
}
