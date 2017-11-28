using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Commons.Interfaces;
using Commons.DB;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiSample.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    // POST api/Users
    [HttpPost]
    public JsonResult Post([FromBody]Dictionary<string, object> param)
    {
      var data = new Dictionary<string, object>();

      // SQL実行
      var userName = string.Empty;
      using (var db = DatabaseFactory.Create())
      {
        userName = Models.Users.getUserName(db, param["userID"].ToString());
      }
      data.Add(nameof(userName), userName);

      var result = new Dictionary<string, Dictionary<string, object>>();
      result.Add("value", data);

      return Json(result);
    }

  }
}
