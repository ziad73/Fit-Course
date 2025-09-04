using BLL.ImplementServices;
using BLL.Services;
using DAL;
using DAL.Database;
using DAL.Entities.course;
using DAL.Entities.section;
using DAL.Entities.user;
using DAL.Repository;
using DAL.Repository.ImplementRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

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
builder.Services.AddScoped<IRepository<Section>, SectionRepo>();


// services injection
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISectionService, SectionService>();

builder.Services.AddIdentity<User, IdentityRole>(option =>
{
    option.Password.RequiredLength = 4;
    option.Password.RequireDigit = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.SignIn.RequireConfirmedAccount = true;


}).AddEntityFrameworkStores<FitCourseDb>().AddDefaultTokenProviders();

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
    name: "area",
    pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseSession();

app.Run();
