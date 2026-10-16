
using EmployeeManagementSystem.AppDbContext;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.Middleware;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var conn = builder.Configuration.GetConnectionString("DefaultConnectionString");
            builder.Services.AddDbContext<AppDbContext1>(option =>
            {
                option.UseSqlServer(conn);
            });
            var secreateKey = builder.Configuration["JWTCredential:SecreateKey"];
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime=true,
                    ClockSkew=TimeSpan.Zero,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreateKey!)),
                    ValidateAudience=true,
                    ValidAudience = builder.Configuration["JWTCredential:aud"],
                    ValidateIssuer=true,
                    ValidIssuer = builder.Configuration["JWTCredential:issuer"]
                });
            builder.Services.AddScoped<ICToService,EmployeeDetailsManagement>();
            builder.Services.AddScoped<IUserCredential,UserDetailsService>();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.UseMiddleware<ExceptionHandelingMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
