using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
var app = builder.Build();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("conntectionString"));
builder.Services.AddSpaStaticFiles(configuration: options => { options.RootPath = "wwwroot"; });
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueCorsPolicy", builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://localhost:5001");
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Okta:Authority"];
    });
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

var dbContext = app.Services.GetRequiredService<ApplicationDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("VueCorsPolicy");

dbContext.Database.EnsureCreated();
app.UseAuthentication();
app.UseMvc();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseSpaStaticFiles();
app.UseSpa(builder =>
{
    if (app.Environment.IsDevelopment())
    {
        builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
    }
});
