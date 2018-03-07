namespace Sino.Application.Services.Dto
{
	/// <summary>
	/// 用于分页中数据的序号
	/// </summary>
	public interface IPageResultIndex
    {
		/// <summary>
		/// 索引序号
		/// </summary>
		int Index { get; set; }
    }
}
