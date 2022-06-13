using Application.ActivityM.Comman;
using Application.ActivityM.Query;
using Application.Extensions;
using Application.Middleware;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers().AddFluentValidation(x => { x.RegisterValidatorsFromAssemblyContaining<Create>();});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var data = app.Services.CreateScope();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
