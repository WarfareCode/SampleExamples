using System;
using System.Windows.Forms;



namespace Events
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        private AGI.STKX.AgSTKXApplication STKXApp;
        private AGI.STKObjects.AgStkObjectRoot stkRootObject = null;

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

        private void Command1_Click(object sender, System.EventArgs e)
        {
            if (Command1.Text == "Open Scenario")
            {
                string scFile = Application.StartupPath + @"\..\..\..\..\..\..\SharedResources\Scenarios\Events\TestEvents.sc";
                stkRoot.CloseScenario();
                stkRoot.LoadScenario(scFile);
                Command1.Text = "Close Scenario";
                checkBox1.Enabled = true;
                if (this.axAgUiAx2DCntrl1.PanModeEnabled)
                    checkBox1.Checked = true;
                else
                    checkBox1.Checked = false;               
            }
            else if (Command1.Text == "Close Scenario")
            {
                checkBox1.Checked = false; 
                checkBox1.Enabled = false;
                if (stkRootObject != null)
                {
                    stkRootObject.CloseScenario();
                }
                Command1.Text = "Open Scenario";
            }
        }

        private void Command2_Click(object sender, System.EventArgs e)
        {
            this.axAgUiAx2DCntrl1.ZoomIn();
        }

        private void Command3_Click(object sender, System.EventArgs e)
        {
            this.axAgUiAx2DCntrl1.ZoomOut();
        }

        private void Command6_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveAsDialog.ShowDialog() == DialogResult.OK)
            {
                stkRoot.SaveScenarioAs(saveAsDialog.FileName);
            }
        }

        private string GetShiftAsStr(short Shift)
        {
            string res;
            res = "";
            if ((Shift & (short)AGI.STKX.AgEShiftValues.eShiftPressed) != 0)
                res = res + "Shift";
            if ((Shift & (short)AGI.STKX.AgEShiftValues.eCtrlPressed) != 0)
                res = res + "Ctrl";
            if ((Shift & (short)AGI.STKX.AgEShiftValues.eAltPressed) != 0)
                res = res + "Alt";

            return (res);
        }

        private string GetButtonAsStr(short Button)
        {
            string res;
            res = "";
            if ((Button & (short)AGI.STKX.AgEButtonValues.eLeftPressed) != 0)
                res = res + "Left";
            if ((Button & (short)AGI.STKX.AgEButtonValues.eRightPressed) != 0)
                res = res + "Right";
            if ((Button & (short)AGI.STKX.AgEButtonValues.eMiddlePressed) != 0)
                res = res + "Middle";

            return (res);
        }

        private void VOPickInfo(string str, int x, int y)
        {
            AGI.STKX.IAgPickInfoData pickInfoData;
            pickInfoData = this.axAgUiAxVOCntrl1.PickInfo(x, y);
            if (pickInfoData.IsObjPathValid)
                WriteMsg(str + "Object: " + pickInfoData.ObjPath);
            if (pickInfoData.IsLatLonAltValid)
                WriteMsg(str + "LLA: " + pickInfoData.Lat + " " + pickInfoData.Lon + " " + pickInfoData.Alt);

            pickInfoData = null;
        }

        private void GxPickInfo(string str, int x, int y)
        {
            AGI.STKX.IAgPickInfoData pickInfoData;
            pickInfoData = this.axAgUiAx2DCntrl1.PickInfo(x, y);
            if (pickInfoData.IsObjPathValid)
                WriteMsg(str + "Object: " + pickInfoData.ObjPath);
            if (pickInfoData.IsLatLonAltValid)
                WriteMsg(str + "LLA: " + pickInfoData.Lat + " " + pickInfoData.Lon + " " + pickInfoData.Alt);

            pickInfoData = null;
        }

        private void Command4_Click(object sender, System.EventArgs e)
        {
            this.axAgUiAxVOCntrl1.ZoomIn();
        }

        private void WriteMsg(String msg)
        {
            int count;
            count = this.Trace.TextLength;

            if (count > 3000)
            {
                this.Trace.Text = "";
                count = 0;
            }

            this.Trace.SelectionStart = count;
            this.Trace.AppendText(msg + "\r\n");
        }

        private void axAgUiAx2DCntrl1_ClickEvent(object sender, System.EventArgs e)
        {
            WriteMsg("Ax2D - Click");
        }

        private void On2DMapDblClick(object sender, System.EventArgs e)
        {
            WriteMsg("Ax2D - DblClick");
        }

        private void axAgUiAx2DCntrl1_KeyDownEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyDownEvent e)
        {
            WriteMsg("Ax2D - KeyDown " + e.keyCode + " " + GetShiftAsStr(e.shift));

        }

        private void axAgUiAx2DCntrl1_KeyPressEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyPressEvent e)
        {
            WriteMsg("Ax2D - KeyPress " + e.keyAscii);
        }

        private void axAgUiAx2DCntrl1_KeyUpEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_KeyUpEvent e)
        {
            WriteMsg("Ax2D - KeyUp" + e.keyCode + " " + GetShiftAsStr(e.shift));
        }

        private void axAgUiAx2DCntrl1_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEvent e)
        {
            WriteMsg("Ax2D - MouseDown " + e.x + " " + e.y + " " + GetShiftAsStr(e.shift) + " " + GetButtonAsStr(e.button));
        }

        private void axAgUiAx2DCntrl1_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseMoveEvent e)
        {
            WriteMsg("Ax2D - MouseMove " + e.x + " " + e.y + " " + GetShiftAsStr(e.shift) + " " + GetButtonAsStr(e.button));
            GxPickInfo("Ax2D - ", e.x, e.y);
        }

        private void axAgUiAx2DCntrl1_MouseUpEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseUpEvent e)
        {
            WriteMsg("Ax2D - MouseUp " + e.x + " " + e.y + " " + GetShiftAsStr(e.shift) + " " + GetButtonAsStr(e.button));
        }

        private void axAgUiAxVOCntrl1_ClickEvent(object sender, System.EventArgs e)
        {
            WriteMsg("AxVO - Click ");
        }

        private void axAgUiAxVOCntrl1_DblClick(object sender, System.EventArgs e)
        {
            WriteMsg("AxVO - DblClick ");
        }

        private void axAgUiAxVOCntrl1_KeyDownEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyDownEvent e)
        {
            WriteMsg("AxVO - KeyDown " + e.keyCode + " " + GetShiftAsStr(e.shift));
        }

        private void axAgUiAxVOCntrl1_KeyPressEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyPressEvent e)
        {
            WriteMsg("AxVO - KeyPress " + e.keyAscii);
        }

        private void axAgUiAxVOCntrl1_KeyUpEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyUpEvent e)
        {
            WriteMsg("AxVO - KeyUp " + e.keyCode + " " + GetShiftAsStr(e.shift));
        }

        private void axAgUiAxVOCntrl1_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
        {
            WriteMsg("AxVO - MouseDown " + e.x + " " + e.y + " " + GetShiftAsStr(e.shift) + " " + GetButtonAsStr(e.button));
        }

        private void axAgUiAxVOCntrl1_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            WriteMsg("AxVO - MouseMove " + e.x + " " + e.y + " " + GetShiftAsStr(e.shift) + " " + GetButtonAsStr(e.button));
            VOPickInfo("AxVO - ", e.x, e.y);
        }

        private void axAgUiAxVOCntrl1_MouseUpEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent e)
        {
            WriteMsg("AxVO - MouseUp " + e.x + " " + e.y + " " + GetShiftAsStr(e.shift) + " " + GetButtonAsStr(e.button));
        }

        private void STKXApp_OnLogMessage(string bstrMsg, AGI.STKX.AgELogMsgType eType, int lErrorCode,
            string bstrFileName, int lLineNo, AGI.STKX.AgELogMsgDispID eDispID)
        {
            WriteMsg("STK/X - Log: ");
            WriteMsg("    Message:    " + bstrMsg);

            string stype;
            if (eType == AGI.STKX.AgELogMsgType.eLogMsgAlarm)
                stype = "Alarm";
            else if (eType == AGI.STKX.AgELogMsgType.eLogMsgDebug)
                stype = "Debug";
            else if (eType == AGI.STKX.AgELogMsgType.eLogMsgForceInfo)
                stype = "ForceInfo";
            else if (eType == AGI.STKX.AgELogMsgType.eLogMsgInfo)
                stype = "Info";
            else if (eType == AGI.STKX.AgELogMsgType.eLogMsgWarning)
                stype = "Warning";
            else
                stype = "Unknown";

            WriteMsg("    Type:    " + stype);
            WriteMsg("    Error code: " + lErrorCode);

            if (bstrFileName != "")
            {
                WriteMsg("    File name: " + bstrFileName);
                WriteMsg("    Line #: " + lLineNo);
            }
            else
                WriteMsg("    DispID #: " + eDispID);

        }

        private void STKXApp_OnScenarioNew(string path)
        {
            WriteMsg("STK/X - New Scenario: " + path);
        }

        private void STKXApp_OnScenarioLoad(string path)
        {
            WriteMsg("STK/X - Scenario loaded: " + path);
        }

        private void STKXApp_OnScenarioClose()
        {
            WriteMsg("STK/X - Scenario Closed");
        }

        private void STKXApp_OnScenarioSave(string path)
        {
            WriteMsg("STK/X - Scenario saved to: " + path);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.STKXApp = new AGI.STKX.AgSTKXApplication();
            this.STKXApp.OnLogMessage += new AGI.STKX.IAgSTKXApplicationEvents_OnLogMessageEventHandler(STKXApp_OnLogMessage);
            this.STKXApp.OnScenarioNew += new AGI.STKX.IAgSTKXApplicationEvents_OnScenarioNewEventHandler(STKXApp_OnScenarioNew);
            this.STKXApp.OnScenarioLoad += new AGI.STKX.IAgSTKXApplicationEvents_OnScenarioLoadEventHandler(STKXApp_OnScenarioLoad);
            this.STKXApp.OnScenarioClose += new AGI.STKX.IAgSTKXApplicationEvents_OnScenarioCloseEventHandler(STKXApp_OnScenarioClose);
            this.STKXApp.OnScenarioSave += new AGI.STKX.IAgSTKXApplicationEvents_OnScenarioSaveEventHandler(STKXApp_OnScenarioSave);

            if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeEngineRuntime))
            {
                MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
                return;
            }
            else if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl))
            {
                MessageBox.Show("Globe is disabled due to your current license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Command4.Enabled = false;
            }
            
        }

        private void axAgUiAxVOCntrl1_OLEDragDrop(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_OLEDragDropEvent e)
        {

        }

        private void axAgUiAx2DCntrl1_OLEDragDrop(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_OLEDragDropEvent e)
        {

        }

        private void Trace_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                this.axAgUiAx2DCntrl1.PanModeEnabled = true;
            else
                this.axAgUiAx2DCntrl1.PanModeEnabled = false;       
        }
    }
}
