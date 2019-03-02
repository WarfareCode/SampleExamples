namespace ObscurationTool
{
    partial class ObscurationTool
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
            CleanReportFile();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObscurationTool));
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.btnNewSensor = new System.Windows.Forms.Button();
            this.btnCloseScenario = new System.Windows.Forms.Button();
            this.btnNewSat = new System.Windows.Forms.Button();
            this.btnCompute = new System.Windows.Forms.Button();
            this.btnNewScenario = new System.Windows.Forms.Button();
            this.AxAgUiAxGfxAnalysisCntrl1 = new AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl();
            this.btnReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAxVOCntrl1.Picture")));
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(576, 304);
            this.axAgUiAxVOCntrl1.TabIndex = 0;
            // 
            // btnNewSensor
            // 
            this.btnNewSensor.Location = new System.Drawing.Point(584, 144);
            this.btnNewSensor.Name = "btnNewSensor";
            this.btnNewSensor.Size = new System.Drawing.Size(104, 48);
            this.btnNewSensor.TabIndex = 22;
            this.btnNewSensor.Text = "New Sensor";
            this.btnNewSensor.Click += new System.EventHandler(this.btnNewSensor_Click);
            // 
            // btnCloseScenario
            // 
            this.btnCloseScenario.Location = new System.Drawing.Point(584, 336);
            this.btnCloseScenario.Name = "btnCloseScenario";
            this.btnCloseScenario.Size = new System.Drawing.Size(104, 56);
            this.btnCloseScenario.TabIndex = 21;
            this.btnCloseScenario.Text = "Close Scenario";
            this.btnCloseScenario.Click += new System.EventHandler(this.btnCloseScenario_Click);
            // 
            // btnNewSat
            // 
            this.btnNewSat.Location = new System.Drawing.Point(584, 80);
            this.btnNewSat.Name = "btnNewSat";
            this.btnNewSat.Size = new System.Drawing.Size(104, 48);
            this.btnNewSat.TabIndex = 20;
            this.btnNewSat.Text = "New Satellite";
            this.btnNewSat.Click += new System.EventHandler(this.btnNewSat_Click);
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(584, 208);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(104, 48);
            this.btnCompute.TabIndex = 19;
            this.btnCompute.Text = "Compute";
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // btnNewScenario
            // 
            this.btnNewScenario.Location = new System.Drawing.Point(584, 16);
            this.btnNewScenario.Name = "btnNewScenario";
            this.btnNewScenario.Size = new System.Drawing.Size(104, 48);
            this.btnNewScenario.TabIndex = 18;
            this.btnNewScenario.Text = "New Scenario";
            this.btnNewScenario.Click += new System.EventHandler(this.btnNewScenario_Click);
            // 
            // AxAgUiAxGfxAnalysisCntrl1
            // 
            this.AxAgUiAxGfxAnalysisCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AxAgUiAxGfxAnalysisCntrl1.ControlMode = AGI.STKX.AgEGfxAnalysisMode.eObscurationTool;
            this.AxAgUiAxGfxAnalysisCntrl1.Location = new System.Drawing.Point(0, 304);
            this.AxAgUiAxGfxAnalysisCntrl1.Name = "AxAgUiAxGfxAnalysisCntrl1";
            this.AxAgUiAxGfxAnalysisCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("AxAgUiAxGfxAnalysisCntrl1.Picture")));
            this.AxAgUiAxGfxAnalysisCntrl1.Size = new System.Drawing.Size(576, 312);
            this.AxAgUiAxGfxAnalysisCntrl1.TabIndex = 17;
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(584, 272);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(104, 48);
            this.btnReport.TabIndex = 23;
            this.btnReport.Text = "Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // ObscurationTool
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(695, 605);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnNewSensor);
            this.Controls.Add(this.btnCloseScenario);
            this.Controls.Add(this.btnNewSat);
            this.Controls.Add(this.btnCompute);
            this.Controls.Add(this.btnNewScenario);
            this.Controls.Add(this.AxAgUiAxGfxAnalysisCntrl1);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Name = "ObscurationTool";
            this.Text = "Obscuration Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ObscurationTool_FormClosing);
            this.Load += new System.EventHandler(this.ObscurationTool_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        internal System.Windows.Forms.Button btnNewSensor;
        internal System.Windows.Forms.Button btnCloseScenario;
        internal System.Windows.Forms.Button btnNewSat;
        internal System.Windows.Forms.Button btnCompute;
        internal System.Windows.Forms.Button btnNewScenario;
        internal AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl AxAgUiAxGfxAnalysisCntrl1;
        internal System.Windows.Forms.Button btnReport;
    }
}