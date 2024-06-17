using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneCRM
{
    public partial class Questions : Form
    {
        public Questions()
        {
            InitializeComponent();
        }
        Connection conobj = new Connection();
        DataTable dt = new DataTable();
        SqlCommand Qus1;
        SqlCommand Qus2;
        SqlCommand Qus3;
        SqlCommand Qus4;
        SqlCommand Qus5;
        string QuestionID_1 = ""; string QuestionID_2 = ""; string QuestionID_3 = ""; string QuestionID_4 = ""; string QuestionID_5 = "";
        string CorrectAnswer_1 = ""; string CorrectAnswer_2 = ""; string CorrectAnswer_3 = ""; string CorrectAnswer_4 = ""; string CorrectAnswer_5 = "";
        protected override void OnLoad(EventArgs e)
        {

           // AddRadioButtonsDynamically(2);


            SqlCommand cmd = new SqlCommand("Usp_Fetch_Question_all_process", conobj.getconn());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Process", CL_AgentDetails.ProcessName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
         
            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                var row1 = dt.Rows[0];
                Question_1.Text = row1["QuestionText"].ToString();
                radioButton1.Text = row1["OptionA"].ToString();
                radioButton2.Text = row1["OptionB"].ToString();
                radioButton3.Text = row1["OptionC"].ToString();
                radioButton4.Text = row1["OptionD"].ToString();
                QuestionID_1 = row1["QuestionID"].ToString();
                CorrectAnswer_1 = row1["CorrectAnswer"].ToString();

                var row2 = dt.Rows[1];
                Question_2.Text = row2["QuestionText"].ToString();
                radioButton5.Text = row2["OptionA"].ToString();
                radioButton6.Text = row2["OptionB"].ToString();
                radioButton7.Text = row2["OptionC"].ToString();
                radioButton8.Text = row2["OptionD"].ToString();
                QuestionID_2 = row2["QuestionID"].ToString();
                CorrectAnswer_2 = row2["CorrectAnswer"].ToString();

                var row3 = dt.Rows[2];
                Question_3.Text = row3["QuestionText"].ToString();
                radioButton9.Text = row3["OptionA"].ToString();
                radioButton10.Text = row3["OptionB"].ToString();
                radioButton11.Text = row3["OptionC"].ToString();
                radioButton12.Text = row3["OptionD"].ToString();
                QuestionID_3 = row3["QuestionID"].ToString();
                CorrectAnswer_3 = row3["CorrectAnswer"].ToString();

                var row4 = dt.Rows[3];
                Question_4.Text = row4["QuestionText"].ToString();
                radioButton13.Text = row4["OptionA"].ToString();
                radioButton14.Text = row4["OptionB"].ToString();
                radioButton15.Text = row4["OptionC"].ToString();
                radioButton16.Text = row4["OptionD"].ToString();
                QuestionID_4 = row4["QuestionID"].ToString();
                CorrectAnswer_4 = row4["CorrectAnswer"].ToString();

                var row5 = dt.Rows[4];
                Question_5.Text = row5["QuestionText"].ToString();
                radioButton17.Text = row5["OptionA"].ToString();
                radioButton18.Text = row5["OptionB"].ToString();
                radioButton19.Text = row5["OptionC"].ToString();
                radioButton20.Text = row5["OptionD"].ToString();
                QuestionID_5 = row5["QuestionID"].ToString();
                CorrectAnswer_5 = row5["CorrectAnswer"].ToString();

            }
        }

        private void AddRadioButtonsDynamically(int numberOfButtons)
        {
            //int spacing = 10; // Adjust the spacing between radio buttons
            //int buttonWidth = 100; // Adjust the width of the radio buttons

            //for (int i = 0; i < numberOfButtons; i++)
            //{
            //    RadioButton radioButton = new RadioButton();
            //    radioButton.Text = "Option " + (i + 1);
            //    radioButton.Width = buttonWidth;
            //    radioButton.Location = new Point(i * (buttonWidth + spacing) + spacing, 30);
            //    panel1.Controls.Add(radioButton); // Add the radio button to the container (e.g., panel1)
            //}
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Questions_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Ans1 = ""; string Ans2 = ""; string Ans3 = ""; string Ans4 = ""; string Ans5 = "";
               
                //List<QuestionAns> list = new List<QuestionAns>();
                //foreach (QuestionAns rate in list)
                //{}

                //Qus1
                

                if (radioButton1.Checked == true)
                {
                    Ans1 = radioButton1.Text.ToString();

                    Qus1 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus1.CommandType = CommandType.StoredProcedure;
                    Qus1.Parameters.AddWithValue("@QuestionID", QuestionID_1);
                    Qus1.Parameters.AddWithValue("@QuestionText", Question_1.Text);
                    Qus1.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_1);
                    Qus1.Parameters.AddWithValue("@Answer_by_User", Ans1);
                    Qus1.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus1.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus1.ExecuteNonQuery();


                }

                if (radioButton2.Checked == true)
                {
                    Ans1 = radioButton2.Text.ToString();

                  Qus1 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus1.CommandType = CommandType.StoredProcedure;
                    Qus1.Parameters.AddWithValue("@QuestionID", QuestionID_1);
                    Qus1.Parameters.AddWithValue("@QuestionText", Question_1.Text);
                    Qus1.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_1);
                    Qus1.Parameters.AddWithValue("@Answer_by_User", Ans1);
                    Qus1.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus1.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus1.ExecuteNonQuery();

                }

                if (radioButton3.Checked == true)
                {
                    Ans1 = radioButton3.Text.ToString();

                    Qus1 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus1.CommandType = CommandType.StoredProcedure;
                    Qus1.Parameters.AddWithValue("@QuestionID", QuestionID_1);
                    Qus1.Parameters.AddWithValue("@QuestionText", Question_1.Text);
                    Qus1.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_1);
                    Qus1.Parameters.AddWithValue("@Answer_by_User", Ans1);
                    Qus1.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus1.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus1.ExecuteNonQuery();

                }

                if (radioButton4.Checked == true)
                {
                    Ans1 = radioButton4.Text.ToString();

                     Qus1 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus1.CommandType = CommandType.StoredProcedure;
                    Qus1.Parameters.AddWithValue("@QuestionID", QuestionID_1);
                    Qus1.Parameters.AddWithValue("@QuestionText", Question_1.Text);
                    Qus1.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_1);
                    Qus1.Parameters.AddWithValue("@Answer_by_User", Ans1);
                    Qus1.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus1.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus1.ExecuteNonQuery();
                }

                //end

                //Qus2

                if (radioButton5.Checked == true)
                {
                    Ans2 = radioButton5.Text.ToString();

                    Qus2 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus2.CommandType = CommandType.StoredProcedure;
                    Qus2.Parameters.AddWithValue("@QuestionID", QuestionID_2);
                    Qus2.Parameters.AddWithValue("@QuestionText", Question_2.Text);
                    Qus2.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_2);
                    Qus2.Parameters.AddWithValue("@Answer_by_User", Ans2);
                 
                    Qus2.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus2.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus2.ExecuteNonQuery();
                }

                if (radioButton6.Checked == true)
                {
                    Ans2 = radioButton6.Text.ToString();
                    Qus2 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus2.CommandType = CommandType.StoredProcedure;
                    Qus2.Parameters.AddWithValue("@QuestionID", QuestionID_2);
                    Qus2.Parameters.AddWithValue("@QuestionText", Question_2.Text);

                    Qus2.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_2);
                    Qus2.Parameters.AddWithValue("@Answer_by_User", Ans2);
                   
                    Qus2.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus2.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus2.ExecuteNonQuery();
                }

                if (radioButton7.Checked == true)
                {
                    Ans2 = radioButton7.Text.ToString();
                    Qus2 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus2.CommandType = CommandType.StoredProcedure;
                    Qus2.Parameters.AddWithValue("@QuestionID", QuestionID_2);
                    Qus2.Parameters.AddWithValue("@QuestionText", Question_2.Text);

                    Qus2.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_2);
                    Qus2.Parameters.AddWithValue("@Answer_by_User", Ans2);
                   
                    Qus2.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus2.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus2.ExecuteNonQuery();
                }


                if (radioButton8.Checked == true)
                {
                    Ans2 = radioButton8.Text.ToString();

                    Qus2 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus2.CommandType = CommandType.StoredProcedure;
                    Qus2.Parameters.AddWithValue("@QuestionID", QuestionID_2);
                    Qus2.Parameters.AddWithValue("@QuestionText", Question_2.Text);

                    Qus2.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_2);
                    Qus2.Parameters.AddWithValue("@Answer_by_User", Ans2);
                    Qus2.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus2.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus2.ExecuteNonQuery();
                }
                //end
                //Qus3
                if (radioButton9.Checked == true)
                {
                    Ans3 = radioButton9.Text.ToString();

                   Qus3 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus3.CommandType = CommandType.StoredProcedure;
                    Qus3.Parameters.AddWithValue("@QuestionID", QuestionID_3);
                    Qus3.Parameters.AddWithValue("@QuestionText", Question_3.Text);
                    Qus3.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_3);
                    Qus3.Parameters.AddWithValue("@Answer_by_User", Ans3);
                  
                    Qus3.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus3.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus3.ExecuteNonQuery();
                }

                if (radioButton10.Checked == true)
                {
                    Ans3 = radioButton10.Text.ToString();

                    Qus3 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus3.CommandType = CommandType.StoredProcedure;
                    Qus3.Parameters.AddWithValue("@QuestionID", QuestionID_3);
                    Qus3.Parameters.AddWithValue("@QuestionText", Question_3.Text);

                    Qus3.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_3);
                    Qus3.Parameters.AddWithValue("@Answer_by_User", Ans3);
                  
                   
                    Qus3.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus3.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus3.ExecuteNonQuery();
                }


                if (radioButton11.Checked == true)
                {
                    Ans3 = radioButton11.Text.ToString();

                    Qus3 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus3.CommandType = CommandType.StoredProcedure;
                    Qus3.Parameters.AddWithValue("@QuestionID", QuestionID_3);
                    Qus3.Parameters.AddWithValue("@QuestionText", Question_3.Text);

                    Qus3.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_3);
                    Qus3.Parameters.AddWithValue("@Answer_by_User", Ans3);
                  
                   
                    Qus3.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus3.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus3.ExecuteNonQuery();
                }

                if (radioButton12.Checked == true)
                {
                    Ans3 = radioButton12.Text.ToString();

                    Qus3 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus3.CommandType = CommandType.StoredProcedure;
                    Qus3.Parameters.AddWithValue("@QuestionID", QuestionID_3);
                    Qus3.Parameters.AddWithValue("@QuestionText", Question_3.Text);

                    Qus3.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_3);
                    Qus3.Parameters.AddWithValue("@Answer_by_User", Ans3);
                  
                    Qus3.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus3.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus3.ExecuteNonQuery();
                }

                //end

                //Qus4

                if (radioButton13.Checked == true)
                {
                    Ans4 = radioButton13.Text.ToString();


                   Qus4 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus4.CommandType = CommandType.StoredProcedure;
                    Qus4.Parameters.AddWithValue("@QuestionID", QuestionID_4);
                    Qus4.Parameters.AddWithValue("@QuestionText", Question_4.Text);
                    Qus4.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_4);
                    Qus4.Parameters.AddWithValue("@Answer_by_User", Ans4);
                  
                  
                    Qus4.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus4.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus4.ExecuteNonQuery();
                }

                if (radioButton14.Checked == true)
                {
                    Ans4 = radioButton14.Text.ToString();

                    Qus4 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus4.CommandType = CommandType.StoredProcedure;
                    Qus4.Parameters.AddWithValue("@QuestionID", QuestionID_4);
                    Qus4.Parameters.AddWithValue("@QuestionText", Question_4.Text);

                    Qus4.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_4);
                    Qus4.Parameters.AddWithValue("@Answer_by_User", Ans4);
                   
                    Qus4.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus4.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus4.ExecuteNonQuery();
                }


                if (radioButton15.Checked == true)
                {
                    Ans4 = radioButton15.Text.ToString();


                    Qus4 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus4.CommandType = CommandType.StoredProcedure;
                    Qus4.Parameters.AddWithValue("@QuestionID", QuestionID_4);
                    Qus4.Parameters.AddWithValue("@QuestionText", Question_4.Text);

                    Qus4.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_4);
                    Qus4.Parameters.AddWithValue("@Answer_by_User", Ans4);
                 
                    Qus4.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus4.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus4.ExecuteNonQuery();
                }


                if (radioButton16.Checked == true)
                {
                    Ans4 = radioButton16.Text.ToString();

                    Qus4 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus4.CommandType = CommandType.StoredProcedure;
                    Qus4.Parameters.AddWithValue("@QuestionID", QuestionID_4);
                    Qus4.Parameters.AddWithValue("@QuestionText", Question_4.Text);

                    Qus4.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_4);
                    Qus4.Parameters.AddWithValue("@Answer_by_User", Ans4);
                    Qus4.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus4.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus4.ExecuteNonQuery();
                }

                //end

                //Qus5
                if (radioButton17.Checked == true)
                {
                    Ans5 = radioButton17.Text.ToString();

                    Qus5 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus5.CommandType = CommandType.StoredProcedure;
                    Qus5.Parameters.AddWithValue("@QuestionID", QuestionID_5);
                    Qus5.Parameters.AddWithValue("@QuestionText", Question_5.Text);
                    Qus5.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_5);
                    Qus5.Parameters.AddWithValue("@Answer_by_User", Ans5);
                  
                    Qus5.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus5.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus5.ExecuteNonQuery();
                }


                if (radioButton18.Checked == true)
                {
                    Ans5 = radioButton18.Text.ToString();

                    Qus5 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus5.CommandType = CommandType.StoredProcedure;
                    Qus5.Parameters.AddWithValue("@QuestionID", QuestionID_5);
                    Qus5.Parameters.AddWithValue("@QuestionText", Question_5.Text);

                    Qus5.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_5);
                    Qus5.Parameters.AddWithValue("@Answer_by_User", Ans5);
                  
                   
                    Qus5.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus5.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                    //Qus5.ExecuteNonQuery();
                }


                if (radioButton19.Checked == true)
                {
                    Ans5 = radioButton19.Text.ToString();

                    Qus5 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus5.CommandType = CommandType.StoredProcedure;
                    Qus5.Parameters.AddWithValue("@QuestionID", QuestionID_5);
                    Qus5.Parameters.AddWithValue("@QuestionText", Question_5.Text);

                    Qus5.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_5);
                    Qus5.Parameters.AddWithValue("@Answer_by_User", Ans5);
                  
                 
                    Qus5.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus5.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus5.ExecuteNonQuery();
                }


                if (radioButton20.Checked == true)
                {
                    Ans5 = radioButton20.Text.ToString();

                    Qus5 = new SqlCommand("Insert_Emp_Question_Answer_Result", conobj.getconn());
                    Qus5.CommandType = CommandType.StoredProcedure;
                    Qus5.Parameters.AddWithValue("@QuestionID", QuestionID_5);
                    Qus5.Parameters.AddWithValue("@QuestionText", Question_5.Text);

                    Qus5.Parameters.AddWithValue("@CorrectAnswer", CorrectAnswer_5);
                    Qus5.Parameters.AddWithValue("@Answer_by_User", Ans5);
                  
                    Qus5.Parameters.AddWithValue("@Empcode", CL_AgentDetails.OPOID);
                    Qus5.Parameters.AddWithValue("@ProcessID", CL_AgentDetails.ProcessName);
                   // Qus5.ExecuteNonQuery();
                }

                if (Ans1 != "" && Ans2 != "" && Ans3 != "" && Ans4 != "" && Ans5 != "")
                {
                    Qus1.ExecuteNonQuery();
                    Qus2.ExecuteNonQuery();
                    Qus3.ExecuteNonQuery();
                    Qus4.ExecuteNonQuery();
                    Qus5.ExecuteNonQuery();
                    MessageBox.Show("Thank you!!!!....Your Test has been successfully Saved");

                    CTI newform = new CTI();
                    newform.Show();
                    this.Hide();

                    this.Close();
                  
                }

                else
                {

                    MessageBox.Show("Please Answer The All Question", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           //emd

                //SqlCommand cmd = new SqlCommand("", conobj.getconn());
               // cmd.Parameters.AddWithValue("@Empcode",);
            
            /*if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                   
                }
            }*/
        }
    }
}
