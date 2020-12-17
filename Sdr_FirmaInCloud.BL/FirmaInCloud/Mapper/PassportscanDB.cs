using MySql.Data.MySqlClient;
using Sdr_FirmaInCloud.BL.FirmaInCloud;
using Sdr_FirmaInCloud.BL.FirmaInCloud.Mapper;
using SDR_FirmaInCloud.BL;
using SDR_FirmaInCloud.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Sdr_FirmaInCloud.BL
{
    public static class PassportscanDB
    {
        public static PASSPORTSCAN Save(PASSPORTSCAN entity)
        {
            if (entity == null)
                return null;

            try
            {
                var righeReservation = new List<RESERVATION>();
                var righeGuests = new List<Guests>();
                var righeUsers = new List<Users>();

                foreach (var datiReservation in entity.RESERVATION)
                {
                    righeReservation.Add(datiReservation);
                }
                SaveReservation(righeReservation);

                foreach (var datiGuest in entity.Guests)
                {
                    righeGuests.Add(datiGuest);
                    Users utente = new Users();
                    utente.NOME = datiGuest.FirstName;
                    utente.COGNOME = datiGuest.LastName;
                    utente.DTINS = DateTime.Now;
                    utente.MAIL = datiGuest.EMail;
                    utente.STRADA = datiGuest.Address1;
                    utente.DTNAS = DateTime.ParseExact(datiGuest.BirthDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                    utente.KEY_NAZ_NAS = IndirizzoDB.LoadNazione(null, (datiGuest.BirthNation + "*")).ID;
                    righeUsers.Add(utente);
                }
                SaveGuest(righeGuests);
                UsersDB.SaveListUsers(righeUsers);

            }
            catch (Exception e)
            {
                throw e;
            }
            return entity;
        }

        private static void SaveGuest(List<Guests> entityList)
        {
            try
            {
                if (!GestioneMySql.OpenConnection())
                    throw new Exception("Errore nell'apertura della connessione.");

                foreach (var datiGuest in entityList)
                {
                    MySqlCommand comm = GestioneMySql.connection.CreateCommand();

                    StringBuilder stringBuilder = new StringBuilder("INSERT INTO GUESTS ");
                    stringBuilder.Append("( ");
                    stringBuilder.Append("PMSID, GUESTSTYPE, TITLE, FIRSTNAME, LASTNAME, MIDDLENAME, SEX, NATIONALITY, BIRTHNATION, NATION, BIRTHDATE, ");
                    stringBuilder.Append("ISSUEDATE, EXPIRATION, DOCTYPE, DOCNUMBER, BIRTHPLACE, BIRTHPROVINCE, ");
                    stringBuilder.Append("ISSUEPLACE, ISSUEPROVINCE, ADDRESS1, ADDRESS2, ADDRESS3, CITY, PROVINCE, STATE, EMAIL, MOBILE, SIGN0, SIGN1, SIGN2, SIGN3 ");
                    stringBuilder.Append(") ");
                    stringBuilder.Append("VALUES ");
                    stringBuilder.Append("( ");
                    stringBuilder.Append("@PMSID, @GUESTSTYPE, @TITLE, @FIRSTNAME, @LASTNAME, @MIDDLENAME, @SEX, @NATIONALITY, @BIRTHNATION, @NATION, @BIRTHDATE, ");
                    stringBuilder.Append("@ISSUEDATE, @EXPIRATION, @DOCTYPE, @DOCNUMBER, @BIRTHPLACE, @BIRTHPROVINCE, ");
                    stringBuilder.Append("@ISSUEPLACE, @ISSUEPROVINCE, @ADDRESS1, @ADDRESS2, @ADDRESS3, @CITY, @PROVINCE, @STATE, @EMAIL, @MOBILE, @SIGN0, @SIGN1, @SIGN2, @SIGN3 ");
                    stringBuilder.Append(") ");
                    comm.CommandText = stringBuilder.ToString();


                    comm.Parameters.AddWithValue("@PMSID", datiGuest.PmsID.StringOrNull());
                    comm.Parameters.AddWithValue("@GUESTSTYPE", datiGuest.GuestsType.StringOrNull());
                    comm.Parameters.AddWithValue("@TITLE", datiGuest.Title.StringOrNull());
                    comm.Parameters.AddWithValue("@FIRSTNAME", datiGuest.FirstName.StringOrNull());
                    comm.Parameters.AddWithValue("@LASTNAME", datiGuest.LastName.StringOrNull());
                    comm.Parameters.AddWithValue("@MIDDLENAME", datiGuest.MiddleName.StringOrNull());
                    comm.Parameters.AddWithValue("@SEX", datiGuest.Sex.StringOrNull());
                    comm.Parameters.AddWithValue("@NATIONALITY", datiGuest.Nationality.StringOrNull());
                    comm.Parameters.AddWithValue("@BIRTHNATION", datiGuest.BirthNation.StringOrNull());
                    comm.Parameters.AddWithValue("@NATION", datiGuest.Nation.StringOrNull());
                    comm.Parameters.AddWithValue("@BIRTHDATE", datiGuest.BirthDate.StringOrNull());
                    comm.Parameters.AddWithValue("@ISSUEDATE", datiGuest.IssueDate.StringOrNull());
                    comm.Parameters.AddWithValue("@EXPIRATION", datiGuest.Expiration.StringOrNull());
                    comm.Parameters.AddWithValue("@DOCTYPE", datiGuest.DocType.StringOrNull());
                    comm.Parameters.AddWithValue("@DOCNUMBER", datiGuest.DocNumber.StringOrNull());
                    comm.Parameters.AddWithValue("@BIRTHPLACE", datiGuest.BirthPlace.StringOrNull());
                    comm.Parameters.AddWithValue("@BIRTHPROVINCE", datiGuest.BirthProvince.StringOrNull());
                    comm.Parameters.AddWithValue("@ISSUEPLACE", datiGuest.IssuePlace.StringOrNull());
                    comm.Parameters.AddWithValue("@ISSUEPROVINCE", datiGuest.IssueProvince.StringOrNull());
                    comm.Parameters.AddWithValue("@ADDRESS1", datiGuest.Address1.StringOrNull());
                    comm.Parameters.AddWithValue("@ADDRESS2", datiGuest.Address2.StringOrNull());
                    comm.Parameters.AddWithValue("@ADDRESS3", datiGuest.Address3.StringOrNull());
                    comm.Parameters.AddWithValue("@CITY", datiGuest.City.StringOrNull());
                    comm.Parameters.AddWithValue("@PROVINCE", datiGuest.Province.StringOrNull());
                    comm.Parameters.AddWithValue("@STATE", datiGuest.State.StringOrNull());
                    comm.Parameters.AddWithValue("@EMAIL", datiGuest.EMail.StringOrNull());
                    comm.Parameters.AddWithValue("@MOBILE", datiGuest.Mobile.StringOrNull());
                    comm.Parameters.AddWithValue("@SIGN0", datiGuest.Sign0.StringOrNull());
                    comm.Parameters.AddWithValue("@SIGN1", datiGuest.Sign1.StringOrNull());
                    comm.Parameters.AddWithValue("@SIGN2", datiGuest.Sign2.StringOrNull());
                    comm.Parameters.AddWithValue("@SIGN3", datiGuest.Sign3.StringOrNull());
                    
                    comm.ExecuteNonQuery();
                }
                if (!GestioneMySql.CloseConnection())
                    throw new Exception("Errore nella chiusura della connessione.");
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
        private static void SaveReservation(List<RESERVATION> entityList)
        {
            try
            {
                //if (!GestioneMySql.OpenConnection())
                //    throw new Exception("Errore nell'apertura della connessione.");

                foreach (var datiReservation in entityList)
                {
                    MySqlCommand comm = GestioneMySql.connection.CreateCommand();
                    string query;
                    query = "INSERT INTO RESERVATION( PMSID, GROUPNAME, ARRIVAL, DEPARTURE, ROOM, ROOMTYPE, RATE, ADULT, CHILD )";
                    query = query + " VALUES ( @PMSID, @GROUPNAME, @ARRIVAL, @DEPARTURE, @ROOM, @ROOMTYPE, @RATE, @ADULT, @CHILD )";
                    comm.CommandText = query;

                    comm.Parameters.AddWithValue("@PMSID", datiReservation.PmsID);
                    comm.Parameters.AddWithValue("@GROUPNAME", datiReservation.GroupName.StringOrNull());
                    comm.Parameters.AddWithValue("@ARRIVAL", datiReservation.Arrival.StringOrNull());
                    comm.Parameters.AddWithValue("@DEPARTURE", datiReservation.Departure.StringOrNull());
                    comm.Parameters.AddWithValue("@ROOM", datiReservation.Room.StringOrNull());
                    comm.Parameters.AddWithValue("@ROOMTYPE", datiReservation.RoomType.StringOrNull());

                    comm.Parameters.AddWithValue("@RATE", datiReservation.Rate.StringOrNull());
                    comm.Parameters.AddWithValue("@ADULT", datiReservation.Adult.ToIntOrDbNull());
                    comm.Parameters.AddWithValue("@CHILD", datiReservation.Child.ToIntOrDbNull());

                    comm.ExecuteNonQuery();
                }
                //if (!GestioneMySql.CloseConnection())
                //    throw new Exception("Errore nella chiusura della connessione.");
                
            }
            catch (Exception ex)
            {
                GestioneMySql.CloseConnection();
                MessageBox.Show("Errore: " + ex.Message);
            }
        }
    }
}
