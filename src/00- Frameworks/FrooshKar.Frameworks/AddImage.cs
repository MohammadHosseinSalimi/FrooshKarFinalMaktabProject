using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FrooshKar.Frameworks
{
	public static class AddImage
	{
		public static async Task<string> AddSingleImage(string folderName, IFormFile fileIFormFile,
			string wwwRootPath, CancellationToken cancellationToken)
		{
			var imageName = Guid.NewGuid().ToString();
			string? filepath = null;


			if (fileIFormFile != null)
			{

				// Get the path of the uploads folder
				var uploads = Path.Combine(wwwRootPath, folderName);
				if (!Directory.Exists(uploads))
				{
					// Create the folder if it doesn't exist
					Directory.CreateDirectory(uploads);
				}

				// Get the file extension
				var fileExtension = Path.GetExtension(fileIFormFile.FileName);
				filepath = imageName + fileExtension;
				// Save the file to the server
				using (var fileStream = new FileStream(Path.Combine(uploads, filepath), FileMode.Create))
				{
					await fileIFormFile.CopyToAsync(fileStream, cancellationToken);
				}
			}

			return filepath;
		}

		public static async Task<List<string>> AddMultipleImage(string folderName, List<IFormFile> fileIFormFiles,
			string wwwRootPath, CancellationToken cancellationToken)
		{
			List<string> filepaths = new List<string>();

			if (fileIFormFiles.Count > 0)
			{

				// Get the path of the uploads folder
				var uploads = Path.Combine(wwwRootPath, folderName);
				if (!Directory.Exists(uploads))
				{
					// Create the folder if it doesn't exist
					Directory.CreateDirectory(uploads);
				}


				foreach (var file in fileIFormFiles)
				{
					var imageName = Guid.NewGuid().ToString();
					// Get the file extension
					var fileExtension = Path.GetExtension(file.FileName);
					var filepath = imageName + fileExtension;
					filepaths.Add(filepath);

					using (var fileStream = new FileStream(Path.Combine(uploads, filepath), FileMode.Create))
					{
						await file.CopyToAsync(fileStream, cancellationToken);
					}

				}
			}


			return filepaths;

		}
	}
}