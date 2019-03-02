using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;



namespace DragAndDrop
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		private AGI.STKObjects.AgStkObjectRoot rootObject = null;

		private int oldX;
		private int oldY;
		private int oldButton;

        private AGI.STKObjects.AgStkObjectRoot root
        {
            get
            {
                if (rootObject == null)
                {
                    rootObject = new AGI.STKObjects.AgStkObjectRoot();
                }
                return rootObject;
            }
        }

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            AGI.STKX.AgSTKXApplication STKXApp = null;
            try
            {
                STKXApp = new AGI.STKX.AgSTKXApplication();

                if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeEngineRuntime))
                {
                    MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (System.Runtime.InteropServices.COMException exception)
            {
                if (exception.ErrorCode == unchecked((int)0x80040154))
                {
                    string errorMessage = "Could not instantiate AgSTKXApplication.";
                    errorMessage += Environment.NewLine;
                    errorMessage += Environment.NewLine;
                    errorMessage += "Check that STK or STK Engine 64-bit is installed on this machine.";

                    MessageBox.Show(errorMessage, "STK Engine Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    throw;
                }
            }
            if (STKXApp != null)
            {
                if (STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl))
                {
                    Application.Run(new Form1());
                }
                else
                {
                    MessageBox.Show("Globe is disabled due to your current license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form1 app = new Form1();
                    app.axAgUiAxVOCntrl1.Visible = false;
                    app.MenuView3D.Enabled = false;
                    app.MenuView3D.Checked = false;
                    app.MenuView2D.Checked = true;
                    app.MenuHomeView.Visible = false;
                    Application.Run(app);
                }
            }
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{			
			root.NewScenario("Test");
			root.ExecuteCommand( "MapAttribs * ScenTime Display On Blue");
			this.axAgUiAx2DCntrl2.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual;
			this.axAgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual;
		}

		private void DoMouseDown(int Button, int Shift, int x, int y)
		{	
			oldX = x;
			oldY = y;
			oldButton = Button;
		}

		private void DoMouseUp(int Button, int Shift, int x, int y)
		{
			if ( oldButton == 2 & Shift == 0 & Math.Abs(oldX-x) < 10 & Math.Abs(oldY-y) < 10 )
				this.MenuRoot.Show(this, new Point(x,y));
		}

		private void DoOLEDragDrop(AGI.STKX.IAgDataObject Data, long Effect,
			int Button, int Shift, long x, long y)
		{
			for (int file=0 ; file < Data.Files.Count ; file++ )
			{

				string line;
				using (StreamReader sr = new StreamReader(Data.Files[file])) 
				{
					while ((line = sr.ReadLine()) != null)
					{
						try
						{
							root.ExecuteCommand(line);
						}
						catch (System.Runtime.InteropServices.COMException /*ex*/)
						{
							
						}

					}
				}
			}
		}

		private void axAgUiAx2DCntrl2_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEvent e)
		{
			DoMouseDown(e.button, e.shift, e.x, e.y);
		}

		private void axAgUiAx2DCntrl2_MouseUpEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseUpEvent e)
		{
			DoMouseUp(e.button, e.shift, e.x, e.y);
		}

		private void axAgUiAxVOCntrl1_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
		{
			DoMouseDown(e.button, e.shift, e.x, e.y);
		}

		private void axAgUiAxVOCntrl1_MouseUpEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent e)
		{
			DoMouseUp(e.button, e.shift, e.x, e.y);
		}

		private void axAgUiAxVOCntrl1_OLEDragDrop(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_OLEDragDropEvent e)
		{
			DoOLEDragDrop(e.data, e.effect, e.button, e.shift, e.x, e.y);
		}

		private void axAgUiAx2DCntrl2_OLEDragDrop(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_OLEDragDropEvent e)
		{
			DoOLEDragDrop(e.data, e.effect, e.button, e.shift, e.x, e.y);
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			this.axAgUiAxVOCntrl1.Left = 0;
			this.axAgUiAxVOCntrl1.Top = 0;
			this.axAgUiAxVOCntrl1.Width = this.ClientRectangle.Width;
			this.axAgUiAxVOCntrl1.Height = this.ClientRectangle.Height;


			this.axAgUiAx2DCntrl2.Left = 0;
			this.axAgUiAx2DCntrl2.Top = 0;
			this.axAgUiAx2DCntrl2.Width = this.ClientRectangle.Width;
			this.axAgUiAx2DCntrl2.Height = this.ClientRectangle.Height;
		}

		private void MenuAutomatic_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAx2DCntrl2.OLEDropMode = AGI.STKX.AgEOLEDropMode.eAutomatic;
			this.axAgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eAutomatic;

			MenuAutomatic.Checked = true;
			MenuNone.Checked = false;
			MenuManual.Checked = false;
		}

		private void MenuManual_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAx2DCntrl2.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual;
			this.axAgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eManual;

			MenuAutomatic.Checked = false;
			MenuNone.Checked = false;
			MenuManual.Checked = true;
		}

		private void MenuNone_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAx2DCntrl2.OLEDropMode = AGI.STKX.AgEOLEDropMode.eNone;
			this.axAgUiAxVOCntrl1.OLEDropMode = AGI.STKX.AgEOLEDropMode.eNone;

			MenuAutomatic.Checked = false;
			MenuNone.Checked = true;
			MenuManual.Checked = false;
		}

		private void MenuHomeView_Click(object sender, System.EventArgs e)
		{
			root.ExecuteCommand("VO * View Home");
		}

		private void MenuView2D_Click(object sender, System.EventArgs e)
		{
			MenuView2D.Checked = true;
			MenuView3D.Checked = false;

			this.axAgUiAx2DCntrl2.Visible = true;
			this.axAgUiAxVOCntrl1.Visible = false;

			MenuZoomOut.Visible = true;
			MenuZoomOut.Enabled = true;

			MenuHomeView.Visible = false;
			MenuHomeView.Visible = false;
		}

		private void MenuView3D_Click(object sender, System.EventArgs e)
		{
			MenuView2D.Checked = false;
			MenuView3D.Checked = true;

			this.axAgUiAx2DCntrl2.Visible = false;
			this.axAgUiAxVOCntrl1.Visible = true;

			MenuZoomOut.Visible = false;
			MenuZoomOut.Enabled = false;

			MenuHomeView.Visible = true;
			MenuHomeView.Visible = true;
		}

		private void MenuZoomIn_Click(object sender, System.EventArgs e)
		{
			if ( MenuView2D.Checked )
				this.axAgUiAx2DCntrl2.ZoomIn();
			if ( MenuView3D.Checked )
				this.axAgUiAxVOCntrl1.ZoomIn();
		}

		private void MenuZoomOut_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAx2DCntrl2.ZoomOut();
		}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rootObject != null)
            {
                rootObject.CloseScenario();
            }
        }

	}
}
