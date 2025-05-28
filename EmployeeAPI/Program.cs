using System.Data;
using EmployeeAPI.Repositories;
using EmployeeAPI.Repositories.Interfaces;
using EmployeeAPI.Services;
using EmployeeAPI.Services.Interfaces;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder.Configuration, builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();

app.Run();


void ConfigureServices(IConfiguration configuration, IServiceCollection services)
{
    services.AddScoped<IDbConnection>(_ =>
        new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

    services.AddRouting(options => { options.LowercaseUrls = true; });

    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    services.AddScoped<IEmployeeService, EmployeeService>();

    services.AddControllers();
}