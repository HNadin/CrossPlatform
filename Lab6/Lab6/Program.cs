using Lab6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;


namespace Lab6
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });


            var databaseType = builder.Configuration["DatabaseType"];
            switch (databaseType)
            {
                case "SqlServer":
                    builder.Services.AddDbContext<ApplicationContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
                    break;
                case "PostgreSQL":
                    builder.Services.AddDbContext<ApplicationContext>(options =>
                        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
                    break;
                case "Sqlite":
                    builder.Services.AddDbContext<ApplicationContext>(options =>
                        options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
                    break;
                case "InMemory":
                    builder.Services.AddDbContext<ApplicationContext>(options =>
                        options.UseInMemoryDatabase("InMemoryDb"));
                    break;
            }

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
                    options.Audience = builder.Configuration["Auth0:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}",
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Auth0:Audience"],
                        ValidateLifetime = true
                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.Migrate(); 
                context.Seed();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
