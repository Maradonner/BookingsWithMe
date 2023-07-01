using BookingsWithMe.BL.Interfaces;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace BookingsWithMe.BL
{
    public class PictureService : IPictureService
    {
        public async Task<bool> UploadImage(Stream fileStream, string filename)
        {
            var aspectWidth = 600;
            var aspectHeight = 800;

            using (Image image = await Image.LoadAsync(fileStream))
            {
                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

                await image.SaveAsJpegAsync("./wwwroot/images" + filename, new JpegEncoder() { Quality = 75 });
            }
            return true;
        }
    }
}
