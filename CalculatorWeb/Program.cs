var builder = WebApplication.CreateBuilder(args);

// Servicios b�sicos
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuraci�n del pipeline
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
	pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();