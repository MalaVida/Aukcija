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
   public class ProizvodDAL
    {
       public List<Proizvod> VratiProizvode()
        {
            SqlConnection scnnPr = new SqlConnection(Konekcija.cnnAukcija);
            SqlCommand scmd = new SqlCommand("SELECT * FROM Proizvodi ", scnnPr);

            List<Proizvod> listProizvoda = new List<Proizvod>();
            try
            {
                scnnPr.Open();
                SqlDataReader dr = scmd.ExecuteReader();
                while (dr.Read())
                {
                    Proizvod p = new Proizvod();
                    p.ProductID = dr.GetInt32(0);
                    p.ProductName = dr.GetString(1);
                    p.ProductPrice = dr.GetDecimal(2);
                    listProizvoda.Add(p);
                }
                scnnPr.Close();

            }
            catch(Exception xcp)
            {
                MessageBox.Show(xcp.Message);
                scnnPr.Close();
            }
            return listProizvoda;
        }
    }
}
