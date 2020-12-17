using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud.Domain
{
    public class Comuni 
    {
        public int ID { get; set; }
        public int ID_NAZIONE { get; set; }
        public string ID_PROV { get; set; }
        public string COD_REG { get; set; }
        public string COD_ISTAT { get; set; }
        public string COMUNE { get; set; }
        public string PROV { get; set; }
        public string CITTADINANZA { get; set; }
        public string CATASTO { get; set; }
        public string CAP { get; set; }
        public string PREF_TEL { get; set; }
        public string LAT { get; set; }
        public string LONGIT { get; set; }
    }
}
