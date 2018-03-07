namespace Sino.Domain.Entities.Auditing
{
	/// <summary>
	/// 记录最后编辑该数据的用户编号
	/// </summary>
	public interface IModificationAudited : IHasModificationTime
	{
		/// <summary>
		/// 最后编辑的用户编号
		/// </summary>
		long? LastModifierUserId { get; set; }
	}
}
