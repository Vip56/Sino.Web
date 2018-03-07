using System;

namespace Sino.Domain.Entities.Auditing
{
	public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
	{
		/// <summary>
		/// Creation time of this entity.
		/// </summary>
		public virtual DateTime CreationTime { get; set; }

		/// <summary>
		/// Creator of this entity.
		/// </summary>
		public virtual long? CreatorUserId { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		protected CreationAuditedEntity()
		{
			CreationTime = DateTime.Now;
		}
	}
}
