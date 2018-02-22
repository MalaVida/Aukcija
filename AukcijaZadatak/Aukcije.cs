using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AukcijaZadatak
{
   public class Aukcije
    {
        public int AukcijaID { get; set; }
        public  string ProductName { get; set; }
        public string Korisnik { get; set; }
        public decimal ProductPrice { get; set; }

        public override string ToString()
        {
            return AukcijaID +" "+ Korisnik+ " "+ ProductName +" " +  ProductPrice;
        }
    }
}
