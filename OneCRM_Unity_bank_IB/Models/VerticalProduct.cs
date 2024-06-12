using System;

namespace UnityBank.Models
{
    public class VerticalProduct
    {
        public int id { get; set; }
        public string Vertical { get; set; }
        public string Product { get; set; }
        public string Secured_Unsecured { get; set; }
        public string Organic_Inorganic { get; set; }
        public string Assets_or_Liabilities { get; set; }

        public DateTime insertDate { get; set; }
    }
}
