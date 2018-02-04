# DinkToPdfを使ってみた

## 前提
- Windows10での作業となります。
- 開発環境(VisualStudioCode+C#拡張機能)での検証です。

## 事前準備
1. wkhtmltopdfのインストール
   1. [https://wkhtmltopdf.org/downloads.html](https://wkhtmltopdf.org/downloads.html/)からOSと32bit・64bitを選択してダウンロード  
   (Windowsの場合は「Windows(MinGW)」を選択)
   1. ダウンロードしたファイルを実行し、インストール
 1. wkhtmltopdfのbinフォルダにパスを通す
    1. Windowsシステムツールのシステムを選択
    1. システムを選択
    1. システムの詳細設定をクリック
    1. ダイアログ下部の環境変数をクリック
    1. システム環境変数のPathを選択
    1. 編集をクリック
    1. インストールフォルダのbinフォルダのパスを追加  
    例)C:\Program Files\wkhtmltopdf\bin
    1. OKを押して閉じる
1. libwkhtmltoxの配置
    1. [https://github.com/rdvojmoc/DinkToPdf/tree/master/v0.12.4](https://github.com/rdvojmoc/DinkToPdf/tree/master/v0.12.4)から32bit・64bitのどちらかをダウンロードし、プロジェクト直下にコピーする。
1. csprojの編集
    1. DinkToPdfを追加する  
```<ItemGroup>```  
```  <PackageReference Include="DinkToPdf" Version="1.0.8" />```  
```  <Content Include="libwkhtmltox.dll" CopyToOutputDirectory="PreserveNewest" />```  
```  <Content Include="libwkhtmltox.so" CopyToOutputDirectory="PreserveNewest" />```  
```  <Content Include="libwkhtmltox.dylib" CopyToOutputDirectory="PreserveNewest" />```  
```<ItemGroup>```  
    1. ```dotnet restore```を実行する

## 開発
1. Startup.csの編集  
ConfigureServicesメソッドにDIの設定を行う  
```services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));```
1. Controllerサブクラスの編集
   1. フィールドの追加  
     ```private IConverter converter;```
   1. コンストラクタのパラメター追加
     ```UsersController(～, IConverter converter)```  
     ```{```  
     ```   this.converter = converter; ```  
     ```} ```  
   1. メソッド追加  
   ``` CSharp
    [HttpPost("PDFDownload")]
    public IActionResult PDFDownload()
    {
      var doc = new HtmlToPdfDocument()
      {
          GlobalSettings = {
          ColorMode = ColorMode.Color,
          Orientation = Orientation.Landscape,
          PaperSize = PaperKind.A4Plus,
        },
        Objects = {
        new ObjectSettings() {
            PagesCount = true,
            HtmlContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur mauris eget ultrices  iaculis. Ut                               odio viverra, molestie lectus nec, venenatis turpis.",
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
   ```

## 所感
まだDinkToPdfのサンプルをコピペしただけなので、どこまで有用かわかっていません。  
今後、もう少し突っ込んだ技術検証をしていこうと思います。

### **2018/02/03追記**
表形式を出力できるように修正してみました。  
日本語もそのまま出力できるようです。

``` CSharp
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

  for(var index = 0;index < 10;index++){
    html.Append("<tr>");
    for(var colIndex = 0; colIndex<5;colIndex++){
      html.AppendFormat(@"<td style=""border-style:solid;border-width:1px;margin:0px;background-color:RGBA({0},{1},{2},255);"">",
        colIndex*50,255,255);
      html.AppendFormat("row{0}:col{1}",index,colIndex);
      html.Append("<br>日本語あいうえお");
      html.Append("</td>");
    }
    html.Append("</tr>");
  }

  html.Append("</table>");

  html.Append(@"</body></html>");

  var doc = new HtmlToPdfDocument()
  {
      GlobalSettings = {
      ColorMode = ColorMode.Color,
      Orientation = Orientation.Landscape,
      PaperSize = PaperKind.A4Plus,
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

  // ~以下略~
}
```

### 参考URL
- [nuget DinkToPdf](https://www.nuget.org/packages/DinkToPdf/)  
- [GitHub DinkToPdf](https://github.com/rdvojmoc/DinkToPdf)


[元に戻る](README.md)