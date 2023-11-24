using Microsoft.AspNetCore.HttpOverrides;

using Project_19.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPhonebookApi(builder.Configuration).AddSocialBar(builder.Configuration);
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseForwardedHeaders(new ForwardedHeadersOptions
	{
		ForwardedHeaders
			= ForwardedHeaders.XForwardedFor |
			  ForwardedHeaders.XForwardedProto
	});
	app.UseExceptionHandler("/Home/Error");
}
else app.UseHsts().UseHttpsRedirection();

app.UseStaticFiles().UseRouting().UsePhonebookApiTokenClaims();
app.MapControllerRoute(name: "default", pattern: "{controller=Contacts}/{action=Index}/{id?}");
app.MapHealthChecks("/healthz");

app.Run();
