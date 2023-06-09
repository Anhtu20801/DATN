using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QLSV.Data;
using QLSV.Data.Data;
using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StudentDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<StudentDBContext>();

builder.Services.AddScoped<IDbInitialUser, DbInitialUser>();

builder.Services.ConfigureApplicationCookie(ops =>
{
    ops.LoginPath = $"/Account/Login";
    ops.LogoutPath = $"/Account/Logout";
    ops.AccessDeniedPath = $"/Account/AccessDenied";

    // Cookie settings 
    // prevent cookie from being accessed 
    // through javascript on the client side 
    ops.Cookie.HttpOnly = true;

    ops.ExpireTimeSpan = TimeSpan.FromMinutes(30);

    ops.SlidingExpiration = true;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapRazorPages();
app.UseStaticFiles();

SeedDatabase();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    app.MapControllerRoute(
        name: "default",
        pattern: "/{controller=Account}/{action=Login}/{id?}");
});

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitialUser>();
        dbInitializer.InitialUser();
    }
}
