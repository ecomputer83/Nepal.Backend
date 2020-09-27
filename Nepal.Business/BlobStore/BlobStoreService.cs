using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace Nepal.Business.Service
{
    public class BlobStoreService : IBlobStoreService
    {
        IConfiguration _configuration;
        public BlobStoreService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> UploadFileToStore(BlobStore blob)
        {
            string result = null;
            var fileShare = "articleimages";
            var number = 1;

            var rndFolderName = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 9);
            int lastIndex = blob.FileName.LastIndexOf('.');
            var name = blob.FileName.Substring(0, lastIndex);
            var ext = blob.FileName.Substring(lastIndex + 1);
            name = name + number;
            blob.FileName = $"{rndFolderName}{DateTime.Now.ToString("yyyyMMddHHmmss")}.{ext}";

            string storageConnection = _configuration.GetConnectionString("BlobStorageConnectionString");
            try
            {
                BlobContainerClient container = new BlobContainerClient(storageConnection, fileShare);
                
                if(!container.Exists())
                container.Create();

                // Get a reference to a blob named "sample-file" in a container named "sample-container"
                BlobClient _blob = container.GetBlobClient(blob.FileName);
                await _blob.UploadAsync(blob.FileStream);
                result = _blob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new Exception(ex.Message, ex);
            }

            return result;
        }
    }
}
