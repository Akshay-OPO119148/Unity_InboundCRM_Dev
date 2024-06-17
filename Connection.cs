using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCRM
{
    class Connection
    {
        public SqlConnection getconn()
        {
            string datasource = ConfigurationSettings.AppSettings["DataSource"];
            string initialcatalog = ConfigurationSettings.AppSettings["InitialCatalog"];
            string persistsecurityinfo = ConfigurationSettings.AppSettings["PersistSecurityInfo"];
            SqlConnection con = new SqlConnection("Data Source=" + datasource + ";Initial Catalog=" + initialcatalog + ";Persist Security Info=" + persistsecurityinfo + ";User ID=sa;Password=sa@123");
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            return con;
        }

        public SqlConnection getconn57()
        {
            SqlConnection con = new SqlConnection("server=192.168.0.57;database=Unity_Bank_INB;UID=opodba;Pwd=opo@1234;Max Pool Size=1000;Connection Timeout=0;MultipleActiveResultSets=true");
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            return con;
        }

       
    }
}
