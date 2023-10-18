using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;
using WebApi.Authorization;
using WebApi.Repositories;
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddCors();
services.AddControllers();
services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("db"));
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
services.AddScoped<IUserService, UserService>();
services.AddScoped<IAdminService, AdminService>();
services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var user = new User
    {
        id = 1,
        login = "test",
        isAdmin = true,
        passwordResetDate = new DateTime(),
        isBlocked = false,
        passwordCriteria = false,
        password = UserPasswordHelper.hashPassword("test"),
    };
    db.Users.Add(user);
    db.SaveChanges();
}

app.Run("http://localhost:4000");
