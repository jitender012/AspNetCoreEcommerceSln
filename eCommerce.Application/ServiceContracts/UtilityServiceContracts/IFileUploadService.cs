using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.ServiceContracts.UtilityServiceContracts
{
    public interface IFileUploadService
    {
        Task<List<string>> UploadImageAsync(IEnumerable<IFormFile> files, string folderPath);
    }
}
