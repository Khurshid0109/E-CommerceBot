using Microsoft.EntityFrameworkCore;
using TelegramBot.AdminPanel.Extentions;
using TelegramBot.Data.DbContexts;
using TelegramBot.Service.Helpers;
using TelegramBot.Service.Mappers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.CustomExtention();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

WebHostEnvironment.WebRootPath = Path.GetFullPath("wwwroot");

//AutoMapper registration
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
