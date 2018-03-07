namespace Sino.Domain.Entities
{
	/// <summary>
	/// 领域模型基类
	/// </summary>
	public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
	{
		/// <summary>
		/// 主键
		/// </summary>
		public virtual TPrimaryKey Id { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Entity<TPrimaryKey>))
			{
				return false;
			}

			var other = (Entity<TPrimaryKey>)obj;
			return Id.Equals(other.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
		{
			if (Equals(left, null))
			{
				return Equals(right, null);
			}
			return left.Equals(right);
		}

		public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
		{
			return !(left == right);
		}

		public override string ToString()
		{
			return $"[{GetType().Name} {Id}]";
		}
	}
}