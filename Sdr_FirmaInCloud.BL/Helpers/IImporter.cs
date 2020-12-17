using MySql.Data.MySqlClient;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Domain;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDR_FirmaInCloud.BL.Helpers
{
    public static class IImporter
    {
        public static void ImportaTemplate(string filePath)
        {
            FileNameParser parser = new FileNameParser(filePath);

            Template template = new Template
            {
                DTINS = DateTime.Now,
                DESCR = parser.DESCR,
                PATH = parser.PATH,
            };

            TemplateDB.SaveTemplate(template);

            TemplateDB.RunScriptTemplate(template.KEY_TEMPL, template.DESCR);

            List<PdfAssociation> pdfAssociations = PdfAssociationDB.LoadPdfAssociation(template.KEY_TEMPL); // chiave template

            if (pdfAssociations == null)
                throw new Exception("Associazioni field/pdf non valorizzate");

        }
        public static void ImportaTemplateLinq(int idKeyUte)
        {
            TemplateLinqDB.SaveTemplateLinq(idKeyUte);
        }
   
    }
}
