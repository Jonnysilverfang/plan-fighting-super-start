// Loại bỏ using Microsoft.EntityFrameworkCore;
// Loại bỏ using Leaderboard.Api.Data;

using Leaderboard.Api.Services; // Thêm using cho AccountRepository

var builder = WebApplication.CreateBuilder(args);

// --- Cấu hình Services ---
// Thay thế AddDbContext bằng cách đăng ký AccountRepository
// Sử dụng Singleton vì repository chỉ cần khởi tạo 1 lần và an toàn thread-safe (chỉ chứa chuỗi kết nối).
builder.Services.AddSingleton<AccountRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình CORS
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// --- Cấu hình Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Loại bỏ code db.Database.Migrate(); và các logic EF Core khác
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();