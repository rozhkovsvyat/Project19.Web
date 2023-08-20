using Microsoft.EntityFrameworkCore;

using Project_19._8.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsContext") ?? 
    throw new InvalidOperationException("Connection string 'ContactsContext' not found.")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.Run();
