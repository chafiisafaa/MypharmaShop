using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Web.Http;

namespace Planning.Functions
{
    public static class FileProcessing
    {
        [FunctionName("FileProcessing")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            // Get the the HTTP basic authorization credentials
           /* var token = req.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            if (string.IsNullOrEmpty(token))
                return new BadRequestErrorMessageResult("Token is required");*/

            var files = req.Form.Files;
            if (files.Count == 0)
                return new BadRequestErrorMessageResult("No files found in the request.");

            try
            {
                var blobContainer = await GetBlobContainer();

                var uploadedFileUrls = await UploadFilesToBlobStorage(files, blobContainer);

                return new OkObjectResult(uploadedFileUrls);

            }
            catch (Exception ex)
            {
                return new BadRequestErrorMessageResult(ex.Message);
            }
        }

        private static async Task<CloudBlobContainer> GetBlobContainer()
        {
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=crmshopblob;AccountKey=AtWI9xgmeenks+ZKGdNF4O/sZq04JVqgaKeG830bxN1YJOZUMGAv6hSB1mBglKMnpfUK20WDn76o+ASt/bADBg==;EndpointSuffix=core.windows.net");
            var blobClient = storageAccount.CreateCloudBlobClient();
            var containerName = "photos";

            var blobContainer = blobClient.GetContainerReference(containerName);
            await blobContainer.CreateIfNotExistsAsync();

            return blobContainer;
        }

        private static async Task<string[]> UploadFilesToBlobStorage(IFormFileCollection files, CloudBlobContainer blobContainer)
        {
            var uploadedFileUrls = new string[files.Count];

            for (var i = 0; i < files.Count; i++)
            {
                var file = files[i];

                var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
                var blob = blobContainer.GetBlockBlobReference(fileName);

                await using (var stream = file.OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }

                uploadedFileUrls[i] = blob.Uri.ToString();
            }

            return uploadedFileUrls;
        }
    }
}
