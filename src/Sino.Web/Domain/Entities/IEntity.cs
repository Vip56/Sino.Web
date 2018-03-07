namespace Sino.Domain.Entities
{
	/// <summary>
	/// 领域模型基础接口
	/// </summary>
	public interface IEntity<TPrimaryKey>
    {
		TPrimaryKey Id { get; set; }
	}
}
