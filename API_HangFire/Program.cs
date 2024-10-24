using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("HangfireConnection");

builder.Services.AddHangfire(x => x.UsePostgreSqlStorage(configuration.GetConnectionString("HangfireConnection")));

//builder.Services.AddHangfireServer();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Saudacao", Description = "Treinamento utiização Hangfire" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Hangfire v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireServer();
app.UseHangfireDashboard();

app.Run();
