using WebApi.Helpers;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WebApi.Entities;
var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("MojaBazaDanych"));
    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();

}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = new User { id = 1, login = "test", role = "user", resetDate = new DateTime(), blocked = false, criteria = false, password = "test", salt =null };

        db.Users.Add(user);
        db.SaveChanges();
    }
    app.MapControllers();
}

app.Run("http://localhost:4000");
