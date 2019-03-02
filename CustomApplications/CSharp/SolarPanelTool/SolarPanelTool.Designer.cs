namespace SolarPanelTool
{
    partial class SolarPanelTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolarPanelTool));
            this.btnCloseScenario = new System.Windows.Forms.Button();
            this.btnNewSat = new System.Windows.Forms.Button();
            this.btnCompute = new System.Windows.Forms.Button();
            this.btnNewScenario = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.axAgUiAxGfxAnalysisCntrl1 = new AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl();
            this.SuspendLayout();
            // 
            // btnCloseScenario
            // 
            this.btnCloseScenario.Location = new System.Drawing.Point(584, 272);
            this.btnCloseScenario.Name = "btnCloseScenario";
            this.btnCloseScenario.Size = new System.Drawing.Size(104, 56);
            this.btnCloseScenario.TabIndex = 10;
            this.btnCloseScenario.Text = "Close Scenario";
            this.btnCloseScenario.Click += new System.EventHandler(this.btnCloseScenario_Click);
            // 
            // btnNewSat
            // 
            this.btnNewSat.Location = new System.Drawing.Point(584, 80);
            this.btnNewSat.Name = "btnNewSat";
            this.btnNewSat.Size = new System.Drawing.Size(104, 48);
            this.btnNewSat.TabIndex = 9;
            this.btnNewSat.Text = "New Satellite";
            this.btnNewSat.Click += new System.EventHandler(this.btnNewSat_Click);
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(584, 144);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(104, 48);
            this.btnCompute.TabIndex = 8;
            this.btnCompute.Text = "Compute";
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // btnNewScenario
            // 
            this.btnNewScenario.Location = new System.Drawing.Point(584, 16);
            this.btnNewScenario.Name = "btnNewScenario";
            this.btnNewScenario.Size = new System.Drawing.Size(104, 48);
            this.btnNewScenario.TabIndex = 7;
            this.btnNewScenario.Text = "New Scenario";
            this.btnNewScenario.Click += new System.EventHandler(this.btnNewScenario_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(584, 208);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(104, 48);
            this.btnReport.TabIndex = 11;
            this.btnReport.Text = "Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(578, 328);
            this.axAgUiAxVOCntrl1.TabIndex = 12;
            // 
            // axAgUiAxGfxAnalysisCntrl1
            // 
            this.axAgUiAxGfxAnalysisCntrl1.Enabled = true;
            this.axAgUiAxGfxAnalysisCntrl1.Location = new System.Drawing.Point(0, 324);
            this.axAgUiAxGfxAnalysisCntrl1.Name = "axAgUiAxGfxAnalysisCntrl1";
            this.axAgUiAxGfxAnalysisCntrl1.Size = new System.Drawing.Size(578, 284);
            this.axAgUiAxGfxAnalysisCntrl1.TabIndex = 13;
            // 
            // SolarPanelTool
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(695, 605);
            this.Controls.Add(this.axAgUiAxGfxAnalysisCntrl1);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnCloseScenario);
            this.Controls.Add(this.btnNewSat);
            this.Controls.Add(this.btnCompute);
            this.Controls.Add(this.btnNewScenario);
            this.Name = "SolarPanelTool";
            this.Text = "Solar Panel Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SolarPanelTool_FormClosing);
            this.ResumeLayout(false);

        }
        #endregion

        internal System.Windows.Forms.Button btnCloseScenario;
        internal System.Windows.Forms.Button btnNewSat;
        internal System.Windows.Forms.Button btnCompute;
        internal System.Windows.Forms.Button btnNewScenario;
        internal System.Windows.Forms.Button btnReport;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl axAgUiAxGfxAnalysisCntrl1;
    }
}