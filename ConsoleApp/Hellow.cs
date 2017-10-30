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
    /// コンソールに表示名を出力する
    /// </summary>  
    public void ConsoleWrite(){
      Console.WriteLine(string.Format("Hello {0}!",this.name));
    }

  }
}