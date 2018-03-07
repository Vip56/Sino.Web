using Sino.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sino.Domain.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
	{
		/// <summary>
		/// 获取所有对象
		/// </summary>
		Task<IEnumerable<TEntity>> GetAllListAsync();

		/// <summary>
		/// 根据主键获取数据
		/// </summary>
		/// <param name="id">主键编号</param>
		Task<TEntity> GetAsync(TPrimaryKey id);

		/// <summary>
		/// 根据主键获取数据，该数据可能不存在。
		/// </summary>
		/// <param name="id">主键</param>
		Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

		/// <summary>
		/// 添加一个实体
		/// </summary>
		Task<TEntity> InsertAsync(TEntity entity);

		/// <summary>
		/// 添加一个实体并获取主键
		/// </summary>
		Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

		/// <summary>
		/// 更新实体
		/// </summary>
		Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 根据查询条件查询总条数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<int> CountAsync(IQueryObject<TEntity> query);

        /// <summary>
        /// 根据查询对象返回集合与总条数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<Tuple<int, IList<TEntity>>> GetListAsync(IQueryObject<TEntity> query);

    }
}