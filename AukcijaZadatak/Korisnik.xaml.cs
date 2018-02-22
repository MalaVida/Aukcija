using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace AukcijaZadatak
{
    /// <summary>
    /// Interaction logic for Korisnik.xaml
    /// </summary>
    public partial class Korisnik : Window
    {
        public int cena = 1;
        public decimal ponuda;
        private int time = 120;
        private DispatcherTimer dpt1;
        public ProizvodDAL pdal = new ProizvodDAL();
        public Proizvod pr = new Proizvod();

      

        public Korisnik()
        {
            InitializeComponent();
            dpt1 = new DispatcherTimer();

            dpt1.Interval = new TimeSpan(0, 0, 1);
            dpt1.Tick += Dpt_Tick;
            foreach(Proizvod p in pdal.VratiProizvode())
            {
                listBox.Items.Add(p);
            }
            
        }

        private void Dpt_Tick(object sender, EventArgs e)
        {
            
            if (time > 0)
            {
                if (time <= 10)
                {

                    time--;
                    labelVreme.Content = string.Format("0{0}:0{1}", time / 60, time % 60);
                }
                else
                {
                    time--;
                    labelVreme.Content = string.Format("0{0}:{1}", time / 60, time % 60);
                }

            }
            else
            {
                
                decimal cena = 1;
                decimal tbox;
                tbox = Convert.ToDecimal(textBoxPonuda.Text);
                tbox = decimal.Parse(textBoxPonuda.Text);

                decimal ukupno = tbox + cena;

                dpt1.Stop();
                if (listBox.SelectedIndex > -1)
                {
                    SqlConnection konekcija = new SqlConnection(Konekcija.cnnAukcija);
                    string upit = "UPDATE Proizvodi SET ProductPrice = @ProductPrice WHERE ProductID = @ProductID ";


                    SqlCommand scmd = new SqlCommand(upit, konekcija);

                    scmd.Parameters.AddWithValue("@ProductID", textID.Text);
                    scmd.Parameters.AddWithValue("@ProductPrice", ukupno);

                    try
                    {
                        konekcija.Open();
                        scmd.ExecuteNonQuery();
                        konekcija.Close();
                    }
                    catch (Exception xcp)
                    {
                        MessageBox.Show(xcp.Message);
                        return;
                    }
                    listBox.Items.Clear();
                    foreach (Proizvod p in pdal.VratiProizvode())
                    {
                        listBox.Items.Add(p);
                    }
                    buttonPonudi.IsEnabled = false;
                }
                
            }
        }

        private void buttonPonudi_Click(object sender, RoutedEventArgs e)
        {
            int index = listBox.SelectedIndex;

            if (index > -1)
            {
                dpt1.Start();
                time = 120;
                decimal cena = 1;
                decimal tbox;
                tbox = Convert.ToDecimal(textBoxPonuda.Text);
                tbox = decimal.Parse(textBoxPonuda.Text);

                decimal ukupno = tbox + cena;

                SqlConnection konekcija = new SqlConnection(Konekcija.cnnAukcija);
                string upit = "UPDATE Proizvodi SET ProductPrice = @ProductPrice WHERE ProductID = @ProductID";
                
                
                SqlCommand scmd = new SqlCommand (upit, konekcija);
                scmd.Parameters.AddWithValue("@ProductID", textID.Text);
                scmd.Parameters.AddWithValue("@ProductPrice",ukupno);
                
                try
                {
                    konekcija.Open();
                    scmd.ExecuteNonQuery();
                    konekcija.Close();
                }
                catch(Exception xcp)
                {
                    MessageBox.Show(xcp.Message);
                    return;
                }
                listBox.Items.Clear();
                foreach (Proizvod p in pdal.VratiProizvode())
                {
                    listBox.Items.Add(p);
                }
                
                listBox.SelectedIndex = index;
            }
            
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                Proizvod selektovano = (Proizvod)listBox.SelectedItem;
                textID.Text = selektovano.ProductID.ToString();
                textNaziv.Text = selektovano.ProductName;
                textBoxPonuda.Text = selektovano.ProductPrice.ToString();
            }
        }
    }
}
