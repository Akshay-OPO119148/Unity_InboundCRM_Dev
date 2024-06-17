using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace OneCRM
{
    public class Class1
    {
        public int[] CurrentStatusCount = new int[30];
        public int[] CurrentStatusCount1 = new int[30];
        public string[] CurrentStatusName = new string[30];
        public string[] CurrentStatusName1 = new string[30];

        public string CurrentStatus = null;
        public Int32 CurrentStatusId1 = 0;
        public Int32 CurrentStatusId2 = 0;
        Connection conobj = new Connection();
        public static string AgentName = string.Empty;
        private double BreakID;
        private double LoginID;

        public void LoadStatusDetails()
        {
            //string url = "http://192.168.0.70:8089/ICEGATEAPI/API/getstatus";

            //var json = "";
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";

            ////using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            ////{
            ////    string hostname = Dns.GetHostName();
            ////    json = "{\"HostName\":\"" + hostname + "\",\"Opoid\":\"" + txtUsername.Text.Trim() + "\"}";

            ////    streamWriter.Write(json);
            ////}

            //var httpresponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamreader = new StreamReader(httpresponse.GetResponseStream()))
            //{
            //    var result = streamreader.ReadToEnd();
            //    result = result.Replace("\"", "");
            //    result = result.Replace(":", "\":\"");
            //    result = result.Replace("{\\", "{\\\"");
            //    result = result.Replace("}", "\"}");
            //    result = result.Replace(",", "\",\"");
            //    result = result.Replace("\"{", "{");
            //    result = result.Replace("}\"", "}");
            //    result = result.Replace("\\", "");
            //    result = result.Replace("T00\":\"00\":\"00", "");

            //    try
            //    {
            //        if (result == "Failure")
            //        {

            //        }
            //        else
            //        {
            //            DataTable dt_Dn = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));
            //            //MessageBox.Show("DN = " + Convert.ToString(dt_Dn.Rows[0]["DN"]));
            //            //Int32 X = 0;
            //            for (Int32 i = 0; i < 26; i++)
            //            {
            //                CurrentStatusCount[i] = 0;
            //                CurrentStatusName[i] = "";


            //            }
            //            //X = 0;
            //            for (int x = 0; x < dt_Dn.Rows.Count; x++)// (rsStatus.Read())
            //            {
            //                // X = X + 1;
            //                CurrentStatusName[x] = dt_Dn.Rows[x]["STATUS_NAME"].ToString();
            //            }
            //        }
            //    }
            //    catch (Exception exc)
            //    {
            //        //MessageBox.Show("Error  - " + exc.Message, "");
            //    }
            //}

            CurrentStatusCount[0] = 0;
            CurrentStatusName[0] = "";
            CurrentStatusCount[1] = 1; CurrentStatusName[1] = "WAITING";
            CurrentStatusCount[2] = 2; CurrentStatusName[2] = "DIALING";
            CurrentStatusCount[3] = 3; CurrentStatusName[3] = "TALKING";
            CurrentStatusCount[4] = 4; CurrentStatusName[4] = "WRAPING";
            CurrentStatusCount[5] = 5; CurrentStatusName[5] = "TEA BREAK";
            CurrentStatusCount[6] = 6; CurrentStatusName[6] = "LUNCH BREAK";
            CurrentStatusCount[7] = 7; CurrentStatusName[7] = "TRAINING BREAK";
            CurrentStatusCount[8] = 8; CurrentStatusName[8] = "QUALITY BREAK";
            CurrentStatusCount[9] = 9; CurrentStatusName[9] = "BIO BREAK";
            CurrentStatusCount[10] = 10; CurrentStatusName[10] = "HOLD";
            CurrentStatusCount[11] = 11; CurrentStatusName[11] = "LOGOUT";
            CurrentStatusCount[12] = 12; CurrentStatusName[12] = "Emergency";
            CurrentStatusCount[13] = 13; CurrentStatusName[13] = "MANUAL DIALING";
            CurrentStatusCount[14] = 14; CurrentStatusName[14] = "Backend_Work BREAK";
            CurrentStatusCount[15] = 15; CurrentStatusName[15] = "Back_to_School BREAK";
            CurrentStatusCount[16] = 16; CurrentStatusName[16] = "CM_Feedback BREAK";
            CurrentStatusCount[17] = 17; CurrentStatusName[17] = "Dialer_NonTech_DownTime BREAK";
            CurrentStatusCount[18] = 18; CurrentStatusName[18] = "Dailer_Tech_DownTime BREAK";
            CurrentStatusCount[19] = 19; CurrentStatusName[19] = "Floor_Help BREAK";
            CurrentStatusCount[20] = 20; CurrentStatusName[20] = "Health_Activities BREAK";
            CurrentStatusCount[21] = 21; CurrentStatusName[21] = "Scheduled BREAK";
            CurrentStatusCount[22] = 22; CurrentStatusName[22] = "Team_Huddle BREAK";
            CurrentStatusCount[23] = 23; CurrentStatusName[23] = "Tech_DownTime BREAK";
            CurrentStatusCount[24] = 24; CurrentStatusName[24] = "Townhall BREAK";
            CurrentStatusCount[25] = 25; CurrentStatusName[25] = "Unwell BREAK";
            CurrentStatusCount[26] = 26; CurrentStatusName[26] = "TL Feedback BREAK";
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = conobj.getconn();
            //cmd.CommandText = "select * from STATUS order by id asc";
            //SqlDataReader rsStatus = cmd.ExecuteReader();
            //Int32 X = 0;
            //for (Int32 i = 0; i < 26; i++)
            //{
            //    CurrentStatusCount[i] = 0;
            //    CurrentStatusName[i] = "";
            //}
            //X = 0;
            //while (rsStatus.Read())
            //{
            //    X = X + 1;
            //    CurrentStatusName[X] = rsStatus["STATUS_NAME"].ToString();
            //}
            //rsStatus.Close();
        }
        //public void AgentLogin(string LType, string Agent, string empid, string extn, string agentid, string process,string systemname)
        //{

        //    try
        //    {
        //        //string systemname = Environment.MachineName;
        //        SqlCommand cmd = new SqlCommand("AgentLogin", conobj.getconn());
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@AgentName", Agent);
        //        cmd.Parameters.AddWithValue("@EmpID", empid);
        //        cmd.Parameters.AddWithValue("@LType", LType);
        //        cmd.Parameters.AddWithValue("@extn", extn);
        //        cmd.Parameters.AddWithValue("@agentID", agentid);
        //        cmd.Parameters.AddWithValue("@process", process);
        //        cmd.Parameters.AddWithValue("@systemname", systemname);
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //}
        //public void createloginlogoutlog(string AgentName, string empid, string extn, string process)
        //{
        //    try
        //    {
        //        string systemname = Environment.MachineName;
        //        SqlCommand cmd1 = new SqlCommand("LoginLogoutLogEntry", conobj.getconn());
        //        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd1.Parameters.AddWithValue("@agent", AgentName);
        //        cmd1.Parameters.AddWithValue("@empid", empid);
        //        cmd1.Parameters.AddWithValue("@extn", extn);
        //        cmd1.Parameters.AddWithValue("@process", process);
        //        cmd1.Parameters.AddWithValue("@id", LoginID);
        //        cmd1.Parameters.AddWithValue("@systemname", systemname);
        //        cmd1.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
        //        cmd1.ExecuteNonQuery();
        //        LoginID = Convert.ToDouble(cmd1.Parameters["@id"].Value);
        //    }
        //    catch 
        //    {
 
        //    }
        //}
        //public void update_endtime_in_loginlogoutlog()
        //{
        //    if (LoginID > 0)
        //    {
        //        SqlCommand cmd = new SqlCommand("UpdateEndTimeInLoginLogoutLog", conobj.getconn());
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", LoginID);
        //        cmd.ExecuteNonQuery();
        //        LoginID = 0;
        //    }
        //}
        //public void Update_Status(string Agent, int CurrentStatusId, string DN,string process)
        
        //{
        //    try
        //    {
        //        if (DN != null)
        //        {
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Connection = conobj.getconn();
        //            //cmd.CommandText = "UPDATE dialer.dbo.tblagentidmap SET CURRSTATUSID='" + CurrentStatusId + "',DN='" + DN + "',Process='" + process + "' where winlogin='" + Agent + "'";
        //            cmd.CommandText = "UPDATE [192.168.0.56].[dialer].[dbo].tblagentidmap SET curstatus='" + CurrentStatusId + "',extn='" + DN + "',Process_Name='" + process + "' where winlogin='" + Agent + "'";
                    
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //}
        //public void UpdateStatistics(string agent, long CurrentStatusCount1, int CurrentStatusCount2, int CurrentStatusCount3,
        //                             int CurrentStatusCount4, int CurrentStatusCount5, int CurrentStatusCount6, int CurrentStatusCount7,
        //                             int CurrentStatusCount8, int CurrentStatusCount10, int CurrentStatusCount12, string process)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("UpdateStatistics", conobj.getconn());
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@AgentName", agent);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount1", CurrentStatusCount1);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount2", CurrentStatusCount2);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount3", CurrentStatusCount3);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount4", CurrentStatusCount4);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount5", CurrentStatusCount5);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount6", CurrentStatusCount6);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount7", CurrentStatusCount7);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount8", CurrentStatusCount8);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount10", CurrentStatusCount10);
        //        cmd.Parameters.AddWithValue("@CurrentStatusCount12", CurrentStatusCount12);
        //        cmd.Parameters.AddWithValue("@process", process);

        //        cmd.ExecuteNonQuery();
        //        conobj.getconn().Close();
        //        for (Int32 i = 1; i <= 12; i++)
        //        {
        //            CurrentStatusCount[i] = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw(ex);
        //    }

       // }
    }
}
