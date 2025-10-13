using KienAPI.Services;
using Microsoft.OpenApi.Models;

namespace KienAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add controllers
            builder.Services.AddControllers();

            // Add Swagger/OpenAPI support
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KienAPI",
                    Version = "v1",
                    Description = "API kết nối Database & AWS RDS"
                });
            });

            // Đăng ký service kết nối DB
            builder.Services.AddSingleton<DatabaseService>();

            var app = builder.Build();

            // Dùng Swagger khi debug
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
