using System.Text.Json;

using Amazon.S3;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

using DotnetSQLServer.Blueprint;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AmazonS3Client>(new AmazonS3Client());

var secretsManagerClient = new AmazonSecretsManagerClient();
var secret = secretsManagerClient.GetSecretValueAsync(
        new GetSecretValueRequest()
        {
            SecretId = builder.Configuration["SecretName"]
        })
    .GetAwaiter()
    .GetResult();

var databaseConnection = JsonSerializer.Deserialize<DatabaseConnection>(secret.SecretString);

builder.Services.AddDbContext<BookContext>(
    options =>
    {
            options.UseMySQL($"server=mysql-aurora-default.c6re1x2hf9zq.eu-west-1.rds.amazonaws.com;uid={databaseConnection.Username};pwd={databaseConnection.Password};database=mysql-aurora-default;Connection Lifetime=900");
    });

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
