using System;

namespace Sino.Domain.Entities.Auditing
{
	/// <summary>
	/// 记录数据最后的编辑时间
	/// </summary>
	public interface IHasModificationTime
    {
		/// <summary>
		/// 最后修改时间
		/// </summary>
		DateTime? LastModificationTime { get; set; }
    }
}
