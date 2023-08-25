namespace Project_19._8.Services
{
	/// <summary>
	/// Содержит методы расширения для <see cref="SocialBar"/>
	/// </summary>
	public static class SocialBarExtensions
	{
		/// <summary>
		/// Регистрирует <see cref="SocialBar"/> в <see cref="IServiceCollection"/>
		/// </summary>
		/// <param name="collection"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static IServiceCollection AddSocialBar
			(this IServiceCollection collection, IConfiguration config)
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));
			if (config == null) throw new ArgumentNullException(nameof(config));

			collection.Configure<SocialBarOptions>(config);
			return collection.AddScoped<IUrlButtonService, SocialBar>();
		}
	}
}
