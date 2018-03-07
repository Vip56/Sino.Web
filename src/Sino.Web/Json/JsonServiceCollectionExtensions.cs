using Sino.Json;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class JsonServiceCollectionExtensions
	{
        /// <summary>
        /// 将Json.net注入
        /// </summary>
		public static IServiceCollection AddJson(this IServiceCollection services)
		{
			services.AddSingleton<IJsonConvertProvider>(new JsonConvertProvider());
			return services;
		}
	}
}
