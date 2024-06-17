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
  
        }
       
    }
}
