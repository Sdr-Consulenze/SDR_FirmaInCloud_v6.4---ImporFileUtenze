using Common.Logging;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Domain;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;



namespace Sdr_FirmaInCloud.BL.FirmaInCloud
{
    public class EricsoftDB
    {
        internal static Header GetHeader(string strSourceGenerator)
        {
            Header header = new Header();
            header.SourceGenerator = strSourceGenerator;
            header.Date = DateTime.Now.ToString("yyyy-MM-dd");
            return header;
        }

        internal static Residence GetResidence(string sst, string strada, string ncn)
        {
            string address = "";
            Residence Residence = new Residence();
            if (!string.IsNullOrEmpty(sst) || !string.IsNullOrEmpty(strada))
                address = sst + " " + strada + " " + ncn;

            Residence.Address = address;
            return Residence;
        }

        internal static Nation GetNation(int? keyNa)
        {
            Nation nation = new Nation();
            string strIdentCode = ""; 
            if (keyNa != null)
                strIdentCode = IndirizzoDB.LoadNazione(keyNa,string.Empty).PROV;

            nation.IdentificationCode = string.IsNullOrEmpty(strIdentCode) ? "" : strIdentCode;
            return nation;
        }

        internal static City GetCity(int? keyCom, int? keyLoc)
        {
            City city = new City();
            if (keyCom != null)
            {
                Comuni comuni = IndirizzoDB.LoadComuni(keyCom);

                city.Name = comuni.COMUNE == null ? "" : comuni.COMUNE;
                city.Zip = comuni.CAP == null ? "" : comuni.CAP;
                city.District = IndirizzoDB.LoadLocalita(keyLoc).LOCALITA;
            }
            else 
            {
                city.Name = "";
                city.Zip = "";
                city.District = "";
            }
            return city;
        }

        internal static BirthData GetBirthDate(DateTime dtnas)
        {
            BirthData birthData = new BirthData();
            birthData.Date = dtnas.ToString("yyyy-MM-dd");            
            return birthData;
        }

        internal static Nationality GetNationality(int? keyNa)
        {
            Nationality nationality = new Nationality();
            if (keyNa != null)
            {
                Nation nation = new Nation();
                nationality.Nation = GetNation(keyNa);
            }
            return nationality;
        }

        internal static IdentificationDocument GetIdentificationDocument(int? keyDoc) 
        {
            IdentificationDocument identificationDocument = new IdentificationDocument();
            if (keyDoc != null)
            {
                Document document = new Document();
                document = DocumentDB.LoadDocument(keyDoc);

                identificationDocument.Type = (Type)Enum.Parse(typeof(Type), document.TYP_DOC.ToString(), true); //Enum. Parse(document.TYP_DOC);
                identificationDocument.Number = string.IsNullOrEmpty(document.NUM_DOC) == true ? "" : document.NUM_DOC;
                identificationDocument.IssueDate = document.ISS_DOC.ToString("yyyy-MM-dd");
                identificationDocument.City = new City();
                identificationDocument.City.Name = string.IsNullOrEmpty(document.CITY_DOC) == true ? "" : document.CITY_DOC; 
                identificationDocument.City.District = "";
                identificationDocument.City.Zip = string.IsNullOrEmpty(document.ZIP_CODE) == true ? "" : document.ZIP_CODE; 
                identificationDocument.Nation = new Nation();
                identificationDocument.Nation.IdentificationCode = string.IsNullOrEmpty(document.ID_DOC) == true ? "" : document.ID_DOC; 
            }
            else 
            {
                identificationDocument.Type = Type.None;
                identificationDocument.Number = "";
                identificationDocument.IssueDate = "";
                identificationDocument.City = new City();
                identificationDocument.City.Name = "";
                identificationDocument.City.District = "";
                identificationDocument.City.Zip = "";
                identificationDocument.Nation = new Nation();
                identificationDocument.Nation.IdentificationCode = "";
            }
            //identificationDocument.Authorization = new Authorization();
            //identificationDocument.Authorization.PersonalDataUse = "";
            return identificationDocument;
        }

        internal static Authorization GetAuthorization()
        {
            Authorization authorization = new Authorization();
            authorization.PersonalDataUse = "";
            return authorization;
        }

        public static EricsoftGuestData LoadEricsoft(Users user)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                EricsoftGuestData ericsoft = new EricsoftGuestData();

                Header header = new Header();
                header.SourceGenerator = "Riferimento programma";
                header.Date = DateTime.Now.ToString("yyyy-MM-dd");
                    
                Body body = new Body();
                body.Name = user.NOME;
                body.Surname = user.COGNOME;
                body.Gender = user.SEX;
                body.Residence = GetResidence(user.SST,user.STRADA,user.NCN);
                body.Residence.City = GetCity(user.KEY_COM,user.KEY_LOC);
                body.Residence.Nation = GetNation(user.KEY_NAZ);
                    
                body.BirthData = GetBirthDate(user.DTNAS);
                body.BirthData.City = GetCity(user.KEY_COM_NAS,user.KEY_LOC_NAS);
                body.BirthData.Nation = GetNation(user.KEY_NAZ_NAS);

                body.Nationality = GetNationality(user.KEY_NAZ_NAS);
                body.IdentificationDocument = GetIdentificationDocument(user.KEY_DOC);
                body.Authorization = GetAuthorization();

                ericsoft.Header = header;
                ericsoft.Body = body;

                if (!GestioneMySql.CloseConnection()) 
                    throw new Exception("Errore nella chiusura della connessione.");

                return ericsoft;

            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
                return null;
            } 
        }
        public static void GetXML(Users user) 
        {
            string strFilePath = FileService.CreateFolder(ConfigDB.LoadConfig(Config.ParamValues.DIRECTORY_XML).CONF_VALUE);

            //deve essere creata una nuova cartella dentro la quale andare a salvare il file chiamato sempre guest
            //string strFilePath = CaricaFileService.GetXMLPathFileToSave();
            if (string.IsNullOrEmpty(strFilePath))
                return;

            strFilePath = strFilePath + "\\Guest.xml";

            FileStream fileStream = new FileStream(strFilePath, FileMode.Create, FileAccess.Write);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(EricsoftGuestData));
                EricsoftGuestData ericsofts = LoadEricsoft(user);

                using (fileStream)
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    serializer.Serialize(fileStream, ericsofts, ns);

                    if (serializer == null)
                        throw new SerializationException();
                }
                if (!File.Exists(strFilePath))
                    throw new ApplicationException(" File non esistente. ");
                
                MessageBox.Show("File è stato correttamente salvato nella directory: " + strFilePath, "Export File", MessageBoxButtons.OK,MessageBoxIcon.Information);                
            }
            catch (ApplicationException e)
            {
                MessageBox.Show("Impossibile trovare il file nella posizione indicata. " + e.Message, "File non trovato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SerializationException e)
            {
                MessageBox.Show("Serializzazione fallita. Motivo: " + e.Message, "Export File", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}
