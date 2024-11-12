using Advanced.Domain.Repositories.Images;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Advanced.Persistence.Repositories.Images;

public class ImageRepository<T> : IImageRepository
{
    private readonly IGridFSBucket _bucket;

    public ImageRepository(IGridFSBucket bucket)
    {
        _bucket = bucket;
    }
    
    public async Task<ObjectId> UploadImageAsync(string filePath)
    {
        byte[] imageBytes = await File.ReadAllBytesAsync(filePath);

        using var stream = new MemoryStream(imageBytes);
        var imageId = await _bucket.UploadFromStreamAsync("filename.jpg", stream);

        return imageId;
    }
    
    public async Task<byte[]> DownloadImageAsync(ObjectId imageId)
    {
        using var stream = new MemoryStream();
        await _bucket.DownloadToStreamAsync(imageId, stream);
        return stream.ToArray();
    }
    
    public async Task SaveImageToLocalAsync(ObjectId imageId, string outputPath)
    {
        var imageData = await DownloadImageAsync(imageId);
        await File.WriteAllBytesAsync(outputPath, imageData);
    }

}