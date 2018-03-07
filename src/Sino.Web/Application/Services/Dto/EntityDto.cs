namespace Sino.Application.Services.Dto
{
	public abstract class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>, IDto
	{
		public EntityDto() { }
		public EntityDto(TPrimaryKey id)
		{
			Id = id;
		}

		public TPrimaryKey Id { get; set; }
	}
}