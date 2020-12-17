using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdr_FirmaInCloud.BL.FirmaInCloud.Mapper
{
    class ExportXmlDB
    {
        public int TotalElements { get; set; }
        public string[] GetXML(string sqlQuery, int elementiPerFile)
        {
            List<string> xmlPreparati = new List<string>();

            TotalElements = 0;
            try
            {
                var list = this.GetEntity(sqlQuery, elementiPerFile);
                if (list == null) return null;

                foreach (var entitaDaSerializzare in list)
                {
                    if (entitaDaSerializzare != null)
                    {
                        xmlPreparati.Add(Serializza(entitaDaSerializzare));
                    }
                }
                return xmlPreparati.ToArray();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message, e);
            }
        }
        private System.Collections.IEnumerable GetEntity(string sqlQuery, int numberOfElements)
        {
            return GetEntity<GESTIONE_POD>(sqlQuery, numberOfElements);
        }

        private string Serializza(object obj)
        {
            string xmlString = Serializzatore.Serializza<GESTIONE_POD>(obj as GESTIONE_POD);

            using (System.IO.StringReader rdr = new System.IO.StringReader(xmlString))
            {
                Serializzatore.Validate(rdr, this.AssemblyPath + @"\xsd\RetiTerna\FlussoG01_GESTIONE_POD.xsd");
            }
            return xmlString;

        }

    }
