using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;
using System.Diagnostics;
using System.Data.SqlClient;

namespace OneCRM
{
    public partial class Login : Form
    {
        Connection conobj = new Connection();
        public Login()
        {
            bool flag = false;
            //Process[] processCollection = Process.GetProcesses();
            //foreach (Process p in processCollection)
            //{
            //    if (p.ProcessName.ToString().Contains("OneCRM"))
            //    {
            //        //this.Close();
            //        flag = true;
            //        MessageBox.Show("You already login");
            //        Application.Exit();
            //        //this.Close();
            //        break;
            //    }
            //    //else
            //    //{
            //    //    InitializeComponent();
            //    //}
            //}
            //Boolean st;
            //ListProcesses();
            //var exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
            //st = IsPrevInstance();
            //if (st == true)
            //{
            //    return;
            //}
            if (flag == false)
            {
                InitializeComponent();
            }
        }
        private void ListProcesses()
        {
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if (p.ProcessName.ToString().Contains("OneCRM"))
                {
                    string mm="";
                }
            }
        }
        private static bool IsPrevInstance()
        {
            string currentProcessName = "OneCRM.exe";
            Process[] processesNamesCollection = Process.GetProcessesByName(currentProcessName);
            if (processesNamesCollection.Length > 1)
            {
                return true;
            }
            else
                return false;
        }

        //private bool FetchAgentDetails(string empcode)
        //{
        //    string url = ConfigurationSettings.AppSettings["MainURL"].ToString();
        //    url = url + empcode;

        //    WebRequest request = HttpWebRequest.Create(url);
        //    WebResponse response = request.GetResponse();
        //    StreamReader reader = new StreamReader(response.GetResponseStream());
        //    string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need  
        //                                         //Response.Write(urlText.ToString());  


        //    string result = urlText.Replace("\n", "");


        //    result = result.Substring(result.LastIndexOf('{') + 1, result.LastIndexOf('}') - 2).Replace("\"", "");
        //    string[] tec = result.Split(',');

        //    for (int i = 0; i < tec.GetUpperBound(0) + 1; i++)
        //    {
        //        string[] data = tec[i].Split(':');
        //        if (data[0].ToUpper() == "STATUS")
        //        {
        //            CL_AgentDetails.Status = data[1].ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (data[0].ToUpper() == "PROCESSNAME")
        //            {
        //                CL_AgentDetails.ProcessName = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "OPOID")
        //            {
        //                CL_AgentDetails.OPOID = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "AGENTNAME")
        //            {
        //                CL_AgentDetails.AgentName = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "AGENTID")
        //            {
        //                CL_AgentDetails.AgentID = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "DN")
        //            {
        //                //CL_AgentDetails.DN = data[1].ToString();
        //                GetDnAPI();
        //            }
        //            else if (data[0].ToUpper() == "ASTR_DN")
        //            {
        //                CL_AgentDetails.DN = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "MOBILENO")
        //            {
        //                CL_AgentDetails.MobileNo = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "EMAIL")
        //            {
        //                CL_AgentDetails.Email = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "PREFIX")
        //            {
        //                CL_AgentDetails.Prefix = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "TSERVERIP")
        //            {
        //                CL_AgentDetails.TserverIP = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "TSERVERPORT")
        //            {
        //                CL_AgentDetails.TserverPort = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ISASTERIKLOGIC")
        //            {
        //                CL_AgentDetails.IsAsterikLogic = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ASTERIKGETNEXTURL")
        //            {
        //                CL_AgentDetails.AsterikGetNextUrl = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ISCONF")
        //            {
        //                CL_AgentDetails.IsConf = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "CLIENTURL")
        //            {
        //                CL_AgentDetails.ClientUrl = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ISMANUAL")
        //            {
        //                CL_AgentDetails.IsManual = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "MANUALPREFIX")
        //            {
        //                CL_AgentDetails.manualprefix = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ASTRIKLOCALIP")
        //            {
        //                CL_AgentDetails.AstrikLocalIP = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ASTRIKIP")
        //            {
        //                //CL_AgentDetails.AstrikIP = data[1].ToString() + data[2].ToString();
        //                for (int d = 1; d < data.GetUpperBound(0) + 1; d++)
        //                {
        //                    CL_AgentDetails.AstrikIP += data[d].ToString();
        //                }
        //            }
        //            else if (data[0].ToUpper() == "APICALLCUT")
        //            {
        //                CL_AgentDetails.ApiCallCut = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "CONFCHANNEL")
        //            {
        //                CL_AgentDetails.ConfChannel = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ASTRIKREQPORT")
        //            {
        //                CL_AgentDetails.AstrikReqPort = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "ASTRIKPORT")
        //            {
        //                CL_AgentDetails.AstrikPort = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "IFRAMESOURCE")
        //            {
        //                //CL_AgentDetails.iframesource = data[1].ToString() + data[2].ToString() + data[3].ToString();
        //                for (int d = 1; d < data.GetUpperBound(0) + 1; d++)
        //                {
        //                    CL_AgentDetails.iframesource += data[d].ToString();
        //                }
        //            }
        //            else if (data[0].ToUpper() == "PHONENOMASKIS")
        //            {
        //                CL_AgentDetails.PhoneNoMaskIs = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "DIALACCESS")
        //            {
        //                CL_AgentDetails.DialAccess = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "SINGLESTEPTRANSFER")
        //            {
        //                CL_AgentDetails.SingleStepTransfer = data[1].ToString();                        
        //            }
        //            else if (data[0].ToUpper() == "THREESTEPTRANSFER")
        //            {
        //                CL_AgentDetails.ThreeStepTransfer = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "VERSION")
        //            {
        //                CL_AgentDetails.Version = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "RESULT")
        //            {
        //                CL_AgentDetails.Result = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "OTPREQUIRED")
        //            {
        //                CL_AgentDetails.OTPRequired = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "OTP")
        //            {
        //                CL_AgentDetails.OTP = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "APKVERSION")
        //            {
        //                CL_AgentDetails.APKVersion = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "APKURL")
        //            {
        //                //CL_AgentDetails.APKURL = data[1].ToString() + data[2].ToString() + data[3].ToString();
        //                for (int d = 1; d < data.GetUpperBound(0) + 1; d++)
        //                {
        //                    CL_AgentDetails.APKURL += data[d].ToString();
        //                }
        //            }
        //            else if (data[0].ToUpper() == "SIPPORT")
        //            {
        //                CL_AgentDetails.SipPort = data[1].ToString();
        //            }
        //            else if (data[0].ToUpper() == "TserverIP_OFFICE")
        //            {
        //                CL_AgentDetails.TserverIP_OFFICE = data[1].ToString();
        //            }
                    
                    
        //        }

        //    }

        //    return true;
        //}

        private bool GGN_LoginAPI(string location)
        {
            string url;
            if (location == "GGN")
            {
                url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/AgentDetail";
            }
            else
            {
                url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/AgentDetail";
            }
            var json = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                json = "{\"Opoid\":\"" + txtUsername.Text.Trim() + "\"}";

                streamWriter.Write(json);
            }

            var httpresponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamreader = new StreamReader(httpresponse.GetResponseStream()))
            {

                //var result = "[{\"ProcessName\":\"DTHHD\",\"OPOID\":\"OPO060001\",\"AgentName\":\"rinki.0\",\"AgentID\":\"13065\",\"DN\":\"17208287813656\",\"Astr_DN\":\"\",\"MobileNo\":\"8287813656\",\"Email\":\"\",\"Prefix\":\"172\",\"TserverIP\":\"gcti.1point1.com\",\"TserverPort\":\"7000\",\"IsAsterikLogic\":null,\"AsterikGetNextUrl\":null,\"IsConf\":0,\"ClientUrl\":null,\"IsManual\":null,\"manualprefix\":null,\"AstrikLocalIP\":\"172.24.11.40\",\"CallLogApi\":\"https://info.onepointone.in:8088/MobileCaptureLogAPI/InsertCallLogAPI.aspx\",\"ConfApi\":\"https://info.onepointone.in:8088/MobileCaptureLogAPI/ConferenceLogAPI.aspx\",\"AstrikIP\":\"http://gcon2.1point1.com\",\"ApiCallCut\":\"\",\"ConfChannel\":\"DTHHD\",\"AstrikReqPort\":\"3000\",\"AstrikPort\":\"8000\",\"iframesource\":\"http://gcti.1point1.com:8088/DishTV_GGN/Info_DTH_PKJ_GGN2.aspx\",\"PhoneNoMaskIs\":0,\"DialAccess\":1,\"SingleStepTransfer\":null,\"ThreeStepTransfer\":null,\"Version\":\"\",\"Result\":\"Success\",\"OTPRequired\":false,\"OTP\":\"\",\"ShiftTime\":\"\",\"APKVersion\":\"1.21\",\"APKURL\":\"http://203.124.144.206:70/OPO_connect_1_21_outbound.apk\",\"SipPort\":\"\"}]";

                var result = streamreader.ReadToEnd();

                dynamic dynJson = JsonConvert.DeserializeObject(result);
                dynJson = dynJson.Replace("[", "").Replace("]", "");
                JavaScriptSerializer ser = new JavaScriptSerializer();

                CL_AgentDet cust = ser.Deserialize<CL_AgentDet>(dynJson);

                //result = result.Replace("\"", "");
                //result = result.Replace(":", "\":\"");
                //result = result.Replace("{\\", "{\\\"");
                //result = result.Replace("}", "\"}");
                //result = result.Replace(",", "\",\"");
                //result = result.Replace("\"{", "{");
                //result = result.Replace("}\"", "}");
                //result = result.Replace("\\", "");
                //result = result.Replace("T00\":\"00\":\"00", "");
                //result = result.Replace("https\":\"", "https:");
                //result = result.Replace("http\":\"", "http:");

                try
                {
                    if (result == "Failure")
                    {
                        //CL_AgentDetails.Status = dt_Agentdet.Rows[0]["STATUS"].ToString();

                        //bool status = FetchAgentDetails(txtUsername.Text.Trim());
                        return false;
                    }
                    else
                    {
                        //DataTable dt_Agentdet = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                        CL_AgentDetails.ProcessName = cust.ProcessName;// dt_Agentdet.Rows[0]["PROCESSNAME"].ToString();
                        CL_AgentDetails.OPOID = cust.OPOID;// dt_Agentdet.Rows[0]["OPOID"].ToString();
                        CL_AgentDetails.AgentName = cust.AgentName;// dt_Agentdet.Rows[0]["AGENTNAME"].ToString();
                        CL_AgentDetails.AgentID = cust.AgentID;// dt_Agentdet.Rows[0]["AGENTID"].ToString();
                        CL_AgentDetails.TserverIP_OFFICE = cust.TserverIP_OFFICE;// dt_Agentdet.Rows[0]["DN"].ToString();                      

                        CL_AgentDetails.Astr_DN = cust.Astr_DN;// dt_Agentdet.Rows[0]["ASTR_DN"].ToString();
                        CL_AgentDetails.MobileNo = cust.MobileNo;// dt_Agentdet.Rows[0]["MOBILENO"].ToString();
                        CL_AgentDetails.Email = cust.Email;// dt_Agentdet.Rows[0]["EMAIL"].ToString();
                        CL_AgentDetails.Prefix = cust.Prefix;// dt_Agentdet.Rows[0]["PREFIX"].ToString();
                        CL_AgentDetails.TserverIP = cust.TserverIP;// dt_Agentdet.Rows[0]["TSERVERIP"].ToString();
                        CL_AgentDetails.TserverPort = cust.TserverPort;// dt_Agentdet.Rows[0]["TSERVERPORT"].ToString();
                        CL_AgentDetails.IsAsterikLogic = cust.IsAsterikLogic;// dt_Agentdet.Rows[0]["ISASTERIKLOGIC"].ToString();
                        CL_AgentDetails.AsterikGetNextUrl = cust.AsterikGetNextUrl;// dt_Agentdet.Rows[0]["ASTERIKGETNEXTURL"].ToString();
                        CL_AgentDetails.IsConf = cust.IsConf;// dt_Agentdet.Rows[0]["ISCONF"].ToString();
                        CL_AgentDetails.ClientUrl = cust.ClientUrl;// dt_Agentdet.Rows[0]["CLIENTURL"].ToString();
                        CL_AgentDetails.IsManual = cust.IsManual;// dt_Agentdet.Rows[0]["ISMANUAL"].ToString();
                        CL_AgentDetails.manualprefix = cust.manualprefix;// dt_Agentdet.Rows[0]["MANUALPREFIX"].ToString();
                        CL_AgentDetails.AstrikLocalIP = cust.AstrikLocalIP;// dt_Agentdet.Rows[0]["ASTRIKLOCALIP"].ToString();
                        CL_AgentDetails.AstrikIP = cust.AstrikIP;// dt_Agentdet.Rows[0]["ASTRIKIP"].ToString();
                        CL_AgentDetails.ApiCallCut = cust.ApiCallCut;// dt_Agentdet.Rows[0]["APICALLCUT"].ToString();
                        CL_AgentDetails.ConfChannel = cust.ConfChannel;// dt_Agentdet.Rows[0]["CONFCHANNEL"].ToString();
                        CL_AgentDetails.AstrikReqPort = cust.AstrikReqPort;// dt_Agentdet.Rows[0]["ASTRIKREQPORT"].ToString();
                        CL_AgentDetails.AstrikPort = cust.AstrikPort;// dt_Agentdet.Rows[0]["ASTRIKPORT"].ToString();
                        CL_AgentDetails.iframesource = cust.iframesource;// dt_Agentdet.Rows[0]["IFRAMESOURCE"].ToString();
                        CL_AgentDetails.iframesource_OFFICE = cust.iframesource_OFFICE;
                        CL_AgentDetails.HistoryPage = cust.HistoryPage;
                        CL_AgentDetails.PhoneNoMaskIs = cust.PhoneNoMaskIs;// dt_Agentdet.Rows[0]["PHONENOMASKIS"].ToString();
                        CL_AgentDetails.DialAccess = cust.DialAccess;// dt_Agentdet.Rows[0]["DIALACCESS"].ToString();
                        CL_AgentDetails.SingleStepTransfer = cust.SingleStepTransfer;// dt_Agentdet.Rows[0]["SINGLESTEPTRANSFER"].ToString();
                        CL_AgentDetails.ThreeStepTransfer = cust.ThreeStepTransfer;// dt_Agentdet.Rows[0]["THREESTEPTRANSFER"].ToString();
                        CL_AgentDetails.Version = cust.Version;// dt_Agentdet.Rows[0]["VERSION"].ToString();
                        CL_AgentDetails.Result = cust.Result;// dt_Agentdet.Rows[0]["RESULT"].ToString();
                        CL_AgentDetails.OTPRequired = cust.OTPRequired;// dt_Agentdet.Rows[0]["OTPREQUIRED"].ToString();
                        CL_AgentDetails.OTP = cust.OTP;// dt_Agentdet.Rows[0]["OTP"].ToString();
                        CL_AgentDetails.APKVersion = cust.APKVersion;// dt_Agentdet.Rows[0]["APKVERSION"].ToString();
                        CL_AgentDetails.APKURL = cust.APKURL;// dt_Agentdet.Rows[0]["APKURL"].ToString();
                        CL_AgentDetails.SipPort = cust.SipPort;// dt_Agentdet.Rows[0]["SIPPORT"].ToString();
                        CL_AgentDetails.Location = cust.Location;// dt_Agentdet.Rows[0]["ASTR_DN"].ToString();
                        CL_AgentDetails.ProcessType = cust.ProcessType;
                        CL_AgentDetails.AsterikGetNextUrl_Office = cust.AsterikGetNextUrl_Office;

                        CL_AgentDetails.KMS_OFFICE = cust.KMS_OFFICE;
                        CL_AgentDetails.password = cust.password;
                        CL_AgentDetails.Ishome = cust.Ishome;
                        CL_AgentDetails.IsAutoWrap = cust.IsAutoWrap;
                        CL_AgentDetails.AutoWrapTime = cust.AutoWrapTime;
                        CL_AgentDetails.HoldMusic_Path = cust.HoldMusic_Path;
                        CL_AgentDetails.IdleGetNextTimer = cust.IdleGetNextTimer;
                        CL_AgentDetails.AutoGetNextTimer = cust.AutoGetNextTimer;
                        CL_AgentDetails.BioLoginStatus = cust.BioLoginStatus;
                        CL_AgentDetails.IsTest_Required = cust.IsTest_Required;
                        CL_AgentDetails.testAttempted = cust.testAttempted;
                        CL_AgentDetails.DN = cust.DN;
                        //if (CL_AgentDetails.Location == "GGN"  || CL_AgentDetails.Location == "CHN")
                        //{
                        //    GetDnAPI_GGN();
                        //}
                        //else
                        //{
                        //    GetDnAPI();
                        //}
                        return true;
                    }
                }
                catch (Exception exc)
                {
                    //MessageBox.Show("Error  - " + exc.Message, "");

                    //bool status = FetchAgentDetails(txtUsername.Text.Trim());
                    return false;
                }
            }
        }
        private static string GetDomainName(string usernameDomain)
        {
            if (string.IsNullOrEmpty(usernameDomain))
            {
                throw (new ArgumentException("Argument can't be null.", "usernameDomain"));
            }
            if (usernameDomain.Contains("\\"))
            {
                int index = usernameDomain.IndexOf("\\");
                return usernameDomain.Substring(0, index);
            }
            else if (usernameDomain.Contains("@"))
            {
                int index = usernameDomain.IndexOf("@");
                return usernameDomain.Substring(index + 1);
            }
            else
            {
                return "";
            }
        }


        private static string GetUsername(string usernameDomain)
        {
            if (string.IsNullOrEmpty(usernameDomain))
            {
                throw (new ArgumentException("Argument can't be null.", "usernameDomain"));
            }
            if (usernameDomain.Contains("\\"))
            {
                int index = usernameDomain.IndexOf("\\");
                return usernameDomain.Substring(index + 1);
            }
            else if (usernameDomain.Contains("@"))
            {
                int index = usernameDomain.IndexOf("@");
                return usernameDomain.Substring(0, index);
            }
            else
            {
                return usernameDomain;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
         


            int i = 0;
            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                if (p.ProcessName.ToString()=="OneCRM")
                {
                    i++;
                    //this.Close();                  
                }
            }
            if (i > 1)
            {                
                MessageBox.Show("You have already login");
                Application.Exit();
                this.Close();
                Environment.Exit(1);
                return;
            }


            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("User ID and password must required");
            }
            else
            {
                //GetDnAPI();
                bool status = GGN_LoginAPI("Mumbai");
                if (status == false)
                {
                    status = GGN_LoginAPI("GGN");
                }
                
                //bool status = FetchAgentDetails(txtUsername.Text.Trim());
                bool loginstatus = false;
               
                if (status == true)
                {
                    //CTI newform = new CTI();
                    //newform.Show();
                    //this.Hide();
                    if (CL_AgentDetails.OTPRequired == "1" && txtOTP.Visible == false)
                    {
                        txtOTP.Visible = true;
                        MessageBox.Show("Please Enter OTP...");
                        return;

                    }
                    else if (txtOTP.Text.Trim() == "" && CL_AgentDetails.OTPRequired == "1" && txtOTP.Visible == true)
                    {
                        MessageBox.Show("Please Enter OTP...");
                        return;
                    }
                    else if (txtOTP.Text.Trim() != CL_AgentDetails.OTP && CL_AgentDetails.OTPRequired == "1" && txtOTP.Visible == true)
                    {
                        MessageBox.Show("OTP is Incorrect...");
                        return;

                    }


                    if (CL_AgentDetails.Ishome == "0")
                    {
                        loginstatus = checkdomain();

                            int loginstatus1 = 1;
                            //SqlCommand cmd1 = new SqlCommand("getloginstatus", conobj.getconn());
                            //cmd1.CommandType = CommandType.StoredProcedure;
                            //cmd1.Parameters.AddWithValue("@empcode", txtUsername.Text.Trim());
                            //cmd1.Parameters.AddWithValue("@status", loginstatus1);
                            //cmd1.Parameters["@status"].Direction = ParameterDirection.Output;
                            //cmd1.ExecuteNonQuery();
                            //loginstatus1 = Convert.ToInt32(cmd1.Parameters["@status"].Value);
                            //*********************************************************************
                            if (CL_AgentDetails.BioLoginStatus == "0")
                            {
                                MessageBox.Show("Bio-Metric Log Not Found..!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else if (CL_AgentDetails.BioLoginStatus == "2")
                            {
                                MessageBox.Show("You Have Already Logged-out from Bio-Metric Machine,Re-Login Required if you wish to access the CRM..", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                    }
                    else
                    {
                        if (txtPassword.Text != CL_AgentDetails.password)
                        {
                            MessageBox.Show("Incorrect UserName or Password...");
                            return;

                        }
                        else
                        {
                            loginstatus = true;
                        }
                    }
                    if (loginstatus == true)
                    {

                        if (CL_AgentDetails.IsTest_Required == "1")
                        
                        {


                            //DataSet ds = new DataSet();
                            //SqlCommand cmd10 = new SqlCommand("Usp_IsTest_Attempted", conobj.getconn());
                            //cmd10.Parameters.AddWithValue("@Empcode", CL_AgentDetails.AgentID);
                            //cmd10.CommandType = CommandType.StoredProcedure;
                            //SqlDataAdapter da = new SqlDataAdapter(cmd10);
                            //da.Fill(ds);
                            if (CL_AgentDetails.testAttempted == "1")
                            {
                                CTI newform = new CTI();
                                newform.Show();
                                this.Hide();
                             
                            }
                            else
                            {

                                //Questions newform1 = new Questions();
                                //newform1.Show();
                                //this.Hide();

                                CTI newform = new CTI();
                                newform.Show();
                                this.Hide();

                            }
                        }
                        else
                        {
                            CTI newform = new CTI();
                            newform.Show();
                            this.Hide();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You can't login.");
                }
            }
        }

        protected bool checkdomain()
        {
            string domainName = GetDomainName(txtUsername.Text); // Extract domain name 
            string userName = GetUsername(txtUsername.Text).Trim();  // Extract user name 
            string DOMAIN = "";
          
            DOMAIN = "onepointone.in";
            //     }

            //from provided DomainUsername e.g Domainname\Username
            IntPtr token = IntPtr.Zero;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, DOMAIN);
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName);

            if (user == null)
            {

                MessageBox.Show("Username and password not match");
                return false;
            }

            bool isValid = ctx.ValidateCredentials(userName, txtPassword.Text);
            user.Enabled = false;

            if (isValid == false)
            {
                MessageBox.Show("Username and password not match");
                return false;
            }
            else
            {
                DateTime dt = (DateTime)user.LastPasswordSet;
                dt = dt.AddMinutes(330);
                System.TimeSpan diffResult = Convert.ToDateTime(dt.AddDays(45).ToShortDateString()).Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                if (diffResult.Days <= 0)
                {
                    //if (!string.IsNullOrEmpty(txtNewPassword.Text))
                    //{
                    //    try
                    //    {
                    //        user.ChangePassword(txtuPassword.Text, txtNewPassword.Text);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show(ex.Message);
                    //        return false;
                    //    }

                    //}
                    //else
                    //{
                        MessageBox.Show("Your password is expired!!! Please reset the password!!!");
                        lblNewPassword.Visible = true;
                        lblConfirmPassword.Visible = true;
                        txtNewPassword.Visible = true;
                        txtConfirmPassword.Visible = true;
                        btnChangePassword.Visible = true;                      
                        return false;
                    //}
                }
                else if (diffResult.Days < 5)
                {
                    //if (!string.IsNullOrEmpty(txtNewPassword.Password))
                    //{

                    //    try
                    //    {
                    //        user.ChangePassword(txtuPassword.Password, txtNewPassword.Password);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show(ex.Message);
                    //        return false;
                    //    }
                    //}
                    //else
                    //{
                        if (MessageBox.Show("Your password will expired in next " + dt.Day.ToString() + " days!! Do you want to reset the password!!!","Passwor Expired!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            lblNewPassword.Visible = true;
                            lblConfirmPassword.Visible = true;
                            txtNewPassword.Visible = true;
                            txtConfirmPassword.Visible=true;
                            btnChangePassword.Visible = true;
                            return false;
                        }

                    //}
                }
                return isValid;
            }
        }
        private void GetDnAPI_GGN()
        {

           
            string url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/GetDN";

            var json = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string hostname = Dns.GetHostName();
                json = "{\"HostName\":\"" + hostname + "\",\"Opoid\":\"" + txtUsername.Text.Trim() + "\"}";

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

                    }
                    else
                    {
                        DataTable dt_Dn = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                        CL_AgentDetails.DN = dt_Dn.Rows[0]["DN"].ToString();
                    }
                }
                catch (Exception exc)
                {
                    //MessageBox.Show("Error  - " + exc.Message, "");
                }
            }
        }

        private void GetDnAPI()
        {


            string url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/GetDN";

            var json = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string hostname = Dns.GetHostName();
                json = "{\"HostName\":\"" + hostname + "\",\"Opoid\":\"" + txtUsername.Text.Trim() + "\"}";

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
                        
                    }
                    else
                    {
                        DataTable dt_Dn = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                        CL_AgentDetails.DN = dt_Dn.Rows[0]["DN"].ToString();
                    }
                }
                catch (Exception exc)
                {
                    //MessageBox.Show("Error  - " + exc.Message, "");
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void lnkChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lblConfirmPassword.Visible = true;
            lblNewPassword.Visible = true;
            txtConfirmPassword.Visible = true;
            txtNewPassword.Visible = true;
            btnChangePassword.Visible = true;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter OPO ID...");
                txtUsername.Focus();
                return;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Old password...");
                txtPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Trim() == "" || txtConfirmPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter New and Confirm password...");
                txtNewPassword.Focus();
                return;
            }
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show("New Passwords didn't get Matched. Please Re-Enter the New Passwords...");
                txtNewPassword.Focus();
                return;
            }
            bool status = false;
            if (CL_AgentDetails.Ishome == "0")
            {
                status = changeDomainPassword();
            }
            else
            {
                //Call API
                string url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/UpdatePassword";
                if (CL_AgentDetails.Location == "GGN" || CL_AgentDetails.Location == "CHN")
                {
                    url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/UpdatePassword";
                }
                else
                {
                    url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/UpdatePassword";
                }
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"Opoid\":\"" + txtUsername.Text.Trim() + "\"," +
                                  "\"Password\": \"" + txtNewPassword.Text.Trim() + "\"}";                   



                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                    try
                    {

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();

                            if (result.Contains("Success"))
                            {
                                MessageBox.Show("Password has been Changed Successfully!!!");
                                lblConfirmPassword.Visible = false;
                                lblNewPassword.Visible = false;
                                txtConfirmPassword.Visible = false;
                                txtNewPassword.Visible = false;
                                btnChangePassword.Visible = false;
                            }
                            else
                            {
                                MessageBox.Show("Password Changing/Updating Failed!!!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something Went Wrong- API Error!!!");
                    }

                }
                }
                if (status == true)
                {
                    MessageBox.Show("Password has been Changed Successfully!!!");
                    lblConfirmPassword.Visible = false;
                    lblNewPassword.Visible = false;
                    txtConfirmPassword.Visible = false;
                    txtNewPassword.Visible = false;
                    btnChangePassword.Visible = false;
                }
            
        }

        
        protected bool changeDomainPassword()
        {
            string domainName = GetDomainName(txtUsername.Text); // Extract domain name 
            string userName = GetUsername(txtUsername.Text).Trim();  // Extract user name 
            string DOMAIN = "";

            DOMAIN = "onepointone.in";            
            IntPtr token = IntPtr.Zero;
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, DOMAIN);
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName);
            if (user == null)
            {

                MessageBox.Show("Username doesn't exist");
                return false;
            }
          
            try
            {
                user.ChangePassword(txtPassword.Text, txtNewPassword.Text);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

       
    }
}
