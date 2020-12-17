using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud.Domain
{
    public class Nazione
    {
        public int ID { get; set; }
        public string COD_REG { get; set; } 
        public string COD_ISTAT { get; set; }
        public string NAZIONE { get; set; }
        public string PROV { get; set; }
        public string CITTADINANZA { get; set; }
        public string CATASTO { get; set; }
    }
}
