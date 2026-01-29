using DBFirst.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;
using Service.Dto;
using System.Text;
using WebApiServer.Controllers;
using AutoMapper;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
//הגדרת התלויות 
//addscoped -לכל גולש יוצר את המופע
//addTrensient -בכל בקשה 
//addsingelton -אחד לכולם
builder.Services.AddScoped<IRepository<ContentType>, ContentTypeRepository>();
builder.Services.AddScoped<IRepository<Coupon>, CouponRepository>();
builder.Services.AddScoped<IRepository<Course>, CourseRepository>();
builder.Services.AddScoped<IRepository<Enrollment>, EnrollmentRepository>();
builder.Services.AddScoped<IRepository<Lesson>, LessonRepository>();
builder.Services.AddScoped<IRepository<Enrollment>, EnrollmentRepository>();
builder.Services.AddScoped<IRepository<Lesson>, LessonRepository>();
builder.Services.AddScoped<IRepository<Owner>, OwnerRepository>();
builder.Services.AddScoped<IRepository<Progress>, ProgressRepository>();
builder.Services.AddScoped<IRepository<Skill>, SkillRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IService<ContentTypeDto>, ContentTypeService>();
builder.Services.AddScoped<IService<CourseDto>, CourseService>();
builder.Services.AddScoped<IService<OwnerDto>, OwnerService>();
builder.Services.AddScoped<IService<SkillDto>, SkillService>();
builder.Services.AddScoped<IService<UserDto>, UserService>();

builder.Services.AddScoped<ILogin, UserLoginService>();


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MyMapper>();
});
//builder.Services.AddScoped<IContext,Database>();
//חיבור ל sql 
builder.Services.AddScoped<IContext, CoursaDbContext>();

////סווגר עם אבטחה
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "securityLessonWebApi", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Please enter your bearer token",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//            {
//                {
//                    new OpenApiSecurityScheme
//                    {
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    },
//                    new string[] {}
//                }
//            });
//});
//שימוש בטוקן כדי לאמת
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//      .AddJwtBearer(option =>
//      option.TokenValidationParameters = new TokenValidationParameters
//      {
//          ValidateIssuer = true,
//          ValidateAudience = true,
//          ValidateLifetime = true,
//          ValidateIssuerSigningKey = true,
//          ValidIssuer = builder.Configuration["Jwt:Issuer"],
//          ValidAudience = builder.Configuration["Jwt:Audience"],
//          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

//      });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
//app.UseAuthentication();
app.MapControllers();

app.Run();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


