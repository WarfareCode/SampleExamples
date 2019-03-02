namespace Events
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
            this.axAgUiAx2DCntrl1 = new AGI.STKX.Controls.AxAgUiAx2DCntrl();
            this.Command2 = new System.Windows.Forms.Button();
            this.Command3 = new System.Windows.Forms.Button();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.Command4 = new System.Windows.Forms.Button();
            this.Trace = new System.Windows.Forms.TextBox();
            this.Command6 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Command1
            // 
            this.Command1.Location = new System.Drawing.Point(8, 8);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(96, 24);
            this.Command1.TabIndex = 2;
            this.Command1.Text = "Open Scenario";
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // axAgUiAx2DCntrl1
            // 
            this.axAgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAx2DCntrl1.Location = new System.Drawing.Point(440, 40);
            this.axAgUiAx2DCntrl1.Name = "axAgUiAx2DCntrl1";
            this.axAgUiAx2DCntrl1.PanModeEnabled = true;
            this.axAgUiAx2DCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAx2DCntrl1.Picture")));
            this.axAgUiAx2DCntrl1.Size = new System.Drawing.Size(500, 440);
            this.axAgUiAx2DCntrl1.TabIndex = 12;
            this.axAgUiAx2DCntrl1.KeyDownEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyDownEventHandler(this.axAgUiAx2DCntrl1_KeyDownEvent);
            this.axAgUiAx2DCntrl1.KeyPressEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyPressEventHandler(this.axAgUiAx2DCntrl1_KeyPressEvent);
            this.axAgUiAx2DCntrl1.KeyUpEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyUpEventHandler(this.axAgUiAx2DCntrl1_KeyUpEvent);
            this.axAgUiAx2DCntrl1.ClickEvent += new System.EventHandler(this.axAgUiAx2DCntrl1_ClickEvent);
            this.axAgUiAx2DCntrl1.DblClick += new System.EventHandler(this.On2DMapDblClick);
            this.axAgUiAx2DCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEventHandler(this.axAgUiAx2DCntrl1_MouseDownEvent);
            this.axAgUiAx2DCntrl1.MouseMoveEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseMoveEventHandler(this.axAgUiAx2DCntrl1_MouseMoveEvent);
            this.axAgUiAx2DCntrl1.MouseUpEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseUpEventHandler(this.axAgUiAx2DCntrl1_MouseUpEvent);
            this.axAgUiAx2DCntrl1.OLEDragDrop += new AxAGI.STKX.IAgUiAx2DCntrlEvents_OLEDragDropEventHandler(this.axAgUiAx2DCntrl1_OLEDragDrop);
            // 
            // Command2
            // 
            this.Command2.Location = new System.Drawing.Point(527, 8);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(104, 24);
            this.Command2.TabIndex = 4;
            this.Command2.Text = "2D - Zoom In";
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Command3
            // 
            this.Command3.Location = new System.Drawing.Point(639, 8);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(88, 24);
            this.Command3.TabIndex = 5;
            this.Command3.Text = "2D - Zoom Out";
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 40);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAxVOCntrl1.Picture")));
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(440, 440);
            this.axAgUiAxVOCntrl1.TabIndex = 11;
            this.axAgUiAxVOCntrl1.KeyDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyDownEventHandler(this.axAgUiAxVOCntrl1_KeyDownEvent);
            this.axAgUiAxVOCntrl1.KeyPressEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyPressEventHandler(this.axAgUiAxVOCntrl1_KeyPressEvent);
            this.axAgUiAxVOCntrl1.KeyUpEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyUpEventHandler(this.axAgUiAxVOCntrl1_KeyUpEvent);
            this.axAgUiAxVOCntrl1.ClickEvent += new System.EventHandler(this.axAgUiAxVOCntrl1_ClickEvent);
            this.axAgUiAxVOCntrl1.DblClick += new System.EventHandler(this.axAgUiAxVOCntrl1_DblClick);
            this.axAgUiAxVOCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.axAgUiAxVOCntrl1_MouseDownEvent);
            this.axAgUiAxVOCntrl1.MouseMoveEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEventHandler(this.axAgUiAxVOCntrl1_MouseMoveEvent);
            this.axAgUiAxVOCntrl1.MouseUpEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEventHandler(this.axAgUiAxVOCntrl1_MouseUpEvent);
            this.axAgUiAxVOCntrl1.OLEDragDrop += new AxAGI.STKX.IAgUiAxVOCntrlEvents_OLEDragDropEventHandler(this.axAgUiAxVOCntrl1_OLEDragDrop);
            // 
            // Command4
            // 
            this.Command4.Location = new System.Drawing.Point(228, 8);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(112, 24);
            this.Command4.TabIndex = 8;
            this.Command4.Text = "3D - Zoom In";
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // Trace
            // 
            this.Trace.Location = new System.Drawing.Point(8, 488);
            this.Trace.Multiline = true;
            this.Trace.Name = "Trace";
            this.Trace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Trace.Size = new System.Drawing.Size(932, 208);
            this.Trace.TabIndex = 9;
            this.Trace.Text = "Trace";
            this.Trace.TextChanged += new System.EventHandler(this.Trace_TextChanged);
            // 
            // Command6
            // 
            this.Command6.Location = new System.Drawing.Point(110, 8);
            this.Command6.Name = "Command6";
            this.Command6.Size = new System.Drawing.Size(112, 24);
            this.Command6.TabIndex = 13;
            this.Command6.Text = "Save Scenario";
            this.Command6.Click += new System.EventHandler(this.Command6_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(735, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Pan Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(943, 705);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Command6);
            this.Controls.Add(this.Trace);
            this.Controls.Add(this.Command4);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.Command3);
            this.Controls.Add(this.Command2);
            this.Controls.Add(this.axAgUiAx2DCntrl1);
            this.Controls.Add(this.Command1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Events";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private AGI.STKX.Controls.AxAgUiAx2DCntrl axAgUiAx2DCntrl1;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private System.Windows.Forms.Button Command2;
        private System.Windows.Forms.Button Command3;
        private System.Windows.Forms.Button Command4;
        private System.Windows.Forms.TextBox Trace;
        private System.Windows.Forms.Button Command1;
        private System.Windows.Forms.Button Command6;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}