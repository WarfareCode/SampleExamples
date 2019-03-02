using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;



namespace AzElMaskTool
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class AzElMaskTool : Form
	{
        private AgStkObjectRoot stkRootObject = null;
		private String ReportFilePath;

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

		public AzElMaskTool()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
                Application.Run(new AzElMaskTool());
            }
		}

		private void btnNewScenario_Click(object sender, System.EventArgs e)
		{
			IAgScenario oScenario;

			stkRoot.NewScenario("AzElMaskToolTest");
			stkRoot.UnitPreferences.SetCurrentUnit("DistanceUnit", "km");
			stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG");
			oScenario = stkRoot.CurrentScenario as IAgScenario;
			oScenario.SetTimePeriod("1 Jul 2007 12:00:00.000", "2 Jul 2007 12:00:00.000");
			oScenario.Epoch = "1 Jul 2007 12:00:00.000";
			oScenario.Animation.StartTime = "1 Jul 2007 12:00:00.000";
			stkRoot.Rewind();

			btnNewScenario.Enabled = false;
			btnNewSat.Enabled = true;
			btnNewSensor.Enabled = false;
			btnCompute.Enabled = false;
			btnCloseScenario.Enabled = true	;
			btnReport.Enabled = false;
		}

		private void btnNewSat_Click(object sender, System.EventArgs e)
		{
			IAgSatellite oSat;
			IAgVePropagatorTwoBody oTwobody ;

			oSat = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Satellite1") as IAgSatellite ;
			oSat.VO.Model.ScaleValue = 0.0;
			oSat.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);
			oTwobody = oSat.Propagator as IAgVePropagatorTwoBody;
            IAgCrdnEventIntervalSmartInterval interval = oTwobody.EphemerisInterval;
            interval.SetExplicitInterval("1 Jul 2007 12:00:00.000", "2 Jul 2007 12:00:00.000");
			oTwobody.Step = 60;

            IAgOrbitState oOrb;
            oOrb = oTwobody.InitialState.Representation as IAgOrbitState;
            oOrb.Epoch = "1 Jul 2007 12:00:00.000";

			oTwobody.InitialState.Representation.AssignClassical(AGI.STKUtil.AgECoordinateSystem.eCoordinateSystemJ2000, 6878.14, 0.0, 45.0, 0.0, 0.0, 0.0);
			oTwobody.Propagate();

			btnNewScenario.Enabled = false;
			btnNewSat.Enabled = false;
			btnNewSensor.Enabled = true;
			btnCompute.Enabled = false;
			btnCloseScenario.Enabled = true;
			btnReport.Enabled = false;
		}

		private void btnNewSensor_Click(object sender, System.EventArgs e)
		{
			IAgStkObject oStkObj;
			IAgSensor oSens ;

			oStkObj = stkRoot.CurrentScenario.Children["Satellite1"]  ;     
			oSens = oStkObj.Children.New(AgESTKObjectType.eSensor, "Sensor1") as IAgSensor;
			oSens.VO.VertexOffset.InheritFromParentObj = false;
			oSens.VO.VertexOffset.EnableTranslational = true;
			oSens.VO.VertexOffset.SetAxisOffsetValue(AgEAxisOffset.eSensorRadius, 0);
			oSens.VO.VertexOffset.X = 0;
			oSens.VO.VertexOffset.Y = 0;
			oSens.VO.VertexOffset.Z = -0.03;
			stkRoot.Rewind();
	
			btnNewScenario.Enabled = false;
			btnNewSat.Enabled = false;
			btnNewSensor.Enabled = false;
			btnCompute.Enabled = true;
			btnCloseScenario.Enabled = true	;
			btnReport.Enabled = false;
		}

		private void btnCompute_Click(object sender, System.EventArgs e)
		{
			GetReportFilePath();
			stkRoot.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool Time \"1 Jul 2007 12:00:00.000\"");
			stkRoot.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool Object On Satellite/Satellite1")   ;    
			stkRoot.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool File \"" + ReportFilePath + "\"");
			stkRoot.ExecuteCommand("VO */Satellite/Satellite1/Sensor/Sensor1 AzElmaskTool Compute")	;
			btnReport.Enabled = true;
		}

		private void btnCloseScenario_Click(object sender, System.EventArgs e)
		{
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
			CleanReportFile();
			btnNewScenario.Enabled = true;
			btnNewSat.Enabled = false;
			btnCompute.Enabled = false;
			btnCloseScenario.Enabled = false;
			btnReport.Enabled = false;
		}

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			ShowReport(ReportFilePath);
		}
		private String GetReportFilePath()
		{
			CleanReportFile();
			ReportFilePath = Path.GetTempFileName();
			return ReportFilePath;
		}

		private void CleanReportFile()
		{
			if (File.Exists(ReportFilePath))
			{
				File.Delete(ReportFilePath);
			}			
			ReportFilePath = "";
		}

		private void ShowReport(String sReport)
		{
			Process p = null;
			try
			{
				p= new Process();
				p.StartInfo.FileName = "wordpad";
				p.StartInfo.Arguments = "\"" + sReport + "\"";
				p.Start();			
			}
			catch (Exception ex)
			{
				String msg = "Exception Occurred : " + ex.Message + ", " + ex.StackTrace.ToString();
				System.Windows.Forms.MessageBox.Show(msg);
			}
		}

        private void AzElMaskTool_Load(object sender, EventArgs e)
        {            
            btnNewScenario.Enabled = true;
            btnNewSat.Enabled = false;
            btnNewSensor.Enabled = false;
            btnCompute.Enabled = false;
            btnCloseScenario.Enabled = false;
            btnReport.Enabled = false;
        }

        private void AzElMaskTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }
	}
}

