using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sino.Application.Services.Dto
{
	public interface IEntityDto<TPrimaryKey> : IDto
	{
		/// <summary>
		/// 主键
		/// </summary>
		TPrimaryKey Id { get; set; }
	}
}