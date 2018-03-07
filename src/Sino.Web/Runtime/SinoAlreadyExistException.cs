using System;

namespace Sino.Runtime
{
    /// <summary>
    /// 已存在相同数据异常
    /// </summary>
    public class SinoAlreadyExistException : SinoException
    {
        public SinoAlreadyExistException() { }

        public SinoAlreadyExistException(int code)
            : base(code) { }

        public SinoAlreadyExistException(string message)
            : base(message) { }

        public SinoAlreadyExistException(string message, int code)
            : base(message, code) { }

        public SinoAlreadyExistException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
