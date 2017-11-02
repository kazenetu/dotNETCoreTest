# dotNETCoreTest
.NET Coreの開発環境作成してみるテスト

## 検証環境
Windows10 Home

## やったこと
### ツール類のインストール
1. .NET Core SDKのインストール  
今回は「[.NET Core SDK 2.0.2 with .NET Core 2.0.0](https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.2-sdk-download.md)」をインストール
 1. Visual Studio Codeのインストール  
[microsoftからダウンロード](https://www.microsoft.com/ja-jp/dev/products/code-vs.aspx)
 1. Visual Studio Codeを起動し、Visual Studio Codeの拡張機能(C#)をインストール  
![installcsharp.png](installcsharp.png)
 1. 拡張機能「XMLドキュメントコメント」をインストール  
 [C# XML Documentation Comments](https://marketplace.visualstudio.com/items?itemName=k--kato.docomment)

### Visual Studio Codeの設定
1. Visual Studio Codeを起動する
1. 左メニューからExplorerを選択しOpenFolderをクリック
1. カレントフォルダを選択

### コンソールアプリサンプルの作成
1. ターミナル(コマンドプロンプト)を起動する
1. 下記のコマンドを実行する  
※カレントフォルダにConsoleAppフォルダが作成される  
  ```dotnet new console -o ConsoleApp```  

### コンソールアプリの実行ファイル作成
1. プロジェクトファイル(*.csproj)に  
  ```<RuntimeIdentifiers>win-x86</RuntimeIdentifiers>```  
  を追加する
1. ターミナル(コマンドプロンプト)で  
   ```dotnet restore```  
   を実行する
1. ターミナル(コマンドプロンプト)で  
   ```dotnet publish -c release -r win-x86```  
   を実行する

### コンソールアプリのテスト作成
1. ターミナル(コマンドプロンプト)で  
   ```dotnet new xunit -o ConsoleApp.Test```  
   を実行し、テストプロジェクトを作成する
1. ターミナル(コマンドプロンプト)で  
   ```cd ConsoleApp.Test```  
   ```dotnet add reference ../ConsoleApp/ConsoleApp.csproj```  
   を実行し、テスト対象のプロジェクトを設定する
1. .vscode/tasks.jsonにtestタスクを追加する  
   [参照：.vscode/tasks.jsonのtestタスク](https://github.com/kazenetu/dotNETCoreTest/blob/master/.vscode/tasks.json#L16-L23)
1. ConsoleApp.Testディレクトリ以下にテストを書く  
   [参照：HellowTest.cs](https://github.com/kazenetu/dotNETCoreTest/blob/master/ConsoleApp.Test/HellowTest.cs)
1. testタスクを実行する   
   1. メニューから「タスクの実行」をクリック
   1. 「test」をクリック

### とりえず使ってみた感想
* 「定義に移動」はVisualStudioと同じようにF12。  
   わかりやすい。
* リネームがF2  
  Ctrl+Rじゃないのか……
* csファイル追加だけでちゃんとビルド対象になる  
  すごい。
* 「///」+改行でメソッドコメントが自動生成できない  
  地味に困った。カスタマイズがあるのかな。  
  [2017/10/31 追記]ちょうどいいプラグインがあった。  
  [C# XML Documentation Comments](https://marketplace.visualstudio.com/items?itemName=k--kato.docomment)
* DebugConsoleで日本語が文字化けする。  
  (実行時にShiftJISで出力されてる？)  
  プログラムに```Console.OutputEncoding =System.Text.Encoding.UTF8;```を追加すると正しく表示される。  
  これでよいのかな？

## TODO
- [X] ネイティブな実行ファイル(exe)作成方法  
[Qiita:[.NET Core].NET Coreで実行ファイルを作成する](https://qiita.com/yaegaki/items/bdf529f07552d72bc6e5)
- [ ] Web API 作成  
[Linux、macOS、Windows で ASP.NET Core MVC と Visual Studio Code を利用して Web API を作成する](https://docs.microsoft.com/ja-jp/aspnet/core/tutorials/web-api-vsc)

## 参考サイト
* [Qiita:[.NET Core].NET Coreで実行ファイルを作成する](https://qiita.com/yaegaki/items/bdf529f07552d72bc6e5)
* [Qiita:Visual Studio Codeタスクのdotnetコマンド出力を文字化けしないようにする](https://qiita.com/masaru_b_cl/items/705b75d256b11cb82feb)
* [Qiita:xUnit.net でユニットテストを始める](https://qiita.com/takutoy/items/84fa6498f0726418825d)