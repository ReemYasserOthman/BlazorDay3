using Campany.BL;
using Campany.BL.Managers;
using Campany.DAL;
using Campany.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace WebApi_Day2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("p", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            var connectionString = builder.Configuration.GetConnectionString("Campany_ConString");
            builder.Services.AddDbContext<CompanyContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ITicketRepo, TicketRepo>();
            builder.Services.AddScoped<ITicketManager, TicketManager>();
            builder.Services.AddScoped<IAccManager, AccManager>();

            #region Identity Manager

            builder.Services.AddIdentity<Employee, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<CompanyContext>();

            #endregion
            #region Authentication

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Cool";
                options.DefaultChallengeScheme = "Cool";
            })
                .AddJwtBearer("Cool", options =>
                {
                    string keyString = builder.Configuration.GetValue<string>("SecretKey") ?? string.Empty;
                    var keyInBytes = Encoding.ASCII.GetBytes(keyString);
                    var key = new SymmetricSecurityKey(keyInBytes);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            #endregion
            #region Authorization

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Managers", policy => policy
                    .RequireClaim(ClaimTypes.Role, "user")
                    .RequireClaim(ClaimTypes.NameIdentifier));

                options.AddPolicy("Employees", policy => policy
                    .RequireClaim(ClaimTypes.Role, "user", "admin")
                    .RequireClaim("Nationality", "Egyptian")
                    .RequireClaim(ClaimTypes.NameIdentifier));
            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("p");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}