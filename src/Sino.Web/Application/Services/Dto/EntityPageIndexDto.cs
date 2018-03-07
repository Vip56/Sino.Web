namespace Sino.Application.Services.Dto
{
	/// <summary>
	/// 带有分页索引的列表数据项
	/// </summary>
	public class EntityPageIndexDto<TPrimaryKey> : EntityDto<TPrimaryKey>, IPageResultIndex
	{
		public int Index { get; set; }
	}
}