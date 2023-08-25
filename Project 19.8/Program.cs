using Microsoft.EntityFrameworkCore;

using Project_19._8.Services;
using Project_19._8.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactsContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ContactsContext)) ??
					 throw new InvalidOperationException($"Connection string {nameof(ContactsContext)} not found.")));

builder.Services.AddControllersWithViews();

builder.Services.AddSocialBar(builder.Configuration.GetSection(nameof(SocialBar)));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(name: "default", 
	pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
