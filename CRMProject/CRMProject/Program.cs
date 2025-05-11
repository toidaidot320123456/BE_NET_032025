using AutoMapper;
using CRMProject.AutoMapper;
using CRMProject.Middleware;
using DataAcccess.DBContext;
using DataAcccess.IServices;
using DataAcccess.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();

// Add services to the container
var configuration = builder.Configuration;

//Add Service connect SQL, kết nối đến DBContext và chuỗi Connection
builder.Services.AddDbContext<BE_NET_032025Context>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ConnStr")));
//Add Redis
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = configuration["RedisCacheUrl"]; });

builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();
app.Run();
