using eCommerce.Application.ServiceContracts.UtilityServiceContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<List<string>> UploadFilesAsync(IEnumerable<IFormFile> files, string folderPath)
        {
            if (files == null || !files.Any())
                throw new ArgumentException("No files to upload");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var uploadedFiles = new List<string>();

            var fullFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderPath);
            Directory.CreateDirectory(fullFolderPath);

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    throw new InvalidOperationException("Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(fullFolderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream); // Save file
                }

                uploadedFiles.Add(Path.Combine("/",folderPath, fileName).Replace("\\", "/")); // Store relative path
            }

            return uploadedFiles;
        }
    }

}
