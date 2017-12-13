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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApiSample.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly IUserService service;
    private readonly ILogger logger;

    public UsersController(IUserService service, ILogger<UsersController> logger)
    {
      this.service = service;
      this.logger = logger;
    }

    // POST api/Users
    [HttpPost]
    public IActionResult Post([FromBody]Dictionary<string, object> param)
    {
      var data = new Dictionary<string, object>();

      var paramNameUserId = "userID";

      // 入力チェック
      if (!param.ContainsKey(paramNameUserId))
      {
        logger.LogError("Pram[{0}]が未設定", paramNameUserId);
        return BadRequest();
      }

      // パラメータ取得
      var user = Models.User.Create();
      user.UserId = param["userID"].ToString();
      logger.LogDebug("Pram[{0}] = {1}", paramNameUserId, user.UserId);

      // サービスのメソッドを呼び出し
      var userName = service.getUserName(user);
      data.Add(nameof(userName), userName);

      var result = new Dictionary<string, Dictionary<string, object>>();
      result.Add("value", data);

      logger.LogDebug("result = {0}", JsonConvert.SerializeObject(result));

      return Json(result);
    }

  }
}
