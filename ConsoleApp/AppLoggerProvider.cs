using System;
using Microsoft.Extensions.Logging;

/// <summary>
/// consoleにログ出力するILoggerProvider
/// </summary>
/// <remarks>
/// 以下のサイトを参考にしています
/// Entity Framework Core - SQLをログで確認する(いちろぐ)
/// http://ichiroku11.hatenablog.jp/entry/2017/10/02/221329
/// </remarks>
public class AppLoggerProvider : ILoggerProvider {
    
    /// <summary>
    /// ロガー作成
    /// </summary>
    /// <param name="categoryName">カテゴリ名</param>
    /// <returns>ILoggerインスタンス</returns>
    public ILogger CreateLogger(string categoryName) {
        return new ConsoleLogger();
    }

    /// <summary>
    /// 解放
    /// </summary>
    public void Dispose() {
    }

    /// <summary>
    /// コンソール出力用ILogger
    /// </summary>
    private class ConsoleLogger : ILogger {
        public IDisposable BeginScope<TState>(TState state){
            return null;
        }
        
        /// <summary>
        /// ログ出力可否
        /// </summary>
        /// <param name="logLevel">対象ログレベル</param>
        /// <returns>出力可否</returns>
        /// <remarks>すべてOKとする</remarks>
        public bool IsEnabled(LogLevel logLevel){
            return true;
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="logLevel">ログレベル</param>
        /// <param name="eventId">イベントID</param>
        /// <param name="state">スコープ</param>
        /// <param name="exception">exceptionインスタンス</param>
        /// <param name="formatter">書式デリゲート</param>
        public void Log<TState>(
            LogLevel logLevel, EventId eventId,
            TState state, Exception exception,
            Func<TState, Exception, string> formatter) {

            // ログをコンソールに出力
            Console.Write($"{logLevel} : {eventId} : ");
            Console.WriteLine(formatter(state, exception));
        }
    }
}