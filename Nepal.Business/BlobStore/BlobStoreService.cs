using Nepal.Abstraction.Service.Business;
using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

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
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(fileShare);

            //create a container if it is not already exists

            if (cloudBlobContainer.CreateIfNotExistsAsync().Result)
            {

                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            }

            try
            {
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blob.FileName);
                cloudBlockBlob.Properties.ContentType = blob.ContentType;

                await cloudBlockBlob.UploadFromStreamAsync(blob.FileStream);
                

                result = cloudBlockBlob.Uri.AbsoluteUri;

            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new Exception("Unable to upload file to the cloud, contact the administrator");
            }

            return result;
        }
    }
}
