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
    private readonly IUserRepository repository;

    public UsersController(IUserRepository repository){
      this.repository = repository;
    }

    // POST api/Users
    [HttpPost]
    public JsonResult Post([FromBody]Dictionary<string, object> param)
    {
      var data = new Dictionary<string, object>();

      // SQL実行
      var userName = repository.getUserName(param["userID"].ToString());
      data.Add(nameof(userName), userName);

      var result = new Dictionary<string, Dictionary<string, object>>();
      result.Add("value", data);

      return Json(result);
    }

  }
}
