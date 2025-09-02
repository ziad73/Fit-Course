using BLL.ImplementServices;
using BLL.Services;
using DAL;
using DAL.Database;
using DAL.Entities.course;
using DAL.ImplementRepository;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
var con = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<SharaawyContext>(options => options.UseSqlServer(con));
builder.Services.AddDbContext<FitCourseDb>(options =>
    options.UseMySql(
        con,
        ServerVersion.AutoDetect(con),
        mysqlOptions => mysqlOptions.MigrationsAssembly("DAL")
    )
);
// repos injection
builder.Services.AddScoped<IRepository<Course>, CourseRepo>();


// services injection
builder.Services.AddScoped<ICourseService, CourseService>();
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
