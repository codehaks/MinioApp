// See https://aka.ms/new-console-template for more information
using Minio;
using Minio.DataModel.Args;

Console.WriteLine("Hello, World!");

// MinIO server credentials
string endpoint = "localhost:9000"; // Your MinIO server endpoint
string accessKey = "minioadmin"; // Your access key
string secretKey = "minioadmin"; // Your secret key

var minioClient = new MinioClient()
    .WithEndpoint(endpoint)
    .WithCredentials(accessKey, secretKey)
    //.WithSSL()
    .Build();

var bucketList = await minioClient.ListBucketsAsync();

foreach (var bucket in bucketList.Buckets)
{
    Console.WriteLine(bucket.Name);
    var objArgs = new ListObjectsArgs().WithBucket(bucket.Name);
    var objList = minioClient.ListObjectsEnumAsync(objArgs);
    await foreach (var item in objList)
    {
        if (item.IsDir)
        {
            continue;
        }
        Console.WriteLine($" -- {item.Key}   |{item.Size/1024} KB");
        
    }
}

