using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace SDR_FirmaInCloud.UI
{
    static class Program
    {            
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {  
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=firmaincloud;password=lelino04");
            Application.Run(new FormLogin(mySqlConnection));
        }
    }
}
