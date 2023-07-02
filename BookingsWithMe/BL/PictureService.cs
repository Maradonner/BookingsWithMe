using System.Security.Cryptography;
using System.Text;
using BookingsWithMe.BL.Interfaces;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace BookingsWithMe.BL;

public class PictureService : IPictureService
{
    private const string BasePath = "./wwwroot";

    public async Task<bool> UploadImage(Stream fileStream, string filename)
    {
        const int aspectWidth = 600;
        const int aspectHeight = 800;

        using var image = await Image.LoadAsync(fileStream);
        image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

        await image.SaveAsJpegAsync(BasePath + filename, new JpegEncoder { Quality = 100 });

        return true;
    }

    public string GetWebFilename(string filename)
    {
        var dir = GetWebFileFolder(filename);

        CreateFolder(BasePath + dir);

        return dir + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpg";
    }

    private static string GetWebFileFolder(string filename)
    {
        var inputBytes = Encoding.ASCII.GetBytes(filename);
        var hashBytes = MD5.HashData(inputBytes);

        var hash = Convert.ToHexString(hashBytes);

        return string.Concat("/images/", hash.AsSpan(0, 2), "/", hash.AsSpan(0, 4));
    }

    private static void CreateFolder(string dir)
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }
}