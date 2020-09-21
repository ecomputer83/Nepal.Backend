using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IBlobStoreService
    {
        Task<string> UploadFileToStore(BlobStore blob);
    }
}
