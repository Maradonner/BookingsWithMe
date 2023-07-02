using BookingsWithMe.ActionFilter;
using BookingsWithMe.BL;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.Cookies;
using BookingsWithMe.DAL;
using BookingsWithMe.DAL.Data;
using BookingsWithMe.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options => { });


builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddSingleton<IPictureService, PictureService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddScoped<IAvailabilitiesRepository, AvailabilitiesRepository>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IWebCookie, WebCookie>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();

app.Run();