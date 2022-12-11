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
