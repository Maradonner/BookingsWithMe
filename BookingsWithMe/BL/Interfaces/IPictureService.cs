namespace BookingsWithMe.BL.Interfaces;

public interface IPictureService
{
    Task<bool> UploadImage(Stream fileStream, string filename);
    string GetWebFilename(string filename);
}