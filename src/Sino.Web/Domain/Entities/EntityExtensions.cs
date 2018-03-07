using Sino.Domain.Entities.Auditing;
using System;

namespace Sino.Domain.Entities
{
	/// <summary>
	/// 领域对象的扩展方法
	/// </summary>
	public static class EntityExtensions
    {
		/// <summary>
		/// 判断该领域是否为空或被软删除
		/// </summary>
		public static bool IsNullOrDeleted(this ISoftDelete entity)
		{
			return entity == null || entity.IsDeleted;
		}

		/// <summary>
		/// 撤销软删除
		/// </summary>
		/// <param name="entity"></param>
		public static void UnDelete(this ISoftDelete entity)
		{
			entity.IsDeleted = false;
			if (entity is IDeletionAudited)
			{
				var deletionAuditedEntity = entity.As<IDeletionAudited>();
				deletionAuditedEntity.DeletionTime = null;
				deletionAuditedEntity.DeleterUserId = null;
			}
		}
	}
}
