
using Genesyslab.Platform.ApplicationBlocks.SipEndpoint;
using Genesyslab.Platform.Commons.Collections;
using Genesyslab.Platform.Commons.Protocols;
using Genesyslab.Platform.Outbound.Protocols.OutboundDesktop;
using Genesyslab.Platform.Voice.Protocols;
using Genesyslab.Platform.Voice.Protocols.TServer;
using Genesyslab.Platform.Voice.Protocols.TServer.Events;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Agent;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Dn;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Party;
using Genesyslab.Platform.Voice.Protocols.TServer.Requests.Special;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCRM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityBank.Models;

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

        int count = 10;
        int currentPageNo = 0;
        int totalPageSize = 0;
        int currentCustPageNo = 0;
        int totalCustPageSize = 0;
        string customerId = string.Empty;
        string accountNo = string.Empty;
        string ProductCode = string.Empty;
        string Product_Name = string.Empty;
        string AccountBalance = string.Empty;
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
            Cmb_Disposition.MouseWheel += ComboBox_MouseWheel;
            Cmb_SubDisposition.MouseWheel += ComboBox_MouseWheel;
            comboxVertical.MouseWheel += ComboBox_MouseWheel;
            comboxProduct.MouseWheel += ComboBox_MouseWheel;

        }
        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
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

           

            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            var version = fieVersionInfo.FileVersion;


            this.Text = CL_AgentDetails.ProcessName + " (Version : " + GetPublishedVersion() + ")";
            txt_AgentName.Text = CL_AgentDetails.AgentName;

            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = true;
            webBrowser1.WebBrowserShortcutsEnabled = true;
            webBrowser1.ObjectForScripting = this;
            
            webBrowserAPR.Url = new Uri("http://192.168.0.93:8088/API/AgentStatus/AgentStatus.aspx?Agentid=" + Convert.ToString(CL_AgentDetails.AgentID) + "");
         

            if (!string.IsNullOrEmpty(CL_AgentDetails.HistoryPage))
            {
                string URL = "";
                URL = CL_AgentDetails.HistoryPage + "/" + CL_AgentDetails.OPOID;
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "chrome.exe";
                process.StartInfo.Arguments = URL;
                process.Start();
            }
            #region GenesysCall
            
            try
            {
                systemname = Environment.MachineName.ToString();

                winlogin = CL_AgentDetails.OPOID;

                if (!string.IsNullOrEmpty(CL_AgentDetails.DN))
                {
                    DN = CL_AgentDetails.DN;
                    extn = DN;
                }
                else
                {
                    MessageBox.Show("DN Information Not Found..!", "NotFound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                    Application.Exit();
                    Environment.Exit(1);
                    return;
                }
                if (String.IsNullOrEmpty(CL_AgentDetails.KMS_OFFICE))
                {
                    unitytab.TabPages.Remove(tabKMS);
                }
                else
                {
                    webKMS.Url = new Uri(CL_AgentDetails.KMS_OFFICE);
                }
                try
                {
                    if (!string.IsNullOrEmpty(CL_AgentDetails.SingleStepTransfer))
                    {
                        string transRP = Convert.ToString(CL_AgentDetails.SingleStepTransfer);
                        if (transRP != "")
                        {
                            string[] mm = transRP.Split(',');
                            for (int i = 0; i < mm.Length; i++)
                            {
                                cmbtrnsRoutepoint.DisplayMember = "Text";
                                cmbtrnsRoutepoint.ValueMember = "Value";
                                cmbWarmtrnsRP.DisplayMember = "Text";
                                cmbWarmtrnsRP.ValueMember = "Value";
                                string[] routpoint = mm[i].ToString().Split(':');
                                for (int j = 0; j < transRP.Length; j++)
                                {
                                    cmbtrnsRoutepoint.Items.Add(new { Text = routpoint[j].ToString(), Value = routpoint[j + 1].ToString() });
                                    cmbWarmtrnsRP.Items.Add(new { Text = routpoint[j].ToString(), Value = routpoint[j + 1].ToString() });
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { }



                Host = CL_AgentDetails.TserverIP_OFFICE.ToString();
                Port = CL_AgentDetails.SipPort.ToString();
                TPort = CL_AgentDetails.TserverPort.ToString();
                callback_day_validation = 7;
                autowraptime = 1500; 
                tServerHost = Host;
                tServerport = TPort;
                winlogin = CL_AgentDetails.OPOID;

                if (Host == "" || Port == "" || TPort == "")
                {
                    MessageBox.Show("Invalid ServerSetting Details...!", "NotFound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                    Application.Exit();
                    Environment.Exit(1);
                    return;
                }
                agentID = CL_AgentDetails.AgentID;
                AgentName = CL_AgentDetails.AgentName;
                if (agentID == "")
                {
                    MessageBox.Show("Agent information not found..", "tblagentidmap", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                    Application.Exit();
                    Environment.Exit(1);
                    return;
                }
                timer1.Enabled = true;
                timer1.Start();

                #endregion

                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kindly Check the Configuration for user", "Login Error");
            }
            GetDetails();
            BindCategory();
            BindDisposition();
            BindVertical();

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

            TimerSipEndpointStatusCheck.Interval = 2;
            TimerSipEndpointStatusCheck.Tick += new EventHandler(TimerSipEndpointStatusCheck_Tick);

        }
        private void TimerAutoWrap_Tick(object sender, EventArgs e)
        {
            savecount++;
            if (savecount == autowraptime)
            {

                TimerAutoWrap.Stop();
                DisposecallTest("6", "29");

                ClearFields();
                ClareDropDawon();
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

                    AgentReady(extn);

                    this.label17.Text = "00:00:00";
                    if (isReady == true) { lblcallstatus.Text = "Agent Ready For Call"; }
                    this.ButtonDial.Enabled = true;
                    this.ButtonHangUp.Enabled = false;

                    this.btn_Conference.Enabled = false;
                    this.ButtonHold.Enabled = false;

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

                Panel pnl = this.Controls.Find("pnl_HeaderWebBrowser", true).FirstOrDefault() as Panel;
                pnl_WebBrowser.Controls.Remove(pnl);


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
            
        }

        private void auto_dial()
        {
            string txph = "";

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

            string CName = campaignName;

            string DialPhone = "";

            if (campaignphone == "")
            {

                DialPhone = txtphone.Text;
                campaignphone = txtphone.Text;
            }
            else if (campaignphone == txph || txph.Contains("X"))
            {

                DialPhone = campaignphone;
            }
            else
            {

                DialPhone = txtphone.Text;
            }
            if (DialPhone != "" && DialPhone.Length > 9)
            {

                isclosed = true;

                if (DialPhone.Length > 14)
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
                else if (DialPhone.Length == 14)
                {

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


                }

                else if (DisconnectStatus == true)
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

                    btnBreak.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnBreak, true });
                    btn_Conference.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btn_Conference, false });

                    btnLogout.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnLogout, true });

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

        private void Show_Data(double mycode)
        {
            try
            {

                if (campaignmode == "Preview")
                {

                    auto_dial();



                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void UpdateRecordingEndTime()
        {
            InsertCallLogApi();

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

                return;
            }
        }

        private void btn_Break_Click(object sender, EventArgs e)
        {
            if (isOnCall == false)
            {
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

            }
        }
        private void frmbreak_CurrentStatusId(int currentstatusid, string brkstatus)
        {
            try
            {
                if (CurrentStatusId != 4 && CurrentStatusId != 14)
                {

                    isclosed = true;
                    isbreak = false;

                    KeyValueCollection reasonCodes = new KeyValueCollection();
                    reasonCodes.Add("ReasonCode", brkstatus);//check before the reasoncode is configured in CCPulseStat
                    RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                    iMessage = tServerProtocol.Request(requestAgentNotReady);
                    checkReturnedMessage(iMessage);

                    
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

                            ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, false });

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

                        ButtonHold.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { ButtonHold, false });

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

                            if (CL_AgentDetails.PhoneNoMaskIs == "1")
                            {
                                txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(eventEstablished.ANI, @"\d(?!\d{0,3}$)", "X") });
                            }
                            else
                            {
                                txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventEstablished.ANI });
                            }

                            campaignphone = eventEstablished.ANI;

                        }

                        try
                        {
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
                            else
                            {
                                CurrentStatusId = 3;
                                isOnCall = true;
                            }

                            distype = "Customer";
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

                        }
                    }
                    break;
                case EventOnHook.MessageName:
                    EventOnHook eventOnhook = msg as EventOnHook;
                    if (eventOnhook.ThisDN == extn)
                    {

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


                        if (CL_AgentDetails.PhoneNoMaskIs == "1")
                        {
                            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, Regex.Replace(eventAbandoned.ANI, @"\d(?!\d{0,3}$)", "X") });
                        }
                        else
                        {
                            txtphone.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txtphone, eventAbandoned.ANI });
                        }
                        campaignphone = eventAbandoned.ANI;
                        
                        lblcallstatus.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallstatus, "Kindly Restart Your System Immediately." });
                        btnsubmit.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { btnsubmit, true });
                        
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
                                    
                                    lblcampaign.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcampaign, eventUserEvent.UserData["GSW_CAMPAIGN_NAME"].ToString() });
                                    txt_campaignmode.Invoke(new UpdateTextCallback(this.UpdateText), new object[] { txt_campaignmode, eventUserEvent.UserData["GSW_CAMPAIGN_MODE"].ToString() });
                                    if (eventUserEvent.UserData["GSW_CAMPAIGN_MODE"].ToString() == "Preview")
                                    {
                                        cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, true });
                                    }
                                    else
                                    {
                                        this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { TimerGetNext.Enabled = false; });
                                        this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { Timer_Autogetnext1.Enabled = false; });
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
                                           
                                            lblcallback.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallback, "Personal Callback" });

                                        }
                                        else
                                        {
                                           
                                            lblcallback.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { lblcallback, " " });
                                            cmdgetnext.Invoke(new UpdateButtonStatusCallback(this.UpdateButtonStatus), new object[] { cmdgetnext, true });
                                            this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate () { Timer_Autogetnext1.Enabled = true; });
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

                try
                {

                    string CBdatetime = "";

                   
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
                    

                    DisposeCall(CBdatetime);

                    if (isbreak == false && CurrentStatusId == 4 && BreakStatus != "")
                    {
                        
                        isclosed = true;
                        isbreak = false;

                        KeyValueCollection reasonCodes = new KeyValueCollection();
                        reasonCodes.Add("ReasonCode", BreakStatus);//check before the reasoncode is configured in CCPulseStat
                        RequestAgentNotReady requestAgentNotReady = RequestAgentNotReady.Create(extn, AgentWorkMode.AuxWork, null, reasonCodes, reasonCodes);
                        iMessage = tServerProtocol.Request(requestAgentNotReady);
                        checkReturnedMessage(iMessage);

                        
                        CurrentStatusId = CurrBreakID;
                        TimerGetNext.Stop();
                        Timer_Autogetnext1.Stop();
                        btnBreak.Enabled = false;
                        cmdgetnext.Enabled = false;
                        ButtonDial.Enabled = false;

                    }
                    else if (islogout == true)
                    {
                        if (isLoggedIn)
                        { LogOut(); }

                        if (isConnectionOpen)
                        { tServerProtocol.Close(); isConnectionOpen = false; }


                        Application.Exit();
                        Environment.Exit(1);
                    }
                    else
                    {
                        if (CL_AgentDetails.ProcessType == "OUTBOUND")
                        {
                        
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

        private void DisposeCall(string CBdatetime)
        {
            try
            {


                if ((CL_AgentDetails.IsManual == "0" && CL_AgentDetails.ProcessType == "OUTBOUND") || (MyCode > 0))
                {
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
                        
                        
                    }
                    else if (finishCode == "CCB")
                    {
                        
                        if (finishCode == "CCB" && campaignName != null)
                        {

                            KeyValueCollection kvp = new KeyValueCollection();
                            kvp.Add("GSW_AGENT_REQ_TYPE", "RecordReschedule");
                            kvp.Add("GSW_APPLICATION_ID", ocsApplicationID);
                            kvp.Add("GSW_CALLBACK_TYPE", "Campaign");
                            kvp.Add("GSW_CAMPAIGN_NAME", campaignName);
                            
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
                        
                    }

                    if (lblcallback.Text == "Personal Callback")
                    {
                        UpdateCallingListForPCB(dispose_code, subdispose_code, finishCode, CBdatetime);

                    }
                }

               
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
            
            Cmb_Disposition.SelectedItem = null;
            Cmb_SubDisposition.SelectedItem = null;
        }
        private void ClearFields()
        {

            
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
            
            webBrowserAPR.Url = new Uri("http://192.168.0.93:8088/API/AgentStatus/AgentStatus.aspx?Agentid=" + Convert.ToString(CL_AgentDetails.AgentID) + "");
            
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

                
                isReady = true;
                btnLogout.Enabled = true;
                btnBreak.Enabled = true;
                btnsubmit.Enabled = false;
                isclosed = true;
                isbreak = true;
                CurrentStatusId = 1;
                
                connID = null;
                IVRconnID = null;
                IVRConnid = null;
                APPREFNO = "";
                CUST_NAME = "";
                Agent_Ready();
                
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


                
            }
            else
            {
                isReady = true;
                btnLogout.Enabled = true;
                btnBreak.Enabled = true;
                btnsubmit.Enabled = false;
                isclosed = true;
                isbreak = true;

                
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
                            
                            cmdgetnext.Enabled = true;
                            if (islogout == true)
                            {
                                if (isLoggedIn)
                                { LogOut(); }

                                if (isConnectionOpen)
                                { tServerProtocol.Close(); isConnectionOpen = false; }

                                Application.Exit();
                                Environment.Exit(1);
                            }
                            TimerGetNext.Start();
                            
                        }
                    }
                    
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
                                
                                txtmycode.Invoke(new UpdateLabelTextCallback(this.UpdateLabelText), new object[] { txtmycode, Convert.ToString(MyCode) });
                                campaignphone = Convert.ToString(dt_EntityType.Rows[0][1]);
                                
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                           
                            BindCategory();
                            BindDisposition();
                           
                        }
                        
                    }
                    
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void BindCategory()
        {
            
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
                
            }
        }

        private void BindIssue(string selectedSubcatID)
        {
            
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
                
                string selectedDisposition = Cmb_Disposition.Text.ToString();
                fillSubdisposition(selectedDisposition);
            }
        }

        private void fillSubdisposition(string selectedDisposition)
        {
            
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
                    
                    label35.Visible = true;
                    Txt_dateTimePickerToDate.Visible = true;


                }
                else
                {
                    
                    label35.Visible = false;
                    Txt_dateTimePickerToDate.Visible = false;
                }

            }
        }

        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            
            try
            {

                

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
                            

                        }
                        catch (Exception)
                        {


                            string str = "Invalid CALL_BACK date is selected...";
                            string script = "alert('" + str + "')";

                            MessageBox.Show("CallerName Field is Required..!", script, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                            
                            Txt_dateTimePickerToDate.Text = "";

                            return;

                        }


                    }

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

                cmddisphist.Parameters.AddWithValue("@Disposition", Disposition);
                cmddisphist.Parameters.AddWithValue("@SubDisposition", subDisposition);
                

                cmddisphist.Parameters.AddWithValue("@EmployeID", winlogin);
                cmddisphist.Parameters.AddWithValue("@Connid", Convert.ToString(connID));
                
                cmddisphist.Parameters.AddWithValue("@Phone", campaignphone);

                cmddisphist.Parameters.AddWithValue("@REMARKS", Txt_Account_Details.Text);
                
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

               
                concmd.Open();
                cmddisphist.ExecuteNonQuery();
                concmd.Close();
                
                string str = "Record Saved Successfully...,Disposition:" + Disposition + ",SubDisposition:" + subDisposition + ",CBTime:" + "" + ",CBType:" + finishCode;

                string script = "alert('" + str + "')";
                savedata(str);
                
            }
            catch (Exception exc)
            {


                string str = "Record not Saved. Error occurred: " + exc.Message.ToString();

                string script = "alert('" + str + "')";
                
                return;

            }
            clearfields();

        }

        private void clearfields()
        {
            comboxVertical.Text = "";
            comboxProduct.Text = "";
            Cmb_Disposition.Text = "";
            Cmb_SubDisposition.Text = "";
            Cmb_Category.Text = "";
            cmb_SubCategory.Text = "";
            
            CmbStatus.Text = "";
            Txt_Account_Details.Text = "";
            TxtCallerName.Text = "";
            TxtBranchName.Text = "";
            TxtCustConcern.Text = "";
            TxtResolution.Text = "";
            TxtConnid.Text = "";
            Cmb_Category.SelectedIndex = -1;
            cmb_SubCategory.SelectedIndex = -1;
            
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
                
                SqlDataAdapter ad = new SqlDataAdapter(cmd);

                ad.Fill(dt);


                
                dataGridView1.DataSource = dt.Tables[0];

                con1.Close();

            }
            catch (Exception ex)
            {

            }
        }

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

        public async void GetCustomers(string mobileNo, int currentCustPageNo = 0, bool isPreviousOrNextButtonCall = false, bool accountDetailsGridViewVisible = false)
        {
            try
            {
                

                if (accountDetailsGridViewVisible && isPreviousOrNextButtonCall)
                {
                    disableOrEnableAccountDetailsGridewithFeatures(true);
                }
                else
                {
                    disableOrEnableAccountDetailsGridewithFeatures(false);
                }
                AccountDetailsPanel.Visible = false;
                TransactionDetailsPanel.Visible = false;

                accountNo = string.Empty;
                int count = 10;
                int totalEntryCount = 0;
                var resultdata = new List<CustomerData>();
                var requestData = new { mobileNumber = mobileNo.Trim() };
                string jsonrequestData = JsonConvert.SerializeObject(requestData);
                TraceService($"Calling API_URL:{CUST_BY_MOBILENO_API_URL}  Resquest Data:{jsonrequestData}, Resquest Time:{DateTime.Now.ToString()}");
                var ApiResponse = await GetResponseFromPostURL(CUST_BY_MOBILENO_API_URL, jsonrequestData);
                var data = JsonConvert.DeserializeObject<CustomerListByMobileResponse>(ApiResponse);

                TraceService($"Calling API_URL:{CUST_BY_MOBILENO_API_URL},Response Status Code:{data?.response_code}, Response Time:{DateTime.Now.ToString()}");

                if (CustomerdetailsdataGridView.Columns.Count == 0)
                {
                    CustomerdetailsdataGridView.AutoGenerateColumns = false;
                    CustomerdetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "customerId", HeaderText = "CustomerId" });
                    CustomerdetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "fullName", HeaderText = "Name" });
                    CustomerdetailsdataGridView.Columns[0].Width = 535;
                    CustomerdetailsdataGridView.Columns[1].Width = 535;

                }

                if (CustomerdetailsdataGridView.DataSource != null)
                    CustomerdetailsdataGridView.DataSource = null;

                if (data?.response_message == "Success")
                {
                    CustomerdetailsdataGridView.Visible = true;
                    customerDetailstitlelabel.Visible = true;
                    PreviousCustButton.Visible = true;
                    NextCustButton.Visible = true;
                    CustomerDetailsPageInfo.Visible = true;

                    resultdata = data?.results.Skip(count * currentPageNo).Take(count).OrderBy(x => x.customerId).ToList();
                    CustomerdetailsdataGridView.DataSource = resultdata;

                }
                else
                {
                    CustomerdetailsdataGridView.Visible = false;
                    PreviousCustButton.Visible = false;
                    NextCustButton.Visible = false;
                    CustomerDetailsPageInfo.Visible = false;
                }

                


                if (resultdata.Count > 0)
                {

                    totalEntryCount = (int)data?.results.ToList().Count;
                    totalCustPageSize = (int)(totalEntryCount / count);

                    int pageFirstRowNo = ((count * currentCustPageNo) + 1);
                    int pageLastRowNo = ((count * currentCustPageNo) + resultdata.Count);

                    CustomerDetailsPageInfo.Text = "Showing " + pageFirstRowNo + " to " + pageLastRowNo + " of " + totalEntryCount + " entries";
                }
                else
                {
                    if (resultdata.Count == 0 && isPreviousOrNextButtonCall == false)
                    {
                        PreviousCustButton.Visible = false;
                        NextCustButton.Visible = false;
                        CustomerDetailsPageInfo.Visible = false;
                    }
                    if (totalEntryCount == 0)
                    {
                        label51.Text = "Showing " + 0 + " to " + 0 + " of " + totalEntryCount + " entries";
                    }
                    else
                    {
                        totalCustPageSize = 0;
                        NextBtnCuctomerDetails();
                    }
                }

               
               
            }
            catch (Exception ex)
            {
                TraceService($"Error While calling API_URL:{CUST_BY_MOBILENO_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                
                PreviousCustButton.Visible = false;
                NextCustButton.Visible = false;
                CustomerDetailsPageInfo.Visible = false;
                
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
            
            int totalWidth = 0;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.Width = 535;
                totalWidth += column.Width + 2;
            }
            dataGridView.Width = totalWidth + dataGridView.RowHeadersWidth + 15;


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

        public void sortdatagridview(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }
        
        public async void DisplayAccountDetails(string customerId, int currentPageNo, bool accountDetailsPanelvisible = false,/*string columnName = null,*/string columnValue = null)
        {

            try
            {

                int totalEntryCount = 0;                

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
                        accountDetailsGridView.DataSource = resultdata.OrderBy(x => x.accountNumber).ToList();
                        if (resultdata.Count > 0)
                        {
                            int pageFirstRowNo = ((count * currentPageNo) + 1);
                            int pageLastRowNo = ((count * currentPageNo) + resultdata.Count);
                            label51.Text = "Showing " + pageFirstRowNo + " to " + pageLastRowNo + " of " + totalEntryCount + " entries";
                        }
                        else
                        {
                            if (totalEntryCount == 0)
                            {
                                label51.Text = "Showing " + 0 + " to " + 0 + " of " + totalEntryCount + " entries";
                            }
                            else
                            {
                                totalPageSize = 0;
                                NetBtnForDisplayAccountDetails();
                            }
                        }
                    }
                    else
                    {
                        VisibilityOfDisplayAccountGridView(false);
                    }

                    
                    sortdatagridview(accountDetailsGridView);

                }
                else
                {
                    accountDetailsGridView.DataSource = data?.results.ToList();                    
                    VisibilityOfDisplayAccountGridView(false);

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While calling API_URL:{ACCOUNTLIST_BY_CUSTID_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
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

            }
            catch (Exception ex)
            {
                TraceService($"Error While calling API_URL:{CUSTOMERDETAILS_BY_CUSTOMERID_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");

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

        public async void GetTransactions(string accountNo, string customerId)
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

                if (TransactionDetailsdataGridView.Columns.Count == 0)
                {
                   

                    TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "txnDate", HeaderText = "Date" });
                    TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "txnAmount", HeaderText = "Amount" });
                    TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "txnNature", HeaderText = "Dr/Cr" });
                    TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "instrNo", HeaderText = "InstrumentNo" });
                    TransactionDetailsdataGridView.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "narration", HeaderText = "Description" });
                }


                if (data?.response_message == "Success")
                {
                    if (data?.results.Count > 0)
                    {
                       
                        TransactionDetailsdataGridView.AutoGenerateColumns = false;
                        
                        if (TransactionDetailsdataGridView.DataSource != null)
                        {
                            TransactionDetailsdataGridView.DataSource = null;
                            dataList = data?.results.Take(10).OrderByDescending(obj => obj.txnDate == null ? DateTime.MinValue : DateTime.Parse(obj.txnDate)).ToList();
                            
                            dataList.ForEach((x) => { x.txnDate = x.txnDate + " " + x.txnTime; });
                            TransactionDetailsdataGridView.DataSource = dataList;

                        }
                        else
                        {
                            dataList = data?.results.Take(10).OrderByDescending(obj => obj.txnDate == null ? DateTime.MinValue : DateTime.Parse(obj.txnDate)).ToList();
                            dataList.ForEach((x) => { x.txnDate = x.txnDate + " " + x.txnTime; });
                            TransactionDetailsdataGridView.DataSource = dataList;
         
                        }
                        
                       
                    }
                    
                }
                else
                {
                    TransactionDetailsdataGridView.DataSource = dataList;
                }
            }
            catch (Exception ex)
            {
                TraceService($"Error While calling API_URL:{MINISTATEMENT_API_URL} Error:{ex} Time:{DateTime.Now.ToString()}");
                TransactionDetailsdataGridView.DataSource = new List<TransactionDetails>();
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
                
                return null;
            }

        }
      
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

                X509Certificate2 certificate = collection[0];

                return certificate;
            }
            catch (Exception ex)
            {

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

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentPageNo = 0;

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataGridViewRow selectedRow = CustomerdetailsdataGridView.Rows[e.RowIndex];
                customerId = selectedRow.Cells[0].Value.ToString();

            }
            if (!string.IsNullOrEmpty(customerId))
                DisplayAccountDetails(customerId, currentPageNo, false);
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

            DisplayAccountDetails(customerId, currentPageNo, accountDetailsPanelVisible, Searchtxt.Text.Trim());
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            NetBtnForDisplayAccountDetails();
            
        }

        private void NetBtnForDisplayAccountDetails()
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
            DisplayAccountDetails(customerId, currentPageNo, accountDetailsPanelVisible, Searchtxt.Text.Trim());
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

            DisplayAccountDetails(customerId, 0, accountDetailsPanelvisible, Searchtxt.Text.Trim());


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

            GetCustomers(mobileNo.Text, currentCustPageNo, true, accountDetailsGridViewVisible);

        }

        private void NextCustButton_Click(object sender, EventArgs e)
        {
            NextBtnCuctomerDetails();
        }

        private void NextBtnCuctomerDetails()
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
