using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class BlobStore
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public long FileLength { get; set; }
        public Stream FileStream { get; set; }
    }
}
