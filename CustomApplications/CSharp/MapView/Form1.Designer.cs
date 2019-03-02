namespace MapView
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
            this.Command1 = new System.Windows.Forms.Button();
            this.Command4 = new System.Windows.Forms.Button();
            this.Command5 = new System.Windows.Forms.Button();
            this.Check1 = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axAgUiAx2DCntrl1 = new AGI.STKX.Controls.AxAgUiAx2DCntrl();
            this.SuspendLayout();
            // 
            // Command1
            // 
            this.Command1.Location = new System.Drawing.Point(819, 11);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(88, 40);
            this.Command1.TabIndex = 1;
            this.Command1.Text = "Open Scenario";
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // Command4
            // 
            this.Command4.Location = new System.Drawing.Point(819, 107);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(88, 40);
            this.Command4.TabIndex = 2;
            this.Command4.Text = "Zoom In";
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // Command5
            // 
            this.Command5.Location = new System.Drawing.Point(819, 155);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(88, 40);
            this.Command5.TabIndex = 3;
            this.Command5.Text = "Zoom Out";
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // Check1
            // 
            this.Check1.Location = new System.Drawing.Point(819, 67);
            this.Check1.Name = "Check1";
            this.Check1.Size = new System.Drawing.Size(104, 24);
            this.Check1.TabIndex = 4;
            this.Check1.Text = "Allow Pan";
            this.Check1.CheckedChanged += new System.EventHandler(this.Check1_CheckedChanged);
            // 
            // axAgUiAx2DCntrl1
            // 
            this.axAgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAx2DCntrl1.Location = new System.Drawing.Point(7, 10);
            this.axAgUiAx2DCntrl1.MinimumSize = new System.Drawing.Size(800, 400);
            this.axAgUiAx2DCntrl1.Name = "axAgUiAx2DCntrl1";
            this.axAgUiAx2DCntrl1.PanModeEnabled = true;
            this.axAgUiAx2DCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAx2DCntrl1.Picture")));
            this.axAgUiAx2DCntrl1.Size = new System.Drawing.Size(800, 400);
            this.axAgUiAx2DCntrl1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(930, 447);
            this.Controls.Add(this.axAgUiAx2DCntrl1);
            this.Controls.Add(this.Check1);
            this.Controls.Add(this.Command5);
            this.Controls.Add(this.Command4);
            this.Controls.Add(this.Command1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MapView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button Command1;
        private System.Windows.Forms.Button Command4;
        private System.Windows.Forms.Button Command5;
        private System.Windows.Forms.CheckBox Check1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private AGI.STKX.Controls.AxAgUiAx2DCntrl axAgUiAx2DCntrl1;
    }
}