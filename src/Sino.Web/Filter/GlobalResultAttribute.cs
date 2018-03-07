using System;

namespace Sino.Web.Filter
{
    /// <summary>
    /// 是否忽略
    /// </summary>
    public class GlobalResultAttribute : Attribute
    {
        public bool Ignore { get; set; }

        public GlobalResultAttribute(bool ignore = true)
        {
            Ignore = ignore;
        }
    }
}
