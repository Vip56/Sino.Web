using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sino.Runtime
{
    /// <summary>
    /// 未找到该数据
    /// </summary>
    public class SinoNotFoundException : SinoException
    {
        public SinoNotFoundException() { }

        public SinoNotFoundException(int code)
            : base(code) { }

        public SinoNotFoundException(string message)
            : base(message) { }

        public SinoNotFoundException(string message, int code)
            : base(message, code) { }

        public SinoNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
