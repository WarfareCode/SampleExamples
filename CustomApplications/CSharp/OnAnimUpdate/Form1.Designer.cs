namespace OnAnimUpdate
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
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.MenuRoot = new System.Windows.Forms.ContextMenu();
            this.MenuLoadScenario = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.MenuAnimStart = new System.Windows.Forms.MenuItem();
            this.MenuAnimPause = new System.Windows.Forms.MenuItem();
            this.MenuAnimReset = new System.Windows.Forms.MenuItem();
            this.MenuAnimFaster = new System.Windows.Forms.MenuItem();
            this.MenuAnimSlower = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.MenuZoomIn = new System.Windows.Forms.MenuItem();
            this.MenuHomeView = new System.Windows.Forms.MenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 551);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(732, 22);
            this.statusBar1.TabIndex = 1;
            // 
            // MenuRoot
            // 
            this.MenuRoot.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuLoadScenario,
            this.menuItem2,
            this.menuItem3});
            // 
            // MenuLoadScenario
            // 
            this.MenuLoadScenario.Index = 0;
            this.MenuLoadScenario.Text = "Load Scenario";
            this.MenuLoadScenario.Click += new System.EventHandler(this.MenuLoadScenario_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuAnimStart,
            this.MenuAnimPause,
            this.MenuAnimReset,
            this.MenuAnimFaster,
            this.MenuAnimSlower});
            this.menuItem2.Text = "Animation";
            // 
            // MenuAnimStart
            // 
            this.MenuAnimStart.Index = 0;
            this.MenuAnimStart.Text = "Start";
            this.MenuAnimStart.Click += new System.EventHandler(this.MenuAnimStart_Click);
            // 
            // MenuAnimPause
            // 
            this.MenuAnimPause.Index = 1;
            this.MenuAnimPause.Text = "Pause";
            this.MenuAnimPause.Click += new System.EventHandler(this.MenuAnimPause_Click);
            // 
            // MenuAnimReset
            // 
            this.MenuAnimReset.Index = 2;
            this.MenuAnimReset.Text = "Reset";
            this.MenuAnimReset.Click += new System.EventHandler(this.MenuAnimReset_Click);
            // 
            // MenuAnimFaster
            // 
            this.MenuAnimFaster.Index = 3;
            this.MenuAnimFaster.Text = "Faster";
            this.MenuAnimFaster.Click += new System.EventHandler(this.MenuAnimFaster_Click);
            // 
            // MenuAnimSlower
            // 
            this.MenuAnimSlower.Index = 4;
            this.MenuAnimSlower.Text = "Slower";
            this.MenuAnimSlower.Click += new System.EventHandler(this.MenuAnimSlower_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuZoomIn,
            this.MenuHomeView});
            this.menuItem3.Text = "View";
            // 
            // MenuZoomIn
            // 
            this.MenuZoomIn.Index = 0;
            this.MenuZoomIn.Text = "Zoom In";
            this.MenuZoomIn.Click += new System.EventHandler(this.MenuZoomIn_Click);
            // 
            // MenuHomeView
            // 
            this.MenuHomeView.Index = 1;
            this.MenuHomeView.Text = "Home View";
            this.MenuHomeView.Click += new System.EventHandler(this.MenuHomeView_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(732, 551);
            this.axAgUiAxVOCntrl1.TabIndex = 2;
            this.axAgUiAxVOCntrl1.OLEDragDrop += new AxAGI.STKX.IAgUiAxVOCntrlEvents_OLEDragDropEventHandler(this.axAgUiAxVOCntrl1_OLEDragDrop);
            this.axAgUiAxVOCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.axAgUiAxVOCntrl1_MouseDownEvent);
            this.axAgUiAxVOCntrl1.MouseUpEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEventHandler(this.axAgUiAxVOCntrl1_MouseUpEvent);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(732, 573);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.statusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "OnAnimUpdate";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ContextMenu MenuRoot;
        private System.Windows.Forms.MenuItem MenuLoadScenario;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem MenuAnimStart;
        private System.Windows.Forms.MenuItem MenuAnimPause;
        private System.Windows.Forms.MenuItem MenuAnimReset;
        private System.Windows.Forms.MenuItem MenuAnimFaster;
        private System.Windows.Forms.MenuItem MenuAnimSlower;
        private System.Windows.Forms.MenuItem MenuZoomIn;
        private System.Windows.Forms.MenuItem MenuHomeView;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
    }
}