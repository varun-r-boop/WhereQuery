using Final.Authorization;
using Final.Data;
using Final.Helpers;
using Final.Services;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program));
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();



// Injecting DbContext and Connection String
builder.Services.AddDbContext<HomezillaContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("_connectionString")));

// configure DI for application services
builder.Services.AddScoped<IJwtUtils, JwtUtils>();


builder.Services.AddScoped<ICustomerService, CustomerService>();

// Configure the HTTP request pipeline.
var app = builder.Build();
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
