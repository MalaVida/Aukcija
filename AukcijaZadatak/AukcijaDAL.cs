using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AukcijaZadatak
{
   public class AukcijaDAL
    {
        public List<Aukcije> VratiAukcije()
        {
            SqlConnection scnn = new SqlConnection(Konekcija.cnnAukcija);
            SqlCommand scmd = new SqlCommand("select Aukcije.AukcijaID ,Aukcije.Korisnik ,Proizvodi.ProductName,Proizvodi.ProductPrice from Proizvodi inner join Aukcije on Aukcije.P_Id = Proizvodi.ProductID", scnn);

            List<Aukcije> listaAukcija = new List<Aukcije>();
            try
            {
                scnn.Open();
                SqlDataReader dr = scmd.ExecuteReader();
                while (dr.Read())
                {
                    Aukcije a = new Aukcije();
                    a.AukcijaID = (int)dr["AukcijaID"];
                    a.Korisnik = dr["Korisnik"].ToString();
                    a.ProductName = dr["ProductName"].ToString();
                    a.ProductPrice = (decimal)dr["ProductPrice"];

                    listaAukcija.Add(a);
                }

                scnn.Close(); 
            }
            catch(Exception xcp)
            {
                MessageBox.Show(xcp.Message);
            }

            return listaAukcija;
        }
    }
}
