namespace Sdr_FirmaInCloud.BL.FirmaInCloud.Domain
{
    public class PdfAssociation
    {
        public int KEY_PDF { get; set; }
        public int KEY_TEMPL { get; set; }
        public string PDF_FIELD { get; set; }
        public string TYPE { get; set; }
        public string FIELD { get; set; }
        public string BIOMETRIC { get; set; }
        public int NUM_PAGE { get; set; }
        public string FORM_DATE { get; set; }
    }
}
