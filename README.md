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

### Visual Studio Codeの設定
1. Visual Studio Codeを起動する
1. 左メニューからExplorerを選択しOpenFolderをクリック
1. カレントフォルダを選択

### コンソールアプリの作成
1. Ctrl+Shift+C でコマンドプロンプトを起動する
1. 下記のコマンドを実行する  
※カレントフォルダにConsoleAppフォルダが作成される  
```
dotnet new console -o ConsoleApp
```  

### とりえず使ってみた感想
* 「定義に移動」はVisualStudioと同じようにF12。  
   わかりやすい。
* リネームがF2  
  Ctrl+Rじゃないのか……
* csファイル追加だけでちゃんとビルド対象になる  
  すごい。
* 「///」+改行でメソッドコメントが自動生成できない  
  地味に困った。カスタマイズがあるのかな。

## TODO
- [ ] ネイティブな実行ファイル(exe)作成方法
- [ ] Web API 作成  
[Linux、macOS、Windows で ASP.NET Core MVC と Visual Studio Code を利用して Web API を作成する](https://docs.microsoft.com/ja-jp/aspnet/core/tutorials/web-api-vsc)
