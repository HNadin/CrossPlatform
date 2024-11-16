using Lab6.Data;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddSwaggerGen();

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
