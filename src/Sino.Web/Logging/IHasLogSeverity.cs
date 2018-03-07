namespace Sino.Logging
{
    public interface IHasLogSeverity
    {
        /// <summary>
        /// 日志记录级别
        /// </summary>
        LogSeverity Severity { get; set; }
    }
}
