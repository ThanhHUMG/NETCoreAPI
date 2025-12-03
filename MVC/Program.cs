using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using MVC.Data;

var builder = WebApplication.CreateBuilder(args);

// EPPlus License - dùng cho phi thương mại
ExcelPackage.License.SetNonCommercialPersonal("Thanh Mai");

// Đăng ký DbContext dùng SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDbContext"))
);

// Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();
