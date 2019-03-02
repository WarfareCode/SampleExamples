using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace Tutorial
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{
        private AGI.STKX.AgSTKXApplication stkxApp;
        private bool scenarioOpen = false;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            // 
            // stkxApp
            // 
            this.stkxApp = new AGI.STKX.AgSTKXApplication();
            this.stkxApp.OnScenarioNew += this.OnNewScenario;
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (button1.Text.Equals("New Scenario"))
            {
                this.axAgUiAx2DCntrl1.Application.ExecuteCommand("New / Scenario Test");
                button1.Text = "Close Scenario";
                scenarioOpen = true;
            }
            else
            {
                this.axAgUiAx2DCntrl1.Application.ExecuteCommand("Unload / *");
                button1.Text = "New Scenario";
                scenarioOpen = false;
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.axAgUiAx2DCntrl1.ZoomIn();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            this.axAgUiAx2DCntrl1.ZoomOut();
        }
        private void On2DMapDblClick(object sender, System.EventArgs e)
        {
            MessageBox.Show("2D Map double click");
        }
        private void OnNewScenario(String path)
        {
            MessageBox.Show("New scenario created " + path);
        }
        private void OnVOMouseMove(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            AGI.STKX.IAgPickInfoData pickInfoData = 
                this.axAgUiAxVOCntrl1.PickInfo(e.x, e.y);
            if (pickInfoData.IsLatLonAltValid)
            {
                this.label1.Text= "Lat: " + pickInfoData.Lat+ "\r\n Lon: "  + 
                    pickInfoData.Lon + "\r\n Alt: " + pickInfoData.Alt;
            }
            else
            {
                this.label1.Text="";
            }

        }

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			System.Runtime.InteropServices.Marshal.ReleaseComObject(stkxApp);
			GC.WaitForPendingFinalizers();
		}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (scenarioOpen)
            {
                this.axAgUiAx2DCntrl1.Application.ExecuteCommand("Unload / *");
            }
        }
    }
}
