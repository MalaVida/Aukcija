using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace AukcijaZadatak
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public ProizvodDAL pdal = new ProizvodDAL();
        public Proizvod pr = new Proizvod();
        public Admin()
        {
            InitializeComponent();
            PuniProizvod();


        }

       public void VratiProizvod()
        {

        }
        public void PuniProizvod()
        {
           
            foreach (Proizvod p in pdal.VratiProizvode())
            {
                listBox.Items.Add(p);
                
            }
            
           
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                Proizvod selektovano = (Proizvod)listBox.SelectedItem;
                textID.Text = selektovano.ProductID.ToString();
                textNaziv.Text = selektovano.ProductName;
                textCena.Text = selektovano.ProductPrice.ToString();
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            textID.Clear();
            textNaziv.Clear();
            textCena.Clear();
            listBox.SelectedIndex = -1;
        }

        private void buttonUbaci_Click(object sender, RoutedEventArgs e)
        {
            int index = listBox.SelectedIndex;
            
           
           
                SqlConnection cnn = new SqlConnection(Konekcija.cnnAukcija);
                string upit = "INSERT  INTO Proizvodi(ProductName,ProductPrice) VALUES (@ProductName,@ProductPrice) ";
                SqlCommand scmd = new SqlCommand(upit, cnn);

                
                scmd.Parameters.AddWithValue("@ProductName",textNaziv.Text);
                scmd.Parameters.AddWithValue("@ProductPrice", textCena.Text);

                try
                {
                    cnn.Open();
                    scmd.ExecuteNonQuery();
                    cnn.Close();
                }
                catch(Exception xcp)
                {
                    MessageBox.Show(xcp.Message);
                    return;
                }
                finally
                {
                    cnn.Close();
                }
                listBox.Items.Clear();
                PuniProizvod();
                
                
            
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            int indeks = listBox.SelectedIndex;
            if(indeks > -1)
            {
               
                SqlConnection scnn = new SqlConnection(Konekcija.cnnAukcija);
                SqlCommand scmd = new SqlCommand("DELETE FROM Proizvodi WHERE ProductID = @ProductID",scnn);

                scmd.Parameters.AddWithValue("@ProductID",textID.Text );
                

                try
                {
                    scnn.Open();
                    scmd.ExecuteNonQuery();
                    scnn.Close();
                }
                catch(Exception xcp)
                {
                    
                    MessageBox.Show(xcp.Message);
                    return;
                }
                listBox.Items.Clear();
                PuniProizvod();

            }
        }
    }
}
