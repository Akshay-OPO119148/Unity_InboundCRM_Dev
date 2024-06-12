namespace OneCRM
{
    partial class SearchCustomerDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_CustomerDetails = new System.Windows.Forms.Panel();
            this.dgv_CustomerDetails = new System.Windows.Forms.DataGridView();
            this.btn_Search = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.txt_TicketNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_IcegateId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Phone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pnl_CustomerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CustomerDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_CustomerDetails
            // 
            this.pnl_CustomerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CustomerDetails.BackColor = System.Drawing.Color.Gray;
            this.pnl_CustomerDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_CustomerDetails.Controls.Add(this.dgv_CustomerDetails);
            this.pnl_CustomerDetails.Controls.Add(this.btn_Search);
            this.pnl_CustomerDetails.Controls.Add(this.label37);
            this.pnl_CustomerDetails.Controls.Add(this.txt_TicketNo);
            this.pnl_CustomerDetails.Controls.Add(this.label2);
            this.pnl_CustomerDetails.Controls.Add(this.txt_IcegateId);
            this.pnl_CustomerDetails.Controls.Add(this.label1);
            this.pnl_CustomerDetails.Controls.Add(this.txt_Phone);
            this.pnl_CustomerDetails.Controls.Add(this.label7);
            this.pnl_CustomerDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_CustomerDetails.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnl_CustomerDetails.Location = new System.Drawing.Point(-1, 1);
            this.pnl_CustomerDetails.Name = "pnl_CustomerDetails";
            this.pnl_CustomerDetails.Size = new System.Drawing.Size(1278, 349);
            this.pnl_CustomerDetails.TabIndex = 3;
            // 
            // dgv_CustomerDetails
            // 
            this.dgv_CustomerDetails.AllowUserToAddRows = false;
            this.dgv_CustomerDetails.AllowUserToDeleteRows = false;
            this.dgv_CustomerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_CustomerDetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_CustomerDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CustomerDetails.Location = new System.Drawing.Point(3, 39);
            this.dgv_CustomerDetails.Name = "dgv_CustomerDetails";
            this.dgv_CustomerDetails.ReadOnly = true;
            this.dgv_CustomerDetails.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgv_CustomerDetails.Size = new System.Drawing.Size(1261, 305);
            this.dgv_CustomerDetails.TabIndex = 47;
            this.dgv_CustomerDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CustomerDetails_CellDoubleClick);
            // 
            // btn_Search
            // 
            this.btn_Search.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Search.Location = new System.Drawing.Point(1127, 10);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(73, 23);
            this.btn_Search.TabIndex = 46;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label37.Location = new System.Drawing.Point(9, 313);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(105, 13);
            this.label37.TabIndex = 10;
            this.label37.Text = "CUSTOMER ID  :";
            this.label37.Visible = false;
            // 
            // txt_TicketNo
            // 
            this.txt_TicketNo.Location = new System.Drawing.Point(849, 10);
            this.txt_TicketNo.Name = "txt_TicketNo";
            this.txt_TicketNo.Size = new System.Drawing.Size(272, 20);
            this.txt_TicketNo.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(763, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "TICKET NO :";
            // 
            // txt_IcegateId
            // 
            this.txt_IcegateId.Location = new System.Drawing.Point(475, 10);
            this.txt_IcegateId.Name = "txt_IcegateId";
            this.txt_IcegateId.Size = new System.Drawing.Size(272, 20);
            this.txt_IcegateId.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(389, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ICEGATE ID :";
            // 
            // txt_Phone
            // 
            this.txt_Phone.Location = new System.Drawing.Point(98, 10);
            this.txt_Phone.Name = "txt_Phone";
            this.txt_Phone.Size = new System.Drawing.Size(272, 20);
            this.txt_Phone.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(12, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "PHONE NO :";
            // 
            // SearchCustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 350);
            this.Controls.Add(this.pnl_CustomerDetails);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchCustomerDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchCustomerDetails";
            this.pnl_CustomerDetails.ResumeLayout(false);
            this.pnl_CustomerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CustomerDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_CustomerDetails;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txt_Phone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_CustomerDetails;
        private System.Windows.Forms.TextBox txt_TicketNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_IcegateId;
        private System.Windows.Forms.Label label1;
    }
}