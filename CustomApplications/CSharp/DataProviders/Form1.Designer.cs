namespace DataProviders
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
            this.GraphButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.MarkerColorButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MarkerColorButton2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.placetext = new System.Windows.Forms.Label();
            this.PlaceFacilityButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ComputeButton = new System.Windows.Forms.Button();
            this.axAgUiAx2DCntrl1 = new AGI.STKX.Controls.AxAgUiAx2DCntrl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // GraphButton
            // 
            this.GraphButton.Enabled = false;
            this.GraphButton.Location = new System.Drawing.Point(21, 74);
            this.GraphButton.Name = "GraphButton";
            this.GraphButton.Size = new System.Drawing.Size(80, 32);
            this.GraphButton.TabIndex = 0;
            this.GraphButton.Text = "Graph";
            this.GraphButton.Click += new System.EventHandler(this.GraphButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.MarkerColorButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(8, 408);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 128);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Satellite Setup";
            // 
            // checkBox1
            // 
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(40, 92);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(152, 24);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Use Elevation Contours";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // MarkerColorButton
            // 
            this.MarkerColorButton.BackColor = System.Drawing.Color.IndianRed;
            this.MarkerColorButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MarkerColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MarkerColorButton.Location = new System.Drawing.Point(104, 56);
            this.MarkerColorButton.Name = "MarkerColorButton";
            this.MarkerColorButton.Size = new System.Drawing.Size(88, 24);
            this.MarkerColorButton.TabIndex = 4;
            this.MarkerColorButton.UseVisualStyleBackColor = false;
            this.MarkerColorButton.Click += new System.EventHandler(this.MarkerColorButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "MarkerColor:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Propagator:";
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(88, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(128, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MarkerColorButton2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.placetext);
            this.groupBox2.Controls.Add(this.PlaceFacilityButton);
            this.groupBox2.Location = new System.Drawing.Point(256, 408);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 128);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Facility Setup";
            // 
            // MarkerColorButton2
            // 
            this.MarkerColorButton2.BackColor = System.Drawing.Color.Snow;
            this.MarkerColorButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MarkerColorButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MarkerColorButton2.Location = new System.Drawing.Point(100, 56);
            this.MarkerColorButton2.Name = "MarkerColorButton2";
            this.MarkerColorButton2.Size = new System.Drawing.Size(124, 24);
            this.MarkerColorButton2.TabIndex = 6;
            this.MarkerColorButton2.UseVisualStyleBackColor = false;
            this.MarkerColorButton2.Click += new System.EventHandler(this.MarkerColorButton2_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "LabelColor:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Location:";
            // 
            // placetext
            // 
            this.placetext.Location = new System.Drawing.Point(24, 89);
            this.placetext.Name = "placetext";
            this.placetext.Size = new System.Drawing.Size(200, 32);
            this.placetext.TabIndex = 1;
            this.placetext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlaceFacilityButton
            // 
            this.PlaceFacilityButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PlaceFacilityButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PlaceFacilityButton.Location = new System.Drawing.Point(100, 24);
            this.PlaceFacilityButton.Name = "PlaceFacilityButton";
            this.PlaceFacilityButton.Size = new System.Drawing.Size(124, 21);
            this.PlaceFacilityButton.TabIndex = 0;
            this.PlaceFacilityButton.Text = "Place Facility On Map";
            this.PlaceFacilityButton.Click += new System.EventHandler(this.PlaceFacilityButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ComputeButton);
            this.groupBox3.Controls.Add(this.GraphButton);
            this.groupBox3.Location = new System.Drawing.Point(504, 408);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 128);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Access";
            // 
            // ComputeButton
            // 
            this.ComputeButton.Location = new System.Drawing.Point(22, 29);
            this.ComputeButton.Name = "ComputeButton";
            this.ComputeButton.Size = new System.Drawing.Size(80, 32);
            this.ComputeButton.TabIndex = 1;
            this.ComputeButton.Text = "Compute";
            this.ComputeButton.Click += new System.EventHandler(this.ComputeButton_Click);
            // 
            // axAgUiAx2DCntrl1
            // 
            this.axAgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAx2DCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAx2DCntrl1.Name = "axAgUiAx2DCntrl1";
            this.axAgUiAx2DCntrl1.NoLogo = true;
            this.axAgUiAx2DCntrl1.PanModeEnabled = true;
            this.axAgUiAx2DCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAx2DCntrl1.Picture")));
            this.axAgUiAx2DCntrl1.Size = new System.Drawing.Size(647, 402);
            this.axAgUiAx2DCntrl1.TabIndex = 5;
            this.axAgUiAx2DCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEventHandler(this.axAgUiAx2DCntrl1_MouseDownEvent);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(633, 532);
            this.Controls.Add(this.axAgUiAx2DCntrl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(653, 575);
            this.MinimumSize = new System.Drawing.Size(653, 575);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private AGI.STKX.Controls.AxAgUiAx2DCntrl axAgUiAx2DCntrl1;
        private System.Windows.Forms.Button PlaceFacilityButton;
        private System.Windows.Forms.Label placetext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button MarkerColorButton2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ComputeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button MarkerColorButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button GraphButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}