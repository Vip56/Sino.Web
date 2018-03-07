using System;

namespace Sino.Application.Services
{
    public class ApplicationService : IApplicationService
	{
		/// <summary>
		/// 将String转换为Guid
		/// </summary>
		/// <param name="s">需要转换的字符串</param>
		/// <returns>成功则返回转换后的Guid否则返回Null</returns>
		protected Guid? StringToGuid(string s)
		{
			if (string.IsNullOrEmpty(s))
				return null;

			Guid guid = Guid.NewGuid();
			if (Guid.TryParse(s, out guid))
			{
				return guid;
			}
			return null;
		}
	}

}
