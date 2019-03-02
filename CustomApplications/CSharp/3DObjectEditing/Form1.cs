using System;
using System.Windows.Forms;



namespace _DObjectEditing
{
    public partial class Form1 : Form
    {
        String objectLocation = "/Application/STK/Scenario/3DObjectEditScenario/Aircraft/Aircraft1";
        public Form1()
        {
            InitializeComponent();
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("New / Scenario 3DObjectEditScenario");
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("New / */Aircraft Aircraft1");
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 47.1 -120.8 3000.0 200");
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 41.8 -111.5 3000.0 200");
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 33.5 -110.0 3000.0 200");
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 45.8 -94.6 3000.0 200");
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 40.2 -49.1 3000.0 200");
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("AddWaypoint */Aircraft/Aircraft1 DetTimeAccFromVel 34.8 -91.2 3000.0 200");
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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        private void editStart_Click(object sender, EventArgs e)
        {
            this.axAgUiAxVOCntrl1.StartObjectEditing(objectLocation);
        }

        private void editOk_Click(object sender, EventArgs e)
        {
            this.axAgUiAxVOCntrl1.StopObjectEditing(false);
        }

        private void editApply_Click(object sender, EventArgs e)
        {
            this.axAgUiAxVOCntrl1.ApplyObjectEditing();
        }

        private void editCancel_Click(object sender, EventArgs e)
        {
            this.axAgUiAxVOCntrl1.StopObjectEditing(true);
        }

        private void UpdateObjectEditing()
        {
            if (this.axAgUiAxVOCntrl1.IsObjectEditing)
            {
                this.editStart.Enabled = false;
                this.editOk.Enabled = true;
                this.editApply.Enabled = true;
                this.editCancel.Enabled = true;
            }
            else
            {
                this.editStart.Enabled = true;
                this.editOk.Enabled = false;
                this.editApply.Enabled = false;
                this.editCancel.Enabled = false;
            }
        }

        private void axAgUiAxVOCntrl1_OnObjectEditingApply(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingApplyEvent e)
        {
            UpdateObjectEditing();
        }

        private void axAgUiAxVOCntrl1_OnObjectEditingCancel(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingCancelEvent e)
        {
            UpdateObjectEditing();
        }

        private void axAgUiAxVOCntrl1_OnObjectEditingStart(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingStartEvent e)
        {
            UpdateObjectEditing();
        }

        private void axAgUiAxVOCntrl1_OnObjectEditingStop(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingStopEvent e)
        {
            UpdateObjectEditing();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.axAgUiAxVOCntrl1.Application.ExecuteCommand("Unload / *");
        }
    }
}