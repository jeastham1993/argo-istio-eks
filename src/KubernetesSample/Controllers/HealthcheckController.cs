using Microsoft.AspNetCore.Mvc;

namespace DotnetSQLServer.Blueprint.Controllers;

using Amazon.S3;

using MySqlConnector;

[ApiController]
[Route("[controller]")]
public class HealthcheckController : ControllerBase
{
    private readonly ILogger<HealthcheckController> _logger;
    private readonly AmazonS3Client s3Client;
    private readonly BookContext context;

    public HealthcheckController(ILogger<HealthcheckController> logger, AmazonS3Client s3Client, BookContext context)
    {
        _logger = logger;
        this.s3Client = s3Client;
        this.context = context;
    }

    [HttpGet(Name = "GetHealthcheck")]
    public async Task<List<string>> Get()
    {
        await this.context.Database.EnsureCreatedAsync();
        
        var buckets = await this.s3Client.ListBucketsAsync();
        
        return buckets.Buckets.Select(p => p.BucketName).ToList();
    }
}
