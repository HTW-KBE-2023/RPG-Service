using API.Utility;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.AddDatabaseConfiguration();
builder.AddRabbitMqConfiguration();

builder.AddApplicationServices();
builder.AddMapper();
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.RecreateDatabaseWithData();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();