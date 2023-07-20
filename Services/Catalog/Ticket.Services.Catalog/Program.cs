using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;
using Ticket.Services.Catalog.Services;
using Ticket.Services.Catalog.Settings;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IEventService, EventService>();
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));


//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
//builder.Services.AddSingleton<DatabaseSettings>(sp =>
//{
//    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
//});


//builder.Services.AddAutoMapper(typeof(Program));

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
