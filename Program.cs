using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Services;

namespace AspNetCoreTodo;

/*
 * AddSingleton 把你的服务作为 singleton 添加进服务容器。
 * 这意味着，只有一个FakeTodoItemService的实例被创建，并在每次被请求的时候都被复用。
 */

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        //builder.Services.AddSingleton<ITodoItemService, FakeTodoItemService>();
        //builder.Services.AddSingleton<ITodoItemService, TodoItemService>();   // 这里是错误的，因为它会以 singleton 的生命周期把你的服务添加到容器里。
        builder.Services.AddScoped<ITodoItemService, TodoItemService>();    // AddScoped 会以 scoped 的生命周期把你的服务添加到容器里。这意味着每次 web 请求中，一个 TodoItemService 类的新实例就会被创建出来。这对于那些跟数据库打交道的类来说，是必要的。

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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
        app.MapRazorPages();

        app.Run();
    }
}
