using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Services;

namespace AspNetCoreTodo;

/*
 * AddSingleton ����ķ�����Ϊ singleton ��ӽ�����������
 * ����ζ�ţ�ֻ��һ��FakeTodoItemService��ʵ��������������ÿ�α������ʱ�򶼱����á�
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
        //builder.Services.AddSingleton<ITodoItemService, TodoItemService>();   // �����Ǵ���ģ���Ϊ������ singleton ���������ڰ���ķ�����ӵ������
        builder.Services.AddScoped<ITodoItemService, TodoItemService>();    // AddScoped ���� scoped ���������ڰ���ķ�����ӵ����������ζ��ÿ�� web �����У�һ�� TodoItemService �����ʵ���ͻᱻ�����������������Щ�����ݿ�򽻵�������˵���Ǳ�Ҫ�ġ�

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
