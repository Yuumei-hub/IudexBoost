using IudexBoost.Models.Classes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using IudexBoost.Repository;
using IudexBoost.ProjectServices.Services;
using IudexBoost.Business.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer("server=LAPTOP-9SC1960C\\SQLEXPRESS;" +
                        "database=IudexBoostDB;" +
                        "integrated security=true;"));


//Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    { options.LoginPath = "/Login/Index";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
    }
);

// Register repositories and services
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>));
//repos
builder.Services.AddScoped<GenericRepository<User> ,UserRepository>();
//services
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllersWithViews();


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

