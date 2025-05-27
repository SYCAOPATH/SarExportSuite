using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// sar: Add MVC services to the DI container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// sar: PDF rendering path setup for internal conversion ops
var env = builder.Environment;
RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Export}/{action=Index}/{id?}");
app.Run();


// Tes perubahan untuk Pull Request by : SAR


