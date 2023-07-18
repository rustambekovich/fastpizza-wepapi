using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Interfaces.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FastPizza.Service.Services.Common;

public class FileService : IFileService
{
    private readonly string MEDIA = "media";
    private readonly string IMAGES = "images";
    private readonly string AVATARS = "avatars";
    private readonly string ROOTPATH;
    public FileService(IWebHostEnvironment env)
    {
        this.ROOTPATH = env.WebRootPath;
    }
    public Task<bool> DeleteAvatarAsync(string subpath)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteImageAsync(string subpath)
    {
        string path = Path.Combine(ROOTPATH, subpath);
        if (File.Exists(path))
        {
            await Task.Run(() =>
            {
                File.Delete(path);
            });
            return true;
        }
        return false;
    }

    public Task<bool> UploadAvatarAsync(IFormFile image)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        string newname = MediaHelper.CreateImageName(image.FileName);
        string subpath = Path.Combine(MEDIA, IMAGES, newname);
        string path = Path.Combine(ROOTPATH, subpath);

        var stream = new FileStream(path, FileMode.Create);
        await image.CopyToAsync(stream);
        stream.Close();
        return subpath;
    }
}
