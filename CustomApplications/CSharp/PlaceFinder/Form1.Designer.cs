namespace PlaceFinder
{
    partial class PlaceFinderForm
    {
        /// <summary>
        /// Required designer variable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaceFinderForm));
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.statusBar.Location = new System.Drawing.Point(0, 563);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(893, 28);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 1;
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(896, 560);
            this.axAgUiAxVOCntrl1.TabIndex = 2;
            this.axAgUiAxVOCntrl1.ClickEvent += new System.EventHandler(this.OnVOCntrlClick);
            this.axAgUiAxVOCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.OnVOCntrlMouseDown);
            this.axAgUiAxVOCntrl1.MouseMoveEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEventHandler(this.OnVOCntrlMouseMove);
            // 
            // PlaceFinderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(892, 587);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.statusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlaceFinderForm";
            this.Text = "STK/X Place Finder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlaceFinderForm_FormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.StatusBar statusBar;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
    }
}