namespace RubberBandSelect
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
            this.BtnLoadISRScen = new System.Windows.Forms.Button();
            this.BtnSelectObjs = new System.Windows.Forms.Button();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.SuspendLayout();
            // 
            // BtnLoadISRScen
            // 
            this.BtnLoadISRScen.Location = new System.Drawing.Point(720, 16);
            this.BtnLoadISRScen.Name = "BtnLoadISRScen";
            this.BtnLoadISRScen.Size = new System.Drawing.Size(112, 40);
            this.BtnLoadISRScen.TabIndex = 1;
            this.BtnLoadISRScen.Text = "Load ISR Scenario";
            this.BtnLoadISRScen.Click += new System.EventHandler(this.BtnLoadISRScen_Click);
            // 
            // BtnSelectObjs
            // 
            this.BtnSelectObjs.Location = new System.Drawing.Point(720, 72);
            this.BtnSelectObjs.Name = "BtnSelectObjs";
            this.BtnSelectObjs.Size = new System.Drawing.Size(112, 40);
            this.BtnSelectObjs.TabIndex = 2;
            this.BtnSelectObjs.Text = "Select objects";
            this.BtnSelectObjs.Click += new System.EventHandler(this.BtnSelectObjs_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(12, 16);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(683, 665);
            this.axAgUiAxVOCntrl1.TabIndex = 3;
            this.axAgUiAxVOCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.axAgUiAxVOCntrl1_MouseDownEvent);
            this.axAgUiAxVOCntrl1.MouseMoveEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEventHandler(this.axAgUiAxVOCntrl1_MouseMoveEvent);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(840, 698);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.BtnSelectObjs);
            this.Controls.Add(this.BtnLoadISRScen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "RubberBandSelect";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button BtnLoadISRScen;
        private System.Windows.Forms.Button BtnSelectObjs;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
    }
}