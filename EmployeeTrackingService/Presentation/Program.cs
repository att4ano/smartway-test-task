using Application.Extensions;
using Infrastructure.DataAccess.Extensions;
using Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataInfrastructure(builder.Configuration);
builder.Services.AddDataAccess();
builder.Services.AddApplication();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.DoMigrations();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
