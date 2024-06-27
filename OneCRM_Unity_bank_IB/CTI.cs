
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

//using System.Collections.Generic;
//using System.Text;
using Genesyslab.Platform.Commons;
using Genesyslab.Platform.Commons.Collections;
using Genesyslab.Platform.Commons.Connection;
using Genesyslab.Platform.Commons.Logging;
using Genesyslab.Platform.Commons.Protocols;
using Genesyslab.Platform.Outbound.Protocols;
using Genesyslab.Platform.Outbound.Protocols.OutboundDesktop;
using Genesyslab.Platform.Voice.Protocols;
using Genesyslab.Platform.Voice.Protocols.TServer;
using Genesyslab.Platform.Voice.Protocols.TServer.Events;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Agent;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Dn;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Party;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Special;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Dtmf;
using Genesyslab.Platform.ApplicationBlocks.SipEndpoint;
using System.Threading;
using System.Configuration;
using System.Collections;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Web;
using UnityBank.Models;
using static System.Collections.Specialized.BitVector32;
using System.Web.UI.HtmlControls;
using OneCRM.Models;

internal delegate void SendMessageDelegate(string message, int sessionId);
internal delegate void ChatStoppedDelegate(int sessionId);

internal delegate void ReleaseCallClickedDelegate();


namespace OneCRM
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class CTI : Form
    {

        public static string DN = "";
        public static string Host = "";
        public static string Port = "";
        public static string TPort = "";
        public static int callback_day_validation = 0;
        public static int autowraptime = 10;
        public static string uid = "";
        public static string pwd = "";
        public static string dmn = "";
        private string extn;
        public Int64 holdid = 0;
        public static long Gen_Event_Start = 0;
        public static long StatConnid = 0;
        public Int64 counter = 0;

        private string distype = "";
        string tServerHost = "";
        string tServerport = "";
        private string winlogin;
        private bool isConnectionOpen = false;
        private bool isRegistered = false;
        private bool isLoggedIn = false;
        private bool islogout = false;
        private TServerProtocol tServerProtocol;
        private Thread receiver;
        private IMessage iMessage;
        private volatile bool isRunning;
        private CallResult callRes;
        bool TServerConnected = false;
        public bool isReady = true;
        private string agentID;
        private string PersonDBID;
        private string agentPassword;
        private string Prefix = "";
        private string Prefix2 = "";
        private string Prefix3 = "";
        private string MaskStatus = "";
        private int AutowrapStatus = 2;
        private int ScreenRecStatus = 2;
        private string ActionDate = "";
        private bool AutoDialStatus = false;
        private string DialaccessStatus = "";
        String finishCode = "GEN";
        String reasonCode;
        public static string AgentName = "";
        public int CurrentStatusId = 0;
        private double recordid;
        private double recordid1;
        private double updatedisprecordid1;
        private double updatedisprecordid;
        private double HoldID;
        private double BreakID;
        private string RecordingPath = "";
        private string DStartTime;
        private string HoldStart;
        private string HoldEnd;
        private int pTraID;
        private DateTime LastUpdateTime;
        private string Breakinfocode;
        public int PreviewTime = 0;
        private string transnumber = "0";
        public Int32 savecount = 0;
        Break frmbreak = new Break();
        public static string PCBCallTYPE = "";
        public static string MasterPhone = "";
        private string BreakStatus = "";
        private int CurrBreakID = 0;
        DataTable dt_disp_full;
        DataTable dt_ticketdet;
        DataTable dt_callhistory;
        DataTable dt_tickethistory;
        DataTable dt_Customerdet;
        DataTable dt_EntityType;
        bool Cust_Register = false;
        public static string disconnecttype = "";
        bool HaveTicketDetail = false;
        string EntityValue = "";
        string CategoryName = "";
        public static Boolean isnotready = false;

        IVideoWindowWrapper receiveWindow, previewWindow;
        bool receiveStarted;
        bool previewStarted;
        bool previewMessageReceived;
        bool receiveMessageReceived;

        private static string Marq = "";

        //************Call Parameter********
        private volatile ConnectionId connID;
        private volatile ConnectionId TransConnid;

        private volatile ConnectionId IVRconnID;
        private volatile ConnectionId TconnID;
        private volatile ConnectionId IVRConnid;
        private string ani;
        private string inbANI;
        private string dnis;
        public static string CLI = "";
        //************************************

        private string GenesysName = "";
        public static string Campaign = "";
        public Int64 id = 0;
        private double updatelocaldisposition;
        private Int32 BreakIDS;
        private string StartTime;
        private string EndTime;
        string ivrlang = "";
        public bool datagrid = false;
        public bool isAgentReady = true;
        private bool isOnCall = false;
        private bool isOnConferenceCall = false;
        private bool isOnACW = false;
        private bool EventdNoutofservice = false;

        private int ocsApplicationID;
        private string callingListName;
        private string[] callResultEnum;
        private int callResult;
        private int recordHandle;
        private int PCBrecordHandle;
        private int attempts;
        private string winName;
        private string testwinlogin;
        private string systemname;
        private string campaignphone = "";
        private string PCBcampaignphone = "";
        public Int64 CallLogRecordId = 0;
        public static bool gatt = false;
        private string TRecordID = "";
        public static double MyCode = 0;
        public static double PCBMyCode = 0;
        public static bool isclosed = true;
        private int alarmTime = 0;
        private int clockTime = 0;
        private string AgentGroup = "";
        //private static int requestID = 1;
        public static int requestID = 1;

        private Hashtable requestHash = new Hashtable();
        public static bool isbreak = true;
        private string campaignName;
        private string campaignmode;
        private string CallLogTableName;
        private string LocalRecordingTableName;
        private string MasterRecordingTableName;
        private string APPREFNO = "";
        private string CUST_NAME = "";
        private double HistoryId;
        private Int32 History_Id;
        private Int32 ID;
        private Int32 History_count;
        private bool URLOpen = false;
        private string phonenumber = "";
        private bool Transfer = false;
        private bool DisconnectStatus = true;
        string ConferenceWith = "";
        private static string trans_reason = "";
        private static int dispose_code = 0;
        private static int subdispose_code = 0;

        private string DENSE_SI = "";
        private string Type = "";

        public static string MappedCalliglist = "";
        private Class1 cls = new Class1();

        System.Windows.Forms.Timer TimerStatus = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerCounter = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerGetNext = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer Timer_Autogetnext1 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerEnableGetNext = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerAutoWrap = new System.Windows.Forms.Timer();

        System.Windows.Forms.Timer TimerDlogin = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerSipEndpointStatusCheck = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer TimerDloginpopup = new System.Windows.Forms.Timer();

        string PRIVATE_KEY_PATH = ConfigurationSettings.AppSettings["PRIVATE_KEY_PATH"].ToString();
        
        string CUST_BY_MOBILENO_API_URL = ConfigurationSettings.AppSettings["CUST_BY_MOBILENO_API_URL"].ToString();
        string MINISTATEMENT_API_URL = ConfigurationSettings.AppSettings["MINISTATEMENT_API_URL"].ToString();
        string CUSTOMERDETAILS_BY_CUSTOMERID_API_URL = ConfigurationSettings.AppSettings["CUSTOMERDETAILS_BY_CUSTOMERID_API_URL"].ToString();
        string ACCOUNTLIST_BY_CUSTID_API_URL = ConfigurationSettings.AppSettings["ACCOUNTLIST_BY_CUSTID_API_URL"].ToString();
        string ACC_DETAILS_FOR_CHANNEL_API_URL = ConfigurationSettings.AppSettings["ACC_DETAILS_FOR_CHANNEL_API_URL"].ToString();
        string PRIVATE_KEY_PASSWORD = ConfigurationSettings.AppSettings["PRIVATE_KEY_PASSWORD"].ToString();
        string APP_KEY = ConfigurationSettings.AppSettings["APP_KEY"].ToString();
        string TENANT = ConfigurationSettings.AppSettings["TENANT"].ToString();
        //string ConnectinString = ConfigurationSettings.AppSettings["ConnectinString"].ToString();
        int count = 10;
        int currentPageNo = 0;
        int totalPageSize = 0;
        int currentCustPageNo = 0;
        int totalCustPageSize = 0;
        string customerId = string.Empty;
        string accountNo = string.Empty;
        string ProductCode = string.Empty;
        string Product_Name = string.Empty;
        string AccountBalance =string.Empty;
        string HomeBranch = string.Empty;
        string EmailId = string.Empty;
        private void UpdateButtonStatus(Button btn, bool status)
        {
            btn.Enabled = status;
        }
        private void UpdateButtontext(Button btn, string txt)
        {
            btn.Text = txt;
        }
        private void UpdateStripButtonStatus(ToolStripButton btn, bool status)
        {
            btn.Enabled = status;
        }
        private void UpdateLabelBackColor(Label lbl, string color)
        {
            lbl.BackColor = Color.FromName(color);
        }
        private void UpdateStripLabelBackColor(ToolStripLabel lbl, string color)
        {
            lbl.BackColor = Color.FromName(color);
        }

        private void UpdateLabelForeColor(Label lbl, string color)
        {
            lbl.ForeColor = Color.FromName(color);
        }
        private void UpdateStripLabelForeColor(ToolStripLabel lbl, string color)
        {
            lbl.ForeColor = Color.FromName(color);
        }

        private void UpdatePanelStatus(Panel pnl, bool status)
        {
            pnl.Visible = status;
        }
        private void UpdateLabelStatus(Label lbl, bool status)
        {
            lbl.Visible = status;
        }
        private void UpdateLabelText(Label lbl, string txt)
        {
            lbl.Text = txt;
        }
        private void UpdateLabelText(ToolStripLabel lbl, string txt)
        {
            lbl.Text = txt;
        }

        private void UpdateText(TextBox txtBox, string txt)
        {
            txtBox.Text = txt;
        }
        private void UpdateStripText(ToolStripTextBox txtBox, string txt)
        {
            txtBox.Text = txt;
        }
        private void UpdateLabelBackColor(Label lbl, Color col)
        {
            lbl.BackColor = col;
        }
        private void UpdateComboText(ComboBox cmbBox, string txt)
        {
            cmbBox.Text = txt;
        }
        private void UpdateComboValue(ComboBox cmbBox, string txt)
        {
            cmbBox.SelectedValue = txt;
        }
        private void ClearComboboxValue(ComboBox cmbBox)
        {
            cmbBox.Items.Clear();
        }
        private void AddComboboxValue(ComboBox cmbBox, string Values)
        {
            cmbBox.Items.Add(Values);
        }
        private void UpdateRichTextBox(RichTextBox rtxtBox, string txt)
        {
            rtxtBox.Text = txt;
        }
        private void FillDatasetWithGridView(DataGridView dv1, DataSet ds)
        {
            dv1.DataSource = ds.Tables[0];
        }
        private void gdviewFetch(DataGridView dv, DataTable dt)
        {
            dv.DataSource = dt;
            dv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dv.AutoResizeColumns();
        }
        private void ReadOnlyText(TextBox txtBox, bool txt)
        {
            txtBox.ReadOnly = txt;
        }
        private void EnableCmbBox(ComboBox cmbBox, bool txt)
        {
            cmbBox.Enabled = txt;
        }
        private void openweb(WebBrowser webbrows1, string txt)
        {
            webBrowser1.Url = new Uri(txt);
        }



        public delegate void ChangeLabelBackColor(Label lbl, Color col);
        public delegate void UpdateButtontxt(Button btn, string txt);
        public delegate void UpdateButtonStatusCallback(Button btn, bool status);
        public delegate void UpdateStripButtonStatusCallback(ToolStripButton btn, bool status);
        public delegate void UpdatePanelStatusCallback(Panel pnl, bool status);
        public delegate void UpdateLabelStatusCallback(Label lbl, bool status);
        public delegate void UpdateLabelTextCallback(Label lbl, string txt);
        public delegate void UpdateStripLabelTextCallback(ToolStripLabel lbl, string txt);
        public delegate void UpdateTextCallback(TextBox txtBox, string txt);
        public delegate void UpdateStripTextCallback(ToolStripTextBox txtBox, string txt);
        public delegate void UpdateComboTextCallback(ComboBox txtBox, string txt);
        public delegate void ClearComboValue(ComboBox cmbBox);
        public delegate void AddComboValue(ComboBox cmbBox, string Values);
        public delegate void gdview(DataGridView dv, DataTable dt);
        public delegate void UpdateRichTectBoxCallback(RichTextBox rtxtBox, string txt);
        public delegate void UpdateLabelBackColorCallback(Label lbl, string color);
        public delegate void UpdateStripLabelBackColorCallback(ToolStripLabel lbl, string color);
        public delegate void UpdateLabelForeColorCallback(Label lbl, string color);
        public delegate void UpdateStripLabelForeColorCallback(ToolStripLabel lbl, string color);
        public delegate void BindGridView(DataGridView dv1, DataSet ds);
        public delegate void ReadOnlyTextBox(TextBox txtBox, bool txt);
        public delegate void EnableComboBox(ComboBox cmbBox, bool txt);
        public delegate void UpdateComboCallback(ComboBox cmbBox, string txt);
        public delegate void Openwebbrowser(WebBrowser webbrows, string txt);

        SqlConnection con1 = new SqlConnection("server=192.168.0.57;database=Unity_Bank_INB;UID=opodba;Pwd=opo@1234;Max Pool Size=1000;Connection Timeout=0;MultipleActiveResultSets=true");

        Connection conObj = new Connection();

        Connection conobj = new Connection();
        Connection conobjdialer = new Connection();

        public CTI()
        {
            InitializeComponent();

            CmbStatus.MouseWheel += ComboBox_MouseWheel;
            Cmb_Category.MouseWheel += ComboBox_MouseWheel;
            cmb_SubCategory.MouseWheel += ComboBox_MouseWheel;
            //Cmb_Banking_Status.MouseWheel += ComboBox_MouseWheel;
            Cmb_Disposition.MouseWheel += ComboBox_MouseWheel;
            Cmb_SubDisposition.MouseWheel += ComboBox_MouseWheel;
            comboxVertical.MouseWheel += ComboBox_MouseWheel;
            comboxProduct.MouseWheel += ComboBox_MouseWheel;

        }
        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            // Prevent the ComboBox value from changing when scrolling the mouse
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void btn_LogOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (isOnCall == true)
                {
                    MessageBox.Show("Not Allowed while agent on call please dispose this record...", "Agent On Call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //if (txtmycode.Text != "")
                //{
                //    MessageBox.Show("Please dispose current record...", "Agent On Call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                //if (CurrentStatusId == 4)
                //{
                //    MessageBox.Show("Please dispose your call...", "Agent On Call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
                //else
                //{
                DialogResult dialogResult = MessageBox.Show("Do You Want To LogOut..?", "LogOut", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (CurrentStatusId == 1 || CurrentStatusId == 0)
                    {
                        isRunning = false;
                        if (isLoggedIn)
                        { LogOut(); }

                        if (isConnectionOpen)
                        { tServerProtocol.Close(); isConnectionOpen = false; }

                        if (receiver.IsAlive)
                        { receiver.Join(); }

                        Application.Exit();
                        Environment.Exit(1);
                    }
                    else
                    {
                        islogout = true;
                    }
                }
                //}
            }
            catch
            {
                this.Close();
            }
        }

        private void LogOut()
        {
            try
            {
                RequestAgentLogout requestAgentLogout = RequestAgentLogout.Create(extn);
                iMessage = tServerProtocol.Request(requestAgentLogout);
                checkReturnedMessage(iMessage);
                isLoggedIn = false;
            }
            catch
            {
                this.Close();
            }
        }


        private string GetPublishedVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.
                    CurrentVersion.ToString();
            }
            return "Not network deployed";
        }
        protected override void OnLoad(EventArgs e)
        {
            accountDetailsGridView.Visible = false;
            CustomerdetailsdataGridView.Visible = false;
            mappedAccountListtitlelabel.Visible = false;
            searchlbl.Visible = false;
            Searchtxt.Visible = false;
            customerDetailstitlelabel.Visible = false;
            Previousbtn.Visible = false;
            Nextbtn.Visible = false;
            AccountDetailsPanel.Visible = false;
            TransactionDetailsPanel.Visible = false;
            button3.Visible = false;
            label51.Visible = false;
            CustomerDetailsPageInfo.Visible = false;
            PreviousCustButton.Visible = false;
            NextCustButton.Visible = false;

            //================ Testing ===================
            //GetEntityMasterApi();
            //==========================================
            //Left = Top = 0;
            //Width = Screen.PrimaryScreen.WorkingArea.Width;
            //Height = Screen.PrimaryScreen.WorkingArea.Height;
            //System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            //var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            //var version = fieVersionInfo.FileVersion;


            //this.Text = CL_AgentDetails.ProcessName + " (Version : " + GetPublishedVersion() + ")";
            //txt_AgentName.Text = CL_AgentDetails.AgentName;

            //webBrowser1.AllowWebBrowserDrop = false;
            //webBrowser1.IsWebBrowserContextMenuEnabled = true;
            //webBrowser1.WebBrowserShortcutsEnabled = true;
            //webBrowser1.ObjectForScripting = this;
            ////webBrowser1.ScriptErrorsSuppressed = true;
            ////commented below line
            ////webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?");//mycode=7557276&ConnID=afadadgsaf&agentID=OPO054069&status=2
            //webBrowserAPR.Url = new Uri("http://192.168.0.93:8088/API/AgentStatus/AgentStatus.aspx?Agentid=" + Convert.ToString(CL_AgentDetails.AgentID) + "");
            ////==============================================================//

            //if (!string.IsNullOrEmpty(CL_AgentDetails.HistoryPage))
            //{
            //    string URL = "";
            //    URL = CL_AgentDetails.HistoryPage + "/" + CL_AgentDetails.OPOID;
            //    var process = new System.Diagnostics.Process();
            //    process.StartInfo.FileName = "chrome.exe";
            //    process.StartInfo.Arguments = URL;// + " --incognito";
            //    process.Start();
            //}
            //#region GenesysCall
            //// Automatically resize height and width relative to content
            ////this.SizeToContent = SizeToContent.WidthAndHeight;


            //// this.SizeToContent = SizeToContent.WidthAndHeight;
            //try
            //{
            //    systemname = Environment.MachineName.ToString();

            //    winlogin = CL_AgentDetails.OPOID;

            //    if (!string.IsNullOrEmpty(CL_AgentDetails.DN))
            //    {
            //        DN = CL_AgentDetails.DN;
            //        extn = DN;
            //    }
            //    else
            //    {
            //        MessageBox.Show("DN Information Not Found..!", "NotFound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        this.Dispose();
            //        Application.Exit();
            //        Environment.Exit(1);
            //        return;
            //    }
            //    if (String.IsNullOrEmpty(CL_AgentDetails.KMS_OFFICE))
            //    {
            //        unitytab.TabPages.Remove(tabKMS);
            //    }
            //    else
            //    {
            //        webKMS.Url = new Uri(CL_AgentDetails.KMS_OFFICE);
            //    }
            //    try
            //    {
            //        if (!string.IsNullOrEmpty(CL_AgentDetails.SingleStepTransfer))
            //        {
            //            string transRP = Convert.ToString(CL_AgentDetails.SingleStepTransfer);
            //            if (transRP != "")
            //            {
            //                string[] mm = transRP.Split(',');
            //                for (int i = 0; i < mm.Length; i++)
            //                {
            //                    cmbtrnsRoutepoint.DisplayMember = "Text";
            //                    cmbtrnsRoutepoint.ValueMember = "Value";
            //                    cmbWarmtrnsRP.DisplayMember = "Text";
            //                    cmbWarmtrnsRP.ValueMember = "Value";
            //                    string[] routpoint = mm[i].ToString().Split(':');
            //                    for (int j = 0; j < transRP.Length; j++)
            //                    {
            //                        cmbtrnsRoutepoint.Items.Add(new { Text = routpoint[j].ToString(), Value = routpoint[j + 1].ToString() });
            //                        cmbWarmtrnsRP.Items.Add(new { Text = routpoint[j].ToString(), Value = routpoint[j + 1].ToString() });
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex) { }



            //    Host = CL_AgentDetails.TserverIP_OFFICE.ToString();
            //    Port = CL_AgentDetails.SipPort.ToString();
            //    TPort = CL_AgentDetails.TserverPort.ToString();
            //    callback_day_validation = 7;// Int32.Parse(dr["callback_day_validation"].ToString());
            //    autowraptime = 1500;//Int32.Parse(dr["auto_wrap"].ToString()); //4 Minutes 
            //    tServerHost = Host;
            //    tServerport = TPort;
            //    winlogin = CL_AgentDetails.OPOID;

            //    if (Host == "" || Port == "" || TPort == "")
            //    {
            //        MessageBox.Show("Invalid ServerSetting Details...!", "NotFound", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        this.Dispose();
            //        Application.Exit();
            //        Environment.Exit(1);
            //        return;
            //    }
            //    agentID = CL_AgentDetails.AgentID;// 
            //    AgentName = CL_AgentDetails.AgentName;//
            //    if (agentID == "")
            //    {
            //        MessageBox.Show("Agent information not found..", "tblagentidmap", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        this.Dispose();
            //        Application.Exit();
            //        Environment.Exit(1);
            //        return;
            //    }
            //    timer1.Enabled = true;
            //    timer1.Start();

            //    #endregion
            //    //=========================================================//



            //    base.OnLoad(e);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Kindly Check the Configuration for user", "Login Error");
            //}
            //GetNextRecord();
            GetDetails();
            BindCategory();
            BindDisposition();
            BindVertical();
           // FillConnID();
        }

        private void Agent_Ready()
        {
            RequestAgentReady requestAgentReady = RequestAgentReady.Create(extn, AgentWorkMode.AutoIn);
            iMessage = tServerProtocol.Request(requestAgentReady);
            checkReturnedMessage(iMessage);
            if (iMessage.Name == "EventError")
            {
                isReady = false;
                checkReturnedMessage(iMessage);
            }
            else
            {
                isclosed = true;

            }
        }
        private void CreateConnection(string TIP, string TPort)
        {
            try
            {
                tServerProtocol = new TServerProtocol(new Endpoint(new Uri("tcp://" + TIP + ":" + TPort)));
                tServerProtocol.ClientName = "AgentDesktop";
                TimeSpan t = new TimeSpan(9, 30, 0);
                isRunning = true;
                receiver.Start();
                tServerProtocol.Timeout = t;
                tServerProtocol.Open();
                isConnectionOpen = true;
            }
            catch (Exception ex)
            {
                lblcallstatus.Text = ex.Message.ToString() + " " + tServerHost + ":" + tServerport;
                isConnectionOpen = false;
                isRunning = false;
                return;
            }
        }
        private void RegisterExtension(string DN)
        {
            try
            {
                RequestRegisterAddress requestRegisterAddress = RequestRegisterAddress.Create(DN, RegisterMode.ModeShare, ControlMode.RegisterDefault, AddressType.DN);
                iMessage = tServerProtocol.Request(requestRegisterAddress);
                if (iMessage.Name == "EventError")
                {
                    isReady = false;
                    isRegistered = false;
                    checkReturnedMessage(iMessage);
                    return;
                }
                else
                {
                    isReady = true;
                    isRegistered = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reg Extn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AgentLogin(string DN)
        {
            try
            {
                RequestAgentLogin requestAgentLogin = RequestAgentLogin.Create(DN, AgentWorkMode.ManualIn);
                requestAgentLogin.AgentID = agentID;
                requestAgentLogin.Password = agentPassword;
                iMessage = tServerProtocol.Request(requestAgentLogin);
                if (iMessage.Name == "EventError")
                {
                    isLoggedIn = false;
                    checkReturnedMessage(iMessage);
                }
                else
                {
                    checkReturnedMessage(iMessage);
                    isLoggedIn = true;
                }
            }
            catch (Exception ex)
            {
                isLoggedIn = false;
                MessageBox.Show(ex.Message, "Agent Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AgentReady(string DN)
        {
            try
            {
                if (isLoggedIn == true)
                {
                    RequestAgentReady requestAgentReady = RequestAgentReady.Create(DN, AgentWorkMode.AutoIn);
                    iMessage = tServerProtocol.Request(requestAgentReady);
                    if (iMessage.Name == "EventError")
                    {
                        checkReturnedMessage(iMessage);
                    }
                    else
                    {
                        isReady = true;
                        checkReturnedMessage(iMessage);
                        InitializeCounterTimer();
                        cls.LoadStatusDetails();
                        CurrentStatusId = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                isReady = false;
                MessageBox.Show(ex.Message.ToString(), "AgentReady", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void TimerCounter_Tick(object sender, EventArgs e)
        {

            this.clockTime++;
            int countdown = this.clockTime - this.alarmTime;
            if (this.clockTime != 0)
            {
                label17.Text = secondsToTime(countdown);

            }

            //char[] chars = lblmarque.Content.ToString().ToCharArray();
            ////MessageBox.Show(chars[1].ToString());            
            //char[] newChar = new char[chars.Length];
            //int l = chars.Length;
            //int k = 0;
            //for (int j = 0; j < chars.Length; j++)
            //{
            //    if (j + 1 < chars.Length)
            //        newChar[j] = chars[j + 1];
            //    else
            //    {
            //        newChar[l - 1] = chars[k];
            //        //  k++;                
            //    }
            //}
            //lblmarque.Content = new string(newChar);
            ////if (countdown == 91)
            ////{
            ////    Show_FP();
            ////}
        }
        public string secondsToTime(int seconds)
        {
            int minutes = 0;
            int hours = 0;

            while (seconds >= 60)
            {
                minutes += 1;
                seconds -= 60;
            }
            while (minutes >= 60)
            {
                hours += 1;
                minutes -= 60;
            }

            string strHours = hours.ToString();
            string strMinutes = minutes.ToString();
            string strSeconds = seconds.ToString();

            if (strHours.Length < 2)
                strHours = "0" + strHours;
            if (strMinutes.Length < 2)
                strMinutes = "0" + strMinutes;
            if (strSeconds.Length < 2)
                strSeconds = "0" + strSeconds;

            return strHours + ":" + strMinutes + ":" + strSeconds;
        }
        private void TimerEnableGetNext_Tick(object sender, EventArgs e)
        {
            if (MyCode > 0)
            {
                cmdgetnext.Enabled = false;
                TimerEnableGetNext.Stop();
            }
            else
            {
                cmdgetnext.Enabled = true;
                TimerEnableGetNext.Stop();
            }
        }
        private void TimerStatus_Tick(object sender, EventArgs e)
        {
            try
            {
                cls.CurrentStatus = cls.CurrentStatusName[CurrentStatusId];
                cls.CurrentStatusCount[CurrentStatusId] = cls.CurrentStatusCount[CurrentStatusId] + 1;
                LblStatus1.Text = cls.CurrentStatus;

                if (campaignmode == "Preview")
                {
                    PreviewTime = PreviewTime + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "timer Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void TimerGetNext_Tick(object sender, EventArgs e)
        {
            if (lblcampaign.Text == "Not Available" && String.IsNullOrEmpty(AgentGroup))
            {
                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Agent is not mapped in any campaign" });
                TimerGetNext.Stop();
                Timer_Autogetnext1.Enabled = true;
            }
            else
            {
                if (txtmycode.Text != "" && txtmycode.Text != " " && txtmycode.Text != "Not Available")
                {
                    TimerGetNext.Stop();
                    Timer_Autogetnext1.Enabled = false;
                }


                else if (isbreak == true)
                {
                    if (CL_AgentDetails.IsAsterikLogic == "1")
                    {
                        GetNextRecord();
                    }
                    else
                    {
                        try
                        {
                            if (campaignmode == "Preview" && campaignName != null)
                            {
                                KeyValueCollection kvp = new KeyValueCollection();
                                kvp.Add("GSW_AGENT_REQ_TYPE", "PreviewRecordRequest");
                                kvp.Add("GSW_APPLICATION_ID", ocsApplicationID);
                                kvp.Add("GSW_CAMPAIGN_NAME", campaignName);

                                CommonProperties commonProperties = CommonProperties.Create();
                                commonProperties.UserData = kvp;

                                RequestDistributeUserEvent requestDistributeUserEvent1 = RequestDistributeUserEvent.Create(extn, commonProperties);
                                int id = GenerateReferenceID();
                                requestDistributeUserEvent1.ReferenceID = id;
                                requestHash.Add(id, "RequestDistributeUserEvent");
                                iMessage = tServerProtocol.Request(requestDistributeUserEvent1);
                                TimerGetNext.Stop();
                                Timer_Autogetnext1.Enabled = false;
                            }
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
            }

        }

        private void GetNextRecord()
        {
            try
            {
                string url = CL_AgentDetails.AsterikGetNextUrl_Office + "?Opocode=" + CL_AgentDetails.OPOID;

                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                var httpresponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamreader = new StreamReader(httpresponse.GetResponseStream()))
                {
                    var result = streamreader.ReadToEnd();
                    string[] fetchresult = result.ToString().Split('}');
                    fetchresult[0] = fetchresult[0] + "}";
                    result = fetchresult[0];
                    if (!result.Contains("Failure"))
                    {
                        var dt = (JObject)JsonConvert.DeserializeObject(result);
                        MyCode = dt["Mycode"].Value<double>();
                        campaignphone = dt["Phone"].Value<string>();
                        txtphone.Text = string.Format("XXXXXX{0}", campaignphone.Trim().Substring(6, 4));
                        Show_Data(MyCode);
                        TimerGetNext.Stop();
                        Timer_Autogetnext1.Enabled = false;
                    }
                    else
                    {
                        TimerGetNext.Stop();
                        Timer_Autogetnext1.Enabled = true;
                        MessageBox.Show("No record available for Asterisk Logic", "Getnextrecord_Asterisk");

                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error while calling API - " + ex.Message, "Getnextrecord_Asterisk");
            }

        }


        private int GenerateReferenceID()
        {
            int uniqueID = ++requestID;
            return uniqueID;
        }
        public void InitializeCounterTimer()
        {
            //frmbreak.CurrentStatusId += new Break.CurrentStatusIdEventHandler(frmbreak_CurrentStatusId);

            TimerCounter.Tick += new EventHandler(TimerCounter_Tick);
            TimerStatus.Tick += new EventHandler(TimerStatus_Tick);


            TimerStatus.Interval = 1000;
            TimerStatus.Enabled = true;
            TimerStatus.Start();

            TimerCounter.Interval = 1000;
            TimerCounter.Enabled = true;
            TimerCounter.Start();

            TimerGetNext.Interval = 1000;
            TimerGetNext.Enabled = false;
            TimerGetNext.Tick += TimerGetNext_Tick;

            Timer_Autogetnext1.Interval = 1000;
            Timer_Autogetnext1.Enabled = false;
            Timer_Autogetnext1.Tick += Timer_Autogetnext1_tick;

            TimerAutoWrap.Interval = 1000;
            TimerAutoWrap.Enabled = false;
            TimerAutoWrap.Tick += TimerAutoWrap_Tick;

            TimerEnableGetNext.Interval = 5000;
            TimerEnableGetNext.Enabled = false;
            TimerEnableGetNext.Tick += new EventHandler(TimerEnableGetNext_Tick);

            TimerSipEndpointStatusCheck.Interval = 2;// TimeSpan.FromSeconds(2);
            TimerSipEndpointStatusCheck.Tick += new EventHandler(TimerSipEndpointStatusCheck_Tick);

        }
        private void TimerAutoWrap_Tick(object sender, EventArgs e)
        {
            savecount++;
            if (savecount == autowraptime)
            {

                TimerAutoWrap.Stop();
                //object[] o = new object[1];
                //o[0] = "check Agent status";
                //this.webBrowser1.Document.InvokeScript("CanSave", o);
                //commented below line

                //webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + campaignphone + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=3&starttime=" + StartTime + "&endtime=" + EndTime + "&DisconnectType=" + distype + "&RecPath=" + RecordingPath + "&CampaignName=" + campaignName);
                DisposecallTest("6", "29");

                ClearFields();
                ClareDropDawon();
                //if ((CL_AgentDetails.IsAsterikLogic == "1") && CL_AgentDetails.ProcessType == "OUTBOUND")
                //{
                //    //connID = (ConnectionId)"1234ssff";
                //    webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?mycode=" + MyCode + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=3"); //
                //}
                //else if (CL_AgentDetails.IsManual == "1" && CL_AgentDetails.ProcessType == "OUTBOUND")
                //{
                //    webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + campaignphone + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=3");
                //}
                //else if (CL_AgentDetails.IsManual == "1" && CL_AgentDetails.ProcessType == "INBOUND")
                //{
                //    webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + campaignphone + "&connID=" + Convert.ToString(connID) + "&Agent_ID=" + CL_AgentDetails.OPOID + "&status=3");
                //}
                //else
                //{
                //    webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + campaignphone + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=3");
                //}

                ////savedata("Record Saved Successfully...,Disposition:0,SubDisposition:0,CBDate:,CBType:");
                savecount = 0;
            }
        }
        public Boolean Checkprocess()
        {
            try
            {
                int cct = 0;
                Process[] processes = Process.GetProcesses();
                foreach (var item in processes)
                {
                    if (item.ProcessName.Length > 0)
                    {
                        if (item.ProcessName.Contains("Genesyslab.Sip.Endpoint.QuickStart.Win") || item.ProcessName.Contains("Genesyslab.Sip.Endpoint"))
                        {

                            cct = 1;
                        }
                    }
                }
                if (cct == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private void TimerSipEndpointStatusCheck_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    int cct = 0;
            //    Process[] processes = Process.GetProcesses();
            //    foreach (var item in processes)
            //    {
            //        if (item.ProcessName.Length > 0)
            //        {
            //            if (item.ProcessName.Contains("Genesyslab.Sip.Endpoint.QuickStart.Win") || item.ProcessName.Contains("Genesyslab.Sip.Endpoint"))
            //            {

            //                cct = 1;
            //            }
            //        }
            //    }
            //    if (cct == 0)
            //    {
            //        LogOut();
            //        statusbarsipinfo.Content = "Inactive";
            //        //lblbreaktype.Visibility = Visibility.Hidden;
            //        statusbarsipinfo.Foreground = new SolidColorBrush(Colors.Red);
            //        MessageBox.Show("Agent is Logged-Out due to SIPEndpoint is not available", "SIPEndpointExit", MessageBoxButton.OK, MessageBoxImage.Error);
            //        lblcallstatus.Content = "Agent is Logged-Out due to SIPEndpoint is not available.";
            //        TimerSipEndpointStatusCheck.Stop();
            //        return;
            //    }
            //    else
            //    {
            //        statusbarsipinfo.Content = "Active";
            //        statusbarsipinfo.Foreground = new SolidColorBrush(Colors.DodgerBlue);
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

        }
        public void LoginafterDlogin()
        {

            receiver = new Thread(new ThreadStart(this.HandleDesktop));
            CreateConnection(tServerHost, tServerport);
            RegisterExtension(extn);
            if (isConnectionOpen == true && isRegistered == true)
            {
                AgentLogin(extn);
                if (isLoggedIn == true)
                {
                    //InitializeCounterTimer();
                    //cls.LoadStatusDetails();
                    //CurrentStatusId = 1;
                    //isFirstlogin = true;
                    AgentReady(extn);
                    // canvas5.Visibility = Visibility.Visible;
                    //wrapPanel1.Visibility = Visibility.Hidden;  
                    //***************************************************************
                    this.label17.Text = "00:00:00";
                    if (isReady == true) { lblcallstatus.Text = "Agent Ready For Call"; }
                    this.ButtonDial.Enabled = true;
                    this.ButtonHangUp.Enabled = false;
                    // this.ButtonAnswer.IsEnabled = false;
                    this.btn_Conference.Enabled = false;
                    this.ButtonHold.Enabled = false;
                    //this.Button16.Enabled = false;
                    this.btnagentready.Enabled = false;
                    this.btnBreak.Enabled = true;
                    this.btnLogout.Enabled = true;
                    //********************fill disposition*****************            


                    try
                    {

                        object pp = CL_AgentDetails.manualprefix;
                        if (pp == null || pp.ToString().Trim() == "")
                        {
                            MessageBox.Show("Prefix Details Not Found of process-" + CL_AgentDetails.ProcessName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ButtonDial.Enabled = false;
                            ButtonHangUp.Enabled = false;
                            cmdgetnext.Enabled = true;
                            return;
                        }
                        else
                        {
                            Prefix = pp.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //------------

                    try
                    {

                        object pp = CL_AgentDetails.Prefix;
                        if (pp == null || pp.ToString().Trim() == "")
                        {
                            Prefix2 = Prefix;
                        }
                        else
                        {
                            Prefix2 = pp.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //*****************************************Fetch Phone Mask Status**********************
                    try
                    {
                        int MsksStatus = Convert.ToInt32(CL_AgentDetails.PhoneNoMaskIs.ToString());
                        if (MsksStatus != null)
                        {
                            MaskStatus = MsksStatus.ToString();
                        }
                        else
                        {
                            MaskStatus = "0";

                        }
                    }
                    catch (Exception)
                    {
                    }
                    //*****************************************Fetch Autodial Status**********************
                    try
                    {

                        if (string.IsNullOrEmpty(CL_AgentDetails.DialAccess) || CL_AgentDetails.DialAccess.ToString() == "0")
                        {
                            AutoDialStatus = false;
                        }
                        else
                        {
                            AutoDialStatus = true;

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Auto Dial Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    try
                    {

                        if (string.IsNullOrEmpty(CL_AgentDetails.IsAutoWrap) || CL_AgentDetails.IsAutoWrap.ToString() == "0")
                        {
                            AutowrapStatus = 0;
                            autowraptime = Convert.ToInt32(CL_AgentDetails.AutoWrapTime.ToString());
                        }
                        else
                        {
                            AutowrapStatus = 1;
                            autowraptime = Convert.ToInt32(CL_AgentDetails.AutoWrapTime.ToString());

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Auto Wrap Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    //refreshgrid();
                }
            }
            lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "DN : " + extn + " is Not registered properly" });
        }


        int cti_height = 0;
        private void btn_ShowHide_CTI_Click(object sender, EventArgs e)
        {
            if (cti_height == 0)
            {
                cti_height = pnl_CTI.Height;
            }
            if (btn_ShowHide_CTI.Tag.ToString().ToUpper() == "EDIT")
            {
                btn_ShowHide_CTI.BackgroundImage = OneCRM.Properties.Resources.plus;
                btn_ShowHide_CTI.Tag = "Show";
                pnl_CTI.Size = new Size(pnl_CTI.Width, 50);
                Panel pnl = new Panel();
                pnl.Name = "pnl_HeaderCTI";
                pnl.Anchor = AnchorStyles.Left;
                pnl.Size = new Size(pnl_CTI.Width - 90, 50);
                //pnl.BackColor = Color.AliceBlue;
                //pnl.BackgroundImage = OneCRM.Properties.Resources._55;
                //pnl.BackgroundImageLayout = ImageLayout.Stretch;
                pnl.BackColor = Color.CornflowerBlue;

                Label lbl = new Label();
                lbl.Text = "CTI";
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.ForeColor = Color.White;
                lbl.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
                lbl.AutoSize = true;


                pnl.Controls.Add(lbl);
                pnl_CTI.Controls.Add(pnl);
                pnl.BringToFront();
            }
            else if (btn_ShowHide_CTI.Tag.ToString().ToUpper() == "SHOW")
            {
                btn_ShowHide_CTI.BackgroundImage = OneCRM.Properties.Resources.minus;
                btn_ShowHide_CTI.Tag = "Edit";
                pnl_CTI.Size = new Size(pnl_CTI.Width, cti_height);
                Panel pnl = this.Controls.Find("pnl_HeaderCTI", true).FirstOrDefault() as Panel;
                pnl_CTI.Controls.Remove(pnl);

                //if (btn_ShowHide_CustomerDetails.Tag.ToString().ToUpper() == "EDIT")
                //{
                //    btn_ShowHide_CustomerDetails_Click(sender, e);
                //}
                //if (btn_ShowHide_History.Tag.ToString().ToUpper() == "EDIT")
                //{
                //    btn_ShowHide_History_Click(sender, e);
                //}
                //if (btn_ShowHide_WebBrowser.Tag.ToString().ToUpper() == "EDIT")
                //{
                //    btn_ShowHide_WebBrowser_Click(sender, e);
                //}
            }
        }



        int web_height = 0;

        public object httpwebrequest { get; private set; }

        private void btn_ShowHide_WebBrowser_Click(object sender, EventArgs e)
        {
            if (web_height == 0)
            {
                web_height = pnl_WebBrowser.Height;
            }
            if (btn_ShowHide_WebBrowser.Tag.ToString().ToUpper() == "EDIT")
            {
                btn_ShowHide_WebBrowser.BackgroundImage = OneCRM.Properties.Resources.plus;
                btn_ShowHide_WebBrowser.Tag = "Show";
                pnl_WebBrowser.Size = new Size(pnl_WebBrowser.Width, 50);
                Panel pnl = new Panel();
                pnl.Name = "pnl_HeaderWebBrowser";
                pnl.Anchor = AnchorStyles.Left;
                pnl.Size = new Size(pnl_WebBrowser.Width - 90, 50);
                //pnl.BackgroundImage = OneCRM.Properties.Resources._55;
                //pnl.BackgroundImageLayout = ImageLayout.Stretch;
                pnl.BackColor = Color.CornflowerBlue;

                Label lbl = new Label();
                lbl.Text = "Contact_Details";
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.ForeColor = Color.White;
                lbl.Font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
                lbl.AutoSize = true;


                pnl.Controls.Add(lbl);
                pnl_WebBrowser.Controls.Add(pnl);
                pnl.BringToFront();
            }
            else if (btn_ShowHide_WebBrowser.Tag.ToString().ToUpper() == "SHOW")
            {
                btn_ShowHide_WebBrowser.BackgroundImage = OneCRM.Properties.Resources.minus;
                btn_ShowHide_WebBrowser.Tag = "Edit";
                pnl_WebBrowser.Size = new Size(pnl_WebBrowser.Width, web_height);//830
                //pnl_WebBrowser.Anchor= (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
                Panel pnl = this.Controls.Find("pnl_HeaderWebBrowser", true).FirstOrDefault() as Panel;
                pnl_WebBrowser.Controls.Remove(pnl);

                //if (btn_ShowHide_CTI.Tag.ToString().ToUpper() == "EDIT")
                //{
                //    btn_ShowHide_CTI_Click(sender, e);
                //}
                //if (btn_ShowHide_CustomerDetails.Tag.ToString().ToUpper() == "EDIT")
                //{
                //    btn_ShowHide_CustomerDetails_Click(sender, e);
                //}
                //if (btn_ShowHide_History.Tag.ToString().ToUpper() == "EDIT")
                //{
                //    btn_ShowHide_History_Click(sender, e);
                //}
            }
        }



        private void btn_Call_Click(object sender, EventArgs e)
        {
            if (AutoDialStatus == true)
            {
                auto_dial();
            }
            else
            {
                MessageBox.Show("You don't have the dial access...", "Dial Error");
                return;
            }
            if (CL_AgentDetails.ProcessType == "OUTBOUND")
            {
                //connID = (ConnectionId)"1234ssff";
                //commented below line
                //webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + campaignphone + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2");
            }
            else if (CL_AgentDetails.IsManual == "1" && CL_AgentDetails.ProcessType == "INBOUND")
            {
                //commented below line
                //webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + campaignphone + "&connID=" + Convert.ToString(connID) + "&Agent_ID=" + CL_AgentDetails.OPOID + "&status=2");
            }
        }

        private void auto_dial()
        {
            string txph = "";
            //TimerAutoWrap.Invoke(new Action(() => TimerAutoWrap.Stop()));

            //System.Windows.Application.Current.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { txph = txtphone.Text; });
            if (txtphone.Text.Contains('X'))
            {
                txph = campaignphone;
            }
            else
            {
                txph = txtphone.Text;
            }
            if (txph == "")
            {
                txph = MasterPhone;
            }
            if (txph == "")
            {
                MessageBox.Show("Not allowed to dial without Number");
                return;
            }
            if (CurrentStatusId == 4)
            {
                KeyValueCollection reasonCodes = new KeyValueCollection();
                reasonCodes.Add("ReasonCode", "ManualDialing");//check before the reasoncode is configured in CCPulseStat
                RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                iMessage = tServerProtocol.Request(requestAgentNotReady);
            }
            //if (campaignmode == "Preview")
            //{
            //    RequestAgentReady requestAgentReady = RequestAgentReady.Create(extn, AgentWorkMode.AuxWork);
            //    iMessage = tServerProtocol.Request(requestAgentReady);
            //}
            string CName = campaignName;

            string DialPhone = "";

            if (campaignphone == "")
            {
                //System.Windows.Application.Current.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { DialPhone = txtphone.Text; });
                DialPhone = txtphone.Text;
                campaignphone = txtphone.Text;
            }
            else if (campaignphone == txph || txph.Contains("X"))
            {
                //System.Windows.Application.Current.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { DialPhone = campaignphone; });
                DialPhone = campaignphone;
            }
            else
            {
                //System.Windows.Application.Current.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { DialPhone = txtphone.Text; });
                DialPhone = txtphone.Text;
            }
            if (DialPhone != "" && DialPhone.Length > 9)
            {

                isclosed = true;

                if (DialPhone.Length > 14)
                {
                    //if ((DialPhone.Substring(0, Prefix.Length) == Prefix))
                    {
                        KeyValueCollection reasonCodes = new KeyValueCollection();
                        reasonCodes.Add("ReasonCode", "ManualDialing");//check before the reasoncode is configured in CCPulseStat
                        RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                        iMessage = tServerProtocol.Request(requestAgentNotReady);

                        RequestMakeCall requestMakeCall = RequestMakeCall.Create(extn, DialPhone, MakeCallType.Regular);
                        iMessage = tServerProtocol.Request(requestMakeCall);
                        if (iMessage.Name == "EventError")
                        {
                            checkReturnedMessage(iMessage);
                        }
                        else
                        {
                            checkReturnedMessage(iMessage);
                            CurrentStatusId = 2;
                            ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                        }
                    }
                }
                else if (DialPhone.Length == 14)
                {
                    //if ((DialPhone.Substring(0, Prefix.Length) == Prefix) || (DialPhone.Substring(0, Prefix.Length) == Prefix2))
                    if (CL_AgentDetails.Prefix.Contains((DialPhone.Substring(0, Prefix.Length))) || CL_AgentDetails.Prefix.Contains((DialPhone.Substring(0, Prefix2.Length))))
                    {
                        KeyValueCollection reasonCodes = new KeyValueCollection();
                        reasonCodes.Add("ReasonCode", "ManualDialing");//check before the reasoncode is configured in CCPulseStat
                        RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                        iMessage = tServerProtocol.Request(requestAgentNotReady);

                        RequestMakeCall requestMakeCall = RequestMakeCall.Create(extn, DialPhone, MakeCallType.Regular);
                        iMessage = tServerProtocol.Request(requestMakeCall);
                        if (iMessage.Name == "EventError")
                        {
                            checkReturnedMessage(iMessage);
                        }
                        else
                        {
                            checkReturnedMessage(iMessage);
                            CurrentStatusId = 2;
                            ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                        }
                    }
                }
                else if (DialPhone.Length >= 10)
                {
                    KeyValueCollection reasonCodes = new KeyValueCollection();
                    reasonCodes.Add("ReasonCode", "ManualDialing");//check before the reasoncode is configured in CCPulseStat
                    RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                    iMessage = tServerProtocol.Request(requestAgentNotReady);

                    RequestMakeCall requestMakeCall = RequestMakeCall.Create(extn, Prefix + DialPhone, MakeCallType.Regular);
                    iMessage = tServerProtocol.Request(requestMakeCall);
                    if (iMessage.Name == "EventError")
                    {
                        checkReturnedMessage(iMessage);
                    }
                    else
                    {
                        checkReturnedMessage(iMessage);
                        CurrentStatusId = 2;
                    }
                    ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                }
                else
                {
                    MessageBox.Show("Enter Proper Phone Number..!", "Proper Phone Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, true });
                    ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, false });
                    cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, false });
                    btnsubmit.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnsubmit, false });
                }

            }
        }

        private void btn_Out_Click(object sender, EventArgs e)
        {
            DialogResult ms = MessageBox.Show("Are you sure? you want to hangup?", "Hang up", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == ms)
            {
                if (IVRconnID != null)
                {
                    RequestReleaseCall requestreleasecall1 = RequestReleaseCall.Create(extn, connID);
                    iMessage = tServerProtocol.Request(requestreleasecall1);
                    if (iMessage.Name == "EventError")
                    {
                        checkReturnedMessage(iMessage);
                    }
                    else
                    {
                        CurrentStatusId = 4;
                    }
                    try
                    {
                        //SqlCommand cmd1 = new SqlCommand("SP_InsertConference_Log", conobj.getconn());
                        //cmd1.CommandType = CommandType.StoredProcedure;
                        //cmd1.Parameters.AddWithValue("@AgentID", AgentName);
                        //cmd1.Parameters.AddWithValue("@Phone", campaignphone.ToString());
                        //cmd1.Parameters.AddWithValue("@ConnID", connID.ToString());
                        //cmd1.Parameters.AddWithValue("@IVRConnid", Convert.ToString(IVRconnID));
                        //cmd1.Parameters.AddWithValue("@ConferenceWith", transnumber);
                        //cmd1.Parameters.AddWithValue("@Emp_ID", winlogin);
                        //cmd1.Parameters.AddWithValue("@Restourant_ID", "");
                        //cmd1.Parameters.AddWithValue("@ConferenceType", ConferenceWith);
                        //cmd1.Parameters.AddWithValue("@EventType", "Self Release");
                        //cmd1.Parameters.AddWithValue("@TransferReason", trans_reason);
                        //cmd1.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Hangup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                else if (DisconnectStatus == true) //if (campaignmode != "Predictive")
                {

                    distype = "Agent";
                    RequestReleaseCall requestreleasecall = RequestReleaseCall.Create(extn, connID);
                    iMessage = tServerProtocol.Request(requestreleasecall);
                    if (iMessage != null)
                    {
                        if (iMessage.Name == "EventError")
                        {
                            checkReturnedMessage(iMessage);
                        }
                        else
                        {
                            CurrentStatusId = 4;
                        }
                    }
                    else
                    { return; }
                }
                else
                { return; }
                try
                {
                    isOnCall = false;
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                    UpdateRecordingEndTime();

                    ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, false });
                    ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, false });
                    ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, true });
                    //btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, true });
                    btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, true });
                    btn_Conference.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btn_Conference, false });
                    //ButtonAnswer.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonAnswer, false });
                    btnLogout.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnLogout, true });
                    //cmddialpad.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmddialpad, false });
                    btnsubmit.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnsubmit, true });
                    btn_Conference.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btn_Conference, false });
                    pnlConf.Invoke(new UpdatePanelStatusCallback(this.UpdatePanelStatus), new object[] { pnlConf, false });

                    if (campaignmode == "Preview" && MyCode <= 0)
                    {
                        cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, true });
                    }
                    gatt = false;
                    isclosed = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hangup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateRecordingStart()
        {


            StatConnid = connID.ToLong();

        }

        private void LoadURL()
        {
            //commented below line
            //webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + txtphone.Text + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2");

        }
        private void Show_Data(double mycode)
        {
            try
            {

                if (campaignmode == "Preview")
                {
                    //if (AutoDialStatus == true)
                    //{
                    auto_dial();

                    //}

                }
                //commented below line
                //webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + txtphone.Text + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            //Without JSON Param
            ////string postData = "Mycode=" + MyCode + "&mobile=" + txtphone.Text + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2";
            ////System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            ////byte[] bytes = encoding.GetBytes(postData);
            ////string url = CL_AgentDetails.iframesource_OFFICE;
            ////webBrowser1.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");
            ////webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + txtphone.Text + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2");
            //With JSON Param
            //string postData = "Mycode=" + MyCode + "&mobile=" + txtphone.Text + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2&JSON=[LeadID:\"12224\",Mobile:\"9934586986\",Customer_name:\"MAnoj\",Form_no:\"21\",Stage:\"asd\",API_Remark:\"sasf\",Pincode:\"400070\",State:\"MH\",City:\"Mum\",Rm_code:\"sa\",ICICI_Bank_Code:\"23adf\",PAN:\"\",Source:\"ICICIDirect\"]";
            //System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            //byte[] bytes = encoding.GetBytes(postData);
            //string url = CL_AgentDetails.iframesource_OFFICE;
            //webBrowser1.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");


            //if ((CL_AgentDetails.IsManual == "0" || CL_AgentDetails.IsAsterikLogic == "1") && CL_AgentDetails.ProcessType == "OUTBOUND")
            //{
            //    //connID = (ConnectionId)"1234ssff";
            //    webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?mycode=" + MyCode + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2"); //
            //}
            //else
            //{
            //    webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?Mycode=" + MyCode + "&mobile=" + campaignphone + "&connID=" + Convert.ToString(connID) + "&AgentID=" + CL_AgentDetails.OPOID + "&status=2");
            //}

        }

        private void UpdateRecordingEndTime()
        {
            InsertCallLogApi();
            //SqlCommand cmddate = new SqlCommand(); cmddate.CommandText = "select convert(varchar,getdate(),121)";
            //cmddate.Connection = conobj.getconn();
            //EndTime = Convert.ToString(cmddate.ExecuteScalar());


        }

        private void btn_Transfer_Click(object sender, EventArgs e)
        {

        }

        private void btn_GetNext_Click(object sender, EventArgs e)
        {
            if (MyCode.ToString() == "0")
            {
                cmdgetnext.Enabled = false;
                TimerGetNext.Start();
                TimerEnableGetNext.Start();
            }
            else
            {
                //MessageBox.Show("Submit the current record(s)...", "CurrentRecordSubmit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btn_Break_Click(object sender, EventArgs e)
        {
            if (isOnCall == false)
            {
                //if (txtmycode.Text == "")
                //{

                //SqlCommand cmd = new SqlCommand("SP_Basic_Process_Details", conobjdialer.getconnDialer());
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@agent_name", AgentName);
                //cmd.Parameters.AddWithValue("@Operation", "SP_NOTREADY_COUNT");

                ////ssm// SqlCommand cmd = new SqlCommand();
                ////ssm// cmd.CommandText = "select count(*) from notready_log where agent_name='" + AgentName + "' and convert(varchar,cdate,101)=convert(varchar,getdate(),101)";
                ////ssm//  cmd.Connection = conobj.getconn();
                //int bcount = Convert.ToInt16(cmd.ExecuteScalar());

                ////if (bcount > 100)
                ////{
                ////    MessageBox.Show("You have exceeded maximum no of breaks limit(50)...!", "BeakLimit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ////    return;
                ////}
                ////else
                ////{
                TimerGetNext.Stop();
                Timer_Autogetnext1.Stop();
                frmbreak = new Break();
                frmbreak.CurrentStatusId += new Break.CurrentStatusIdEventHandler(frmbreak_CurrentStatusId);
                frmbreak.ShowDialog();
                if (isnotready == false)
                {
                    TimerGetNext.Start();
                    isclosed = false;
                    isbreak = true;
                }
                else
                {
                    isclosed = true;
                    isbreak = false;
                    isnotready = false;
                }
                ////}
                //}
            }
        }
        private void frmbreak_CurrentStatusId(int currentstatusid, string brkstatus)
        {
            try
            {
                if (CurrentStatusId != 4 && CurrentStatusId != 14)
                {
                    //lblbreaktype.Visibility = Visibility.Visible;
                    isclosed = true;
                    isbreak = false;

                    KeyValueCollection reasonCodes = new KeyValueCollection();
                    reasonCodes.Add("ReasonCode", brkstatus);//check before the reasoncode is configured in CCPulseStat
                    RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                    iMessage = tServerProtocol.Request(requestAgentNotReady);
                    checkReturnedMessage(iMessage);

                    InitializeBreakShowPanel(brkstatus);
                    CurrentStatusId = currentstatusid;
                    TimerGetNext.Stop();
                    Timer_Autogetnext1.Enabled = false;
                    btnBreak.Enabled = false;
                    cmdgetnext.Enabled = false;
                    ButtonDial.Enabled = false;


                }
                else
                {
                    isclosed = true;
                    isbreak = false;
                    CurrBreakID = currentstatusid;
                    BreakStatus = brkstatus;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RequestAgentBreak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnBreak.Enabled = false;
                btnLogout.Enabled = true;
                return;
            }
        }

        private void btn_Conference_Click(object sender, EventArgs e)
        {
            if (isOnCall == true)
            {
                if (CL_AgentDetails.IsConf == "1")
                {
                    //txtconfmobile.Text = "";
                    //btnconfdial.Text = "Dial";
                    if (pnlConf.Visible == true)
                    {
                        pnlConf.Visible = false;
                    }
                    else
                    {
                        pnlConf.Visible = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Kindly connect the Call first...", "Call not connected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }
        private void checkReturnedMessageIVR(IMessage msg)
        {
            switch (msg.Name)
            {
                case EventDialing.MessageName:
                    EventDialing eventdialing = msg as EventDialing;
                    if (eventdialing.ThisDN == extn)
                    {
                        IVRconnID = eventdialing.ConnID;
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Conference Dialing" });
                    }
                    break;
            }
        }

        public class WSounds
        {
            [DllImport("WinMM.dll")]
            public static extern bool PlaySound(string fname, int Mod, int flag);
            // these are the SoundFlags we are using here, check mmsystem.h for more    
            public int SND_ASYNC = 0x0001;     // play asynchronously    
            public int SND_FILENAME = 0x00020000; // use file name    ew repl
            public int SND_PURGE = 0x0040;     // purge non-static events     
            public void Play(string fname, int SoundFlags)
            {
                PlaySound(fname, 0, SoundFlags);
            }
            public void StopPlay()
            {
                PlaySound(null, 0, SND_PURGE);
            }
        }

        private void HandleDesktop()
        {
            try
            {
                while (isRunning)
                {
                    if (tServerProtocol.State != ChannelState.Opened)
                    {
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    iMessage = tServerProtocol.Receive();
                    if (iMessage != null)
                    {
                        checkReturnedMessage(iMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Handle Desktop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkReturnedMessage(IMessage msg)
        {
            lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, msg.Name.ToString() });
            lblcallstatus.Invoke(new UpdateLabelForeColorCallback(this.UpdateLabelForeColor), new object[] { lblcallstatus, "DeepPink" });

            switch (msg.Name)
            {
                case EventError.MessageName:
                    EventError eventerror = msg as EventError;
                    if (eventerror.ThisDN == extn)
                    {
                        lblcallstatus.Invoke(new UpdateLabelForeColorCallback(this.UpdateLabelForeColor), new object[] { lblcallstatus, "Red" });

                        switch (eventerror.ErrorCode)
                        {
                            case 527:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Sign-in AgentID : " + agentID + " OR DN : " + extn + " is already active at another console" });
                                break;
                            case 1706:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Extension " + extn + " is already logged in" });
                                break;
                            case 59:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "DN " + extn + " is not configured in CME" });
                                break;
                            case 61:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Invalid Calling DN.Prefix is not configured properly in CME" });
                                break;
                            case 71:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Invalid Calling DN.Prefix is not configured properly in CME" });
                                break;
                            case 223:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Bad Parameter Passed To Function >> Logout && Re-Login" });
                                break;
                            case 231:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "DN Is Busy >> Logout && Re-Login" });
                                break;
                            default:
                                lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, Convert.ToString(eventerror.ErrorMessage) });
                                break;
                        }
                        btnLogout.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnLogout, true });
                        btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, false });
                    }
                    break;
                case EventLinkConnected.MessageName:
                    EventLinkConnected eventlinkconnected = msg as EventLinkConnected;
                    break;
                case EventDialing.MessageName:
                    EventDialing eventdialing = msg as EventDialing;
                    if (eventdialing.ThisDN == extn)
                    {
                        connID = eventdialing.ConnID;
                        TxtConnid.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { TxtConnid, Convert.ToString(eventdialing.ConnID) });
                        isclosed = true;
                        btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, false });
                    }
                    break;
                case EventNetworkReached.MessageName:
                    EventNetworkReached eventnetworkreached = msg as EventNetworkReached;
                    if (eventnetworkreached.ThisDN == extn)
                    {
                        if (connID == null)
                        {
                            connID = eventnetworkreached.ConnID;
                            ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, false });
                            ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                        }
                    }
                    break;
                case EventAgentReady.MessageName:
                    try
                    {
                        EventAgentReady eventagentready = msg as EventAgentReady;
                        if (eventagentready.ThisDN == extn)
                        {
                            Gen_Event_Start = eventagentready.Time.TimeinSecs;
                            btnagentready.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnagentready, false });
                            btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, true });

                            ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, true });
                            ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, false });
                            //ButtonAnswer.Dispatcher.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonAnswer, false });
                            ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, false });
                            //Button16.Dispatcher.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { Button16, false });
                            btnLogout.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnLogout, true });

                            if (campaignmode == "Preview" && MyCode > 0)
                            {
                                cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, false });
                                CurrentStatusId = 4;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Event Agent Ready", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case EventAgentNotReady.MessageName:
                    EventAgentNotReady eventagentnotready = msg as EventAgentNotReady;
                    if (eventagentnotready.ThisDN == extn)
                    {
                        btnagentready.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnagentready, true });
                        ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, true });
                        ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, false });
                        // ButtonAnswer.Dispatcher.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonAnswer, false });
                        ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, false });
                        //Button16.Dispatcher.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { Button16, false });
                    }
                    break;
                case EventAgentLogout.MessageName:
                    EventAgentLogout eventagentlogout = msg as EventAgentLogout;
                    if (eventagentlogout.ThisDN == extn)
                    {
                        CurrentStatusId = 9;
                        isLoggedIn = false;
                    }
                    break;
                case EventDNOutOfService.MessageName:
                    EventDNOutOfService eventdNOutOfservice = msg as EventDNOutOfService;
                    if (eventdNOutOfservice.ThisDN == extn)
                    {
                        RequestAgentLogout requestAgentLogout = RequestAgentLogout.Create(extn);
                        iMessage = tServerProtocol.Request(requestAgentLogout);
                        if (iMessage.Name == "EventError")
                        {
                            checkReturnedMessage(iMessage);
                        }
                        cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, false });
                        btnagentready.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnagentready, false });
                        lblcallstatus.Invoke(new UpdateLabelForeColorCallback(this.UpdateLabelForeColor), new object[] { lblcallstatus, "Red" });
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "EventDNOutOfService >>> Contact Dialer Team..." });
                        EventdNoutofservice = true;
                    }
                    break;
                case EventDNBackInService.MessageName:
                    EventDNBackInService eventdnbackinservice = msg as EventDNBackInService;
                    if (eventdnbackinservice.ThisDN == extn)
                    {
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "EventDNBackInService >>> Re-Login Required..." });
                    }
                    break;
                case EventDestinationBusy.MessageName:
                    EventDestinationBusy eventdestinationbusy = msg as EventDestinationBusy;
                    if (eventdestinationbusy.ThisDN == extn)
                    {
                        isclosed = true;
                        ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, false });
                        ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                    }
                    break;
                case EventRinging.MessageName:
                    EventRinging eventRinging = msg as EventRinging;
                    if (eventRinging.ThisDN == extn)
                    {
                        WSounds ws = new WSounds();
                        string PS = @Application.StartupPath + "\\ringin.wav";
                        ws.Play(PS, ws.SND_FILENAME | ws.SND_ASYNC);

                        ani = eventRinging.ANI;
                        dnis = eventRinging.DNIS;
                        connID = eventRinging.ConnID;
                        CLI = eventRinging.ANI;
                        isclosed = true;

                        if (CLI == "Anonymous")
                        {

                        }
                        else
                        {
                            for (int i = 0; i < eventRinging.UserData.Count; i++)
                            {
                                if (eventRinging.UserData.Keys[i] == "RTargetAgentGroup")
                                {
                                    AgentGroup = eventRinging.UserData["RTargetAgentGroup"].ToString();
                                    //lblagentgroup.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblagentgroup, eventRinging.UserData["RTargetAgentGroup"].ToString() });
                                    lblcampaign.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, eventRinging.UserData["RTargetAgentGroup"].ToString() });
                                }
                                if (eventRinging.UserData.Keys[i] == "RTargetObjectSelected")
                                {
                                    AgentGroup = eventRinging.UserData["RTargetObjectSelected"].ToString();
                                    lblcampaign.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, eventRinging.UserData["RTargetObjectSelected"].ToString() });
                                }
                            }
                        }

                        try
                        {
                            //RequestAnswerCall requestanswercall = RequestAnswerCall.Create(extn, connID);
                            //iMessage = tServerProtocol.Request(requestanswercall);
                            //checkReturnedMessage(iMessage);

                            ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                            btnagentready.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnagentready, true });
                        }
                        catch (Exception)
                        { }

                    }
                    break;
                case EventEstablished.MessageName:
                    EventEstablished eventEstablished = msg as EventEstablished;
                    if (eventEstablished.ThisDN.ToString().Equals(extn))
                    {
                        CurrentStatusId = 3;
                        if (connID == null)
                        {
                            connID = eventEstablished.ConnID;

                        }
                        TxtConnid.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { TxtConnid, Convert.ToString(eventEstablished.ConnID) });
                        isOnCall = true;
                        isclosed = true;

                        if (eventEstablished.CallType == CallType.Inbound)
                        {
                            //ClareDropDawon();
                            if (CL_AgentDetails.PhoneNoMaskIs == "1")
                            {
                                txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(eventEstablished.ANI, @"\d(?!\d{0,3}$)", "X") });
                            }
                            else
                            {
                                txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventEstablished.ANI });
                            }
                            //txt_outstandingamt.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_outstandingamt, eventEstablished.ANI });
                            //lblcalltype.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcalltype, "Inbound" });
                            campaignphone = eventEstablished.ANI;
                            //commented below line
                            //string URLinb=CL_AgentDetails.iframesource_OFFICE + "?connid="+connID.ToString()+"&Agent_ID=" +CL_AgentDetails.OPOID+"&Mobile=" + campaignphone+"&Status=2";
                            //webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?connid="+connID.ToString()+"&Agent_ID=" +CL_AgentDetails.OPOID+"&Mobile=" + campaignphone+"&Status=2");
                            //openweb(WebBrowser webbrows1, string txt)
                            //commented below line
                            //webBrowser1.Invoke(new Openwebbrowser(this.openweb), new object[] { webBrowser1, URLinb });
                            //if (string.IsNullOrWhiteSpace(txt_EMIOutstanding.Text))
                            //{
                            //    GetCustomerApi();
                            //}

                        }
                        else
                        {

                            //txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventEstablished.ANI });
                            //txt_CustPhone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_CustPhone, eventEstablished.ANI });
                            //lblcalltype.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcalltype, "Outbound" });
                            //if (string.IsNullOrWhiteSpace(txt_EMIOutstanding.Text))
                            //{
                            //    GetCustomerApi();
                            //}

                        }



                        try
                        {
                            //TimeStamp sdt1 = eventEstablished.Time.TimeinSecs;
                            int ts = eventEstablished.Time.TimeinSecs;
                            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts).ToLocalTime();
                            StartTime = dt.ToString("yyyy-MM-dd hh:mm:ss tt");

                        }
                        catch (Exception)
                        {
                        }
                        UpdateRecordingStart();

                        if (eventEstablished.UserData != null)
                        {
                            for (int i = 0; i < eventEstablished.UserData.Count; i++)
                            {
                                if (eventEstablished.UserData.Keys[i] == "GSIP_REC_FN")
                                {
                                    RecordingPath = eventEstablished.UserData[i].ToString();
                                    RecordingPath = RecordingPath + "_pcmu.wav";

                                    //SqlCommand cmd = new SqlCommand("UpdateRecordingFileNameINB", conobj.getconn());
                                    //cmd.CommandType = CommandType.StoredProcedure;
                                    //cmd.Parameters.AddWithValue("@RecordingPath", RecordingPath);
                                    //cmd.Parameters.AddWithValue("@id", recordid1);
                                    //cmd.Parameters.AddWithValue("@id1", recordid);
                                    //cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, true });
                        ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                        btn_Conference.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btn_Conference, true });

                    }

                    break;
                case EventReleased.MessageName:
                    EventReleased eventReleased = msg as EventReleased;
                    if (eventReleased.ThisDN == extn)
                    {
                        if (IVRconnID != null && eventReleased.ANI != null)
                        {
                            RequestRetrieveCall retrievecall = RequestRetrieveCall.Create(extn, connID);
                            iMessage = tServerProtocol.Request(retrievecall);
                            if (iMessage.Name == "EventError")
                            {
                                CurrentStatusId = 4;
                                isOnCall = false;
                                btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, true });
                            }
                            btnconfdial.Invoke(new UpdateButtontxt(this.UpdateButtontext), new object[] { btnconfdial, "Dial" });
                            IVRconnID = null;
                            return;
                        }
                        else
                        {
                            if (IVRconnID != null && eventReleased.ANI == null)
                            {
                                RequestReleaseCall releasecall1 = RequestReleaseCall.Create(extn, IVRconnID);
                                iMessage = tServerProtocol.Request(releasecall1);                                
                                btnconfdial.Invoke(new UpdateButtontxt(this.UpdateButtontext), new object[] { btnconfdial, "Dial" });
                                IVRconnID = null;
                            }
                            //RequestReleaseCall releasecall = RequestReleaseCall.Create(extn, IVRconnID);
                            //iMessage = tServerProtocol.Request(releasecall);

                            //RequestRetrieveCall retrievecall = RequestRetrieveCall.Create(extn, connID);
                            //iMessage = tServerProtocol.Request(retrievecall);
                            //if (iMessage.Name == "EventError")
                            //{
                            //    CurrentStatusId = 4;
                            //    isOnCall = false;
                            //    btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, true });
                            //}
                            //else
                            {
                                CurrentStatusId = 3;
                                isOnCall = true;
                            }

                            //////if (IVRconnID != null )
                            //////{
                            //////    RequestRetrieveCall retrievecall = RequestRetrieveCall.Create(extn, connID);
                            //////    iMessage = tServerProtocol.Request(retrievecall);
                            //////    //btndisconnectconf.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btndisconnectconf, false });
                            //////    btnconfdial.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { btnconfdial, "Dial" });
                            //////    IVRconnID = null;
                            //////    CurrentStatusId = 3;
                            //////    return;
                            //////}
                            //TimeStamp eventendcallTime = eventReleased.Time;
                            distype = "Customer";
                            ////CurrentStatusId = 4;
                            ////isOnCall = false;
                            connID = eventReleased.ConnID;
                            isclosed = false;
                            gatt = false;
                            UpdateRecordingEndTime();

                            try
                            {
                                TimeStamp sdt1 = eventReleased.Time;
                                int ts = eventReleased.Time.TimeinSecs;
                                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts).ToLocalTime();
                                EndTime = dt.ToString("yyyy-MM-dd hh:mm:ss tt");

                            }
                            catch (Exception)
                            {
                            }

                            if (Convert.ToString(TransConnid) != "")
                            {
                                RequestRetrieveCall requestRetrieveCall = RequestRetrieveCall.Create(extn, connID);
                                iMessage = tServerProtocol.Request(requestRetrieveCall);
                            }
                            lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Customer Disconnect" });
                            btnsubmit.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnsubmit, true });
                            ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, false });
                            ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, false });
                            btn_Conference.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btn_Conference, false });
                            //try
                            //{
                            //    SqlCommand cmd2 = new SqlCommand();
                            //    cmd2.Connection = conobj.getconn();
                            //    //cmd2.CommandText = "update HISTORY set DISCONNECT_TIME =getdate(),DISPTIME=getdate() where Historycode='" + History_Id + "'";
                            //    cmd2.CommandText = "update HISTORY_New set DISCONNECT_TIME =getdate(),DISPTIME=getdate() where ID='" + ID + "'";
                            //    cmd2.Connection = conobj.getconn();
                            //    cmd2.ExecuteNonQuery();


                            //    SqlCommand dcmd = new SqlCommand();
                            //    dcmd.CommandText = "INSERT INTO DISCONNECT_LOG(Phone,Connid,Agent,Connect_time,Disconnect_time,CallType,Disconnect_Type) VALUES('" + txtphone.Text + "','" + connID + "','" + AgentName + "','" + StartTime + "',getdate(),'" + lblcalltype.Text + "','Customer')";
                            //    dcmd.Connection = conobj.getconn();
                            //    dcmd.ExecuteNonQuery();



                            //}
                            //catch (Exception)
                            //{ }
                        }
                    }
                    break;
                case EventOnHook.MessageName:
                    EventOnHook eventOnhook = msg as EventOnHook;
                    if (eventOnhook.ThisDN == extn)
                    {
                        //RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AfterCallWork);
                        //iMessage = tServerProtocol.Request(requestAgentNotReady);
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "After Call Work" });
                        ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, true });
                        btn_Conference.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btn_Conference, false });
                        CurrentStatusId = 4;
                    }
                    break;
                case EventPartyChanged.MessageName:
                    EventPartyChanged eventPartyChanged = msg as EventPartyChanged;
                    if (eventPartyChanged.ThisDN == extn)
                    {
                        try
                        {
                            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventPartyChanged.ANI });
                            campaignphone = eventPartyChanged.ANI;
                            connID = eventPartyChanged.ConnID;
                            //commented below line
                            //string URLinb = CL_AgentDetails.iframesource_OFFICE + "?connid=" + connID.ToString() + "&Agent_ID=" + CL_AgentDetails.OPOID + "&Mobile=" + campaignphone + "&Status=2";
                            //webBrowser1.Invoke(new Openwebbrowser(this.openweb), new object[] { webBrowser1, URLinb });
                        }
                        catch (Exception ex)
                        { }
                    }
                    break;
                case EventPartyDeleted.MessageName:
                    EventPartyDeleted eventpartydelete = msg as EventPartyDeleted;
                    if (eventpartydelete.ThisDN == extn)
                    {
                        RequestRetrieveCall requestRetrieveCall = RequestRetrieveCall.Create(extn, connID);
                        iMessage = tServerProtocol.Request(requestRetrieveCall);
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Party deleted" });
                        btn_Conference.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btn_Conference, false });
                    }
                    break;
                case EventAbandoned.MessageName:
                    EventAbandoned eventAbandoned = msg as EventAbandoned;
                    if (eventAbandoned.ThisDN == extn)
                    {
                        isclosed = true;
                        ani = eventAbandoned.ANI;
                        dnis = eventAbandoned.DNIS;
                        connID = eventAbandoned.ConnID;
                        //txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventAbandoned.ANI });

                        if (CL_AgentDetails.PhoneNoMaskIs == "1")
                        {
                            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(eventAbandoned.ANI, @"\d(?!\d{0,3}$)", "X") });
                        }
                        else
                        {
                            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventAbandoned.ANI });
                        }
                        campaignphone = eventAbandoned.ANI;
                        //txt_outstandingamt.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_outstandingamt, eventAbandoned.ANI });
                        //lblcallstatus.GetCurrentParent().Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Kindly Restart Your System Immediately,There Are Some Critical Issue With Your System." });
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Kindly Restart Your System Immediately." });
                        btnsubmit.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnsubmit, true });
                        //if (string.IsNullOrWhiteSpace(txt_EMIOutstanding.Text))
                        //{
                        //    GetCustomerApi(); 
                        //}

                        //try
                        //{
                        //    SqlCommand cmd1 = new SqlCommand();
                        //    cmd1.Connection = conobj.getconn();
                        //    cmd1.CommandText = "insert into HISTORY_New(MYCODE,CALL_DATE,CALL_TIME,connect_time,disconnect_time,campaign,DISPTIME,PHONE,employeid,AGENT_ID,DISPDATE,connid,Call_Type,AgentSkill) values('" + MyCode + "','" + System.DateTime.Now.ToShortDateString() + "',getdate(),getdate(),getdate(),'" + lblcampaign.Text + "',getdate(),'" + txtphone.Text + "','" + winlogin + "','" + AgentName + "',getdate(),'" + connID + "','" + lblcalltype.Text + "','" + lblagentgroup.Text + "')";
                        //    cmd1.ExecuteNonQuery();

                        //    SqlCommand cmd2 = new SqlCommand();
                        //    cmd2.CommandText = "select max(ID) from HISTORY_New where AGENT_ID='" + AgentName + "'";
                        //    cmd2.Connection = conobj.getconn();

                        //    History_Id = Convert.ToInt32(cmd2.ExecuteScalar());

                        //    SqlCommand dcmd = new SqlCommand();
                        //    dcmd.CommandText = "INSERT INTO DISCONNECT_LOG(Phone,Connid,Agent,connect_time,Disconnect_time,CallType,Disconnect_Type) VALUES('" + txtphone.Text + "','" + connID + "','" + AgentName + "'," + StartTime + ",getdate(),'" + lblcalltype.Text + "','Abandoned')";
                        //    dcmd.Connection = conobj.getconn();
                        //    dcmd.ExecuteNonQuery();

                        //}
                        //catch (Exception ex)
                        //{
                        //    SqlCommand cmd = new SqlCommand();
                        //    cmd.Connection = conobj.getconn();
                        //    cmd.CommandText = "insert into History_error (error,agentname,DATE) values ('" + ex.Message.Replace("'", "") + "','" + AgentName + "',getdate())";
                        //    cmd.Connection = conobj.getconn();
                        //    cmd.ExecuteNonQuery();

                        //    System.Windows.Forms.MessageBox.Show("insert History error" + ex, "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                    }
                    break;
                case EventAttachedDataChanged.MessageName:
                    try
                    {
                        EventAttachedDataChanged eventattacheddatachanged = msg as EventAttachedDataChanged;
                        if (eventattacheddatachanged.ThisDN == extn)
                        {
                            gatt = true;
                            for (int i = 0; i < eventattacheddatachanged.UserData.Count; i++)
                            {
                                if (eventattacheddatachanged.UserData.Keys[i] == "GSW_CALLING_LIST")
                                {
                                    callingListName = eventattacheddatachanged.UserData[i].ToString();
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "GSW_RECORD_HANDLE")
                                {
                                    recordHandle = int.Parse(eventattacheddatachanged.UserData["GSW_RECORD_HANDLE"].ToString());
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "GSW_ATTEMPTS")
                                {
                                    attempts = int.Parse(eventattacheddatachanged.UserData["GSW_ATTEMPTS"].ToString());
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "Record_id")
                                {
                                    TRecordID = eventattacheddatachanged.UserData[i].ToString();
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "GSW_recordid")
                                {
                                    TRecordID = eventattacheddatachanged.UserData[i].ToString();
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "Batchid")
                                {
                                    txtbatchid.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtbatchid, eventattacheddatachanged.UserData[i].ToString() });
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "GSW_PHONE")
                                {
                                    if (CL_AgentDetails.PhoneNoMaskIs == "1")
                                    {
                                        txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(eventattacheddatachanged.UserData[i].ToString(), @"\d(?!\d{0,3}$)", "X") });
                                    }
                                    else
                                    {
                                        txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventattacheddatachanged.UserData[i].ToString() });
                                    }
                                    campaignphone = eventattacheddatachanged.UserData[i].ToString();
                                    MasterPhone = eventattacheddatachanged.UserData[i].ToString();
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "TMasterID")
                                {
                                    MyCode = Convert.ToDouble(eventattacheddatachanged.UserData[i].ToString());
                                    txtmycode.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { txtmycode, eventattacheddatachanged.UserData[i].ToString() });
                                    Show_Data(MyCode);
                                }
                                else if (eventattacheddatachanged.UserData.Keys[i] == "Cus_Number")
                                {
                                    txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventattacheddatachanged.UserData[i].ToString() });
                                }


                                if (eventattacheddatachanged.UserData.Keys[i] == "GSIP_REC_FN")
                                {
                                    RecordingPath = "";
                                    RecordingPath = eventattacheddatachanged.UserData[i].ToString();
                                    RecordingPath = RecordingPath + "_pcmu.wav";

                                    //SqlCommand cmd = new SqlCommand("UpdateRecordingFileNameINB", conobj.getconn());
                                    //cmd.CommandType = CommandType.StoredProcedure;
                                    //cmd.Parameters.AddWithValue("@RecordingPath", RecordingPath);
                                    //cmd.Parameters.AddWithValue("@id", recordid1);
                                    //cmd.Parameters.AddWithValue("@id1", recordid);
                                    //cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, ex.Message.ToString() });
                    }
                    break;
                case EventUserEvent.MessageName:
                    try
                    {
                        EventUserEvent eventUserEvent = msg as EventUserEvent;
                        if (eventUserEvent.ThisDN == extn)
                        {

                            if (eventUserEvent.UserData.GetAsString("GSW_USER_EVENT") != null)
                            {
                                string sss = eventUserEvent.UserData["GSW_USER_EVENT"].ToString();
                                if (eventUserEvent.UserData["GSW_USER_EVENT"].ToString() == "CampaignStarted")
                                {
                                    ocsApplicationID = Convert.ToInt16(eventUserEvent.UserData["GSW_APPLICATION_ID"].ToString());
                                    campaignName = eventUserEvent.UserData["GSW_CAMPAIGN_NAME"].ToString();
                                    campaignmode = eventUserEvent.UserData["GSW_CAMPAIGN_MODE"].ToString();
                                    ////lblcampaign.GetCurrentParent().Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, eventUserEvent.UserData["GSW_CAMPAIGN_NAME"].ToString() + " [" + eventUserEvent.UserData["GSW_CAMPAIGN_MODE"].ToString() + "]" });
                                    lblcampaign.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, eventUserEvent.UserData["GSW_CAMPAIGN_NAME"].ToString() });
                                    txt_campaignmode.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_campaignmode, eventUserEvent.UserData["GSW_CAMPAIGN_MODE"].ToString() });
                                    if (eventUserEvent.UserData["GSW_CAMPAIGN_MODE"].ToString() == "Preview")
                                    {
                                        cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, true });
                                    }
                                    else
                                    {
                                        this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { TimerGetNext.Enabled = false; });
                                        this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { Timer_Autogetnext1.Enabled = false; });
                                    }
                                    if (campaignName == null)
                                    {
                                        MessageBox.Show("No Compaign Available", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else if (eventUserEvent.UserData["GSW_USER_EVENT"].ToString() == "CampaignStopped")
                                {
                                    lblcampaign.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, "Campaign Stopped" });
                                    cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, false });
                                }
                                else if (eventUserEvent.UserData["GSW_USER_EVENT"].ToString() == "CampaignLoaded")
                                {
                                    lblcampaign.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, "Campaign Loaded" });
                                    cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, false });
                                }
                                else if (eventUserEvent.UserData["GSW_USER_EVENT"].ToString() == "CampaignUnloaded")
                                {
                                    lblcampaign.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, "Campaign UnLoaded" });
                                    cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, false });
                                }
                            }
                            if (eventUserEvent.UserData.GetAsString("GSW_USER_EVENT") != null)
                            {
                                string ss = eventUserEvent.UserData["GSW_USER_EVENT"].ToString();
                                if (eventUserEvent.UserData["GSW_USER_EVENT"].ToString() == "PreviewRecord")
                                {
                                    callingListName = eventUserEvent.UserData["GSW_CALLING_LIST"].ToString();
                                    recordHandle = int.Parse(eventUserEvent.UserData["GSW_RECORD_HANDLE"].ToString());
                                    attempts = int.Parse(eventUserEvent.UserData["GSW_ATTEMPTS"].ToString());
                                    try
                                    {
                                        TRecordID = eventUserEvent.UserData["record_id"].ToString();
                                    }
                                    catch (Exception ex1)
                                    {
                                        TRecordID = eventUserEvent.UserData["GSW_recordid"].ToString();
                                    }

                                    MyCode = Convert.ToDouble(eventUserEvent.UserData["TMasterID"].ToString());
                                    txtmycode.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { txtmycode, eventUserEvent.UserData["TMasterID"].ToString() });
                                    try
                                    {
                                        txtbatchid.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtbatchid, eventUserEvent.UserData["Batch_id"].ToString() });
                                    }
                                    catch (Exception ex)
                                    {
                                        txtbatchid.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtbatchid, eventUserEvent.UserData["Batchid"].ToString() });
                                    }
                                    if (MyCode > 0)
                                    {
                                        if (CL_AgentDetails.PhoneNoMaskIs == "1")
                                        {
                                            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(eventUserEvent.UserData["GSW_PHONE"].ToString(), @"\d(?!\d{0,3}$)", "X") });
                                        }
                                        else
                                        {
                                            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventUserEvent.UserData["GSW_PHONE"].ToString() });
                                        }
                                        //txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventUserEvent.UserData["GSW_PHONE"].ToString() });                        
                                        campaignphone = eventUserEvent.UserData["GSW_PHONE"].ToString();
                                        MasterPhone = eventUserEvent.UserData["GSW_PHONE"].ToString();
                                        Show_Data(MyCode);
                                    }
                                }
                                if (eventUserEvent.UserData["GSW_USER_EVENT"].ToString() == "ScheduledCall")
                                {
                                    if (CurrentStatusId == 1 && PCBMyCode == 0)
                                    {
                                        callingListName = eventUserEvent.UserData["GSW_CALLING_LIST"].ToString();
                                        recordHandle = int.Parse(eventUserEvent.UserData["GSW_RECORD_HANDLE"].ToString());
                                        PCBrecordHandle = int.Parse(eventUserEvent.UserData["GSW_RECORD_HANDLE"].ToString());

                                        attempts = int.Parse(eventUserEvent.UserData["GSW_ATTEMPTS"].ToString());
                                        try
                                        {
                                            TRecordID = eventUserEvent.UserData["record_id"].ToString();
                                        }
                                        catch (Exception e)
                                        {
                                            TRecordID = eventUserEvent.UserData["GSW_recordid"].ToString();
                                        }

                                        MyCode = Convert.ToDouble(eventUserEvent.UserData["TMasterID"].ToString());
                                        txtmycode.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { txtmycode, eventUserEvent.UserData["TMasterID"].ToString() });
                                        try
                                        {
                                            txtbatchid.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtbatchid, eventUserEvent.UserData["Batch_id"].ToString() });
                                        }
                                        catch (Exception ex)
                                        {
                                            txtbatchid.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtbatchid, eventUserEvent.UserData["Batchid"].ToString() });
                                        }

                                        if (MyCode > 0)
                                        {
                                            //txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventUserEvent.UserData["GSW_PHONE"].ToString() });
                                            if (CL_AgentDetails.PhoneNoMaskIs == "1")
                                            {
                                                txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(eventUserEvent.UserData["GSW_PHONE"].ToString(), @"\d(?!\d{0,3}$)", "X") });
                                            }
                                            else
                                            {
                                                txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventUserEvent.UserData["GSW_PHONE"].ToString() });
                                            }
                                            campaignphone = eventUserEvent.UserData["GSW_PHONE"].ToString();
                                            MasterPhone = eventUserEvent.UserData["GSW_PHONE"].ToString();
                                            lblcallback.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallback, "Personal Callback" });
                                            if (campaignmode != "Preview")
                                            {
                                                auto_dial();

                                            }

                                            Show_Data(MyCode);

                                        }
                                    }
                                    else
                                    {
                                        PCBMyCode = Convert.ToDouble(eventUserEvent.UserData["TMasterID"].ToString());
                                        PCBcampaignphone = eventUserEvent.UserData["GSW_PHONE"].ToString();
                                    }
                                }

                            }
                            if (eventUserEvent.UserData.GetAsString("GSW_ERROR") != null)
                            {
                                if (eventUserEvent.UserData["GSW_ERROR"].ToString() == "No Records Available")
                                {
                                    lblcallstatus.Invoke(new UpdateLabelForeColorCallback(this.UpdateLabelForeColor), new object[] { lblcallstatus, "Red" });
                                    lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "No Records Available In Campaign" });
                                    CurrentStatusId = 1;
                                    //GetNextRecord();
                                    //Show_FP();
                                    if (CL_AgentDetails.ProcessType == "OUTBOUND")
                                    {
                                        MyCode = GetPCB();
                                        if (MyCode > 0)
                                        {
                                            if (campaignmode != "Preview")
                                            {
                                                auto_dial();
                                            }
                                            Show_Data(MyCode);
                                            //lblcallback.Text = "Personal Callback";
                                            lblcallback.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallback, "Personal Callback" });

                                        }
                                        else
                                        {
                                            //lblcallback.Text = "";
                                            lblcallback.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallback, " " });
                                            cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, true });
                                            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate() { Timer_Autogetnext1.Enabled = true; });
                                        }
                                    }

                                }
                                else
                                {
                                    lblcallstatus.Invoke(new UpdateLabelForeColorCallback(this.UpdateLabelForeColor), new object[] { lblcallstatus, "Black" });
                                    lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "" });
                                    cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, false });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, ex.Message.ToString() });
                    }
                    break;
            }

        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            TraceService("Submit Button Start : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            try
            {
                if (isOnCall == true)
                {
                    MessageBox.Show("Not Allowed While Agent On Call...", "Agent On Call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (CurrentStatusId != 4)
                {
                    MessageBox.Show("Kindly dispose the call...", "Agent Not On Wrap", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (txtphone.Text == "")
                {
                    MessageBox.Show("Phone Field Can't Accept Null Value...", "Phone Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtphone.Focus();
                    return;
                }

                // SaveData();
                //}
                TraceService("Submit Button Completed : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "submiterror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TraceService("Submit Button Catch Error : " + campaignphone + ":" + ex.Message + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                return;
            }
            finally
            {

            }
        }

        public string savedata(String message)
        {
            if (message == "check Agent status")
            {
                if (CurrentStatusId == 4 || CurrentStatusId == 1)
                {
                    if (PCBMyCode == MyCode)
                    {
                        return ("starttime=" + StartTime + ",endtime=" + EndTime + ",DisconnectType=" + distype + ",RecPath=" + RecordingPath + ",CampaignName=" + campaignName);

                    }
                    else
                    {
                        return ("starttime=" + StartTime + ",endtime=" + EndTime + ",DisconnectType=" + distype + ",RecPath=" + RecordingPath + ",CampaignName=" + campaignName);

                    }
                }
                else
                {
                    return ("Please disconnect the call");
                }
            }
            else
            {
                //MessageBox.Show(message, "Called window function");

                try
                {

                    string CBdatetime = "";

                    // MessageBox.Show(message, "Called window function");
                    string[] result = new string[5];
                    result = message.Split(',');
                    for (int i = 0; i < result.Length; i++)
                    {
                        string[] disp = new string[2];
                        disp = result[i].ToString().Split(':');
                        if (disp[0].ToString().ToUpper() == "DISPOSITION")
                        {
                            dispose_code = Convert.ToInt16(disp[1].ToString());
                        }
                        if (disp[0].ToString().ToUpper() == "SUBDISPOSITION")
                        {
                            subdispose_code = Convert.ToInt16(disp[1].ToString());
                        }
                        if (disp[0].ToString().ToUpper() == "CBTYPE")
                        {
                            finishCode = disp[1].ToString();
                        }
                        if (disp[0].ToString().ToUpper() == "CBTIME")
                        {
                            if (disp.Length > 2)
                            {
                                CBdatetime = disp[1].ToString() + ":" + disp[2].ToString();
                            }
                        }

                    }
                    //

                    DisposeCall(CBdatetime);

                    if (isbreak == false && CurrentStatusId == 4 && BreakStatus != "")
                    {
                        //lblbreaktype.Visibility = Visibility.Visible;
                        isclosed = true;
                        isbreak = false;

                        KeyValueCollection reasonCodes = new KeyValueCollection();
                        reasonCodes.Add("ReasonCode", BreakStatus);//check before the reasoncode is configured in CCPulseStat
                        RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                        iMessage = tServerProtocol.Request(requestAgentNotReady);
                        checkReturnedMessage(iMessage);

                        InitializeBreakShowPanel(BreakStatus);
                        CurrentStatusId = CurrBreakID;
                        TimerGetNext.Stop();
                        Timer_Autogetnext1.Stop();
                        btnBreak.Enabled = false;
                        cmdgetnext.Enabled = false;
                        ButtonDial.Enabled = false;

                        //ClearFields();
                    }
                    else if (islogout == true)
                    {
                        if (isLoggedIn)
                        { LogOut(); }

                        if (isConnectionOpen)
                        { tServerProtocol.Close(); isConnectionOpen = false; }

                        //if (receiver.IsAlive)
                        //{ receiver.Join(); }

                        Application.Exit();
                        Environment.Exit(1);
                    }
                    else
                    {
                        if (CL_AgentDetails.ProcessType == "OUTBOUND")
                        {
                            ////MyCode = GetPCB();
                            if (MyCode > 0)
                            {
                                if (campaignmode != "Preview")
                                {
                                    auto_dial();
                                }
                                Show_Data(MyCode);
                                lblcallback.Text = "Personal Callback";

                            }
                            else
                            {
                                lblcallback.Text = "";
                            }
                        }
                        Show_FP();

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
                return "";
            }

        }

        private void InitializeBreakShowPanel(string brkstatus)
        {
            //lblbreaktype.Visibility = Visibility.Visible;
            //lblbreaktype.Content = brkstatus + " Break";
        }

        private void DisposeCall(string CBdatetime)
        {
            try
            {


                if ((CL_AgentDetails.IsManual == "0" && CL_AgentDetails.ProcessType == "OUTBOUND") || (MyCode > 0))
                {
                    //if (PCBMyCode == MyCode)
                    //{
                    //    if (campaignName.Contains("PAYPOINT"))
                    //    {
                    //        SqlCommand cmd = new SqlCommand("SP_UpdateCallinglist", conobj.getconn());
                    //        cmd.CommandType = CommandType.StoredProcedure;
                    //        cmd.Parameters.AddWithValue("@DISPOSITION", dispose_code);
                    //        cmd.Parameters.AddWithValue("@subDISPOSITION", subdispose_code);
                    //        cmd.Parameters.AddWithValue("@Mycode", MyCode);
                    //        cmd.ExecuteNonQuery();
                    //    }

                    //}
                    //else
                    //{
                    if (finishCode == "PCB")
                    {
                        InsertPCB(CBdatetime);
                        KeyValueCollection kvp = new KeyValueCollection();
                        kvp.Add("Disposition", dispose_code.ToString());
                        kvp.Add("Sub_Disposition", subdispose_code.ToString());
                        kvp.Add("GSW_AGENT_REQ_TYPE", "RecordProcessed");
                        kvp.Add("GSW_APPLICATION_ID", ocsApplicationID);
                        kvp.Add("GSW_RECORD_HANDLE", recordHandle);
                        kvp.Add("GSW_RECORD_STATUS", 3);
                        CommonProperties commonProperties = CommonProperties.Create();
                        commonProperties.UserData = kvp;
                        RequestDistributeUserEvent requestDistributeUserEvent1 = RequestDistributeUserEvent.Create(extn, commonProperties);
                        int id = GenerateReferenceID();
                        requestDistributeUserEvent1.ReferenceID = id;
                        requestHash.Add(id, "RequestDistributeUserEvent");
                        iMessage = tServerProtocol.Request(requestDistributeUserEvent1);
                        System.Threading.Thread.Sleep(1000);
                        /*********************************PCB Genesis*************************/
                        //KeyValueCollection kvp = new KeyValueCollection();
                        //kvp.Add("GSW_AGENT_REQ_TYPE", "RecordReschedule");
                        //kvp.Add("GSW_APPLICATION_ID", ocsApplicationID);
                        //kvp.Add("GSW_CALLBACK_TYPE", "Personal");
                        //kvp.Add("GSW_CAMPAIGN_NAME", campaignName);
                        ////kvp.Add("GSW_DATE_TIME", "2020-09-26 14:24:45".ToString("MM/dd/yyyy HH:mm"));
                        //kvp.Add("GSW_DATE_TIME", Convert.ToDateTime(CBdatetime).ToString("MM/dd/yyyy HH:mm"));
                        //kvp.Add("GSW_RECORD_HANDLE", recordHandle);
                        //kvp.Add("Disposition", dispose_code.ToString());
                        //kvp.Add("Sub_Disposition", subdispose_code.ToString());
                        //kvp.Add("attempt", attempts + 1);
                        //kvp.Add("GSW_RECORD_STATUS", 1);

                        //CommonProperties commonProperties = CommonProperties.Create();
                        //commonProperties.UserData = kvp;
                        //RequestDistributeUserEvent requestDistributeUserEvent1 = RequestDistributeUserEvent.Create(extn, commonProperties);
                        //int id = GenerateReferenceID();
                        //requestDistributeUserEvent1.ReferenceID = id;
                        //requestHash.Add(id, "RequestDistributeUserEvent");
                        //iMessage = tServerProtocol.Request(requestDistributeUserEvent1);
                        /***********************************************************************/

                    }
                    else if (finishCode == "CCB")
                    {
                        //frameCDTime.Visibility = Visibility.Visible;
                        //DTPCallBack.Value = Convert.ToDateTime(System.DateTime.Now.ToString());
                        if (finishCode == "CCB" && campaignName != null)
                        {

                            KeyValueCollection kvp = new KeyValueCollection();
                            kvp.Add("GSW_AGENT_REQ_TYPE", "RecordReschedule");
                            kvp.Add("GSW_APPLICATION_ID", ocsApplicationID);
                            kvp.Add("GSW_CALLBACK_TYPE", "Campaign");
                            kvp.Add("GSW_CAMPAIGN_NAME", campaignName);
                            //kvp.Add("GSW_DATE_TIME", "2020-09-26 14:24:45".ToString("MM/dd/yyyy HH:mm"));
                            kvp.Add("GSW_DATE_TIME", Convert.ToDateTime(CBdatetime).ToString("MM/dd/yyyy HH:mm"));
                            kvp.Add("GSW_RECORD_HANDLE", recordHandle);
                            kvp.Add("Disposition", dispose_code.ToString());
                            kvp.Add("Sub_Disposition", subdispose_code.ToString());
                            kvp.Add("attempt", attempts + 1);
                            kvp.Add("GSW_RECORD_STATUS", 1);

                            CommonProperties commonProperties = CommonProperties.Create();
                            commonProperties.UserData = kvp;
                            RequestDistributeUserEvent requestDistributeUserEvent1 = RequestDistributeUserEvent.Create(extn, commonProperties);
                            int id = GenerateReferenceID();
                            requestDistributeUserEvent1.ReferenceID = id;
                            requestHash.Add(id, "RequestDistributeUserEvent");
                            iMessage = tServerProtocol.Request(requestDistributeUserEvent1);

                        }
                    }
                    else
                    {

                        KeyValueCollection kvp = new KeyValueCollection();
                        kvp.Add("Disposition", dispose_code.ToString());
                        kvp.Add("Sub_Disposition", subdispose_code.ToString());
                        kvp.Add("GSW_AGENT_REQ_TYPE", "RecordProcessed");
                        kvp.Add("GSW_APPLICATION_ID", ocsApplicationID);
                        kvp.Add("GSW_RECORD_HANDLE", recordHandle);
                        kvp.Add("GSW_RECORD_STATUS", 3);
                        CommonProperties commonProperties = CommonProperties.Create();
                        commonProperties.UserData = kvp;
                        RequestDistributeUserEvent requestDistributeUserEvent1 = RequestDistributeUserEvent.Create(extn, commonProperties);
                        int id = GenerateReferenceID();
                        requestDistributeUserEvent1.ReferenceID = id;
                        requestHash.Add(id, "RequestDistributeUserEvent");
                        iMessage = tServerProtocol.Request(requestDistributeUserEvent1);
                        System.Threading.Thread.Sleep(1000);
                        //checkReturnedMessage(iMessage);

                        //}
                    }

                    if (lblcallback.Text == "Personal Callback")
                    {
                        UpdateCallingListForPCB(dispose_code, subdispose_code, finishCode, CBdatetime);

                    }
                }

                ////UpdateCallingList_AsteriskLogic(dispose_code, subdispose_code);

                isOnCall = false;

                ClearFields();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dispose Call", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClareDropDawon()
        {
            CmbStatus.SelectedItem = null;
            Cmb_Category.SelectedItem = null;
            cmb_SubCategory.SelectedItem = null;
            //Cmb_Banking_Status.SelectedItem = null;
            Cmb_Disposition.SelectedItem = null;
            Cmb_SubDisposition.SelectedItem = null;
        }
        private void ClearFields()
        {

            //RecCount++;
            savecount = 0;
            txtphone.Text = "";
            PCBCallTYPE = "";
            txtbatchid.Text = "";
            txtmycode.Text = "";
            if (PCBMyCode == MyCode)
            {
                PCBMyCode = 0;
                PCBcampaignphone = "";
                PCBrecordHandle = 0;
            }
            lblcallback.Text = "";
            MyCode = 0;
            pnlConf.Visible = false;
            pnl_transfer.Visible = false;
            ConferenceWith = "";
            transnumber = "0";
            MasterPhone = "";
            campaignphone = "";
            cmbphonenos.Invoke(new ClearComboValue(this.ClearComboboxValue), new object[] { cmbphonenos });
            EntityValue = "";
            Cust_Register = false;
            HaveTicketDetail = false;
            StartTime = null;
            EndTime = null;
            //txt_CustType.Text = "";
            //commented below line
            //webBrowser1.Url = new Uri(CL_AgentDetails.iframesource_OFFICE + "?");
            webBrowserAPR.Url = new Uri("http://192.168.0.93:8088/API/AgentStatus/AgentStatus.aspx?Agentid=" + Convert.ToString(CL_AgentDetails.AgentID) + "");
            //lbl_TicketNo_Mendate.Visible = false;
            //lbl_TicketType_Mendate.Visible = false;
            //lbl_TicketStatus_Mendate.Visible = false;
            ////btn_Search.Enabled = false;
            //lblmulticategory.Text = "0";
            //lblcampaign.Text = "Not Available";
            txtconfmobile.Text = "";
            btnconfdial.Text = "Dial";
        }
        private void Show_FP()
        {

            if (MyCode > 0)
            {
                return;
            }
            if (islogout == true)
            {
                isRunning = false;
                if (isLoggedIn)
                { LogOut(); }

                if (isConnectionOpen)
                { tServerProtocol.Close(); isConnectionOpen = false; }

                if (receiver.IsAlive)
                { receiver.Join(); }

                Application.Exit();
                Environment.Exit(1);
            }
            if (callingListName != null)
            {

                //lblcalltype.Text = "Not Available";
                //  lblbreaktype.Visibility = Visibility.Hidden;
                isReady = true;
                btnLogout.Enabled = true;
                btnBreak.Enabled = true;
                btnsubmit.Enabled = false;
                isclosed = true;
                isbreak = true;
                CurrentStatusId = 1;
                //MyCode = 0;
                connID = null;
                IVRconnID = null;
                IVRConnid = null;
                APPREFNO = "";
                CUST_NAME = "";
                Agent_Ready();
                //System.Threading.Thread.Sleep(4000);
                if (campaignmode == "Preview")
                {
                    if (Convert.ToInt32(CL_AgentDetails.IdleGetNextTimer) > 0)
                    {
                        Timer_Autogetnext1.Enabled = true;
                    }
                    else
                    {
                        TimerGetNext.Start();
                    }
                }


                //}
            }
            else
            {
                //lblbreaktype.Visibility = Visibility.Hidden;
                isReady = true;
                btnLogout.Enabled = true;
                btnBreak.Enabled = true;
                btnsubmit.Enabled = false;
                isclosed = true;
                isbreak = true;

                //MyCode = 0;
                connID = null;
                IVRconnID = null;
                IVRConnid = null;
                APPREFNO = "";
                CUST_NAME = "";

                CurrentStatusId = 1;
                if (campaignmode == "Preview")
                {
                    if (Convert.ToInt32(CL_AgentDetails.IdleGetNextTimer) > 0)
                    {
                        Timer_Autogetnext1.Enabled = true;
                    }
                    else
                    {
                        TimerGetNext.Start();
                    }
                }
                Agent_Ready();
                //TimerGetNext.Start();
            }

        }

        private void btnagentready_Click(object sender, EventArgs e)
        {

            try
            {
                if (isLoggedIn == true)
                {
                    TraceService("Ready Button Click Start : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                    if (CurrentStatusId > 4 && CurrentStatusId != 10)
                    {
                        RequestAgentReady requestAgentReady = RequestAgentReady.Create(extn, AgentWorkMode.AutoIn);
                        iMessage = tServerProtocol.Request(requestAgentReady);
                        if (iMessage.Name == "EventError")
                        {
                            checkReturnedMessage(iMessage);
                        }
                        else
                        {
                            CurrentStatusId = 1;
                            checkReturnedMessage(iMessage);
                            isReady = true;
                            isbreak = true;
                            ButtonDial.Enabled = true;
                            btnLogout.Enabled = true;
                            btnBreak.Enabled = true;
                            //btnConference.IsEnabled = false;
                            //lblbreaktype.Visibility = Visibility.Hidden;
                            //if (campaignmode == "Preview" && txtmycode.Text == "")
                            //{
                            cmdgetnext.Enabled = true;
                            if (islogout == true)
                            {
                                if (isLoggedIn)
                                { LogOut(); }

                                if (isConnectionOpen)
                                { tServerProtocol.Close(); isConnectionOpen = false; }

                                //if (receiver.IsAlive)
                                //{ receiver.Join(); }

                                Application.Exit();
                                Environment.Exit(1);
                            }
                            TimerGetNext.Start();
                            //}
                        }
                    }
                    //}
                }
                TraceService("Ready Button Click Completed : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AgentReady", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogout.Enabled = true;
                return;
            }

        }


        private void LblStatus1_TextChanged(object sender, EventArgs e)
        {
            this.clockTime = 0;
            UpdateAgentStatusApi();
            if (CurrentStatusId == 1)
            {
                TimerAutoWrap.Stop();
                Boolean sstatus = Checkprocess();
                if (sstatus == true)
                {
                    //btn_Search.Enabled = true;
                }
                else
                {
                    LogOut();
                    Application.Exit();
                    Environment.Exit(1);
                }

            }
            else if (CurrentStatusId == 4)
            {
                if (AutowrapStatus == 1)
                {
                    TimerAutoWrap.Start();
                }
            }
            else
            {
                TimerAutoWrap.Stop();
            }
        }

        private void CTI_Load(object sender, EventArgs e)
        {

        }

        private void ButtonHold_Click(object sender, EventArgs e)
        {

            if (CurrentStatusId == 3)
            {
                TraceService("Hold Button Click Start : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                try
                {
                    String hold_music_path = CL_AgentDetails.HoldMusic_Path;
                    KeyValueCollection extensionData = new KeyValueCollection();
                    extensionData.Add("music", hold_music_path);
                    RequestHoldCall requestHoldCall = RequestHoldCall.Create(extn, connID, extensionData, extensionData);
                    iMessage = tServerProtocol.Request(requestHoldCall);
                    checkReturnedMessage(iMessage);
                    ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                    ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, false });
                    btnagentready.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnagentready, false });
                    CurrentStatusId = 10;
                    ////RequestHoldCall requestHoldCall = RequestHoldCall.Create(extn, connID);
                    ////iMessage = tServerProtocol.Request(requestHoldCall);
                    ////checkReturnedMessage(iMessage);
                    //////ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, false });
                    ////ButtonHangUp.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHangUp, true });
                    ////ButtonDial.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonDial, false });
                    ////btnagentready.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnagentready, false });
                    ////CurrentStatusId = 10;
                }
                catch (Exception ex)
                {
                    TraceService("Hold Button Click First Catch Error : " + campaignphone + ":" + ex.Message + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                }
                TraceService("Hold Button Click Completed : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
            }
            else if (CurrentStatusId == 10)
            {
                TraceService("UnHold Button Click Start : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                try
                {
                    RequestRetrieveCall requestRetrieveCall = RequestRetrieveCall.Create(extn, connID);
                    iMessage = tServerProtocol.Request(requestRetrieveCall);
                    checkReturnedMessage(iMessage);
                    ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, true });
                    CurrentStatusId = 3;

                }
                catch (Exception ex)
                {
                    TraceService("UnHold Button Click Catch Error : " + campaignphone + ":" + ex.Message + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                }

            }
            TraceService("UnHold Button Click Completed : " + campaignphone + ":" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
        }
        private void InsertPCB(string callbackdatetime)
        {
            try
            {

                string url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/InsertIntoPCB";
                if (CL_AgentDetails.Location == "GGN" || CL_AgentDetails.Location == "CHN")
                {
                    url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/InsertIntoPCB";
                }
                else
                {
                    url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/InsertIntoPCB";
                }
                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"Mycode\":\"" + MyCode + "\",\"Phone\":\"" + campaignphone + "\",\"Agent_Name\":\"" + AgentName + "\",\"Campaign\":\"" + lblcampaign.Text + "\",\"FTIME\":\"" + callbackdatetime + "\",\"Connid\":\"" + connID.ToString() + "\",\"Dispo_code\":\"" + dispose_code + "\",\"Subdispo_code\":\"" + subdispose_code + "\",\"ProcessName\":\"" + CL_AgentDetails.ProcessName + "\",\"Opocode\":\"" + CL_AgentDetails.OPOID + "\"}";
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
                            MessageBox.Show("Error in saving call log.");
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
                MessageBox.Show("Error while calling API - " + ex.Message, "InsertPCB");
            }
        }

        private void InsertCallLogApi()
        {
            try
            {

                string url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/InsertCallLog";
                if (CL_AgentDetails.Location == "GGN" || CL_AgentDetails.Location == "CHN")
                {
                    url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/InsertCallLog";
                }
                else
                {
                    url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/InsertCallLog";
                }


                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"Phonenumber\":\"" + campaignphone + "\",\"DialTime\":\"" + StartTime + "\",\"ConnectTime\":\"" + StartTime + "\",\"DisconnectTime\":\"" + EndTime + "\",\"DisconnectType\":\"" + distype + "\",\"AgentID\":\"" + CL_AgentDetails.OPOID + "\",\"Process\":\"" + CL_AgentDetails.ProcessName + "\",\"Location\":\"" + CL_AgentDetails.Location + "\",\"TconnID\":\"" + connID.ToString() + "\",\"CampaignName\":\"" + lblcampaign.Text + "\",\"CampaignMode\":\"" + campaignmode + "\",\"agentgroup\":\"" + AgentGroup + "\",\"VoiceFilePath\":\"" + RecordingPath.Replace("\\", "/") + "\",\"Mycode\":\"" + MyCode.ToString() + "\"}";

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
                            MessageBox.Show("Error in saving call log.");
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
                MessageBox.Show("Error while calling API - " + ex.Message, "InsertCallLogApi");
            }
        }
        private void UpdateCallingList_AsteriskLogic(int dispose_code, int subdispose_code)
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("SP_UpdateCallinglist_AsteriskLogic", conobj.getconn());
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@DISPOSITION", dispose_code);
                cmd1.Parameters.AddWithValue("@subDISPOSITION", subdispose_code);
                cmd1.Parameters.AddWithValue("@Mycode", MyCode.ToString());
                cmd1.Parameters.AddWithValue("@OPOcode", winlogin);
                cmd1.Parameters.AddWithValue("@Process", CL_AgentDetails.ProcessName);
                cmd1.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Callinglist Asterisk", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void UpdateCallingListForPCB(int dispose_code, int subdispose_code, string finishCode, string CBdatetime)
        {
            try
            {
                string url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/UpdatePassword";

                if (CL_AgentDetails.Location == "GGN" || CL_AgentDetails.Location == "CHN")
                {
                    url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/UpdatePCB";
                }
                else
                {
                    url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/UpdatePCB";
                }

                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                if (CBdatetime.ToString() == "")
                {
                    CBdatetime = "1900-01-01 00:00:00";
                }
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"Mycode\":\"" + MyCode + "\",\"DISPOSITION\":\"" + dispose_code + "\",\"Subdisposition\":\"" + subdispose_code + "\",\"Process\":\"" + CL_AgentDetails.ProcessName + "\",\"callbackTime\":\"" + CBdatetime + "\",\"FinishCode\":\"" + finishCode + "\"}";

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
                            MessageBox.Show("Error in Update Calling list.");
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
                MessageBox.Show("Error while calling API - " + ex.Message, "UpdateCallingListApi");
            }
        }
        private void UpdateAgentStatusApi()
        {
            try
            {
                string url = "";
                if (CL_AgentDetails.Location == "GGN" || CL_AgentDetails.Location == "CHN")
                {
                    url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/UpdateAgentStatus";
                }
                else
                {
                    url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/UpdateAgentStatus";
                }

                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"Opoid\":\"" + CL_AgentDetails.OPOID + "\",\"AgentStatus\":\"" + CurrentStatusId + "\"}";

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
                MessageBox.Show("Error while calling API - " + ex.Message, "UpdateAgentStatusApi");
            }
        }
        private double GetPCB()
        {
            try
            {
                string url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/Select";
                if (CL_AgentDetails.Location == "GGN" || CL_AgentDetails.Location == "CHN")
                {
                    url = "http://172.24.11.36:8088/AgentDetailAPI_GGN/API/Select";
                }
                else
                {
                    url = "http://192.168.0.93:8088/API/AgentDetailAPI_Mum/Api/Select";
                }


                var json = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    json = "{\"ProcessName\":\"" + CL_AgentDetails.ProcessName + "\",\"Opocode\":\"" + CL_AgentDetails.OPOID + "\"}";

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

                    try
                    {
                        if (result == "Failure")
                        {
                            MessageBox.Show("PCB not found.");
                            return 0;
                        }
                        else
                        {
                            dt_EntityType = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                            if (dt_EntityType.Rows.Count > 0)
                            {
                                MyCode = Convert.ToDouble(dt_EntityType.Rows[0][0]);
                                //txtmycode.Text = MyCode.ToString();
                                txtmycode.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { txtmycode, Convert.ToString(MyCode) });
                                campaignphone = Convert.ToString(dt_EntityType.Rows[0][1]);
                                //txtphone.Text = Convert.ToString(dt_EntityType.Rows[0][1]);
                                if (CL_AgentDetails.PhoneNoMaskIs == "1")
                                {
                                    txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(Convert.ToString(dt_EntityType.Rows[0][1]), @"\d(?!\d{0,3}$)", "X") });
                                }
                                else
                                {
                                    txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Convert.ToString(dt_EntityType.Rows[0][1]) });
                                }
                            }
                            return MyCode;
                            //var Disposition_distinct = (from DataRow dRow in dt_disp_full.Rows select new { col1 = dRow["Disp_Name"], col2 = dRow["DispositionCode"] }).Distinct().ToList();
                            //ListtoDataTable lsttodt_disp = new ListtoDataTable();
                            //DataTable dt_disp = lsttodt_disp.ToDataTable(Disposition_distinct);
                            //cmb_EntityType.DataSource = null;
                            //cmb_EntityType.Items.Clear();
                            //cmb_EntityType.DisplayMember = "EntityName";
                            //cmb_EntityType.ValueMember = "EntityValue";
                            //cmb_EntityType.DataSource = dt_EntityType;
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error - " + exc.Message, "");
                        return 0;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error while calling API - " + ex.Message, "GetEntityMasterApi");
                return 0;
            }
        }

        private void btn_AddAlternateNumber_Click(object sender, EventArgs e)
        {

        }

        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (btnconfdial.Text.ToString() == "Dial")
            {
                //RequestInitiateConference requestic = new RequestInitiateConference();
                if (cmbWarmtrnsRP.SelectedIndex > -1)
                {
                    string transRP = cmbWarmtrnsRP.SelectedItem.ToString();
                    string[] mm = transRP.Split(',');
                    string routepoint = mm[1].ToString().Replace(" Value = ", "").Replace(" }", "");
                    txtconfmobile.Text = routepoint;

                    String hold_music_path = CL_AgentDetails.HoldMusic_Path;
                    KeyValueCollection extensionData = new KeyValueCollection();
                    extensionData.Add("music", hold_music_path);
                    RequestHoldCall requestHoldCall = RequestHoldCall.Create(extn, connID, extensionData, extensionData);
                    iMessage = tServerProtocol.Request(requestHoldCall);
                    checkReturnedMessage(iMessage);

                    RequestInitiateConference requestic = RequestInitiateConference.Create(extn, connID, routepoint);
                    iMessage = tServerProtocol.Request(requestic);
                    btnconfdial.Text = "Conference";
                    if (iMessage != null)
                    {
                        checkReturnedMessageIVR(iMessage);
                    }
                }
                else if (!String.IsNullOrEmpty(txtconfmobile.Text))
                {
                    String hold_music_path = CL_AgentDetails.HoldMusic_Path;
                    KeyValueCollection extensionData = new KeyValueCollection();
                    extensionData.Add("music", hold_music_path);
                    RequestHoldCall requestHoldCall = RequestHoldCall.Create(extn, connID, extensionData, extensionData);
                    iMessage = tServerProtocol.Request(requestHoldCall);
                    checkReturnedMessage(iMessage);

                    RequestInitiateConference requestic = RequestInitiateConference.Create(extn, connID, Prefix + txtconfmobile.Text);
                    iMessage = tServerProtocol.Request(requestic);
                    btnconfdial.Text = "Conference";
                    if (iMessage != null)
                    {
                        checkReturnedMessageIVR(iMessage);
                    }
                }
                else
                {
                    MessageBox.Show("Please select either routepoint or enter Mobile");
                    return;
                }


            }
            else if (btnconfdial.Text.ToString() == "Conference")
            {
                RequestCompleteConference recomplete = RequestCompleteConference.Create(extn, connID, IVRconnID);
                iMessage = tServerProtocol.Request(recomplete);
                btnconfdial.Text = "Delete Party";
            }
            else if (btnconfdial.Text.ToString() == "Delete Party")
            {
                if (cmbWarmtrnsRP.SelectedIndex > -1)
                {
                    string transRP = cmbWarmtrnsRP.SelectedItem.ToString();
                    string[] mm = transRP.Split(',');
                    string routepoint = mm[1].ToString().Replace(" Value = ", "").Replace(" }", "");
                    txtconfmobile.Text = routepoint;
                    RequestReleaseCall requestreleasecall1 = RequestReleaseCall.Create(extn, connID);
                    iMessage = tServerProtocol.Request(requestreleasecall1);
                    CurrentStatusId = 4;
                    //RequestDeleteFromConference requestdeletefromconference = RequestDeleteFromConference.Create(extn, connID, Prefix + txtconfmobile.Text);
                    //iMessage = tServerProtocol.Request(requestdeletefromconference);
                }
                else
                {
                    RequestDeleteFromConference requestdeletefromconference = RequestDeleteFromConference.Create(extn, connID, Prefix + txtconfmobile.Text);
                    iMessage = tServerProtocol.Request(requestdeletefromconference);
                }

                CurrentStatusId = 3;
                btnconfdial.Text = "Dial";
            }

        }

        private void cmb_EntityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EntityValue = cmb_EntityType.SelectedText.ToString();
            //CategoryName = cmb_EntityType.SelectedText.ToString();
        }

        private void Timer_Autogetnext1_tick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CL_AgentDetails.IdleGetNextTimer) > 0)
            {
                if (Convert.ToInt32(CL_AgentDetails.IdleGetNextTimer) < counter)
                {
                    TimerGetNext.Start();
                    Timer_Autogetnext1.Enabled = false;
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }
            else if (Convert.ToInt32(CL_AgentDetails.AutoGetNextTimer) > 0)
            {
                if (Convert.ToInt32(CL_AgentDetails.AutoGetNextTimer) < counter)
                {
                    if (CurrentStatusId == 1)
                    {
                        TimerGetNext.Start();
                    }
                    Timer_Autogetnext1.Enabled = false;
                    counter = 0;
                }
                else
                {
                    counter++;
                }
            }
            else
            {
                TimerGetNext.Start();
                counter = 0;
            }

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Boolean sstatus = Checkprocess();
            if (sstatus == true)
            {
                timer1.Stop();
                LoginafterDlogin();
                if (isLoggedIn == true)
                {
                    if (isConnectionOpen == true && isRegistered == true)
                    {
                        Agent_Ready();
                    }
                    if (MyCode.ToString() == "0")
                    {
                        cmdgetnext.Enabled = false;
                        TimerGetNext.Start();
                        //TimerEnableGetNext.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Agent is not Properly logged in");
                    LogOut();
                    Application.Exit();
                    this.Close();
                    Environment.Exit(1);
                }
            }
            else
            {
                timer1.Enabled = false;
                MessageBox.Show("Agent is Logged out due to SipEndpoint is not Available");
                LogOut();
                Application.Exit();
                Environment.Exit(1);
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


        private void cmbphonenos_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Convert.ToString(cmbphonenos.SelectedItem) });
        }
        bool backCalled = false;
        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

            if (e.KeyCode == Keys.Back)
            {
                backCalled = true;
            }
            else
            {
                backCalled = false;
            }
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (backCalled)
            {
                e.Cancel = true;
                backCalled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isOnCall == true)
            {
                //txtconfmobile.Text = "";
                //btnconfdial.Text = "Dial";
                pnl_transfer.Visible = true;
            }
            else
            {
                MessageBox.Show("Kindly connect the Call first...", "Call not connected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void btn_trans_Click(object sender, EventArgs e)
        {
            if (cmbtrnsRoutepoint.SelectedIndex > -1)
            {
                string transRP = cmbtrnsRoutepoint.SelectedItem.ToString();
                string[] mm = transRP.Split(',');
                string routepoint = mm[1].ToString().Replace(" Value = ", "").Replace(" }", "");
                RequestSingleStepTransfer requestsinglesteptransfer = RequestSingleStepTransfer.Create(extn, connID, routepoint);
                iMessage = tServerProtocol.Request(requestsinglesteptransfer);
                checkReturnedMessage(iMessage);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void TraceService(string content)
        {
            //set up a filestream
            string LogPath = ConfigurationSettings.AppSettings["LogPath"].ToString();
            if (!Directory.Exists(LogPath + @"\Logs\"))
            {
                DirectoryInfo di = Directory.CreateDirectory(LogPath + @"\Logs\");
            }
            FileStream fs = new FileStream(LogPath + @"\Logs\" + "ONECRMLOG_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".txt", FileMode.OpenOrCreate, FileAccess.Write);

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);
            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);
            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream
            sw.Flush();
            //close the writer
            sw.Close();
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void btndisconnectconf_Click(object sender, EventArgs e)
        {
            if (btnconfdial.Text.ToString() == "Conference")
            {
                if (IVRconnID != null)
                {
                    RequestReleaseCall requestreleasecall1 = RequestReleaseCall.Create(extn, IVRconnID);
                    iMessage = tServerProtocol.Request(requestreleasecall1);
                    RequestRetrieveCall retrievecall = RequestRetrieveCall.Create(extn, connID);
                    iMessage = tServerProtocol.Request(retrievecall);

                    if (iMessage.Name == "EventError")
                    {
                        checkReturnedMessage(iMessage);
                    }
                    btnconfdial.Text = "Dial";
                }
                CurrentStatusId = 3;
            }
        }


        //===============================Updated Code here ============================//

        private void GetDetails()
        {
            if (!string.IsNullOrWhiteSpace(txtphone.Text))
            {

                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd10 = new SqlCommand("usp_fetchMycode", conObj.getconn57());
                    cmd10.Parameters.AddWithValue("@Registered_Mobile", txtphone.Text);
                    cmd10.CommandType = CommandType.StoredProcedure;
                    //conobj.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd10);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][1].ToString() == "")
                        {

                            //fillTextBoxes(ds);

                            BindCategory();
                            BindDisposition();
                            //BindGridView();
                            //insertcalllog();
                        }
                        else
                        {
                            //Label11.Text = ss + " Customer Not Registered";
                            //return;

                        }
                    }
                    else
                    {
                        //if (ss != null || ss != "")
                        //{
                        //   
                        //    Label11.Text = ss + " Customer Not Registered";
                        //}
                        //else
                        //{
                        //    Label11.Text = ss + " : Form Submitted";
                        //    return;
                        //}

                    }
                }
                catch (Exception ex)
                {
                    //Label11.Text = ss + " Records not found";
                    return;
                }
            }
        }

        //private void FillConnID()
        //{
        //    TxtConnid.Text = Convert.ToString(connID);
        //}



        private void BindCategory()
        {
            //Cmb_Category.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdCat = new SqlCommand("SP_category_new", conObj.getconn57());
            cmdCat.CommandType = CommandType.StoredProcedure;
            cmdCat.Parameters.AddWithValue("@Operation", "Show_Category");
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdCat);
            da.Fill(dt);
            Cmb_Category.DataSource = dt;
            Cmb_Category.DisplayMember = "category";
            Cmb_Category.ValueMember = "category";
            Cmb_Category.SelectedIndex = -1;
        }

        private void Cmb_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_Category.SelectedValue != null)
            {
                string selectedcatID = Cmb_Category.SelectedValue.ToString();
                BindSubCategory(selectedcatID);
            }
        }

        private void BindSubCategory(string selectedcatID)
        {
            //cmb_SubCategory.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdSubCat = new SqlCommand("SP_category_new", conObj.getconn57());
            cmdSubCat.CommandType = CommandType.StoredProcedure;
            cmdSubCat.Parameters.AddWithValue("@Operation", "Show_SubCategory");
            cmdSubCat.Parameters.AddWithValue("@Category", selectedcatID);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdSubCat);
            da.Fill(dt);
            cmb_SubCategory.DataSource = dt;
            cmb_SubCategory.DisplayMember = "SubCategory";
            cmb_SubCategory.ValueMember = "SubCategory";
            cmb_SubCategory.SelectedIndex = -1;
        }

        private void cmb_SubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_SubCategory.SelectedValue != null)
            {
                string selectedSubcatID = cmb_SubCategory.SelectedValue.ToString();
                //BindIssue(selectedSubcatID);
            }
        }

        private void BindIssue(string selectedSubcatID)
        {
            //Cmb_Banking_Status.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdIssue = new SqlCommand("SP_category", conObj.getconn57());
            cmdIssue.CommandType = CommandType.StoredProcedure;
            cmdIssue.Parameters.AddWithValue("@Operation", "Show_Issue");
            cmdIssue.Parameters.AddWithValue("@SubCategory", selectedSubcatID);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdIssue);
            da.Fill(dt);
            Cmb_Banking_Status.DataSource = dt;
            Cmb_Banking_Status.DisplayMember = "Issue";
            Cmb_Banking_Status.ValueMember = "Issue";
            Cmb_Banking_Status.SelectedIndex = -1;
        }
        public void BindVertical()
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdDisp234 = new SqlCommand("SP_BindVertical", conObj.getconn57());
            cmdDisp234.CommandType = CommandType.StoredProcedure;
            cmdDisp234.Parameters.AddWithValue("@Operation", "GetVertical");
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdDisp234);
            da.Fill(dt);
            comboxVertical.DataSource = dt;
            DataRow selectRow = dt.NewRow();
            selectRow["vertical"] = "--Select--";
            selectRow["vertical"] = DBNull.Value; // or you can set it to null if needed
            dt.Rows.InsertAt(selectRow, 0);
            comboxVertical.DisplayMember = "vertical";

            comboxVertical.ValueMember = "vertical";

            comboxVertical.SelectedIndex = 0;
            comboxProduct.DataSource = null;

            

        }
        private void BindDisposition()
        {
            //Cmb_Disposition.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdDisp = new SqlCommand("SP_Disposition", conObj.getconn57());
            cmdDisp.CommandType = CommandType.StoredProcedure;
            cmdDisp.Parameters.AddWithValue("@Operation", "SHOW_DISPOSITION");
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdDisp);
            da.Fill(dt);
            Cmb_Disposition.DataSource = dt;
            Cmb_Disposition.DisplayMember = "DISPOSITION";
            Cmb_Disposition.ValueMember = "DispositionCode";
            Cmb_Disposition.SelectedIndex = -1;
        }

        private void Cmb_Disposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_Disposition.SelectedIndex >= 0)
            {
                //fillSubdisposition(Cmb_Disposition.SelectedItem.ToString());
                string selectedDisposition = Cmb_Disposition.Text.ToString();
                fillSubdisposition(selectedDisposition);
            }
        }

        private void fillSubdisposition(string selectedDisposition)
        {
            //Cmb_SubDisposition.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmdSubDisp = new SqlCommand("SP_Disposition", conObj.getconn57());
            cmdSubDisp.CommandType = CommandType.StoredProcedure;
            cmdSubDisp.Parameters.AddWithValue("@Operation", "ShowSUBDisposition_INB");
            cmdSubDisp.Parameters.AddWithValue("@Disp_Name", selectedDisposition);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdSubDisp);
            da.Fill(dt);
            Cmb_SubDisposition.DataSource = dt;
            Cmb_SubDisposition.DisplayMember = "SUBDISPOSITION";
            Cmb_SubDisposition.ValueMember = "subDispositionCode";
            Cmb_SubDisposition.SelectedIndex = -1;
        }

        private void Cmb_SubDisposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cmb_SubDisposition.SelectedIndex > 0)
            {

                //fillSubSubdisposition(ddlDisposition.SelectedItem.ToString(), ddlSubDisposition.SelectedItem.ToString());
                SqlCommand cmdDisp = new SqlCommand("SP_Disposition", conObj.getconn57());
                cmdDisp.CommandType = CommandType.StoredProcedure;
                cmdDisp.Parameters.AddWithValue("@disp_name", Cmb_Disposition.Text.ToString());
                cmdDisp.Parameters.AddWithValue("@disp_code", Cmb_SubDisposition.Text.ToString());
                cmdDisp.Parameters.AddWithValue("@Operation", "SHOW_DISPCODE_INB");
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmdDisp);
                da1.Fill(dt1);
                if (dt1.Rows[0]["DISP_TYPE"].ToString() == "CCB" || dt1.Rows[0]["DISP_TYPE"].ToString() == "PCB")
                {
                    //divCallBack.Attributes.Add("class", "col-xs-6 col-md-3");
                    //divCallBack.Attributes.Add("style", "display:block");
                    label35.Visible = true;
                    Txt_dateTimePickerToDate.Visible = true;


                }
                else
                {
                    //divCallBack.Attributes.Add("style", "display:none");
                    label35.Visible = false;
                    Txt_dateTimePickerToDate.Visible = false;
                }

            }
        }

        private void Btn_Submit_Click(object sender, EventArgs e)
        {
           // DisposecallTest("6", "29");
            try
            {

                //txtPTPDate.Text = Convert.ToDateTime

                if (isOnCall == true)
                {
                    MessageBox.Show("Not Allowed While Agent On Call...", "Agent On Call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (isOnCall == true)
                {
                    MessageBox.Show("Not Allowed While Agent On Call...", "Agent On Call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (CurrentStatusId != 4)
                {
                    MessageBox.Show("Kindly dispose the call...", "Agent Not On Wrap", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (Cmb_Disposition.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Disposition Field is Required..!", "Check Disposition Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (comboxVertical.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Vertical Field is Required..!", "Check Vertical Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (comboxProduct.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Product Field is Required..!", "Check Product Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (Cmb_SubDisposition.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("SubDisposition Field is Required..!", "Check SubDisposition Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (Cmb_Category.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Category Field is Required..!", "Check Category Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (cmb_SubCategory.Text == "")
                {
                    MessageBox.Show("SubCategory Field is Required..!", "Check SubCategory Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //if (Cmb_Banking_Status.Text=="")
                //{
                //    MessageBox.Show("Banking_Status Field is Required..!", "Check Banking_Status Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
                //if (Cmb_Banking_Status.Text == null)
                //{
                //    MessageBox.Show("Banking_Status Field is Required..!", "Check Banking_Status Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}


                if (Txt_Account_Details.Text.Trim() == "")
                {
                    MessageBox.Show("Account_Details Field is Required..!", "Check field Account_Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (TxtCallerName.Text.Trim() == "")
                {
                    MessageBox.Show("CallerName Field is Required..!", "Check field CallerName", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (TxtBranchName.Text.Trim() == "")
                {
                    MessageBox.Show("BranchName Field is Required..!", "Check field BranchName", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (TxtCustConcern.Text.Trim() == "")
                {
                    MessageBox.Show("CustomerConcern Field is Required..!", "Check field CustomerConcern", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (TxtResolution.Text.Trim() == "")
                {
                    MessageBox.Show("Resolution Field is Required..!", "Check field Resolution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (CmbStatus.Text == "")
                {
                    MessageBox.Show("Status Field is Required..!", "Check Status Dropdown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                SqlCommand cmdDisp = new SqlCommand("SP_Disposition", con1);
                cmdDisp.CommandType = CommandType.StoredProcedure;
                cmdDisp.Parameters.AddWithValue("@disp_name", Cmb_Disposition.SelectedValue.ToString());
                cmdDisp.Parameters.AddWithValue("@disp_code", Cmb_SubDisposition.SelectedValue.ToString());
                cmdDisp.Parameters.AddWithValue("@Operation", "SHOW_DISPCODE_INB");
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmdDisp);
                da1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {

                    //finishCode = dt1.Rows[0]["DISP_TYPE"].ToString();
                    finishCode = dt1.Rows[0]["DISP_TYPE"].ToString();


                }



                if (finishCode == "CCB" || finishCode == "PCB")
                {
                    if (Txt_dateTimePickerToDate.Text == "")
                    {
                        MessageBox.Show("CallBackDataTime Field is Required..!", "Check field CallBackDataTime", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {

                        try
                        {

                            DateTime selectedDateTime = Txt_dateTimePickerToDate.Value;
                            //ViewState["CallBackDateTime"] = Convert.ToDateTime(Txt_dateTimePickerToDate.Text).ToString("yyyy-MM-dd HH:mm");

                        }
                        catch (Exception)
                        {


                            string str = "Invalid CALL_BACK date is selected...";
                            string script = "alert('" + str + "')";

                            MessageBox.Show("CallerName Field is Required..!", script, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Btn_Submit, this.GetType(), "Message", script, true);
                            Txt_dateTimePickerToDate.Text = "";

                            return;

                        }


                    }

                }
                else
                {
                    //ViewState["CallBackDateTime"] = "";
                }


                DisposecallTest(Cmb_Disposition.SelectedValue.ToString(), Cmb_SubDisposition.SelectedValue.ToString());
            }
            catch (Exception ex)
            {


            }
        }


        private void DisposecallTest(string Disposition, string subDisposition)
        {
            try
            {

                SqlConnection concmd = new SqlConnection("Data Source=192.168.0.57; Initial Catalog=Unity_Bank_INB; Uid=opodba; Password=opo@1234");
                SqlCommand cmddisphist = new SqlCommand("USP_InsertHistory", concmd);

                cmddisphist.CommandType = CommandType.StoredProcedure;
                cmddisphist.Parameters.AddWithValue("@MyCode", MyCode);

                cmddisphist.Parameters.AddWithValue("@Disposition", Disposition);//Convert.ToInt16(ddlDisposition.SelectedValue.ToString()));
                cmddisphist.Parameters.AddWithValue("@SubDisposition", subDisposition);//Convert.ToInt16(ddlSubDisposition.SelectedValue.ToString()));
                //cmd.Parameters.AddWithValue("@SubSubDisposition", subsubDisposition);//Convert.ToInt16(ddlSubSubDisposition.SelectedValue.ToString()));

                cmddisphist.Parameters.AddWithValue("@EmployeID", winlogin);
                cmddisphist.Parameters.AddWithValue("@Connid", Convert.ToString(connID));//Session["connid"]);//ViewState["connid"]
                //cmd.Parameters.AddWithValue("@Phone", Convert.ToString(phonenumber));
                cmddisphist.Parameters.AddWithValue("@Phone", campaignphone);

                cmddisphist.Parameters.AddWithValue("@REMARKS", Txt_Account_Details.Text);
                //cmd.Parameters.AddWithValue("@CALLBACKDATE", Convert.ToDateTime(Txt_dateTimePickerToDate.Text).ToString("yyyy-MM-dd HH:mm"));
                cmddisphist.Parameters.AddWithValue("@CALLBACKDATE", "");

                ////////////Additional for Recording entry///////////////
                cmddisphist.Parameters.AddWithValue("@ConnectTime", StartTime);
                cmddisphist.Parameters.AddWithValue("@DisconnectType", distype);
                cmddisphist.Parameters.AddWithValue("@DisconnectTime", EndTime);
                cmddisphist.Parameters.AddWithValue("@RecPath", RecordingPath);
                cmddisphist.Parameters.AddWithValue("@CAMPAIGNNAME", campaignName);

                cmddisphist.Parameters.AddWithValue("@Category", Cmb_Category.Text.ToString());
                cmddisphist.Parameters.AddWithValue("@SubCategory", cmb_SubCategory.Text.ToString());
                cmddisphist.Parameters.AddWithValue("@Issue", "");

                cmddisphist.Parameters.AddWithValue("@CallerName", TxtCallerName.Text.ToString());
                cmddisphist.Parameters.AddWithValue("@BranchName", TxtBranchName.Text.ToString());
                cmddisphist.Parameters.AddWithValue("@CustomerConcern", TxtCustConcern.Text.ToString());
                cmddisphist.Parameters.AddWithValue("@Resolution", TxtResolution.Text.ToString());
                cmddisphist.Parameters.AddWithValue("@Status", CmbStatus.Text.ToString());

                cmddisphist.Parameters.AddWithValue("@comboxVertical", Convert.ToString(comboxVertical.SelectedValue));
                cmddisphist.Parameters.AddWithValue("@comboxProduct", Convert.ToString(comboxProduct.SelectedValue));
                cmddisphist.Parameters.AddWithValue("@txtSecured_Unsecured", Convert.ToString(txtSecured_Unsecured.Text));
                cmddisphist.Parameters.AddWithValue("@txtOrganic_Inorganic", Convert.ToString(txtOrganic_Inorganic.Text));
                cmddisphist.Parameters.AddWithValue("@txtAssets_Liabilities", Convert.ToString(txtAssets_Liabilities.Text));

                /////////////////////////////////


                //if (con1.State != System.Data.ConnectionState.Open)
                //{
                //    con1.Open();
                //}
                concmd.Open();
                cmddisphist.ExecuteNonQuery();
                concmd.Close();
                //if (con1.State != System.Data.ConnectionState.Closed)
                //{
                //    con1.Close();
                //}

                //string CBDate = Convert.ToString(Txt_dateTimePickerToDate);

                string str = "Record Saved Successfully...,Disposition:" + Disposition + ",SubDisposition:" + subDisposition + ",CBTime:" + "" + ",CBType:" + finishCode;

                string script = "alert('" + str + "')";
                savedata(str);
                //MessageBox.Show(str , "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //if (ViewState["Status"].ToString() == "2" || ViewState["Status"].ToString() == "3")
                //{
                //    script = "CanSave('" + str + "')";
                //}


                // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Btn_Submit, this.GetType(), "Message", script, true);




            }
            catch (Exception exc)
            {


                string str = "Record not Saved. Error occurred: " + exc.Message.ToString();

                string script = "alert('" + str + "')";
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(Btn_Submit, this.GetType(), "Message", script, true);
                return;

            }

            //  if (Conn57.State != ConnectionState.Closed)
            // {
            //// Conn57.Close();
            // }
            clearfields();

            //TraceService("End of LeadUpdate : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

        }

        private void clearfields()
        {
            comboxVertical.Text = "";
            comboxProduct.Text = "";
            Cmb_Disposition.Text = "";
            Cmb_SubDisposition.Text = "";
            Cmb_Category.Text = "";
            cmb_SubCategory.Text = "";
            //Cmb_Banking_Status.Text = "";
            CmbStatus.Text = "";
            Txt_Account_Details.Text = "";
            TxtCallerName.Text = "";
            TxtBranchName.Text = "";
            TxtCustConcern.Text = "";
            TxtResolution.Text = "";
            TxtConnid.Text = "";
            Cmb_Category.SelectedIndex = -1;
            cmb_SubCategory.SelectedIndex = -1;
            //Cmb_Banking_Status.SelectedIndex = -1;
            Cmb_Disposition.SelectedIndex = -1;
            Cmb_SubDisposition.SelectedIndex = -1;
            CmbStatus.SelectedIndex = -1;
            Cmb_SearchType.SelectedIndex = -1;


        }

        private void Btn_SearchByValue_Click(object sender, EventArgs e)
        {
            DataSet dt = new DataSet();
            try
            {

                SqlConnection con1 = new SqlConnection("server=192.168.0.57;database=Unity_Bank_INB;uid=opodba;password=opo@1234");
                con1.Open();

                SqlCommand cmd = new SqlCommand("SEARCH_DETAILS_UNITY_BANK", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                if (Cmb_SearchType.SelectedItem.ToString() == "CUSTOMER_ID")
                {
                    cmd.Parameters.AddWithValue("@CUSTOMER_ID", Txt_SearchByValue.Text);
                    cmd.Parameters.AddWithValue("@Operation", "CUSTOMER_ID");
                }

                if (Cmb_SearchType.SelectedItem.ToString() == "REGISTERED_MOBILE")
                {
                    cmd.Parameters.AddWithValue("@Registered_Mobile", Txt_SearchByValue.Text);
                    cmd.Parameters.AddWithValue("@Operation", "REGISTERED_MOBILE");
                }

                if (Cmb_SearchType.SelectedItem.ToString() == "CUSTOMER_ACCOUNT")
                {
                    cmd.Parameters.AddWithValue("@DENSE_SI", Txt_SearchByValue.Text);
                    cmd.Parameters.AddWithValue("@Operation", "CUSTOMER_ACCOUNT");
                }
                // cmd.Parameters.AddWithValue("@DENSE_SI", TxtCusId.Text);
                // SqlDataReader dr = cmd.ExecuteReader();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);

                ad.Fill(dt);


                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dt.Tables[0];


                con1.Close();



                //if (ViewState["dt"] = null)
                //{
                //    gvDetails.DataSource = null;
                //}
            }
            catch (Exception ex)
            {

            }
        }


        //TraceService("End of LeadUpdate : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (row != null)
                {

                    Type = row.Cells[3].Value.ToString();
                    DENSE_SI = row.Cells[4].Value.ToString();

                }

            }

            AddRowToTargetGridView();
        }




        private void AddRowToTargetGridView()
        {
            DataSet dt = new DataSet();
            try
            {

                SqlConnection con1 = new SqlConnection("server=192.168.0.57;database=Unity_Bank_INB;uid=opodba;password=opo@1234");
                con1.Open();

                SqlCommand cmd = new SqlCommand("View_DENSE_SI_Details", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DENSE_SI", DENSE_SI);
                cmd.Parameters.AddWithValue("@Type", Type);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);

                ad.Fill(dt);

                dataGridView2.DataSource = dt.Tables[0];

                con1.Close();

            }
            catch (Exception ex)
            {

            }
        }

        private void Btn_ShowHistory_Click(object sender, EventArgs e)
        {
            DataSet dt = new DataSet();
            try
            {

                SqlConnection con1 = new SqlConnection("server=192.168.0.166;database=Unity_Bank_INB;uid=opodba;password=opo@1234");
                con1.Open();

                SqlCommand cmd = new SqlCommand("SP_Show_History", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Registered_Mobile", txtphone.Text);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                // Bind the DataTable to the DataGridView
                dataGridView3.DataSource = dt.Tables[0];
                con1.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void tabInfo_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxVertical.SelectedIndex > 0)
            {
                SqlCommand cmdDisp12 = new SqlCommand("SP_BindVertical", conObj.getconn57());
                cmdDisp12.CommandType = CommandType.StoredProcedure;
                cmdDisp12.Parameters.AddWithValue("@Operation", "GetProduct");
                cmdDisp12.Parameters.AddWithValue("@Vertical", comboxVertical.SelectedValue.ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmdDisp12);
                da.Fill(dt);
                DataRow selectRow = dt.NewRow();
                selectRow["product"] = "--Select--";
                selectRow["product"] = DBNull.Value; // or you can set it to null if needed
                dt.Rows.InsertAt(selectRow, 0);
                comboxProduct.DataSource = dt;

                comboxProduct.DisplayMember = "product";
                comboxProduct.ValueMember = "product";
                comboxProduct.SelectedIndex = 0;
            }
            else
            {
                comboxProduct.DataSource = null;
            }
            txtSecured_Unsecured.Text = "";
            txtOrganic_Inorganic.Text = "";
            txtAssets_Liabilities.Text = "";

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void comboxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboxVertical.SelectedIndex > 0) && (comboxProduct.SelectedIndex > 0))
            {
                SqlCommand cmdDisp1212 = new SqlCommand("SP_BindVertical", conObj.getconn57());
                cmdDisp1212.CommandType = CommandType.StoredProcedure;
                cmdDisp1212.Parameters.AddWithValue("@Operation", "GETALLField");
                cmdDisp1212.Parameters.AddWithValue("@Vertical", comboxVertical.SelectedValue.ToString());
                cmdDisp1212.Parameters.AddWithValue("@product", comboxProduct.SelectedValue.ToString());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmdDisp1212);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    // Assign values to text boxes based on column names
                    txtSecured_Unsecured.Text = row["Secured_Unsecured"].ToString();
                    txtOrganic_Inorganic.Text = row["Organic_Inorganic"].ToString();
                    txtAssets_Liabilities.Text = row["Assets_or_Liabilities"].ToString();
                }

            }
            else
            {
                txtSecured_Unsecured.Text = "";
                txtOrganic_Inorganic.Text = "";
                txtAssets_Liabilities.Text = "";
            }


        }

        private void txtSecured_Unsecured_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void teTxt_Account_DetailsxtBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtResolution_TextChanged(object sender, EventArgs e)
        {

        }

        private void Txt_Account_Details_TextChanged(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            GetCustomers(mobileNo.Text);

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        public async void GetCustomers(string mobileNo,int currentCustPageNo = 0,bool isPreviousOrNextButtonCall = false,bool accountDetailsGridViewVisible =false)
        {
            try
            {
                //_logger.LogInformation($"Calling API_URL:{GetCustomerByCustomerId} Resquest Time:{DateTime.Now.ToString()}");
                

                if (accountDetailsGridViewVisible && isPreviousOrNextButtonCall)
                {
                    disableOrEnableAccountDetailsGridewithFeatures(true);
                }
                else
                {
                    disableOrEnableAccountDetailsGridewithFeatures(false);
                }

                CustomerdetailsdataGridView.Visible = true;
                AccountDetailsPanel.Visible = false;
                TransactionDetailsPanel.Visible = false;
                customerDetailstitlelabel.Visible = true;
                PreviousCustButton.Visible = true;
                NextCustButton.Visible = true;
                CustomerDetailsPageInfo.Visible = true;
                //customerId = string.Empty;
                accountNo = string.Empty;
                int count = 10;
                int totalEntryCount = 0;
                var resultdata = new List<CustomerData>();
                var requestData = new { mobileNumber = mobileNo.Trim() };
                string jsonrequestData = JsonConvert.SerializeObject(requestData);
                TraceService($"Calling API_URL:{CUST_BY_MOBILENO_API_URL}  Resquest Data:{jsonrequestData}, Resquest Time:{DateTime.Now.ToString()}");
                var ApiResponse = await  GetResponseFromPostURL(CUST_BY_MOBILENO_API_URL, jsonrequestData);
                var data = JsonConvert.DeserializeObject<CustomerListByMobileResponse>(ApiResponse);
                
                TraceService($"Calling API_URL:{CUST_BY_MOBILENO_API_URL},Response Status Code:{data?.response_code}, Response Time:{DateTime.Now.ToString()}");
                
                if (CustomerdetailsdataGridView.DataSource != null)
                    CustomerdetailsdataGridView.DataSource = null;

                if (data?.response_message == "Success")
                {
                        CustomerdetailsdataGridView.AutoGenerateColumns = false;

                        if (CustomerdetailsdataGridView.Columns.Count == 0)
                        {
                            CustomerdetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "customerId", HeaderText = "CustomerId"});
                            CustomerdetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "fullName", HeaderText = "Name" });
                        }
                        resultdata = data?.results.Skip(count * currentPageNo).Take(count).OrderBy(x => x.customerId).ToList();
                        CustomerdetailsdataGridView.DataSource = resultdata;

                    //return PartialView("CustomerList", data?.results.ToList());
                }
                else
                {
                    CustomerdetailsdataGridView.DataSource = resultdata;
                    //return PartialView("CustomerList", new List<CustomerData>());
                    PreviousCustButton.Visible = false;
                    NextCustButton.Visible = false;
                    CustomerDetailsPageInfo.Visible = false;
                }
                
                if(resultdata.Count == 0 && isPreviousOrNextButtonCall == false)
                {
                    PreviousCustButton.Visible = false;
                    NextCustButton.Visible = false;
                    CustomerDetailsPageInfo.Visible = false;
                }

                
                totalEntryCount = (int)data?.results.ToList().Count;
                totalCustPageSize = (int)(totalEntryCount / count);
                
                int pageFirstRowNo = ((count * currentCustPageNo) + 1);
                int pageLastRowNo = ((count * currentCustPageNo) + resultdata.Count);
                CustomerDetailsPageInfo.Text = "Showing " + pageFirstRowNo + " to " + pageLastRowNo + " of " + totalEntryCount + " entries";

                CustomerdetailsdataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                adjustDataGridView(CustomerdetailsdataGridView);
                //adjustDataGridViewWithoutScroll(CustomerdetailsdataGridView);
                addlabeltogridview(CustomerdetailsdataGridView);
                

            }
            catch (Exception ex)
            {
                TraceService($"Error While calling API_URL:{CUST_BY_MOBILENO_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                //return View("CustomerList", new List<CustomerData>());
                PreviousCustButton.Visible = false;
                NextCustButton.Visible = false;
                CustomerDetailsPageInfo.Visible = false;
                //CustomerdetailsdataGridView.DataSource = new List<CustomerData>();
            }

        }

        public void disableOrEnableAccountDetailsGridewithFeatures(bool visible)
        {
            accountDetailsGridView.Visible = visible;
            mappedAccountListtitlelabel.Visible = visible;
            searchlbl.Visible = visible;
            Searchtxt.Visible = visible;
            button3.Visible = visible;
            Previousbtn.Visible = visible;
            Nextbtn.Visible = visible;
            label51.Visible = visible;
        }
        private void adjustDataGridViewWithoutScroll(DataGridView dataGridView)
        {
            int totalWidth = dataGridView.RowHeadersWidth;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                totalWidth += column.Width;
            }
            int totalHeight = dataGridView.ColumnHeadersHeight;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                totalHeight += row.Height;
            }
            totalWidth += 2;
            totalHeight += 2;

            dataGridView.Size = new Size(totalWidth, totalHeight);
        }
        private void adjustDataGridView(DataGridView dataGridView)
        {
            //foreach (DataGridViewColumn column in dataGridView.Columns)
            //{
            //    column.Width = 100;
            //}
            int totalWidth = 0;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                totalWidth += column.Width + 2;
            }
            dataGridView.Width = totalWidth + dataGridView.RowHeadersWidth + 15;

            //dataGridView.Width = 1173;

        }

        private void addlabeltogridview(DataGridView dataGridView)
        {
            CustomerDetailsGridTitle = new Label
            {
                Text = "Customer Details",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            dataGridView = new DataGridView { Location = new Point(10, 40) };
            this.Controls.Add(CustomerDetailsGridTitle);
            this.Controls.Add(dataGridView);
        }

        public void sortdatagridview(DataGridView dataGridView,int columnCount)
        {
            for (int i=0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }
        //public void sortdatagridview(DataGridView dataGridView, int columnCount)
        //{
        //    for (int i = 0; i < columnCount; i++)
        //    {
        //        dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
        //    }
        //}
        public async void DisplayAccountDetails(string customerId,int currentPageNo,bool accountDetailsPanelvisible = false,/*string columnName = null,*/string columnValue = null)
        {

            try
            {
                
                int totalEntryCount = 0; 
                // ViewBag.customerId = customerId;
                
                accountDetailsGridView.Visible = true;
                mappedAccountListtitlelabel.Visible = true;
                searchlbl.Visible = true;
                Searchtxt.Visible = true;
                AccountDetailsPanel.Visible = accountDetailsPanelvisible;
                button3.Visible = true;

                TransactionDetailsPanel.Visible = false;
                accountNo = string.Empty;
                var requestData = new { customerId = customerId.Trim() };
                string jsonrequestData = JsonConvert.SerializeObject(requestData);
                TraceService($"Calling API_URL:{ACCOUNTLIST_BY_CUSTID_API_URL}, Resquest Data:{jsonrequestData}, Resquest Time:{DateTime.Now.ToString()}");
                var ApiResponse = await GetResponseFromPostURL(ACCOUNTLIST_BY_CUSTID_API_URL, jsonrequestData);
                var data = JsonConvert.DeserializeObject<AccountResponse>(ApiResponse);
                TraceService($"Calling API_URL:{ACCOUNTLIST_BY_CUSTID_API_URL},Response Status Code:{data?.response_code},Response Time:{DateTime.Now.ToString()}");
                
                if (accountDetailsGridView.Columns.Count == 0)
                {
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountNumber", HeaderText = "Account Number" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountName", HeaderText = "Account Name" });
                    //accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountId", HeaderText = "Account Id" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountClassification", HeaderText = "Account Type" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "customerType", HeaderText = "Customer Type" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountStatus", HeaderText = "Account Status" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "customerName", HeaderText = "Customer Name" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "customerId", HeaderText = "Customer Id" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountOpenDate", HeaderText = "Account Open Date" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountCloseDate", HeaderText = "Account Close Date" });
                    accountDetailsGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "accountFreezeStatus", HeaderText = "Freeze Status" });
                }

                if (data?.response_message == "Success")
                {
                    if (data?.results.ToList().Count > 0)
                    {
                        accountDetailsGridView.AutoGenerateColumns = false;
                        Previousbtn.Visible = true;
                        Nextbtn.Visible = true;
                        label51.Visible = true;
                        var resultdata = new List<AccountDetails>();
                        
                        if (accountDetailsGridView.DataSource != null)
                                    accountDetailsGridView.DataSource = null;

                        //var resultdata = data?.results.Skip(count * currentPageNo).Take(count).ToList();

                        if (string.IsNullOrEmpty(columnValue))
                        {
                            resultdata = data?.results.Skip(count * currentPageNo).Take(count).ToList();
                            totalEntryCount = (int)data?.results.ToList().Count;
                            totalPageSize = (int)(totalEntryCount / count);
                        } 
                        else
                        {
                            columnValue = columnValue.Trim();
                            var totalData = data?.results.Where(x => x.accountNumber.Contains(columnValue) || x.accountName.Contains(columnValue)
                            || x.accountClassification.Contains(columnValue) || x.customerType.Contains(columnValue)
                            || x.accountStatus.Contains(columnValue) || x.customerName.Contains(columnValue)
                            || x.customerId.Contains(columnValue) || x.accountOpenDate.Contains(columnValue)
                            || x.accountCloseDate.Contains(columnValue) || x.accountFreezeStatus.Contains(columnValue)
                            ).ToList();
                            resultdata = totalData.Skip(count * currentPageNo).Take(count).ToList();
                            totalEntryCount = totalData.Count;
                            totalPageSize = (int)(totalEntryCount / count);
                        }
                            accountDetailsGridView.DataSource = resultdata.OrderBy(x=>x.accountNumber).ToList();
                            int pageFirstRowNo = ((count * currentPageNo) + 1);
                            int pageLastRowNo = ((count * currentPageNo) + resultdata.Count);
                            label51.Text = "Showing " + pageFirstRowNo + " to " + pageLastRowNo + " of "+ totalEntryCount + " entries";
                    }
                    else
                    {
                        VisibilityOfDisplayAccountGridView(false);
                    }
                        
                        adjustDataGridView(accountDetailsGridView);
                        sortdatagridview(accountDetailsGridView,14);
                    //return View("AccountList", data?.results.ToList());

                }
                else
                {
                    accountDetailsGridView.DataSource = data?.results.ToList();
                    //return View("AccountList", data?.results.ToList());
                    VisibilityOfDisplayAccountGridView(false);

                }
                accountDetailsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //adjustDataGridViewWithoutScroll(accountDetailsGridView);
                addlabeltogridview(accountDetailsGridView);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While calling API_URL:{ACCOUNTLIST_BY_CUSTID_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                //return View("AccountList", new List<AccountDetails>());
                VisibilityOfDisplayAccountGridView(false);

            }

        }
        private void VisibilityOfDisplayAccountGridView(bool visibility)
        {
            searchlbl.Visible = visibility;
            Searchtxt.Visible = visibility;
            label51.Visible = visibility;
            Previousbtn.Visible = visibility;
            Nextbtn.Visible = visibility;
            button3.Visible = visibility;
        }

        public async void TransactionView(string accountNo)
        {
            try
            {
                AccountDetailsPanel.Visible = true;
                TransactionDetailsPanel.Visible = false;
                TraceService($"Calling API_URL:{ACC_DETAILS_FOR_CHANNEL_API_URL},Resquest Data:{accountNo}, Resquest Time:{DateTime.Now.ToString()}");
                var accountDetails = await GetAccountDetailsForChannel(accountNo);
                if (accountDetails != null)
                {
                    TraceService($"Calling API_URL:{ACC_DETAILS_FOR_CHANNEL_API_URL},Response Status Code:{accountDetails.response_code}, Response Time:{DateTime.Now.ToString()}");
                    Product_code.Text = accountDetails?.results?.Select(x => x.productCode).FirstOrDefault() == null ? "" : accountDetails?.results.Select(x => x.productCode).FirstOrDefault().ToString();
                    Product_name.Text = accountDetails?.results?.Select(x => x.productName).FirstOrDefault() == null ? "" : accountDetails?.results.Select(x => x.productName).FirstOrDefault();
                    Account_Balance.Text = accountDetails?.results?.Select(x => x.availableBalance).FirstOrDefault() == null ? "" : accountDetails?.results.Select(x => x.availableBalance).FirstOrDefault().ToString();
                    Home_brn.Text = accountDetails?.results?.Select(x => x.branchName).FirstOrDefault() == null ? "" : accountDetails?.results.Select(x => x.branchName).FirstOrDefault();
                    Email_id.Text = accountDetails?.results?.Select(x => x.emailId).FirstOrDefault() == null ? "" : accountDetails?.results.Select(x => x.emailId).FirstOrDefault();
                }
                //return PartialView("Transaction");
            }
            catch (Exception ex)
            {
                TraceService($"Error While calling API_URL:{CUSTOMERDETAILS_BY_CUSTOMERID_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                //return PartialView("Transaction");
            }

        }

        public async Task<AccountDetailChannelResponse> GetAccountDetailsForChannel(string accountNo)
        {
            AccountDetailChannelResponse data;
            try
            {

                
                var requestData = new { accountNo = accountNo };
                TraceService($"Calling API_URL:{ACC_DETAILS_FOR_CHANNEL_API_URL},Resquest Data:{requestData}, Resquest Time:{DateTime.Now.ToString()}");
                var ApiResponse = await GetResponseFromGetURL(ACC_DETAILS_FOR_CHANNEL_API_URL, requestData);
                data = JsonConvert.DeserializeObject<AccountDetailChannelResponse>(ApiResponse);
                TraceService($"Calling API_URL:{ACC_DETAILS_FOR_CHANNEL_API_URL},Response Status Code:{data?.response_code}, Response Time:{DateTime.Now.ToString()}");
                return data;
            }
            catch (Exception ex)
            {
                TraceService($"Error While calling API_URL:{CUSTOMERDETAILS_BY_CUSTOMERID_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                return null;
            }

        }

        public async void GetTransactions(string accountNo, string customerId/*, string fromDate, string toDate*/)
        {
            try
            {
                string fromDate = "1900-01-01";
                string toDate = "1900-01-01";
                TransactionDetailsPanel.Visible = true;
                var dataList = new List<TransactionDetails>();
                
                var requestData = new { accountNumber = accountNo, fromDate = fromDate, toDate = toDate };

                string jsonrequestData = JsonConvert.SerializeObject(requestData);
                
                TraceService($"Calling API_URL:{MINISTATEMENT_API_URL},Resquest Data:{jsonrequestData}, Resquest Time:{DateTime.Now.ToString()}");
                var ApiResponse = await GetResponseFromPostURL(MINISTATEMENT_API_URL, jsonrequestData);
                var data = JsonConvert.DeserializeObject<MiniStatementResponse>(ApiResponse);
                TraceService($"Calling API_URL:{MINISTATEMENT_API_URL},Response Status Code:{data?.response_code}, Response Time:{DateTime.Now.ToString()}");

                TraceService($"Calling API_URL:{"GetCustomerByCustomerId"} Resquest Time:{DateTime.Now.ToString()}");
                var customerDetails = await GetCustomerByCustomerId(customerId);
                TraceService($"Calling API_URL:{"GetCustomerByCustomerId"} Response Time:{DateTime.Now.ToString()}");

                string MailingAddress1 = customerDetails?.results?.Select(x => x.mailingAddress.address1).FirstOrDefault() == null ? "" : customerDetails?.results.Select(x => x.mailingAddress.address1).FirstOrDefault();
                string MailingAddress2 = customerDetails?.results?.Select(x => x.mailingAddress.address2).FirstOrDefault() == null ? "" : customerDetails?.results.Select(x => x.mailingAddress.address2).FirstOrDefault();
                string MailingAddress3 = customerDetails?.results?.Select(x => x.mailingAddress.address3).FirstOrDefault() == null ? "" : customerDetails?.results.Select(x => x.mailingAddress.address3).FirstOrDefault();
                string Address1 = customerDetails?.results?.Select(x => x.permenantAddress.address1).FirstOrDefault() == null ? "" : customerDetails?.results.Select(x => x.permenantAddress.address1).FirstOrDefault();
                string Address2 = customerDetails?.results?.Select(x => x.permenantAddress.address2).FirstOrDefault() == null ? "" : customerDetails?.results.Select(x => x.permenantAddress.address2).FirstOrDefault();
                string Address3 = customerDetails?.results?.Select(x => x.permenantAddress.address3).FirstOrDefault() == null ? "" : customerDetails?.results.Select(x => x.permenantAddress.address3).FirstOrDefault();

                MailingAddressRichTextBox.Text = MailingAddress1 + MailingAddress2 + MailingAddress3;
                PermanentAddressRichTextBox.Text = Address1 + Address2 + Address3;

                if (data?.response_message == "Success")
                {
                    if (data?.results.Count > 0)
                    {
                        //var last10Transactions = data?.results.Take(10).ToList();
                        TransactionDetailsdataGridView.AutoGenerateColumns = false;
                        if (TransactionDetailsdataGridView.Columns.Count == 0)
                        {
                            List<DataGridViewModel> dataGridViewModels = new List<DataGridViewModel>();
                           
                            TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "txnDate", HeaderText = "Date" });
                            TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "txnAmount", HeaderText = "Amount" });
                            TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "txnNature", HeaderText = "Dr/Cr" });
                            TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "instrNo", HeaderText = "InstrumentNo" });
                            TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "narration", HeaderText = "Description" });
                        }
                        if (TransactionDetailsdataGridView.DataSource != null)
                        {
                            TransactionDetailsdataGridView.DataSource = null;
                            dataList = data?.results.Take(10).OrderByDescending(obj => obj.txnDate == null ? DateTime.MinValue : DateTime.Parse(obj.txnDate)).ToList();
                            //var dataList = dataList.ToList().ForEach(x=);
                            //var dataList = data?.results.Take(10).OrderByDescending(obj => obj.txnDate == null ? DateTime.MinValue : DateTime.Parse(obj.txnDate)).ToList();
                            dataList.ForEach((x) => { x.txnDate = x.txnDate +" "+ x.txnTime; });
                            TransactionDetailsdataGridView.DataSource = dataList;


                           // TransactionDetailsdataGridView.DataSource = data?.results.Take(10).OrderByDescending(obj => obj.txnDate == null ? DateTime.MinValue : DateTime.Parse(obj.txnDate)).ToList();
                           
                        }
                        else
                        {
                            dataList = data?.results.Take(10).OrderByDescending(obj => obj.txnDate == null ? DateTime.MinValue : DateTime.Parse(obj.txnDate)).ToList();
                            dataList.ForEach((x) => { x.txnDate = x.txnDate +" "+x.txnTime; });
                            TransactionDetailsdataGridView.DataSource = dataList;
                            //TransactionDetailsdataGridView.DataSource = data?.results.Take(10).OrderByDescending(obj => obj.txnDate == null ? DateTime.MinValue : DateTime.Parse(obj.txnDate)).ToList();
                          
                        }
                        TransactionDetailsdataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        //adjustDataGridViewWithoutScroll(TransactionDetailsdataGridView);
                        adjustDataGridView(TransactionDetailsdataGridView);
                        //adjustDataGridViewWithoutScroll(TransactionDetailsdataGridView);
                    }
                    //return PartialView("TransactionList", new List<TransactionDetails>());
                }
                else
                {
                    TransactionDetailsdataGridView.DataSource = dataList;
                }
            }
            catch (Exception ex)
            {
                TraceService($"Error While calling API_URL:{MINISTATEMENT_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                
            }

        }
        public async Task<CustomerDetailsResponse> GetCustomerByCustomerId(string customerId)
        {
            CustomerDetailsResponse data;
            try
            {

                
                var requestData = new { customerId = customerId };
                TraceService($"Calling API_URL:{CUSTOMERDETAILS_BY_CUSTOMERID_API_URL},Resquest Data:{requestData}, Resquest Time:{DateTime.Now.ToString()}");
                var ApiResponse = await GetResponseFromGetURL(CUSTOMERDETAILS_BY_CUSTOMERID_API_URL, requestData);
                data = JsonConvert.DeserializeObject<CustomerDetailsResponse>(ApiResponse);
                TraceService($"Calling API_URL:{CUSTOMERDETAILS_BY_CUSTOMERID_API_URL},Response Status Code:{data?.response_code}, Response Time:{DateTime.Now.ToString()}");
                return data;
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error While calling API_URL:{_appIdentitySettings.Api.CUSTOMERDETAILS_BY_CUSTOMERID_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                return null;
            }

        }

        //public void CrmProductIndex()
        //{
        //    List<TblCategory> tblCategories = new List<TblCategory>();
        //    List<VerticalProduct> verticalProducts = new List<VerticalProduct>();
        //    try
        //    {
        //        string queryString1 = "SELECT CATID, SUBCATID, ISSUEID,Category, SubCategory,Issue,isActive FROM TBL_Category";
        //        string queryString2 = "SELECT id, Vertical, Product, Secured_Unsecured,Organic_Inorganic,Assets_or_Liabilities FROM Tbl_VerticalProduct_List";


        //        using (SqlConnection connection = new SqlConnection(ConnectinString))
        //        {
        //            try
        //            {
        //                connection.Open();
        //                using (SqlCommand firstCommand = new SqlCommand(queryString1, connection))
        //                {
        //                    using (SqlDataReader reader1 = firstCommand.ExecuteReader())
        //                    {
        //                        while (reader1.Read())
        //                        {
        //                            tblCategories.Add(new TblCategory()
        //                            {
        //                                CATID = reader1["CATID"].ToString(),
        //                                SUBCATID = reader1["SUBCATID"].ToString(),
        //                                ISSUEID = reader1["ISSUEID"].ToString(),
        //                                Category = reader1["Category"].ToString(),
        //                                SubCategory = reader1["SubCategory"].ToString(),
        //                                Issue = reader1["Issue"].ToString(),
        //                                isActive = reader1["isActive"].ToString()
        //                            });
        //                        }
        //                    }
        //                }
        //                using (SqlCommand secondCommand = new SqlCommand(queryString2, connection))
        //                {
        //                    using (SqlDataReader reader2 = secondCommand.ExecuteReader())
        //                    {
        //                        while (reader2.Read())
        //                        {
        //                            verticalProducts.Add(new VerticalProduct()
        //                            {
        //                                id = (int)reader2["id"],
        //                                Vertical = reader2["Vertical"].ToString(),
        //                                Product = reader2["Product"].ToString(),
        //                                Secured_Unsecured = reader2["Secured_Unsecured"].ToString(),
        //                                Organic_Inorganic = reader2["Organic_Inorganic"].ToString(),
        //                                Assets_or_Liabilities = reader2["Assets_or_Liabilities"].ToString()
        //                            });
        //                        }

        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                connection.Close();
        //                Console.WriteLine($"Error While getting categories and verticles data :{ex.Message}");
        //            }

        //        }

        //        Session["tblCategories1"] =  JsonConvert.SerializeObject(tblCategories));
        //        HttpContext.Session.SetString("verticalProducts1", Newtonsoft.Json.JsonConvert.SerializeObject(verticalProducts));

        //        //verticalProducts1 = verticalProducts;
        //        var categories = tblCategories.Select(x => x.Category).ToList();
        //        ViewBag.Categories = categories.Distinct().ToList();
        //        var verticles = verticalProducts.Select(x => x.Vertical).ToList();
        //        ViewBag.Verticles = verticles.Distinct().ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error While getting categories and verticles data :{ex.Message}");
        //    }
        //    return PartialView("CrmProductIndex");
        //}
        //public void GetSubCategoriesByCategory(string category)
        //{
        //    try
        //    {
        //        List<TblCategory> subcategories = null;
        //        List<TblCategory> categories = null;
        //        var serializedCategories = HttpContext.Session.GetString("tblCategories1");
        //        subcategories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TblCategory>>(serializedCategories);
        //        if (subcategories?.Count > 0)
        //        {
        //            categories = subcategories.Where(x => x.Category?.Trim() == category.Trim()).ToList();
        //            if (categories?.Count > 0)
        //            {
        //                var filteredsubcategories = categories.Select(x => x.SubCategory).ToList();
        //                return Json(new { data = filteredsubcategories });
        //            }

        //        }
        //        return Json(new { data = new List<string>() });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error While getting subcategories data for category :{category} :{ex.Message}");
        //        return Json(new { data = new List<string>() });
        //    }

        //}
        //public IActionResult GetProductsByVerticle(string verticle)
        //{
        //    try
        //    {
        //        List<VerticalProduct> verticalProducts = null;
        //        List<VerticalProduct> verticals = null;
        //        var serializedverticalProducts = HttpContext.Session.GetString("verticalProducts1");
        //        verticalProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VerticalProduct>>(serializedverticalProducts);
        //        if (verticalProducts?.Count > 0)
        //        {
        //            verticals = verticalProducts.Where(x => x.Vertical?.Trim() == verticle.Trim()).ToList();
        //            if (verticals?.Count > 0)
        //            {
        //                var products = verticals.Select(x => x.Product).ToList(); ;
        //                return Json(new { data = products });
        //            }

        //        }
        //        return Json(new { data = new List<string>() });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error While getting subcatergories data for verticle:{verticle}:{ex.Message}");
        //        return Json(new { data = new List<string>() });
        //    }

        //}
        //public IActionResult GetAccesslevel_Material_Financial_TypesByProduct(string product)
        //{
        //    VerticalProduct verticalProduct = new VerticalProduct();
        //    try
        //    {
        //        List<VerticalProduct> verticalProducts = null;
        //        var serializedverticalProducts = HttpContext.Session.GetString("verticalProducts1");
        //        verticalProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VerticalProduct>>(serializedverticalProducts);
        //        if (verticalProducts?.Count > 0)
        //        {
        //            var prod = verticalProducts.Where(x => x.Product.Trim() == product.Trim()).FirstOrDefault();
        //            if (prod != null)
        //            {
        //                verticalProduct.Secured_Unsecured = prod.Secured_Unsecured ?? "";
        //                verticalProduct.Organic_Inorganic = prod.Organic_Inorganic ?? "";
        //                verticalProduct.Assets_or_Liabilities = prod.Assets_or_Liabilities ?? "";

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error While getting Secured_Unsecured,Organic_Inorganic and Assets_or_Liabilities for product:{product} :{ex.Message}");
        //    }
        //    return Json(new { data = verticalProduct });
        //}
        public async Task<string> GetResponseFromPostURL(string url, string jsonrequestData)
        {
            string result = "";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {


                    StringContent content = new StringContent(jsonrequestData, Encoding.UTF8, "application/json");

                    using (var certificate = new X509Certificate2(PRIVATE_KEY_PATH, PRIVATE_KEY_PASSWORD))
                    {

                        httpClient.DefaultRequestHeaders.Add("x-key", APP_KEY);


                        var data = GetSign(jsonrequestData, certificate);

                        httpClient.DefaultRequestHeaders.Add("signed-data", data);
                        httpClient.DefaultRequestHeaders.Add("TENANT", TENANT);

                        using (HttpResponseMessage response = await httpClient.PostAsync(url, content))
                        {
                            result = await response.Content.ReadAsStringAsync();


                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private async Task<string> GetResponseFromGetURL(string url, object formParams)
        {
            string result = "";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var queryString = string.Join("&", formParams.GetType().GetProperties()
                        .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(formParams)?.ToString())}"));

                    var fullUrl = $"{url}?{queryString}";
                    
                    var data = GetSign(queryString, GetPrivateKey(PRIVATE_KEY_PATH, PRIVATE_KEY_PASSWORD));

                    // Add headers
                    httpClient.DefaultRequestHeaders.Add("x-key", APP_KEY);
                    httpClient.DefaultRequestHeaders.Add("signed-data", data);

                    using (var response = await httpClient.GetAsync(fullUrl))
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    TraceService(ex.StackTrace);
                }
            }

            TraceService("result: " + result);
            return result;
        }

        private static X509Certificate2 GetPrivateKey(string privateKeyPath, string privateKeyPassword)
        {
            try
            {
                X509Certificate2Collection collection = new X509Certificate2Collection();
                collection.Import(System.IO.File.ReadAllBytes(privateKeyPath), privateKeyPassword, X509KeyStorageFlags.PersistKeySet);

                // Assuming that the first certificate in the collection contains the private key
                X509Certificate2 certificate = collection[0];

                return certificate;
            }
            catch (Exception ex)
            {

                //TraceService($"Error loading private key: {ex.Message}");
                return null;
            }
            
        }

        public static bool Verify(string plainText, string signature, X509Certificate2 publicKey)
        {
            using (RSA rsa = publicKey.GetRSAPublicKey())
            {
                byte[] signatureBytes = Convert.FromBase64String(signature);

                return rsa.VerifyData(Encoding.UTF8.GetBytes(plainText), signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }

        //dataGridView4_CellContentClick
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentPageNo = 0;
           
            if (e.RowIndex >=0 && e.ColumnIndex == 0)
            {
                DataGridViewRow selectedRow = CustomerdetailsdataGridView.Rows[e.RowIndex];
                customerId = selectedRow.Cells[0].Value.ToString();
               
            }
            if(!string.IsNullOrEmpty(customerId))
                DisplayAccountDetails(customerId,currentPageNo,false);
        }

        private void accountDetailsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentPageNo = 0;
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataGridViewRow selectedRow = accountDetailsGridView.Rows[e.RowIndex];
                accountNo = selectedRow.Cells[0].Value.ToString();

            }
            if (!string.IsNullOrEmpty(accountNo))
                TransactionView(accountNo);
        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click_1(object sender, EventArgs e)
        {

        }

        private void Previousbtn_Click(object sender, EventArgs e)
        {
            bool accountDetailsPanelVisible = false;

            if (currentPageNo > 0)
            {
                currentPageNo--;
            }
            else
            {
                currentPageNo = 0;
            }
               
            if (totalPageSize < currentPageNo)
                currentPageNo = 0;

            if (AccountDetailsPanel.Visible)
                accountDetailsPanelVisible = true;

            DisplayAccountDetails(customerId, currentPageNo, accountDetailsPanelVisible, /*selectedColumnName,*/ Searchtxt.Text.Trim());
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            bool accountDetailsPanelVisible = false;
           
            if (currentPageNo >= 0)
            {
                currentPageNo++;
            }
            
            if (totalPageSize < currentPageNo)
                currentPageNo = 0;
            if (AccountDetailsPanel.Visible)
                accountDetailsPanelVisible = true; 
            DisplayAccountDetails(customerId, currentPageNo, accountDetailsPanelVisible, /*selectedColumnName, */Searchtxt.Text.Trim());
        }

        private void ProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void AccountDetailsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            GetTransactions(accountNo, customerId);
        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool accountDetailsPanelvisible = false;

            if (AccountDetailsPanel.Visible)
                accountDetailsPanelvisible = true;
            
            DisplayAccountDetails(customerId, 0, accountDetailsPanelvisible,/* selectedColumnName,*/ Searchtxt.Text.Trim());


        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void txtphone_TextChanged(object sender, EventArgs e)
        {

        }

        private void PreviousCustButton_Click(object sender, EventArgs e)
        {
           bool accountDetailsGridViewVisible = false;

            if (currentCustPageNo > 0)
            {
                currentCustPageNo--;
            }
            else
            {
                currentCustPageNo = 0;
            }

            if (totalCustPageSize < currentCustPageNo)
                currentCustPageNo = 0;

            if (accountDetailsGridView.Visible)
                accountDetailsGridViewVisible = true;

            GetCustomers(mobileNo.Text, currentCustPageNo,true, accountDetailsGridViewVisible);

        }

        private void NextCustButton_Click(object sender, EventArgs e)
        {
            bool accountDetailsGridViewVisible = false;

            if (currentCustPageNo >= 0)
            {
                currentCustPageNo++;
            }

            if (totalCustPageSize < currentCustPageNo)
                currentCustPageNo = 0;

            if (accountDetailsGridView.Visible)
                accountDetailsGridViewVisible = true;

            GetCustomers(mobileNo.Text, currentCustPageNo, true, accountDetailsGridViewVisible);
        }

        private static string GetSign(string message, X509Certificate2 certificate)
        {
            using (RSA privateKey = certificate.GetRSAPrivateKey())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] signature = privateKey.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(signature);
            }
        }
    }
}
