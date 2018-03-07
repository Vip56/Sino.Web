namespace Sino.Application.Services.Dto
{
	/// <summary>
	/// 用于DTO中分页数据
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
	{

	}
}