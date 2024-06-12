using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneCRM
{
    public partial class SearchCustomerDetails : Form
    {
        DataTable dt_Customerdet;
        public static string txtphone { get; set; }        
        public static string Cust_Id { get; set; }
        public static string Cust_Name { get; set; }
        public static string Cust_Mobile { get; set; }
        public static string Cust_AlternateNumber { get; set; }
        public static string Cust_EmailID { get; set; }
        public static string Cust_IceGateID { get; set; }
        public static string Cust_CompanyName { get; set; }
        public static string Cust_EnityType { get; set; }
        public static string Cust_City { get; set; }
        public static string Cust_RegisterStatus { get; set; }

        public SearchCustomerDetails()
        {
            InitializeComponent();
            txt_Phone.Text = txtphone;
            txtphone = "";
            Cust_Id = "";
            Cust_Name = "";
            Cust_Mobile = "";
            Cust_AlternateNumber = "";
            Cust_EmailID = "";
            Cust_IceGateID = "";
            Cust_CompanyName = "";
            Cust_EnityType = "0";
            Cust_City = "";
            Cust_RegisterStatus = "";
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_Phone.Text) || !string.IsNullOrWhiteSpace(txt_IcegateId.Text) || !string.IsNullOrWhiteSpace(txt_TicketNo.Text))
            {
                SearchCustDetails();
            }
            else
            {
                MessageBox.Show("Enter either Phone No or Icegate Id or Ticket No.");
                txt_Phone.Focus();
                return;
            }
        }

        private void SearchCustDetails()
        {
            try
            {
                string url = "http://192.168.0.93:8088/API/AgentDetail_Crm_API/Api/SearchCustDetails";

                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"Operation\":\"" + "SearchCustDetails" + "\",\"Mobile\":\"" + txt_Phone.Text.Trim() + "\",\"IcegateId\":\"" + txt_IcegateId.Text.Trim() + "\",\"TicketNo\":\"" + txt_TicketNo.Text.Trim() + "\"}";

                    streamWriter.Write(json);
                }

                var httpresponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamreader = new StreamReader(httpresponse.GetResponseStream()))
                {
                    var result = streamreader.ReadToEnd();
                    result = result.Replace("\"", "");
                    result = result.Replace(":", "\":\"");
                    result = result.Replace("{\\", "{\\\"");
                    result = result.Replace("}", "\"}");
                    result = result.Replace(",", "\",\"");
                    result = result.Replace("\"{", "{");
                    result = result.Replace("}\"", "}");
                    result = result.Replace("\\", "");
                    result = result.Replace("T00\":\"00\":\"00", "");

                    try
                    {
                        if (result == "Failure")
                        {
                            MessageBox.Show("CustomerDetails not found.");
                        }
                        else
                        {
                            dt_Customerdet = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                            dgv_CustomerDetails.DataSource = null;
                            dgv_CustomerDetails.Rows.Clear();
                            dgv_CustomerDetails.DataSource = dt_Customerdet;                            
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error  - " + exc.Message, "");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error while calling API - " + ex.Message, "SearchCustDetails");
            }
        }        
        private void dgv_CustomerDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            Cust_Id = dgv_CustomerDetails.Rows[e.RowIndex].Cells[0].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
            Cust_Name = dgv_CustomerDetails.Rows[e.RowIndex].Cells[1].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
            Cust_Mobile = dgv_CustomerDetails.Rows[e.RowIndex].Cells[2].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
            Cust_AlternateNumber = dgv_CustomerDetails.Rows[e.RowIndex].Cells[3].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
            Cust_EmailID = dgv_CustomerDetails.Rows[e.RowIndex].Cells[4].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
            Cust_IceGateID = dgv_CustomerDetails.Rows[e.RowIndex].Cells[5].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
            Cust_CompanyName = dgv_CustomerDetails.Rows[e.RowIndex].Cells[6].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
            Cust_EnityType = dgv_CustomerDetails.Rows[e.RowIndex].Cells[7].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[7].Value.ToString();
            Cust_City = dgv_CustomerDetails.Rows[e.RowIndex].Cells[8].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[8].Value.ToString();
            Cust_RegisterStatus = dgv_CustomerDetails.Rows[e.RowIndex].Cells[9].Value.ToString() == "null" ? "" : dgv_CustomerDetails.Rows[e.RowIndex].Cells[9].Value.ToString();

            this.Close();

            //SELECT ID, Name, Mobile, AlternateNumber, EmailID, IceGateID, CompanyName, EnityType, City, RegisterStatus, Createdate, Cust_InsertFlag, AlternateNumber1,
            //AlternateNumber2, AlternateNumber3, AlternateNumber4, AlternateNumber5, AlternateNumber6, AlternateNumber7, AlternateNumber8, AlternateNumber9
            //FROM CustomerDetails


        }
    }
}
