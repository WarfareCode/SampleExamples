namespace AzElMaskTool
{
    partial class AzElMaskTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AzElMaskTool));
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.AxAgUiAxGfxAnalysisCntrl1 = new AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl();
            this.btnNewSensor = new System.Windows.Forms.Button();
            this.btnCloseScenario = new System.Windows.Forms.Button();
            this.btnNewSat = new System.Windows.Forms.Button();
            this.btnCompute = new System.Windows.Forms.Button();
            this.btnNewScenario = new System.Windows.Forms.Button();
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
            // AxAgUiAxGfxAnalysisCntrl1
            // 
            this.AxAgUiAxGfxAnalysisCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AxAgUiAxGfxAnalysisCntrl1.ControlMode = AGI.STKX.AgEGfxAnalysisMode.eAzElMaskTool;
            this.AxAgUiAxGfxAnalysisCntrl1.Location = new System.Drawing.Point(184, 304);
            this.AxAgUiAxGfxAnalysisCntrl1.Name = "AxAgUiAxGfxAnalysisCntrl1";
            this.AxAgUiAxGfxAnalysisCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("AxAgUiAxGfxAnalysisCntrl1.Picture")));
            this.AxAgUiAxGfxAnalysisCntrl1.Size = new System.Drawing.Size(280, 280);
            this.AxAgUiAxGfxAnalysisCntrl1.TabIndex = 29;
            // 
            // btnNewSensor
            // 
            this.btnNewSensor.Location = new System.Drawing.Point(584, 152);
            this.btnNewSensor.Name = "btnNewSensor";
            this.btnNewSensor.Size = new System.Drawing.Size(104, 48);
            this.btnNewSensor.TabIndex = 28;
            this.btnNewSensor.Text = "New Sensor";
            this.btnNewSensor.Click += new System.EventHandler(this.btnNewSensor_Click);
            // 
            // btnCloseScenario
            // 
            this.btnCloseScenario.Location = new System.Drawing.Point(584, 344);
            this.btnCloseScenario.Name = "btnCloseScenario";
            this.btnCloseScenario.Size = new System.Drawing.Size(104, 56);
            this.btnCloseScenario.TabIndex = 27;
            this.btnCloseScenario.Text = "Close Scenario";
            this.btnCloseScenario.Click += new System.EventHandler(this.btnCloseScenario_Click);
            // 
            // btnNewSat
            // 
            this.btnNewSat.Location = new System.Drawing.Point(584, 88);
            this.btnNewSat.Name = "btnNewSat";
            this.btnNewSat.Size = new System.Drawing.Size(104, 48);
            this.btnNewSat.TabIndex = 26;
            this.btnNewSat.Text = "New Satellite";
            this.btnNewSat.Click += new System.EventHandler(this.btnNewSat_Click);
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(584, 216);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(104, 48);
            this.btnCompute.TabIndex = 25;
            this.btnCompute.Text = "Compute";
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // btnNewScenario
            // 
            this.btnNewScenario.Location = new System.Drawing.Point(584, 24);
            this.btnNewScenario.Name = "btnNewScenario";
            this.btnNewScenario.Size = new System.Drawing.Size(104, 48);
            this.btnNewScenario.TabIndex = 24;
            this.btnNewScenario.Text = "New Scenario";
            this.btnNewScenario.Click += new System.EventHandler(this.btnNewScenario_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(584, 280);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(104, 48);
            this.btnReport.TabIndex = 30;
            this.btnReport.Text = "Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // AzElMaskTool
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(693, 588);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.AxAgUiAxGfxAnalysisCntrl1);
            this.Controls.Add(this.btnNewSensor);
            this.Controls.Add(this.btnCloseScenario);
            this.Controls.Add(this.btnNewSat);
            this.Controls.Add(this.btnCompute);
            this.Controls.Add(this.btnNewScenario);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Name = "AzElMaskTool";
            this.Text = "AzElMask Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AzElMaskTool_FormClosing);
            this.Load += new System.EventHandler(this.AzElMaskTool_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        internal AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl AxAgUiAxGfxAnalysisCntrl1;
        internal System.Windows.Forms.Button btnNewSensor;
        internal System.Windows.Forms.Button btnCloseScenario;
        internal System.Windows.Forms.Button btnNewSat;
        internal System.Windows.Forms.Button btnCompute;
        internal System.Windows.Forms.Button btnNewScenario;
        internal System.Windows.Forms.Button btnReport;
    }
}