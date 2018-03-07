using System;

namespace Sino.Domain.Entities.Auditing
{
	/// <summary>
	/// 记录数据的创建时间
	/// </summary>
	public interface IHasCreationTime
    {
		/// <summary>
		/// 创建时间
		/// </summary>
		DateTime CreationTime { get; set; }
	}
}
