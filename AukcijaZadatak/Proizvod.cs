using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AukcijaZadatak
{
   public class Proizvod
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public override string ToString()
        {
            return string.Format ("{0}  {1}         {2}", ProductID, ProductName,ProductPrice )  ;
        }
    }
}
