using Sino.Domain.Entities;

namespace Sino.Domain.Repositories
{
	public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
	{

	}
}
