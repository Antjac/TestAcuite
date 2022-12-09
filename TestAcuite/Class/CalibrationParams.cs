using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAcuite.Class
{
    public class ConvertUnit
    {
        public decimal Logmar { get; set; }
        public double DecFrac { get; set; }
        public string Monoyer { get; set; }
        public ConvertUnit(decimal logmar, double decFrac, string monoyer)
        {
            Logmar = logmar;
            DecFrac = decFrac;
            Monoyer = monoyer;
        }

       
    }
}
