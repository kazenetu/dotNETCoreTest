namespace Models
{
  /// <summary>
  /// Modelクラスのスーパークラス
  /// </summary>
  public abstract class ModelBase<T> where T : new()
  {

    /// <summary>
    /// Modelクラスのインスタンス作成
    /// </summary>
    public static T Create()
    {
      return new T();
    }
  }
}