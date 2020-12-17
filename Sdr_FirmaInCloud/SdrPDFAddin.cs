using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDR_FirmaInCloud.UI;

namespace SDR_FirmaInCloud
{
    class SdrPDFAddin
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
 