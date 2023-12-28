//------------------------------------------------------------
// Erinn Network
// Copyright © 2023 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Erinn
{
    /// <summary>
    ///     日志
    /// </summary>
    public static class Log
    {
        /// <summary>
        ///     当前日志
        /// </summary>
        private static readonly Logger ErinnLogger = LogManager.GetLogger("Log");

        /// <summary>
        ///     配置
        /// </summary>
        static Log()
        {
            var config = new LoggingConfiguration();
            var logfile = new FileTarget("logfile") { FileName = $"logs/{GetBeginTimeFully()}.txt" };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;
        }

        /// <summary>
        ///     控制台打印
        /// </summary>
        /// <param name="message">信息</param>
        public static void WriteLine<T>(T message) => Console.WriteLine($"[{GetCurrentTimeFully()}] [{message}]");

        /// <summary>
        ///     日志
        /// </summary>
        /// <param name="message">信息</param>
        public static void Info<T>(T message)
        {
            WriteLine(message);
            ErinnLogger.Info($"[{message}]");
        }

        /// <summary>
        ///     警告
        /// </summary>
        /// <param name="message">信息</param>
        public static void Warning<T>(T message)
        {
            WriteLine(message);
            ErinnLogger.Warn($"[{message}]");
        }

        /// <summary>
        ///     报错
        /// </summary>
        /// <param name="message">信息</param>
        public static void Error<T>(T message)
        {
            WriteLine(message);
            ErinnLogger.Error($"[{message}]");
        }

        /// <summary>
        ///     获取完整时间戳
        /// </summary>
        /// <returns>获得的完整时间戳</returns>
        private static string GetBeginTimeFully()
        {
            var now = DateTime.Now;
            var interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 6);
            interpolatedStringHandler.AppendFormatted(now.Year, "D4");
            interpolatedStringHandler.AppendLiteral("-");
            interpolatedStringHandler.AppendFormatted(now.Month, "D2");
            interpolatedStringHandler.AppendLiteral("-");
            interpolatedStringHandler.AppendFormatted(now.Day, "D2");
            interpolatedStringHandler.AppendLiteral("-");
            interpolatedStringHandler.AppendFormatted(now.Hour, "D2");
            interpolatedStringHandler.AppendLiteral("-");
            interpolatedStringHandler.AppendFormatted(now.Minute, "D2");
            interpolatedStringHandler.AppendLiteral("-");
            interpolatedStringHandler.AppendFormatted(now.Second, "D2");
            return interpolatedStringHandler.ToStringAndClear();
        }

        /// <summary>
        ///     获取完整时间戳
        /// </summary>
        /// <returns>获得的完整时间戳</returns>
        private static string GetCurrentTimeFully()
        {
            var now = DateTime.Now;
            var interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 6);
            interpolatedStringHandler.AppendFormatted(now.Year, "D4");
            interpolatedStringHandler.AppendLiteral("/");
            interpolatedStringHandler.AppendFormatted(now.Month, "D2");
            interpolatedStringHandler.AppendLiteral("/");
            interpolatedStringHandler.AppendFormatted(now.Day, "D2");
            interpolatedStringHandler.AppendLiteral(" ");
            interpolatedStringHandler.AppendFormatted(now.Hour, "D2");
            interpolatedStringHandler.AppendLiteral(":");
            interpolatedStringHandler.AppendFormatted(now.Minute, "D2");
            interpolatedStringHandler.AppendLiteral(":");
            interpolatedStringHandler.AppendFormatted(now.Second, "D2");
            interpolatedStringHandler.AppendLiteral(":");
            interpolatedStringHandler.AppendFormatted(now.Millisecond, "D3");
            interpolatedStringHandler.AppendLiteral(":");
            interpolatedStringHandler.AppendFormatted(now.Microsecond, "D3");
            return interpolatedStringHandler.ToStringAndClear();
        }
    }
}