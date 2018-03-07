using System;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Sino.Dependency;
using Sino.Json;

namespace Sino.AutoIndex
{
	public class AutoIndexAppService : IAutoIndexAppService, ISingletonDependency
	{
		private IJsonConvertProvider Json { get; set; }
		private IDistributedCache Cache { get; set; }
		public AutoIndexAppService(IJsonConvertProvider json, IDistributedCache cache)
		{
			Json = json;
			Cache = cache;
		}

		public string GetAutoIndex(int enumKey)
		{
			string enumStr = enumKey.ToString();
			StringBuilder code = new StringBuilder(enumStr);
			code.Append(DateTime.Now.ToString("yyMMdd"));

			lock (this)
			{
				int index = 1;
				long ticks = DateTime.Now.Date.Ticks;
				var json = Cache.GetString(enumStr);
				var result = Json.Deserialize<AutoIndexItem>(json);
				if (result != null)
				{
					if (result.Ticks == ticks)
					{
						index = ++result.Index;
					}
				}
				var indexItem = new AutoIndexItem
				{
					Index = index,
					Ticks = ticks
				};
				Cache.SetString(enumStr, Json.Serialize(indexItem), new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = new TimeSpan(1, 0, 0, 1)
				});

				code.Append(string.Format("{0:D4}", index));
			}

			return code.ToString();
		}
	}
}