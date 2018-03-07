using System;

namespace Sino
{
    public class SinoException : Exception
    {
        public int Code { get; private set; }

        public SinoException() { }

        public SinoException(int code)
        {
            this.Code = code;
        }

        public SinoException(string message)
            : base(message) { }

        public SinoException(string message,int code)
            : base(message)
        {
            this.Code = code;
        }

        public SinoException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
