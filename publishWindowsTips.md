# WindowsへのPublishのTips

## 初めに
下記環境をWin-64用に発行してコマンドプロンプトで実行できるようにします。
- WebAPI + html(AngularJS)
- [DinkToPDF](https://www.nuget.org/packages/DinkToPdf/) によるPDF出力
- [NLog](https://www.nuget.org/packages/NLog.Web.AspNetCore/4.5.0-rc2)によるログ出力

## 発行方法
- 以下のコマンドを実行する  
```dotnet publish --configuration Release --self-contained -r win-x64```

### 実行方法
- 以下のコマンドを実行する  
```cd プロジェクトフォルダ/bin/Release/netcoreapp2.0/win-x64/publish```  
```実行ファイル名.exe```

## Tips
- 下記のエラーが発生する場合はCultureInfoの設定が必要の可能性がある。  
```
Description: Infinite recursion during resource lookup within System.Private.CoreLib.  
This may be a bug in System.Private.CoreLib, or potentially in certain extensibility points 
such as assembly resolve events or CultureInfo names.  Resource name: ArgumentNull_Generic
```  
Program.csのMainメソッドの最初に下記を追記する  
``` Charp
Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
```

[READMEに戻る](README.md)