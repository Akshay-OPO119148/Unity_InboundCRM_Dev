using System.Collections.Generic;

namespace UnityBank.Models
{
    public class AtmDebitCard
    {
        public List<string> Categories { get; set; }
        public List<string> SubCategories { get; set; }
        public Productinfo productinfo { get; set; }
        public List<string> Verticles { get; set; }
        
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Verticle { get; set; }
        public string Product { get; set; }

        public string AccessLevel { get; set; }
        public string MaterialType { get; set; }
        public string FinancialPosition { get; set; }

    }
    public class Productinfo
    {
        public List<string> Verticles { get; set; }
        public List<string> Products { get; set; }
        public List<string> AccessLevels { get; set; }
        public List<string> MaterialTypes { get; set; }
        public List<string> FinancialPositions { get; set; }
    }
}
