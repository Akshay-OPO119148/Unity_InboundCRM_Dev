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
    public partial class AddAlternateNumber : Form
    {
        private void UpdateText(TextBox txtBox, string txt)
        {
            txtBox.Text = txt;
        }
        public AddAlternateNumber()
        {
            InitializeComponent();
        }
        private void UpdateLabelText(Label lbl, string txt)
        {
            lbl.Text = txt;
        }

        public delegate void UpdateLabelTextCallback(Label lbl, string txt);
        public delegate void UpdateTextCallback(TextBox txtBox, string txt);
        
        public string txtphone { get; set; }

        private void dgv_TicketDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddAlternateNumber_Load(object sender, EventArgs e)
        {
            GetCustomerApi();
        }

        private void GetCustomerApi()
        {
            try
            {
                string url = "http://192.168.0.93:8088/API/AgentDetail_Crm_API/Api/CustDetails";

                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"Operation\":\"" + "CustDetails" + "\",\"Mobile\":\"" + txtphone + "\"}";

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
                            DataTable dt_Customerdet = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                            lbl_Customer_Id.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lbl_Customer_Id, dt_Customerdet.Rows[0]["ID"].ToString() });
                            txt_Name.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_Name, dt_Customerdet.Rows[0]["Name"].ToString() });
                            txt_CustPhone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_CustPhone, dt_Customerdet.Rows[0]["Mobile"].ToString() });
                            txt_AlternatePhone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone, dt_Customerdet.Rows[0]["AlternateNumber"].ToString() });
                            txt_Email.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_Email, dt_Customerdet.Rows[0]["EmailID"].ToString() });
                            txt_IceGateId.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_IceGateId, dt_Customerdet.Rows[0]["IceGateID"].ToString() });
                            txt_CompanyName.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_CompanyName, dt_Customerdet.Rows[0]["CompanyName"].ToString() });
                            txt_EntityType.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_EntityType, dt_Customerdet.Rows[0]["EnityType"].ToString() });
                            txt_CityOfRegdUser.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_CityOfRegdUser, dt_Customerdet.Rows[0]["City"].ToString() });
                            txt_StatusOfRegdUser.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_StatusOfRegdUser, dt_Customerdet.Rows[0]["RegisterStatus"].ToString() });

                            txt_AlternatePhone1.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone1, dt_Customerdet.Rows[0]["AlternateNumber1"].ToString() });
                            txt_AlternatePhone2.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone2, dt_Customerdet.Rows[0]["AlternateNumber2"].ToString() });
                            txt_AlternatePhone3.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone3, dt_Customerdet.Rows[0]["AlternateNumber3"].ToString() });
                            txt_AlternatePhone4.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone4, dt_Customerdet.Rows[0]["AlternateNumber4"].ToString() });
                            txt_AlternatePhone5.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone5, dt_Customerdet.Rows[0]["AlternateNumber5"].ToString() });
                            txt_AlternatePhone6.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone6, dt_Customerdet.Rows[0]["AlternateNumber6"].ToString() });
                            txt_AlternatePhone7.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone7, dt_Customerdet.Rows[0]["AlternateNumber7"].ToString() });
                            txt_AlternatePhone8.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone8, dt_Customerdet.Rows[0]["AlternateNumber8"].ToString() });
                            txt_AlternatePhone9.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_AlternatePhone9, dt_Customerdet.Rows[0]["AlternateNumber9"].ToString() });
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
                MessageBox.Show("Error while calling API - " + ex.Message, "GetCustomerApi");
            }
        }

        private void btn_AddAlternateNumber_Click(object sender, EventArgs e)
        {
            UpdateAlternateNoApi();
            this.Close();
        }
        private void UpdateAlternateNoApi()
        {
            try
            {
                string url = "http://192.168.0.93:8088/API/AgentDetail_Crm_API/Api/UpdateAlternateNumber";

                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"RegisteredMoNo\":\"" + txt_CustPhone.Text.Trim() + "\",\"AlternateNo1\":\"" + txt_AlternatePhone1.Text.Trim() + "\",\"AlternateNo2\":\"" + txt_AlternatePhone2.Text.Trim() + "\",\"AlternateNo3\":\"" + txt_AlternatePhone3.Text.Trim() + "\",\"AlternateNo4\":\"" + txt_AlternatePhone4.Text.Trim() + "\",\"AlternateNo5\":\"" + txt_AlternatePhone5.Text.Trim() + "\",\"AlternateNo6\":\"" + txt_AlternatePhone6.Text.Trim() + "\",\"AlternateNo7\":\"" + txt_AlternatePhone7.Text.Trim() + "\",\"AlternateNo8\":\"" + txt_AlternatePhone8.Text.Trim() + "\",\"AlternateNo9\":\"" + txt_AlternatePhone9.Text.Trim() + "\"}";
                    
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
                            MessageBox.Show("Error in Update Agent Status.");
                        }
                        else
                        {

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
                MessageBox.Show("Error while calling API - " + ex.Message, "UpdateAlternateNoApi");
            }
        }

        private void NumbersOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

    }
}
