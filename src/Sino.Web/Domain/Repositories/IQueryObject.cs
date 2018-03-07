using Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sino.Domain.Repositories
{
	/// <summary>
	/// 查询对象模式接口
	/// </summary>
	public interface IQueryObject<Entity>
	{
		int Skip { get; set; }

		int Count { get; set; }

		List<Expression<Func<Entity, bool>>> QueryExpression { get; }

		Dictionary<string, DynamicParameters> QuerySql { get; }

		SortOrder OrderSort { get; }

		Expression<Func<Entity, object>> OrderField { get; }
	}
}
