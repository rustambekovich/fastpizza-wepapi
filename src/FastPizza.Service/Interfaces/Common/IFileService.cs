using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Interfaces.Common
{
    public interface IFileService
    {
        // returun sub image 

        public Task<string> UploadImageAsync(IFormFile image);
        public Task<bool> DeleteImageAsync(string subpath);
        // returun sub avatar 
        public Task<bool> UploadAvatarAsync(IFormFile image);
        public Task<bool> DeleteAvatarAsync(string subpath);

    }
}
