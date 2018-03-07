using System;

namespace Sino.Domain.Entities.Auditing
{
	public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
	{
		private DateTime? lastModificationTime;
		/// <summary>
		/// 最后修改时间
		/// </summary>
		public virtual DateTime? LastModificationTime
		{
			get
			{
				return lastModificationTime;
			}
			set
			{
				lastModificationTime = value;
			}
		}

		private long? lastModifierUserId;
		/// <summary>
		/// 最后修改人编号
		/// </summary>
		public virtual long? LastModifierUserId
		{
			get
			{
				return lastModifierUserId;
			}
			set
			{
				lastModificationTime = DateTime.Now;
				lastModifierUserId = value;
			}
		}
	}
}
