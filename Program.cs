using Microsoft.EntityFrameworkCore;
using PersonManagementAPI.Data;
using PersonManagementAPI.Filters;
using PersonManagementAPI.Middleware;
using PersonManagementAPI.Repositories;
using PersonManagementAPI.Services;
using PersonManagementAPI.Validations;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IConnectedPersonService, ConnectedPersonService>();

// Register Unit of Work and repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IConnectedPersonRepository, ConnectedPersonRepository>();

// Register validators
builder.Services.AddScoped<PersonValidator>();

// Configure controllers with filters and JSON options
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationActionFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Person Directory API", Version = "v1" });

    c.OperationFilter<FileUploadOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Add custom middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LocalizationMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();