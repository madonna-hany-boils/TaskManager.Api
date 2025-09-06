
using Microsoft.IdentityModel.Tokens;

using Microsoft.EntityFrameworkCore;
using Tasky;
using Tasky.Services;
using System.Text;
using TaskManager.Api.Repository;
using Tasky.Models;
using TaskManager.Api.AutoMapper;
namespace TaskManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //add dbcontext
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
            builder.Services.AddScoped<JwtTokenService>();
            builder.Services.AddScoped<ITaskRepository,TaskRepository>();
            builder.Services.AddAutoMapper(typeof(TaskProfile).Assembly);


            // Authentication Middleware
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ValidateLifetime = true
                    };
                });

            builder.Services.AddAuthorization();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Enable CORS         
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
