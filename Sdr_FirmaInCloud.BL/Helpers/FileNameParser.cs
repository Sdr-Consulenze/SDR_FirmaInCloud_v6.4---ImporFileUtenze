using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDR_FirmaInCloud.BL.Helpers
{
    public class FileNameParser
    {
        public string DESCR { get; set; }
        public string PATH { get; set; }

        public FileNameParser(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            var nomeFile = fileInfo.Name.Replace(fileInfo.Extension, "");
            string[] valoriNomeFile = nomeFile.Split('_');
            this.PATH = fileInfo.FullName;
            this.DESCR = nomeFile;
        }     
    }
} 