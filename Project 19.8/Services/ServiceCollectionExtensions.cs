using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

using Project_19.Models;

namespace Project_19.Services;

/// <summary>
/// Содержит методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Регистрирует сервисы PhonebookApi в <see cref="IServiceCollection"/>
	/// </summary>
	/// <exception cref="InvalidOperationException"></exception>
	public static IServiceCollection AddPhonebookApi(this IServiceCollection collection, IConfiguration config)
	{
		collection.AddHttpClient(nameof(HttpClient)); 
		return collection.AddSession(o => o.IdleTimeout = TimeSpan.FromMinutes(30))
			.AddApiContacts(config).AddApiIdentity(config);
	}

	/// <summary>
	/// Регистрирует <see cref="ApiContacts"/> в <see cref="IServiceCollection"/>
	/// </summary>
	/// <exception cref="InvalidOperationException"></exception>
	private static IServiceCollection AddApiContacts(this IServiceCollection collection, IConfiguration config)
	{
		var url = Environment.GetEnvironmentVariable("API_CONTACTS_URL");

		if (string.IsNullOrEmpty(url)) url = config.GetConnectionString(nameof(ApiContacts)) ??
		                                     throw new InvalidOperationException($"Connection string " +
			                                     $"{nameof(ApiContacts)} not found.");

		collection.AddHealthChecks()
			.AddTypeActivatedCheck<ApiContactsHealthCheck>
			(nameof(ApiContacts), HealthStatus.Unhealthy, new[]
				{ nameof(ApiContacts).ToLower(), "api" }, url);

		var json = JsonSerializer.Serialize(new ApiContactsOptions { Url = url });

		config = new ConfigurationBuilder().AddJsonStream
			(new MemoryStream(Encoding.ASCII.GetBytes(json))).Build();

		return collection.Configure<ApiContactsOptions>(config).AddScoped<IContacts, ApiContacts>();
	}

	/// <summary>
	/// Регистрирует <see cref="ApiIdentity"/> в <see cref="IServiceCollection"/>
	/// </summary>
	/// <exception cref="InvalidOperationException"></exception>
	private static IServiceCollection AddApiIdentity(this IServiceCollection collection, IConfiguration config)
	{
		var url = Environment.GetEnvironmentVariable("API_IDENTITY_URL");

		if (string.IsNullOrEmpty(url)) url = config.GetConnectionString(nameof(ApiIdentity)) ??
		                                     throw new InvalidOperationException($"Connection string " +
			                                     $"{nameof(ApiIdentity)} not found.");

		collection.AddHealthChecks()
			.AddTypeActivatedCheck<ApiIdentityHealthCheck>
			(nameof(ApiIdentity), HealthStatus.Unhealthy, new[]
				{ nameof(ApiIdentity).ToLower(), "api" }, url);

		var json = JsonSerializer.Serialize(new ApiIdentityOptions { Url = url });

		config = new ConfigurationBuilder().AddJsonStream
			(new MemoryStream(Encoding.ASCII.GetBytes(json))).Build();

		return collection.Configure<ApiIdentityOptions>(config).AddScoped<IIdentity, ApiIdentity>();
	}

	/// <summary>
	/// Регистрирует <see cref="SocialBar"/> в <see cref="IServiceCollection"/>
	/// </summary>
	public static IServiceCollection AddSocialBar(this IServiceCollection collection, IConfiguration config)
		=> collection.Configure<SocialBarOptions>(config.GetSection(nameof(SocialBar)) ??
			throw new InvalidOperationException($"Section {nameof(SocialBar)} not found."))
				.AddTransient<IUrlButtonService, SocialBar>();

	/// <summary>
	/// Включает получения <see cref="ClaimsPrincipal"/> из токенов PhonebookApi
	/// </summary>
	public static IApplicationBuilder UsePhonebookApiTokenClaims(this IApplicationBuilder builder)
		=> builder.UseSession().Use(async (context, next) =>
		{
		  var token = context.Session.GetString(nameof(HttpContext));
		  if (!string.IsNullOrEmpty(token))
		  {
			  var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
			  context.User = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, nameof(HttpContext)));
		  }
		  await next();
		});
}
