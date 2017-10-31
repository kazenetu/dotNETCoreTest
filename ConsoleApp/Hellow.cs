using System;

namespace ConsoleApp
{
  /// <summary>
  /// Hellowクラス
  /// </summary>  
  public class Hellow
  {
    /// <summary>
    /// 表示名
    /// </summary>  
    private string name;

    /// <summary>
    /// コンストラクタ
    /// </summary>  
    /// <param name="name">表示名</param>
    public Hellow(string name)
    {
      this.name = name;
    }

    /// <summary>
    /// 情報付き表示名を出力する
    /// </summary>
    /// <returns>情報付き表示名</returns>
    public string GetFormatName(){
      return string.Format("こんにちは {0}!",this.name);
    }

  }
}