using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Sino.Json
{
	public class JsonConvertProvider : IJsonConvertProvider
	{
		public string Serialize(object obj)
		{
			if (obj == null)
				throw new ArgumentNullException(nameof(obj));
			return JsonConvert.SerializeObject(obj);
		}

		public Task<string> SerializeAsync(object obj)
		{
			return Task.Factory.StartNew(() => Serialize(obj));
		}

		public T Deserialize<T>(string str)
		{
			T t = Activator.CreateInstance<T>();
			if (!string.IsNullOrWhiteSpace(str))
			{
				t = JsonConvert.DeserializeObject<T>(str);
			}
			return t;
		}

		public Task<T> DeserializeAsync<T>(string str)
		{
			return Task.Factory.StartNew(() => Deserialize<T>(str));
		}
	}
}
