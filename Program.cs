using Microsoft.EntityFrameworkCore;
using RPG.Data;
using RPG.Services.CharacterService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(
    options =>
        options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();
// AddSingleton: only one instance of the service is created and shared across all requests.
// AddScoped: a new instance of the service is created for each request.
// AddTransient: a new instance of the service is created each time it is requested.


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();