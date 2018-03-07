using System;

namespace Sino.Domain.Entities.Auditing
{
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited
    {
        /// <summary>
        /// 是否被删除
        /// </summary>
        public virtual bool IsDeleted { get; set; } = false;

		private DateTime? deletionTime;
		/// <summary>
		/// 删除时间
		/// </summary>
		public virtual DateTime? DeletionTime
		{
			get
			{
				return deletionTime;
			}
			set
			{
				deletionTime = value;
			}
		}

		private long? deleterUserId;
		/// <summary>
		/// 删除该数据的用户编号
		/// </summary>
		public virtual long? DeleterUserId
		{
			get
			{
				return deleterUserId;
			}
			set
			{
				deletionTime = DateTime.Now;
				deleterUserId = value;
			}
		}
    }
}
