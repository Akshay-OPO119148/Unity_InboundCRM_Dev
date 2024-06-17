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

                var result = streamreader.ReadToEnd();

                dynamic dynJson = JsonConvert.DeserializeObject(result);
                dynJson = dynJson.Replace("[", "").Replace("]", "");
                JavaScriptSerializer ser = new JavaScriptSerializer();

                CL_AgentDet cust = ser.Deserialize<CL_AgentDet>(dynJson);

                
                try
                {
                    if (result == "Failure")
                    {
                       
                        return false;
                    }
                    else
                    {
                       
                        CL_AgentDetails.ProcessName = cust.ProcessName;
                        CL_AgentDetails.OPOID = cust.OPOID;
                        CL_AgentDetails.AgentName = cust.AgentName;
                        CL_AgentDetails.AgentID = cust.AgentID;
                        CL_AgentDetails.TserverIP_OFFICE = cust.TserverIP_OFFICE;                     

                        CL_AgentDetails.Astr_DN = cust.Astr_DN;
                        CL_AgentDetails.MobileNo = cust.MobileNo;
                        CL_AgentDetails.Email = cust.Email;
                        CL_AgentDetails.Prefix = cust.Prefix;
                        CL_AgentDetails.TserverIP = cust.TserverIP;
                        CL_AgentDetails.TserverPort = cust.TserverPort;
                        CL_AgentDetails.IsAsterikLogic = cust.IsAsterikLogic;
                        CL_AgentDetails.AsterikGetNextUrl = cust.AsterikGetNextUrl;
                        CL_AgentDetails.IsConf = cust.IsConf;
                        CL_AgentDetails.ClientUrl = cust.ClientUrl;
                        CL_AgentDetails.IsManual = cust.IsManual;
                        CL_AgentDetails.manualprefix = cust.manualprefix;
                        CL_AgentDetails.AstrikLocalIP = cust.AstrikLocalIP;
                        CL_AgentDetails.AstrikIP = cust.AstrikIP;
                        CL_AgentDetails.ApiCallCut = cust.ApiCallCut;
                        CL_AgentDetails.ConfChannel = cust.ConfChannel;
                        CL_AgentDetails.AstrikReqPort = cust.AstrikReqPort;
                        CL_AgentDetails.AstrikPort = cust.AstrikPort;
                        CL_AgentDetails.iframesource = cust.iframesource;
                        CL_AgentDetails.iframesource_OFFICE = cust.iframesource_OFFICE;
                        CL_AgentDetails.HistoryPage = cust.HistoryPage;
                        CL_AgentDetails.PhoneNoMaskIs = cust.PhoneNoMaskIs;
                        CL_AgentDetails.DialAccess = cust.DialAccess;
                        CL_AgentDetails.SingleStepTransfer = cust.SingleStepTransfer;
                        CL_AgentDetails.ThreeStepTransfer = cust.ThreeStepTransfer;
                        CL_AgentDetails.Version = cust.Version;
                        CL_AgentDetails.Result = cust.Result;
                        CL_AgentDetails.OTPRequired = cust.OTPRequired;
                        CL_AgentDetails.OTP = cust.OTP;
                        CL_AgentDetails.APKVersion = cust.APKVersion;
                        CL_AgentDetails.APKURL = cust.APKURL;
                        CL_AgentDetails.SipPort = cust.SipPort;
                        CL_AgentDetails.Location = cust.Location;
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
                        
                        return true;
                    }
                }
                catch (Exception exc)
                {                    
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
              
                bool status = GGN_LoginAPI("Mumbai");
                if (status == false)
                {
                    status = GGN_LoginAPI("GGN");
                }
                
                
                bool loginstatus = false;
               
                if (status == true)
                {
                    
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

                            if (CL_AgentDetails.testAttempted == "1")
                            {
                                CTI newform = new CTI();
                                newform.Show();
                                this.Hide();
                             
                            }
                            else
                            {

                                Questions newform1 = new Questions();
                                newform1.Show();
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
                    
                        MessageBox.Show("Your password is expired!!! Please reset the password!!!");
                        lblNewPassword.Visible = true;
                        lblConfirmPassword.Visible = true;
                        txtNewPassword.Visible = true;
                        txtConfirmPassword.Visible = true;
                        btnChangePassword.Visible = true;                      
                        return false;
                    
                }
                else if (diffResult.Days < 5)
                {
                    
                        if (MessageBox.Show("Your password will expired in next " + dt.Day.ToString() + " days!! Do you want to reset the password!!!","Passwor Expired!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            lblNewPassword.Visible = true;
                            lblConfirmPassword.Visible = true;
                            txtNewPassword.Visible = true;
                            txtConfirmPassword.Visible=true;
                            btnChangePassword.Visible = true;
                            return false;
                        }

                   
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
