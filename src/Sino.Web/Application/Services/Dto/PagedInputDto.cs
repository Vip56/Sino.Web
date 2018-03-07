namespace Sino.Application.Services.Dto
{
	/// <summary>
	/// 用于分页输入Dto
	/// </summary>
	public class PagedInputDto : IPagedInputDto
	{
		public int Skip { get; set; }

		public int Take { get; set; }
	}
}
