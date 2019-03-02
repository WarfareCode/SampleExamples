using System;
using System.Windows.Forms;



namespace RubberBandSelect
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{
        private AGI.STKObjects.AgStkObjectRoot stkRootObject = null;
        private AGI.STKX.AgDrawElemRect curRect;

		private int x0;
		private int y0;
		private int pickMode;


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
           
		}

		private void BtnLoadISRScen_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

            string scFile = Application.StartupPath + @"\..\..\..\..\..\..\SharedResources\Scenarios\ISR\ISR.sc";
			stkRoot.CloseScenario();
            stkRoot.LoadScenario(scFile);

				
			Cursor.Current = Cursors.Default;
		}

		private void axAgUiAxVOCntrl1_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
		{
			if (pickMode > 0)
			{
				if (pickMode == 1)
				{
					x0 = e.x;
					y0 = e.y;
					pickMode = 2;
				}
				else
				{
					curRect.Set(x0, y0, e.x, e.y);
					curRect = null;

					AGI.STKX.AgRubberBandPickInfoData pickInfo;
					pickInfo = this.axAgUiAxVOCntrl1.RubberBandPickInfo( x0, y0, e.x, e.y);
					if ( pickInfo.ObjPaths.Count!=0 )
					{
						string msg;
						msg = pickInfo.ObjPaths.Count + " Object(s):" + "\r\n";

						for (int obj=0 ; obj < pickInfo.ObjPaths.Count ; obj++ )
							msg = msg + "   " + pickInfo.ObjPaths[obj] + "\r\n";

						MessageBox.Show(msg);
					}
					else
					{
						MessageBox.Show("No Object Selected");
					}

					this.axAgUiAxVOCntrl1.DrawElements.Clear();
					this.axAgUiAxVOCntrl1.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeAutomatic;
					pickMode = 0;
				}
			}

		}

		private void axAgUiAxVOCntrl1_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
		{
			if (pickMode == 2 )
			{
				if (curRect == null)
				{
					AGI.STKX.AgDrawElemRect r;
					r = (AGI.STKX.AgDrawElemRect) this.axAgUiAxVOCntrl1.DrawElements.Add("Rect");
						
					r.LineStyle = AGI.STKX.AgELineStyle.eSolid;
					r.LineWidth = 1;
					r.Color = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red));
					curRect = r;
				}

				curRect.Set(x0, y0, e.x, e.y);
			}
		}

		private void BtnSelectObjs_Click(object sender, System.EventArgs e)
		{
			pickMode = 1;
            this.axAgUiAxVOCntrl1.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeManual;
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
