using MvcTodo.Services;
using MvcTodo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI’è‹`
builder.Services.AddSingleton<ITodoService,TodoService>();

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
