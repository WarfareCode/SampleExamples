using System;
using System.Windows.Forms;



namespace DrawRects
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		private AGI.STKObjects.AgStkObjectRoot rootObject = null;

		private int x0;
		private int y0;
		private int pickMode;
		private AGI.STKX.AgDrawElemRect curRect;

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

		private void axAgUiAxVOCntrl2_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
		{
			if ( pickMode > 0 )
			{
				if ( pickMode == 1 )
				{
					x0 = e.x;
					y0 = e.y;
					pickMode = 2;
				}
				else
				{
					curRect.Set(x0, y0, e.x, e.y);
					curRect = null;
					pickMode = 0;
					this.axAgUiAxVOCntrl2.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeAutomatic;
				}
			}
		}

		private void axAgUiAxVOCntrl2_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
		{
			if ( pickMode == 2 )
			{
				if ( curRect == null )
				{
					AGI.STKX.AgDrawElemRect r;
					r = (AGI.STKX.AgDrawElemRect) this.axAgUiAxVOCntrl2.DrawElements.Add("Rect");

					
					if (Combo1.Text == "Solid")
						r.LineStyle = AGI.STKX.AgELineStyle.eSolid;
					else if (Combo1.Text == "Dashed")
                        r.LineStyle = AGI.STKX.AgELineStyle.eDashed;
					else if (Combo1.Text == "Dotted")
                        r.LineStyle = AGI.STKX.AgELineStyle.eDotted;
					else if (Combo1.Text == "DotDashed")
                        r.LineStyle = AGI.STKX.AgELineStyle.eDotDashed;
					else if (Combo1.Text == "LongDashed")
                        r.LineStyle = AGI.STKX.AgELineStyle.eLongDashed;
					else if (Combo1.Text == "DashDotDashed")
                        r.LineStyle = AGI.STKX.AgELineStyle.eDashDotDotted;
					else if (Combo1.Text == "MDash")
                        r.LineStyle = AGI.STKX.AgELineStyle.eMDash;
					else if (Combo1.Text == "LDash")
                        r.LineStyle = AGI.STKX.AgELineStyle.eLDash;
					else if (Combo1.Text == "SDashDot")
                        r.LineStyle = AGI.STKX.AgELineStyle.eSDashDot;
					else if (Combo1.Text == "MDashDot")
                        r.LineStyle = AGI.STKX.AgELineStyle.eMDashDot;
					else if (Combo1.Text == "LDashDot")
                        r.LineStyle = AGI.STKX.AgELineStyle.eLDashDot;
					else if (Combo1.Text == "MSDash")
                        r.LineStyle = AGI.STKX.AgELineStyle.eMSDash;
					else if (Combo1.Text == "LSDash")
                        r.LineStyle = AGI.STKX.AgELineStyle.eLSDash;
					else if (Combo1.Text == "LMDash")
                        r.LineStyle = AGI.STKX.AgELineStyle.eLMDash;
					else if (Combo1.Text == "LMSDash")
                        r.LineStyle = AGI.STKX.AgELineStyle.eLMSDash;

					if (Combo2.Text == "1 pt")
						r.LineWidth = 1;
					else if (Combo2.Text == "2 pt")
						r.LineWidth = 2;
					else if (Combo2.Text == "3 pt")
						r.LineWidth = 3;
					else if (Combo2.Text == "4 pt")
						r.LineWidth = 4;
					else if (Combo2.Text == "5 pt")
						r.LineWidth = 5;

					r.Color = System.Convert.ToUInt32(System.Drawing.ColorTranslator.ToOle(this.colorDialog1.Color));

					curRect = r;
				}

				curRect.Set(x0, y0, e.x, e.y);
			}
		}

		private void BtnNewScen_Click(object sender, System.EventArgs e)
		{
			root.CloseScenario();
			root.NewScenario("Test");
		}

		private void BtnSelectColor_Click(object sender, System.EventArgs e)
		{
			this.colorDialog1.ShowDialog();
		}

		private void BtnAddRect_Click(object sender, System.EventArgs e)
		{
			pickMode = 1;
			this.axAgUiAxVOCntrl2.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeManual;
		}

		private void BtnClearAll_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAxVOCntrl2.DrawElements.Clear();
		}

		private void BtnListAll_Click(object sender, System.EventArgs e)
		{
			string msg;
			msg = "";
			AGI.STKX.AgDrawElemRect r;
			for (int i=0 ; i<this.axAgUiAxVOCntrl2.DrawElements.Count ; i++)
			{
				r = (AGI.STKX.AgDrawElemRect) this.axAgUiAxVOCntrl2.DrawElements[i];
				msg = msg + "[left, top, right, bottom]=[" + r.Left + ", " + r.Top + ", " +
					r.Right + ", " + r.Bottom + "] LineWidth=" + r.LineWidth + " LineStyle=" +
					r.LineStyle + " Color=" + r.Color + "\r\n";
			}
			MessageBox.Show( msg, "DrawRects" );
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			pickMode = 0;           
			root.NewScenario("Test");

			Combo1.Items.Add( "Solid" );
			Combo1.Items.Add( "Dashed" );
			Combo1.Items.Add( "Dotted" );
			Combo1.Items.Add( "DotDashed" );
			Combo1.Items.Add( "LongDashed" );
			Combo1.Items.Add( "DashDotDashed" );
			Combo1.Items.Add( "MDash" );
			Combo1.Items.Add( "LDash" );
			Combo1.Items.Add( "SDashDot" );
			Combo1.Items.Add( "MDashDot" );
			Combo1.Items.Add( "LDashDot" );
			Combo1.Items.Add( "MSDash" );
			Combo1.Items.Add( "LSDash" );
			Combo1.Items.Add( "LMDash" );
			Combo1.Items.Add( "LMSDash" );

			Combo1.Text = "Solid";

			Combo2.Items.Add( "1 pt" );
			Combo2.Items.Add( "2 pt" );
			Combo2.Items.Add( "3 pt" );
			Combo2.Items.Add( "4 pt" );
			Combo2.Items.Add( "5 pt" );

			Combo2.Text = "1 pt";
		}

		private void BtnListAllForEach_Click(object sender, System.EventArgs e)
		{
			string msg;
			msg = "";
			AGI.STKX.AgDrawElemRect r;
			for (int i=0 ; i<this.axAgUiAxVOCntrl2.DrawElements.Count ; i++)
			{
				r = (AGI.STKX.AgDrawElemRect) this.axAgUiAxVOCntrl2.DrawElements[i];
				msg = msg + "[left, top, right, bottom]=[" + r.Left + ", " + r.Top + ", " +
					r.Right + ", " + r.Bottom + "] LineWidth=" + r.LineWidth + " LineStyle=" +
					r.LineStyle + " Color=" + r.Color + "\r\n";
			}
			MessageBox.Show( msg, "DrawRects" );
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

