using System.Collections.Generic;

namespace Sino.Application.Services.Dto
{
	public interface IListResult<T>
	{
		IReadOnlyList<T> Items { get; set; }
	}
}