using iText.Forms;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Domain;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;
using static SDR_FirmaInCloud.BL.Helpers.Api;

namespace SDR_FirmaInCloud.BL
{
    public class PDFManager
    {
        static Rectangle rectangle;
        private static int numPage;

        public static string RunSignPro(string FileNameOut,int keyTemplate, Users user)
        {
            var pathString = FirmaInCloudDB.LoadTemplate(keyTemplate, "").AsEnumerable().First();

            string strFileNameInput = pathString.ItemArray[2].ToString();
            string strFileNameOutput = FileNameOut;

            string strFileName = PDFManager.FillPdf(strFileNameInput, strFileNameOutput, user, keyTemplate);

            if (string.IsNullOrEmpty(strFileName))
            {
                if (DialogResult.No == MessageBox.Show("Documento non firmato, Vuoi riprovare?", "Firma non valida", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return strFileName;
                }
                else
                {
                    return RunSignPro(FileNameOut, keyTemplate,user);
                }
            }
            else
            {
                return strFileName;
            }
        }
        private static string FillPdf(string strFileNameInput, string strFileNameOuput, Users user, int intKey)
        {
            try
            {
                //strFileNameInput = "C:\\Users\\Notebook\\source\\repos\\SDR_FirmaInCloud\\Template FirmaInCloud\\GrandePrivacy.pdf";

                string jsonString = System.IO.File.ReadAllText(".\\Pdf\\configApi.json");
                SignProApi signProConfig = JsonSerializer.Deserialize<SignProApi>(jsonString);

                string strFileOutPut = CreatePdf(strFileNameInput, strFileNameOuput, user, intKey);
                if (string.IsNullOrEmpty(strFileOutPut))
                    return null;

                signProConfig.file.input.filesystem = strFileOutPut;

                FileInfo fileInfo = new FileInfo(strFileOutPut); 

                signProConfig.file.output.filesystem =  $"{FileHelper.GetPath("pdfElab")}{user.COGNOME}" + " " + $"{user.NOME}" + " " + $"{DateTime.Today:yyyyMMdd}" + "_Out_" + fileInfo.Extension;

                //strFileOutPut = signProConfig.file.output.filesystem;

                Signature signature = SetSignature(user.COGNOME + " " + user.NOME);

                signProConfig.signatures = new List<Signature>();
                signProConfig.signatures.Add(signature);

                jsonString = JsonSerializer.Serialize<SignProApi>(signProConfig);
                string b64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonString));

                var runCommand = ConfigDB.LoadConfig(Config.ParamValues.SIGN_PRO_PDF).CONF_VALUE;
                if (string.IsNullOrEmpty(runCommand))
                    throw new Exception("stringa di configurazione per Wacom Sign Pro Pdf non settata correttamente.");

                //string runCommand = config.CONF_VALUE;//"C:\\Program Files (x86)\\Wacom sign pro PDF\\Sign Pro PDF.exe"; // - api signpro:< base64 - json >";

                var param = "-api signpro:" + b64;
                //Process.Start("C:\\Program Files (x86)\\Wacom sign pro PDF\\Sign Pro PDF.exe", param);

                
                ProcessStartInfo processStartInfo = new ProcessStartInfo(runCommand, param);
                Process process = new Process();
                process.StartInfo = processStartInfo;

                if (!process.Start())
                    throw new InvalidOperationException(" configurazione errata - Wacom Sign Pro Pdf");

                process.WaitForExit();
                if (System.IO.File.Exists(signProConfig.file.output.filesystem))
                {
                    return signProConfig.file.output.filesystem;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch(InvalidOperationException invEx) 
            {
                MessageBox.Show("Errore:" + invEx.Message,"Wacom Sign Pro Pdf",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                return null;
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Errore: " + ex.Message, "Configurazione errata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        } 
        private static void AddSignature(PdfAcroForm form, PdfAssociation itempdf) 
        {
            PdfArray position = form.GetField(itempdf.PDF_FIELD).GetWidgets()[0].GetRectangle();
            rectangle = position.ToRectangle();

            numPage = itempdf.NUM_PAGE;

            if (!string.IsNullOrEmpty(itempdf.BIOMETRIC))
                form.RemoveField(itempdf.PDF_FIELD);
        }
        private static void MergePdfTxt(PdfAcroForm form, PdfAssociation itempdf, object obj) 
        {
            try
            {
                if (obj == null)
                    throw new Exception("Oggetto non definito.");

                string strValue = ManageValue(obj, itempdf);
                form.GetField(itempdf.PDF_FIELD).SetValue(strValue);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Errore: " + ex.Message, "Configurazione errata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                throw ex;
            }
        }
        private static string GetPropValue(object obj, string propName)
        {
            string ret = null;
            try
            {
                if (obj == null)
                    throw new Exception("Utente non configurato.");

                Type type = obj.GetType();
                if (type != null)
                {
                    foreach (string part in propName.Split('.'))
                    {
                        PropertyInfo info = type.GetProperty(part);
                        if (info != null)
                        {
                            obj = info.GetValue(obj, null);
                            if (obj != null)
                                ret = obj.ToString();
                        }
                    }
                }
                
                return ret;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        } 
        private static string ManageValue(object obj, PdfAssociation itempdf) 
        {
            string ret = null;
            try
            {
                if (itempdf.FIELD == null)
                {
                    switch (itempdf.FORM_DATE)
                    {
                        case "dd":
                            ret = DateTime.Now.Day.ToString();
                            break;
                        case "MM":
                            ret = DateTime.Now.Month.ToString();
                            break;
                        case "yyyy":
                            ret = DateTime.Now.Year.ToString();
                            break;
                    }                    
                }
                else
                {
                    foreach (string part in itempdf.FIELD.Split(','))
                    {
                        ret = string.Concat(GetPropValue(obj, part), " ", ret);
                    }
                }
                return ret;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
        private static string CreatePdf(string strFileNameInput, string strFileNameOutput, Users user, int intKey) 
        {
            PdfReader reader = new PdfReader(strFileNameInput);
            PdfDocument pdf = new PdfDocument(reader, new PdfWriter(strFileNameOutput));
            try
            {
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, false);

                List<PdfAssociation> pdfAssociations = PdfAssociationDB.LoadPdfAssociation(intKey); // chiave template

                if (pdfAssociations == null)
                    throw new Exception("Associazioni field/pdf non valorizzate");

                if(pdfAssociations.Count == 1)
                {
                    MessageBox.Show("Template non correttamente configurato", "Errore Configurazione",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                // ciclo la lista e per ciascun ciclo vado a leggere il campo TIPO CAMPO 
                foreach (var itempdf in pdfAssociations)
                {
                    if (form.GetFormFields().ContainsKey(itempdf.PDF_FIELD)) // "txtMail, txtNameSurname ecc ecc"
                    {
                        switch (itempdf.TYPE)//"TXT","SIG" ecc ecc 
                        {
                            case "TXT":
                                MergePdfTxt(form, itempdf, user);
                                
                                break;
                            case "SIG":
                                AddSignature(form, itempdf);
                                break;
                        }
                    }
                }

                #region
                //IDictionary<string, PdfFormField> keyValuePairs = form.GetFormFields();
                //foreach (var item in keyValuePairs)
                //{

                //   item.Key --> Search modello + item.Key su pdf associati.
                //   se esiste
                //   in base al tipo campo fai switch
                //   se txt :
                //   se valore associato = "#CURRENTDATE#" fai insert data
                //   se diverso fai verificare che non contenga campo ","
                //   se non la contiene associ ute.campo corrispondente
                //   se contiene virgola fai split 
                //switch (item.Key)
                //{
                //    case "TXT":
                //        MergePdfTxt(pdffieldName, Associazione rk)
                //        break;
                //    case "SIG":
                //        AddSignature(pdffieldName)
                //        break;
                //    case default:
                //        MessageBox.Show("Tipo Associazione non valido");
                //        break;
                //}

                //switch (item.Key)
                //{
                //    case "txtNameSurname":
                //        item.Value.SetValue(ute.COGNOME + " " + ute.NOME);
                //        break;
                //    case "txtDate":
                //        item.Value.SetValue(DateTime.Now.ToString("mm/dd/yyyy"));
                //        break;
                //    case "txtMail":
                //        item.Value.SetValue(ute.MAIL);
                //        break;
                //    case "txtCell":
                //        item.Value.SetValue(ute.CELL);
                //        break;
                //    case "txtSignature":
                //        PdfArray position = form.GetField("txtSignature").GetWidgets()[0].GetRectangle();                         
                //        rectangle = position.ToRectangle();
                //        removeSigForBiometric = true;                        
                //        break;
                //}
                //}
                #endregion

                form.FlattenFields();
                pdf.Close();
                
                reader.Close();

                return strFileNameOutput;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Errore: " + ex.Message,"Configurazione errata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return null;
            }
            finally
            {
                pdf.Close();
            }
        } 
        private static Signature SetSignature(string strName) 
        {
            Signature signature = new Signature();
            signature.name = "Sig1";
            signature.location = new Location()
            {
                H = (int)rectangle.GetHeight(),
                Y = (int)rectangle.GetY(),
                X = (int)rectangle.GetX(),
                W = (int)rectangle.GetWidth(),
                Page = numPage.ToString(),
            };
            signature.biometric = false;
            signature.required = true;
            signature.reason = "Accettazione firma";
            signature.name = strName;

            return signature;
        }
    }
}

