using System;
using System.Drawing;
using System.Windows.Forms;



namespace OnAnimUpdate
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : System.Windows.Forms.Form
	{
		private AGI.STKX.AgSTKXApplication STKXApp;
        private AGI.STKObjects.AgStkObjectRoot stkRootObject = null;

		private int oldX;
		private int oldY;

		private int oldButton;

        private AGI.STKObjects.AgStkObjectRoot stkRoot
        {
            get
            {
                if (stkRootObject == null)
                {
                    stkRootObject = new AGI.STKObjects.AgStkObjectRoot();
                }
                return stkRootObject;
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
			this.STKXApp = new AGI.STKX.AgSTKXApplication();
			this.STKXApp.OnAnimUpdate += this.STKXApp_OnAnimUpdate;			

			StatusBarPanel pn10;
			StatusBarPanel pn11;
			StatusBarPanel pn12;
			
			pn10 = new StatusBarPanel();
			pn11 = new StatusBarPanel();
			pn12 = new StatusBarPanel();

			this.statusBar1.Panels.Add(pn10);
			this.statusBar1.Panels.Add(pn11);
			this.statusBar1.Panels.Add(pn12);
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

                if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl))
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
                Application.Run(new Form1());
            }
		}
		
		private void Form1_Load(object sender, System.EventArgs e)
		{
			StatusBarPanel pn10 = this.statusBar1.Panels[0];
			StatusBarPanel pn11 = this.statusBar1.Panels[1];
			StatusBarPanel pn12 = this.statusBar1.Panels[2];

			pn10.Width=this.Width/3;
			pn11.Width=this.Width/3;
			pn12.Width=this.Width/3;

			stkRoot.NewScenario("Test");
			stkRoot.ExecuteCommand("ConControl / VerboseOff");
		}

		private string FilterChars( string text )
		{
			string result;

			result=text.Replace("\r","");
			result=text.Replace("\n","");
			result=text.Replace("\"","");

			return (result);
		}

		private void Form1_Resize(object sender, System.EventArgs e)
		{
			this.axAgUiAxVOCntrl1.Left=0;
			this.axAgUiAxVOCntrl1.Top=0;
			this.axAgUiAxVOCntrl1.Width=this.ClientRectangle.Width;
			this.axAgUiAxVOCntrl1.Height=this.ClientRectangle.Height-this.statusBar1.Height;
			
			StatusBarPanel pn10 = this.statusBar1.Panels[0];
			StatusBarPanel pn11 = this.statusBar1.Panels[1];
			StatusBarPanel pn12 = this.statusBar1.Panels[2];
			
			pn10.Width = this.Width / 3;
			pn11.Width = this.Width / 3;
			pn12.Width = this.Width / 3;
		
		}

		private void DoMouseDown(int Button, int Shift, int x, int y)
		{
			oldX = x;
			oldY = y;
			oldButton = Button;
		}

		private void DoMouseUp(int Button, int Shift, int x, int y)
		{
			if (oldButton==2 & Shift==0 & Math.Abs(oldX-x)<10 & Math.Abs(oldY-y)<10)
			{
				this.MenuRoot.Show(this, new Point(x,y));
			}
		}

		private void MenuAnimFaster_Click(object sender, System.EventArgs e)
		{
			stkRoot.Faster();
		}

		private void MenuAnimPause_Click(object sender, System.EventArgs e)
		{
			stkRoot.Pause();
		}

		private void MenuAnimReset_Click(object sender, System.EventArgs e)
		{
			stkRoot.Rewind();
		}

		private void MenuAnimSlower_Click(object sender, System.EventArgs e)
		{
			stkRoot.Slower();
		}

		private void MenuAnimStart_Click(object sender, System.EventArgs e)
		{
			stkRoot.PlayForward();
		}

		private void MenuHomeView_Click(object sender, System.EventArgs e)
		{
			stkRoot.ExecuteCommand("VO * View Home");
		}

		private void MenuZoomIn_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAxVOCntrl1.ZoomIn();
		}

		private void axAgUiAxVOCntrl1_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
		{
			DoMouseDown(e.button, e.shift, e.x, e.y);
		}

		private void axAgUiAxVOCntrl1_MouseUpEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent e)
		{
			DoMouseUp(e.button, e.shift, e.x, e.y);
		}

		private void MenuLoadScenario_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.InitialDirectory = @"C:\My Documents" ;
			openFileDialog1.Filter = "Scenario (.sc)|*.sc" ;
			openFileDialog1.FilterIndex = 2 ;
			openFileDialog1.RestoreDirectory = true ;

			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				stkRoot.CloseScenario();
				stkRoot.LoadScenario(this.openFileDialog1.FileName);
			}

		}

		private void STKXApp_OnAnimUpdate(double TimeEpSec)
		{
			this.statusBar1.Panels[0].Text = TimeEpSec + " EpSec";
			AGI.STKUtil.AgExecCmdResult retlist;
            retlist = (AGI.STKUtil.AgExecCmdResult)stkRoot.ExecuteCommand("GetAnimTime *");

			if ( retlist.Count > 0 )
				this.statusBar1.Panels[1].Text = FilterChars(retlist[0]);
			else
 				this.statusBar1.Panels[1].Text = "N/A";

            retlist = (AGI.STKUtil.AgExecCmdResult)stkRoot.ExecuteCommand("AnimFrameRate *");

			if ( retlist.Count > 0 & retlist[0] != null )
				this.statusBar1.Panels[2].Text = FilterChars( retlist[0] ) + " fps";
			else
				this.statusBar1.Panels[2].Text = "N/A";

		}

		private void Form1_Closed(object sender, System.EventArgs e)
		{
			stkRoot.Pause();
		}

		private void axAgUiAxVOCntrl1_OLEDragDrop(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_OLEDragDropEvent e)
		{
		
		}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }

	}
}
