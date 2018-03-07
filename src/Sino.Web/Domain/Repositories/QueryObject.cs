using Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sino.Domain.Repositories
{
	/// <summary>
	/// 查询对象实现
	/// </summary>
	public abstract class QueryObject<Entity> : IQueryObject<Entity>
	{
		private SortOrder _sortOrder;
		private Expression<Func<Entity, object>> _orderField;

		public int Count { get; set; } = 10;

		/// <summary>
		/// 排序字段
		/// </summary>
		public Expression<Func<Entity, object>> OrderField
		{
			get
			{
				return _orderField;
			}
		}

		/// <summary>
		/// 排序规则
		/// </summary>
		public SortOrder OrderSort
		{
			get
			{
				return _sortOrder;
			}
		}

		/// <summary>
		/// 查询表达式
		/// </summary>
		public abstract List<Expression<Func<Entity, bool>>> QueryExpression { get; }

		/// <summary>
		/// 查询sql
		/// </summary>
		public virtual Dictionary<string, DynamicParameters> QuerySql { get; }

		public int Skip { get; set; } = 0;

		public void OrderBy(Expression<Func<Entity, object>> order)
		{
			_sortOrder = SortOrder.ASC;
			_orderField = order;
		}

		public void OrderByDesc(Expression<Func<Entity, object>> order)
		{
			_sortOrder = SortOrder.DESC;
			_orderField = order;
		}
	}
}
