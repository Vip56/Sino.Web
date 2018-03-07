namespace Sino.Domain.Repositories
{
	/// <summary>
	/// 排序
	/// </summary>
	public class Sort
	{
		/// <summary>
		/// 排序顺序
		/// </summary>
		public SortOrder SortOrder { get; set; }

		/// <summary>
		/// 排序索引
		/// </summary>
		public int Index { get; set; }
	}
}
