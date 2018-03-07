namespace Sino.Domain.Entities.Auditing
{
	/// <summary>
	/// 包含所有审计功能
	/// </summary>
	public interface IFullAudited : IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete
	{

    }
}
