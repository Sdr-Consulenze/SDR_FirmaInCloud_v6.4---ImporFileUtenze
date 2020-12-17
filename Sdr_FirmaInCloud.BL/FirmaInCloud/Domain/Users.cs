using System;

namespace SDR_FirmaInCloud.BL
{
    public class Users
    {
        public int KEY_USER { get; set; }
        public string NOME { get; set; }
        public string COGNOME { get; set; }
        public DateTime DTINS { get; set; }
        public DateTime DTNAS { get; set; }
        public string RAG_SOCIALE { get; set; }
        public string TYP_UTE { get; set; }
        public string SEX { get; set; }
        public string P_IVA { get; set; }
        public string COD_FIS { get; set; } 
        public int? KEY_NAZ { get; set; }
        public int? KEY_REG { get; set; }
        public int? KEY_PRO { get; set; }
        public int? KEY_COM { get; set; }
        public int? KEY_LOC { get; set; }

        public string STRADA { get; set; }
        public string SST { get; set; } 
        public string NCN { get; set; }        
        public string CELL { get; set; }
        public string TEL { get; set; }
        public string MAIL { get; set; }
        public int KEY_TEMPL { get; set; }
        public string X_LINQ { get; set; }
        public DateTime? DT_LINQ { get; set; }

        public string PERS1 { get; set; }
        public string PERS2 { get; set; }
        public string PERS3 { get; set; }
        public string PERS4 { get; set; }
        public string PERS5 { get; set; }
        public string PERS6 { get; set; }

        public int? KEY_NAZ_NAS { get; set; }
        public int? KEY_REG_NAS { get; set; }
        public int? KEY_PRO_NAS { get; set; }
        public int? KEY_COM_NAS { get; set; }
        public int? KEY_LOC_NAS { get; set; }
        public int? KEY_DOC { get; set; }
    }
}
