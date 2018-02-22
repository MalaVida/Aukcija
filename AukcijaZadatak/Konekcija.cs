using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AukcijaZadatak
{
   public class Konekcija
    {
        public static string cnnAukcija = Properties.Settings.Default.Aukcija1ConnectionString;
    }
}
