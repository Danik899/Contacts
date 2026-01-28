using Microsoft.EntityFrameworkCore;
using Task.DataBase;
using Task.Services;

var builder = WebApplication.CreateBuilder(args);

// Подключение postgres к проекту через защищенный файл json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Сервисы
builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IContactsService, ContactsService>();

builder.Services.AddControllers();

var app = builder.Build();

// Инициализация БД
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
    db.Database.EnsureCreated();
}

// Место для отлова ошибки при запуске
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(appError =>
    {
        appError.Run(async context =>
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error");
        });
    });
}

// Middleware
app.UseRouting();
app.UseAuthorization();

// Эндпоинты
app.MapControllers();

app.Run();