namespace Sino.Domain.Entities.Auditing
{
	/// <summary>
	/// 包含常用审计功能，比如数据的创建时间和用户、最后编辑时间和用户
	/// </summary>
	public interface IAudited : ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime
	{

	}
}
