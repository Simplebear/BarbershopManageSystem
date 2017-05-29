using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace BMS.Utils
{
    public class FormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private List<string> _originname = new List<string>();
        public FormDataStreamProvider(string path)
        : base(path)
        { }

        public Stream GetAttachmentStream(string path, int index)
        {
            // restrict what images can be selected
            var extensions = new[] { "png", "gif", "jpg", "csv", "xlsx", "xls", "jpeg", "bmp" };


            if (OriginName[index].IndexOf('.') < 0)
                return Stream.Null;

            var extension = OriginName[index].Split('.').Last();

            return extensions.Any(i => i.Equals(extension, StringComparison.InvariantCultureIgnoreCase))
                       ? new FileStream(path, FileMode.Open)
                       : Stream.Null;

        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            // override the filename which is stored by the provider (by default is bodypart_x)
            string oldfileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            _originname.Add(oldfileName);
            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(oldfileName);
            Debug.WriteLine("ASSSSSSSSS   " + newFileName);
            return newFileName;
        }

        public List<string> OriginName
        {
            get
            {
                return _originname;
            }
        }

    }
}
