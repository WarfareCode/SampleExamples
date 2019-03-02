using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace MapView
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{
        AGI.STKObjects.AgStkObjectRoot rootObject = null;

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
                Application.Run(new Form1());
            }
		}

		private void Command4_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAx2DCntrl1.ZoomIn();
		}

		private void Command5_Click(object sender, System.EventArgs e)
		{
			this.axAgUiAx2DCntrl1.ZoomOut();
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
    		this.Check1.Checked = false;
		}

		private void Command1_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.Filter = "Scenario (.sc)|*.sc" ;
            string path = Application.StartupPath + @"\..\..\..\..\..\..\SharedResources\Scenarios\Events\";
            openFileDialog1.InitialDirectory = System.IO.Path.GetFullPath(path);
			openFileDialog1.Title = "Open STK scenario...";
			
			openFileDialog1.RestoreDirectory = true ;

			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{				
				root.CloseScenario();
				root.LoadScenario(this.openFileDialog1.FileName);
                if (this.Check1.Checked)
                {
                    this.axAgUiAx2DCntrl1.PanModeEnabled = true;
                }
                else
                {
                    this.axAgUiAx2DCntrl1.PanModeEnabled = false;
                }
			}
		}

		private void Check1_CheckedChanged(object sender, System.EventArgs e)
		{
            if (rootObject != null)
            {
                if (this.Check1.Checked)
                {
                    this.axAgUiAx2DCntrl1.PanModeEnabled = true;
                }
                else
                {
                    this.axAgUiAx2DCntrl1.PanModeEnabled = false;
                }
            }
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
