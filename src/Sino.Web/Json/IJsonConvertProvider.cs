using Sino.Dependency;
using System.Threading.Tasks;

namespace Sino.Json
{
	/// <summary>
	/// Json转换服务
	/// </summary>
	public interface IJsonConvertProvider : ISingletonDependency
	{
		/// <summary>
		/// 将指定类型转换成Json格式
		/// </summary>
		string Serialize(object obj);

		/// <summary>
		/// 将指定类型转换成Json格式（异步）
		/// </summary>
		Task<string> SerializeAsync(object obj);

		/// <summary>
		/// 将Json字符串转换成类
		/// </summary>
		T Deserialize<T>(string str);

		/// <summary>
		/// 将Json字符串转换成类（异步）
		/// </summary>
		Task<T> DeserializeAsync<T>(string str);
	}
}
