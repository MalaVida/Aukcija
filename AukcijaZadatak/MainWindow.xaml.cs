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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AukcijaZadatak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PuniListu();         
        }
        public ProizvodDAL pdal = new ProizvodDAL();
        
      public void PuniListu()
        {
            foreach(Proizvod p in pdal.VratiProizvode())
            {
                listBox.Items.Add(p);
            }
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text == "user" && passwordBox.Password == "user")
            {
                Korisnik k = new Korisnik();
                k.Show();
                textBox.Clear();
                passwordBox.Clear();
            }
            else if(textBox.Text == "admin" && passwordBox.Password == "admin")
            {
                Admin a = new Admin();
                a.Show();
                textBox.Clear();
                passwordBox.Clear();
            }
            else
            {
                MessageBox.Show("Korisnik ili Admin ne postoje! \n Pokusajte ponovo.");
            }
        }

        private void opis2_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
