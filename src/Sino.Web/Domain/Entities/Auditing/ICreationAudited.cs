namespace Sino.Domain.Entities.Auditing
{
	/// <summary>
	/// 记录创建数据的用户和时间
	/// </summary>
	public interface ICreationAudited : IHasCreationTime
    {
		/// <summary>
		/// 用户编号
		/// </summary>
		long? CreatorUserId { get; set; }
	}
}
