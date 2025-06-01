using AutoMapper;
using CRMProject;
using CRMProject.AutoMapper;
using CRMProject.Middleware;
using DataAcccess.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

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

//Add Repository
builder.Services.AddRepositoryService();

//Add Service
builder.Services.AddServiceRegistrations();

//Add JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,//tự động kiểm tra nhà phát hành token
        ValidIssuer = configuration["Jwt:ValidIssuer"],//tên nhà phát hành được cấu hình trong file appsettings.json

        ValidateAudience = true,//tự động kiểm tra người được sử dụng token
        ValidAudience = configuration["Jwt:ValidAudience"],//tên người được sử dụng token

        ValidateLifetime = true,//tự động kiểm tra thời hạn của token
        ClockSkew = TimeSpan.Zero,// bỏ thời gian trễ mặc định (5 phút) giữa client/server

        ValidateIssuerSigningKey = true,//tự động kiểm tra với SecretKey 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))

        //sử dụng Middleware app.UseAuthentication(); thì tất cả việc kiểm tra mới được thực hiện
    };
});

//Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//cấu hình để tự động gán token cho các request
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "BE03_2025 API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
//Khi triển khai trên server sẽ không hiển thị màn hình SwaggerUI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();
app.Run();
