using System;

namespace Sino.Runtime
{
    /// <summary>
    /// 操作异常
    /// </summary>
    public class SinoOperationException : SinoException
    {
        public SinoOperationException() { }

        public SinoOperationException(int code)
            : base(code) { }

        public SinoOperationException(string message)
            : base(message) { }

        public SinoOperationException(string message, int code)
            : base(message, code) { }

        public SinoOperationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
