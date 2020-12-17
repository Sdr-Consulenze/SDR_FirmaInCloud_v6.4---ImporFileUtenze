

namespace SDR_FirmaInCloud.BL
{
    public class Config
    {
        public string CONF_PARAM { get; set; }
        public string CONF_VALUE { get; set; }

        public enum ParamValues
        {
            CITY,               //Città file pdf
            COUNT,              //Progressivo File pdf
            DOMINIO_MAIL,       //Dominio indirizzo mail
            MITTENTE_MAIL,      //Mittente mail
            OGGETTO_MAIL,       //Oggetto mail
            TESTO_MAIL,         //Testo mail
            SIGN_PRO_PDF,       //Eseguibile Sign Pro Pdf
            I_EXPL,             //Internet Explorer
            RANDOM_CODE,        //Codice recupero password
            DIRECTORY_XML,      //Directory per export xml
        }
    }
}