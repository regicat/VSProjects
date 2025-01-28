using MvcTodo.Services;
using MvcTodo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcTodo.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcTodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcTodoContext") ?? throw new InvalidOperationException("Connection string 'MvcTodoContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI定義
builder.Services.AddScoped<ITodoService,TodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Todo}/{action=Index}/{id?}/{checkValue?}/{listMode?}");

//app.MapControllerRoute(
//	name: "detail",
//	pattern: "{controller=Todo}/{action}/{id?}");

//app.MapControllerRoute(
//	name: "check",
//	pattern: "{controller=Todo}/{action=Check}/{id?}/{check?}/{mode?}");

app.Run();
