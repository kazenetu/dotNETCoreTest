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

### コンソールアプリの作成
1. ターミナル(コマンドプロンプト)を起動する
1. 下記のコマンドを実行する  
※カレントフォルダにConsoleAppフォルダが作成される  
  ```  
dotnet new console -o ConsoleApp
  ```  

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

### とりえず使ってみた感想
* 「定義に移動」はVisualStudioと同じようにF12。  
   わかりやすい。
* リネームがF2  
  Ctrl+Rじゃないのか……
* csファイル追加だけでちゃんとビルド対象になる  
  すごい。
* 「///」+改行でメソッドコメントが自動生成できない  
  地味に困った。カスタマイズがあるのかな。  
  [追記]ちょうどいいプラグインがあった。  
  [C# XML Documentation Comments](https://marketplace.visualstudio.com/items?itemName=k--kato.docomment)
  

## TODO
- [X] ネイティブな実行ファイル(exe)作成方法  
[[.NET Core].NET Coreで実行ファイルを作成する](https://qiita.com/yaegaki/items/bdf529f07552d72bc6e5)
- [ ] Web API 作成  
[Linux、macOS、Windows で ASP.NET Core MVC と Visual Studio Code を利用して Web API を作成する](https://docs.microsoft.com/ja-jp/aspnet/core/tutorials/web-api-vsc)
