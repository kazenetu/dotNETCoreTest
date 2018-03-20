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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;
using DinkToPdf;
using System.Web;
using DinkToPdf.Contracts;
using System.Text;

namespace WebApiSample.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly IUserService service;
    private readonly ILogger logger;

    private IConverter converter;

    private const string SessionKeyName = "_Name";

    public UsersController(IUserService service, ILogger<UsersController> logger, IConverter converter)
    {
      this.service = service;
      this.logger = logger;
      this.converter = converter;
    }

    // POST api/Users
    [HttpPost]
    [AutoValidateAntiforgeryToken]
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
      var userName = service.GetUserName(user);
      data.Add(nameof(userName), userName);

      // 前回の戻り値を設定
      if (HttpContext.Session.Keys.Contains(SessionKeyName))
      {
        data.Add("beforeValue", HttpContext.Session.GetString(SessionKeyName));
      }
      // セッションにユーザー名を設定
      HttpContext.Session.SetString(SessionKeyName, userName);

      var result = new Dictionary<string, Dictionary<string, object>>();
      result.Add("value", data);

      logger.LogDebug("result = {0}", JsonConvert.SerializeObject(result));

      return Json(result);
    }

    // POST api/Users/Clear
    [HttpPost("Clear")]
    [AutoValidateAntiforgeryToken]
    public IActionResult ClearPost([FromBody]Dictionary<string, object> param)
    {
      // セッションクリア
      HttpContext.Response.Cookies.Delete(Static.SessionName);
      HttpContext.Session.Clear();

      var data = new Dictionary<string, object>();
      data.Add("result", "ok");

      var result = new Dictionary<string, Dictionary<string, object>>();
      result.Add("value", data);

      return Json(result);
    }

    [HttpPost("PDFDownload")]
    public IActionResult PDFDownload()
    {
      var html = new StringBuilder();

      html.Append(@"
      <!DOCTYPE html>
      <html lang=""ja""\>
      <head> 
      <meta charset=""utf-8"">
      </head>
      <body>");

      html.Append(@"<table style=""border-spacing:0px;width:100%;"">");

      for (var index = 0; index < 10; index++)
      {
        html.Append("<tr>");
        for (var colIndex = 0; colIndex < 5; colIndex++)
        {
          html.AppendFormat(@"<td style=""border-style:solid;border-width:1px;margin:0px;background-color:RGBA({0},{1},{2},255);"">",
            colIndex * 50, 255, 255);
          html.AppendFormat("row{0}:col{1}", index, colIndex);
          html.Append("<br>日本語あいうえお");
          html.Append("</td>");
        }
        html.Append("</tr>");
      }

      html.Append("</table>");

      html.Append(@"<div style='page-break-after: always;'></div>");
      html.Append(@"page break<br>");
      html.Append(@"ページブレイク");

      html.Append(@"</body></html>");

      var doc = new HtmlToPdfDocument()
      {
        GlobalSettings = {
          ColorMode = ColorMode.Color,
          Orientation = Orientation.Landscape,
          PaperSize = PaperKind.A4Plus,
          DPI = 48,
        },
        Objects = {
        new ObjectSettings() {
            PagesCount = true,
            HtmlContent = html.ToString(),
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
        }
      }
      };

      byte[] pdf = converter.Convert(doc);

      // サンプルのファイル名
      string fileName = string.Format("テスト_{0:yyyyMMddHHmmss}.pdf", DateTime.Now);
      fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);

      // ファイル名を設定
      Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);

      return new FileContentResult(pdf, "application/pdf");
    }

    [HttpPost("upload")]
    public IActionResult Upload()
    {
      if (Request.Form.Files.Any())
      {
        var fileData = Request.Form.Files[0];

        if (fileData.FileName == string.Empty)
        {
          return BadRequest();
        }
        if (Path.GetExtension(fileData.FileName) != ".csv")
        {
          return BadRequest();
        }

        var fileName = string.Format("{0}_{1:yyyyMMddHHmmss}.{2}",
                          Path.GetFileNameWithoutExtension(fileData.FileName),
                          DateTime.Now,
                          Path.GetExtension(fileData.FileName));

        // ファイル名を設定
        fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
        Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);

        return new FileStreamResult(fileData.OpenReadStream(), "APPLICATION/octet-stream");
      }

      return BadRequest();
    }

    [HttpPost("uploadJS")]
    public IActionResult UploadJS()
    {
      if (Request.Form.Files.Any())
      {
        var fileData = Request.Form.Files[0];

        if (fileData.FileName == string.Empty)
        {
          return BadRequest();
        }
        if (Path.GetExtension(fileData.FileName) != ".csv")
        {
          return BadRequest();
        }

        // ファイルの中身を取得する
        var fileResult = new List<List<string>>();
        using (var sr = new StreamReader(fileData.OpenReadStream()))
        {
          while (!sr.EndOfStream)
          {
            var lineCells = sr.ReadLine().Split(",");
            if (lineCells.Count() == 3)
            {
              fileResult.Add(new List<string>(lineCells.ToList()));
            }
          }
        }

        var data = new Dictionary<string, object>();
        data.Add("result", "ok");
        data.Add("fileData", fileResult);

        var result = new Dictionary<string, Dictionary<string, object>>();
        result.Add("value", data);

        return Json(result);
      }

      return BadRequest();
    }
  }
}
