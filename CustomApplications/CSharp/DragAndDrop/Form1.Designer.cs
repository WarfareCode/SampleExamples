namespace DragAndDrop
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
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.axAgUiAx2DCntrl2 = new AGI.STKX.Controls.AxAgUiAx2DCntrl();
            this.MenuRoot = new System.Windows.Forms.ContextMenu();
            this.MenuView2D = new System.Windows.Forms.MenuItem();
            this.MenuView3D = new System.Windows.Forms.MenuItem();
            this.MenuDropMenu = new System.Windows.Forms.MenuItem();
            this.MenuAutomatic = new System.Windows.Forms.MenuItem();
            this.MenuManual = new System.Windows.Forms.MenuItem();
            this.MenuNone = new System.Windows.Forms.MenuItem();
            this.MenuZoomIn = new System.Windows.Forms.MenuItem();
            this.MenuZoomOut = new System.Windows.Forms.MenuItem();
            this.MenuHomeView = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAxVOCntrl1.Picture")));
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(680, 589);
            this.axAgUiAxVOCntrl1.TabIndex = 0;
            this.axAgUiAxVOCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.axAgUiAxVOCntrl1_MouseDownEvent);
            this.axAgUiAxVOCntrl1.MouseUpEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEventHandler(this.axAgUiAxVOCntrl1_MouseUpEvent);
            this.axAgUiAxVOCntrl1.OLEDragDrop += new AxAGI.STKX.IAgUiAxVOCntrlEvents_OLEDragDropEventHandler(this.axAgUiAxVOCntrl1_OLEDragDrop);
            // 
            // axAgUiAx2DCntrl2
            // 
            this.axAgUiAx2DCntrl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAx2DCntrl2.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAx2DCntrl2.Name = "axAgUiAx2DCntrl2";
            this.axAgUiAx2DCntrl2.NoLogo = true;
            this.axAgUiAx2DCntrl2.PanModeEnabled = true;
            this.axAgUiAx2DCntrl2.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAx2DCntrl2.Picture")));
            this.axAgUiAx2DCntrl2.Size = new System.Drawing.Size(680, 589);
            this.axAgUiAx2DCntrl2.TabIndex = 0;
            this.axAgUiAx2DCntrl2.MouseDownEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEventHandler(this.axAgUiAx2DCntrl2_MouseDownEvent);
            this.axAgUiAx2DCntrl2.MouseUpEvent += new AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseUpEventHandler(this.axAgUiAx2DCntrl2_MouseUpEvent);
            this.axAgUiAx2DCntrl2.OLEDragDrop += new AxAGI.STKX.IAgUiAx2DCntrlEvents_OLEDragDropEventHandler(this.axAgUiAx2DCntrl2_OLEDragDrop);
            // 
            // MenuRoot
            // 
            this.MenuRoot.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuView2D,
            this.MenuView3D,
            this.MenuDropMenu,
            this.MenuZoomIn,
            this.MenuZoomOut,
            this.MenuHomeView});
            // 
            // MenuView2D
            // 
            this.MenuView2D.Index = 0;
            this.MenuView2D.Text = "2D View";
            this.MenuView2D.Click += new System.EventHandler(this.MenuView2D_Click);
            // 
            // MenuView3D
            // 
            this.MenuView3D.Checked = true;
            this.MenuView3D.Index = 1;
            this.MenuView3D.Text = "3D View";
            this.MenuView3D.Click += new System.EventHandler(this.MenuView3D_Click);
            // 
            // MenuDropMenu
            // 
            this.MenuDropMenu.Index = 2;
            this.MenuDropMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuAutomatic,
            this.MenuManual,
            this.MenuNone});
            this.MenuDropMenu.Text = "Drop Mode";
            // 
            // MenuAutomatic
            // 
            this.MenuAutomatic.Index = 0;
            this.MenuAutomatic.Text = "Automatic";
            this.MenuAutomatic.Click += new System.EventHandler(this.MenuAutomatic_Click);
            // 
            // MenuManual
            // 
            this.MenuManual.Checked = true;
            this.MenuManual.Index = 1;
            this.MenuManual.Text = "Manual";
            this.MenuManual.Click += new System.EventHandler(this.MenuManual_Click);
            // 
            // MenuNone
            // 
            this.MenuNone.Index = 2;
            this.MenuNone.Text = "None";
            this.MenuNone.Click += new System.EventHandler(this.MenuNone_Click);
            // 
            // MenuZoomIn
            // 
            this.MenuZoomIn.Index = 3;
            this.MenuZoomIn.Text = "Zoom In";
            this.MenuZoomIn.Click += new System.EventHandler(this.MenuZoomIn_Click);
            // 
            // MenuZoomOut
            // 
            this.MenuZoomOut.Index = 4;
            this.MenuZoomOut.Text = "Zoom Out";
            this.MenuZoomOut.Click += new System.EventHandler(this.MenuZoomOut_Click);
            // 
            // MenuHomeView
            // 
            this.MenuHomeView.Index = 5;
            this.MenuHomeView.Text = "Home View";
            this.MenuHomeView.Click += new System.EventHandler(this.MenuHomeView_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(671, 589);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.axAgUiAx2DCntrl2);
            this.Name = "Form1";
            this.Text = "DragAndDrop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }
        #endregion

        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private AGI.STKX.Controls.AxAgUiAx2DCntrl axAgUiAx2DCntrl2;
        private System.Windows.Forms.MenuItem MenuView2D;
        private System.Windows.Forms.MenuItem MenuView3D;
        private System.Windows.Forms.MenuItem MenuDropMenu;
        private System.Windows.Forms.MenuItem MenuZoomIn;
        private System.Windows.Forms.MenuItem MenuHomeView;
        private System.Windows.Forms.MenuItem MenuAutomatic;
        private System.Windows.Forms.MenuItem MenuManual;
        private System.Windows.Forms.MenuItem MenuNone;
        private System.Windows.Forms.MenuItem MenuZoomOut;
        private System.Windows.Forms.ContextMenu MenuRoot;
    }
}