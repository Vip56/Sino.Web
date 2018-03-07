namespace Sino.Domain.Entities
{
	/// <summary>
	/// 让领域对象支持软删除
	/// </summary>
	public interface ISoftDelete
	{
		/// <summary>
		/// 是否已被删除
		/// </summary>
		bool IsDeleted { get; set; }
	}
}