namespace OMDemo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loadSatButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.loadFacilityButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ComputeButton = new System.Windows.Forms.Button();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.loadSatButton);
            this.groupBox1.Location = new System.Drawing.Point(8, 492);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 128);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Satellite Setup";
            // 
            // loadSatButton
            // 
            this.loadSatButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.loadSatButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadSatButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.loadSatButton.Location = new System.Drawing.Point(24, 24);
            this.loadSatButton.Name = "loadSatButton";
            this.loadSatButton.Size = new System.Drawing.Size(307, 89);
            this.loadSatButton.TabIndex = 7;
            this.loadSatButton.Text = "Load Satellites From Database";
            this.loadSatButton.Click += new System.EventHandler(this.loadSatButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.loadFacilityButton);
            this.groupBox2.Location = new System.Drawing.Point(382, 492);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 128);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Facility Setup";
            // 
            // loadFacilityButton
            // 
            this.loadFacilityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadFacilityButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadFacilityButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.loadFacilityButton.Location = new System.Drawing.Point(32, 24);
            this.loadFacilityButton.Name = "loadFacilityButton";
            this.loadFacilityButton.Size = new System.Drawing.Size(181, 89);
            this.loadFacilityButton.TabIndex = 0;
            this.loadFacilityButton.Text = "Load Facility From XML";
            this.loadFacilityButton.Click += new System.EventHandler(this.loadFacilityButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ComputeButton);
            this.groupBox3.Location = new System.Drawing.Point(630, 492);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 128);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Access";
            // 
            // ComputeButton
            // 
            this.ComputeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ComputeButton.Enabled = false;
            this.ComputeButton.Location = new System.Drawing.Point(12, 24);
            this.ComputeButton.Name = "ComputeButton";
            this.ComputeButton.Size = new System.Drawing.Size(92, 88);
            this.ComputeButton.TabIndex = 1;
            this.ComputeButton.Text = "Compute And Write To XML";
            this.ComputeButton.Click += new System.EventHandler(this.ComputeButton_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(633, 402);
            this.axAgUiAxVOCntrl1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(758, 627);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.MinimumSize = new System.Drawing.Size(533, 501);
            this.Name = "Form1";
            this.Text = "GPSDemo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ComputeButton;
        private System.Windows.Forms.Button loadSatButton;
        private System.Windows.Forms.Button loadFacilityButton;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
    }
}