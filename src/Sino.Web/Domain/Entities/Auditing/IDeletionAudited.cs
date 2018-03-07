namespace Sino.Domain.Entities.Auditing
{
	/// <summary>
	/// 记录删除用户
	/// </summary>
	public interface IDeletionAudited : IHasDeletionTime, ISoftDelete
	{
		/// <summary>
		/// 删除数据的用户编号
		/// </summary>
		long? DeleterUserId { get; set; }
	}
}
