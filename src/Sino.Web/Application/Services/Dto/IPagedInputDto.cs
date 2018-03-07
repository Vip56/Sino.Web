namespace Sino.Application.Services.Dto
{
	/// <summary>
	/// 用于分页输入Dto
	/// </summary>
	public interface IPagedInputDto : IInputDto
    {
		/// <summary>
		/// 跳过项数
		/// </summary>
		int Skip { get; set; }

		/// <summary>
		/// 获取项数
		/// </summary>
		int Take { get; set; }
    }
}
