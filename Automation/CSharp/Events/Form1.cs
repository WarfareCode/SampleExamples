//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: Form1.cs
//  Events
//
//
//  The features used in this example show how to hook event notification
//  up to STK through C# so that you can listen to events such as animation
//  and object creation.
//
//--------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using AGI.STKUtil;

namespace AGI.STKObjects.Samples
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListBox lstScenarioObjects;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnUnloadObjects;
		private System.Windows.Forms.ListBox lstEventLog;
		private System.Windows.Forms.Button btnNewScen;
		private System.Windows.Forms.Button btnNewSat;
		private System.Windows.Forms.Button btnNewFacility;
		private System.Windows.Forms.Button btnAreaTarget;
		private System.Windows.Forms.Button btnCloseScen;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.Button btnPlayBackward;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.GroupBox groupBox2;
        private Button btnDecreaseStep;
        private Button btnIncreaseStep;
        private Button btnStepBack;
        private Button btnStep;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

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
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUnloadObjects = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstScenarioObjects = new System.Windows.Forms.ListBox();
            this.btnCloseScen = new System.Windows.Forms.Button();
            this.btnAreaTarget = new System.Windows.Forms.Button();
            this.btnNewFacility = new System.Windows.Forms.Button();
            this.btnNewSat = new System.Windows.Forms.Button();
            this.btnNewScen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDecreaseStep = new System.Windows.Forms.Button();
            this.btnIncreaseStep = new System.Windows.Forms.Button();
            this.btnStepBack = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlayBackward = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstEventLog = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 262);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUnloadObjects);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lstScenarioObjects);
            this.groupBox2.Controls.Add(this.btnCloseScen);
            this.groupBox2.Controls.Add(this.btnAreaTarget);
            this.groupBox2.Controls.Add(this.btnNewFacility);
            this.groupBox2.Controls.Add(this.btnNewSat);
            this.groupBox2.Controls.Add(this.btnNewScen);
            this.groupBox2.Location = new System.Drawing.Point(8, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 232);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scenario";
            // 
            // btnUnloadObjects
            // 
            this.btnUnloadObjects.Enabled = false;
            this.btnUnloadObjects.Location = new System.Drawing.Point(136, 152);
            this.btnUnloadObjects.Name = "btnUnloadObjects";
            this.btnUnloadObjects.Size = new System.Drawing.Size(160, 32);
            this.btnUnloadObjects.TabIndex = 6;
            this.btnUnloadObjects.Text = "Unload All";
            this.btnUnloadObjects.Click += new System.EventHandler(this.btnUnloadObjects_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(136, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Scenario Objects:";
            // 
            // lstScenarioObjects
            // 
            this.lstScenarioObjects.Enabled = false;
            this.lstScenarioObjects.Location = new System.Drawing.Point(136, 32);
            this.lstScenarioObjects.Name = "lstScenarioObjects";
            this.lstScenarioObjects.Size = new System.Drawing.Size(160, 108);
            this.lstScenarioObjects.TabIndex = 4;
            // 
            // btnCloseScen
            // 
            this.btnCloseScen.Enabled = false;
            this.btnCloseScen.Location = new System.Drawing.Point(8, 192);
            this.btnCloseScen.Name = "btnCloseScen";
            this.btnCloseScen.Size = new System.Drawing.Size(120, 32);
            this.btnCloseScen.TabIndex = 7;
            this.btnCloseScen.Text = "Close Scenario";
            this.btnCloseScen.Click += new System.EventHandler(this.btnCloseScen_Click);
            // 
            // btnAreaTarget
            // 
            this.btnAreaTarget.Enabled = false;
            this.btnAreaTarget.Location = new System.Drawing.Point(8, 152);
            this.btnAreaTarget.Name = "btnAreaTarget";
            this.btnAreaTarget.Size = new System.Drawing.Size(120, 32);
            this.btnAreaTarget.TabIndex = 3;
            this.btnAreaTarget.Text = "New Area Target";
            this.btnAreaTarget.Click += new System.EventHandler(this.btnAreaTarget_Click);
            // 
            // btnNewFacility
            // 
            this.btnNewFacility.Enabled = false;
            this.btnNewFacility.Location = new System.Drawing.Point(8, 104);
            this.btnNewFacility.Name = "btnNewFacility";
            this.btnNewFacility.Size = new System.Drawing.Size(120, 32);
            this.btnNewFacility.TabIndex = 2;
            this.btnNewFacility.Text = "New Facility";
            this.btnNewFacility.Click += new System.EventHandler(this.btnNewFacility_Click);
            // 
            // btnNewSat
            // 
            this.btnNewSat.Enabled = false;
            this.btnNewSat.Location = new System.Drawing.Point(8, 56);
            this.btnNewSat.Name = "btnNewSat";
            this.btnNewSat.Size = new System.Drawing.Size(120, 32);
            this.btnNewSat.TabIndex = 1;
            this.btnNewSat.Text = "New Satellite";
            this.btnNewSat.Click += new System.EventHandler(this.btnNewSat_Click);
            // 
            // btnNewScen
            // 
            this.btnNewScen.Location = new System.Drawing.Point(8, 16);
            this.btnNewScen.Name = "btnNewScen";
            this.btnNewScen.Size = new System.Drawing.Size(120, 32);
            this.btnNewScen.TabIndex = 0;
            this.btnNewScen.Text = "New Scenario";
            this.btnNewScen.Click += new System.EventHandler(this.btnNewScen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDecreaseStep);
            this.groupBox1.Controls.Add(this.btnIncreaseStep);
            this.groupBox1.Controls.Add(this.btnStepBack);
            this.groupBox1.Controls.Add(this.btnStep);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnPlayBackward);
            this.groupBox1.Controls.Add(this.btnPause);
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Location = new System.Drawing.Point(336, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 232);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation";
            // 
            // btnDecreaseStep
            // 
            this.btnDecreaseStep.Enabled = false;
            this.btnDecreaseStep.Location = new System.Drawing.Point(118, 144);
            this.btnDecreaseStep.Name = "btnDecreaseStep";
            this.btnDecreaseStep.Size = new System.Drawing.Size(119, 32);
            this.btnDecreaseStep.TabIndex = 6;
            this.btnDecreaseStep.Text = "Decrease Time Step";
            this.btnDecreaseStep.Click += new System.EventHandler(this.btnDecreaseStep_Click);
            // 
            // btnIncreaseStep
            // 
            this.btnIncreaseStep.Enabled = false;
            this.btnIncreaseStep.Location = new System.Drawing.Point(118, 104);
            this.btnIncreaseStep.Name = "btnIncreaseStep";
            this.btnIncreaseStep.Size = new System.Drawing.Size(119, 32);
            this.btnIncreaseStep.TabIndex = 5;
            this.btnIncreaseStep.Text = "Increase Time Step";
            this.btnIncreaseStep.Click += new System.EventHandler(this.btnIncreaseStep_Click);
            // 
            // btnStepBack
            // 
            this.btnStepBack.Enabled = false;
            this.btnStepBack.Location = new System.Drawing.Point(118, 64);
            this.btnStepBack.Name = "btnStepBack";
            this.btnStepBack.Size = new System.Drawing.Size(119, 32);
            this.btnStepBack.TabIndex = 3;
            this.btnStepBack.Text = "Step Backward";
            this.btnStepBack.Click += new System.EventHandler(this.btnStepBack_Click);
            // 
            // btnStep
            // 
            this.btnStep.Enabled = false;
            this.btnStep.Location = new System.Drawing.Point(118, 24);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(119, 32);
            this.btnStep.TabIndex = 4;
            this.btnStep.Text = "Step Forward";
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(8, 144);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(104, 32);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlayBackward
            // 
            this.btnPlayBackward.Enabled = false;
            this.btnPlayBackward.Location = new System.Drawing.Point(8, 104);
            this.btnPlayBackward.Name = "btnPlayBackward";
            this.btnPlayBackward.Size = new System.Drawing.Size(104, 32);
            this.btnPlayBackward.TabIndex = 2;
            this.btnPlayBackward.Text = "Backward";
            this.btnPlayBackward.Click += new System.EventHandler(this.btnPlayBackward_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(8, 64);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(104, 32);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "Pause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Location = new System.Drawing.Point(8, 24);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(104, 32);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lstEventLog);
            this.panel2.Location = new System.Drawing.Point(0, 262);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(589, 112);
            this.panel2.TabIndex = 2;
            // 
            // lstEventLog
            // 
            this.lstEventLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEventLog.Location = new System.Drawing.Point(0, 0);
            this.lstEventLog.Name = "lstEventLog";
            this.lstEventLog.ScrollAlwaysVisible = true;
            this.lstEventLog.Size = new System.Drawing.Size(589, 108);
            this.lstEventLog.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(589, 374);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Object Model: STK Event Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		#region Private Members 

		private AGI.STKObjects.AgStkObjectRoot objModelRoot;
		private AGI.Ui.Application.AgUiApplication stkApplication;
		private int autoIncrement = 1;

		#endregion

		private enum AgEEventTypes 
		{
            eAnimationFaster,
			eAnimationPause,
			eAnimationPlayback,
			eAnimationRewind,
            eAnimationSlower,
            eAnimationStep,
            eAnimationStepBack,
			eAnimationUpdate,
			eLogMessage,
            ePercentCompleteBegin,
            ePercentCompleteEnd,
            ePercentCompleteUpdate,
            eScenarioBeforeClose,
            eScenarioBeforeSave,
			eScenarioClose,
			eScenarioLoad,
			eScenarioNew,
			eScenarioSave,
			eObjectAdded,
			eObjectDeleted,
            eObjectChanged,
			eObjectRenamed
		}

		#region Private Member Functions

        delegate void LogEventDelegate(AgEEventTypes eType, string message);

		private void LogEvent(AgEEventTypes eType, string message) 
		{
            if (this.lstEventLog.InvokeRequired)
            {
                this.lstEventLog.BeginInvoke(new LogEventDelegate(LogEvent), new object[] { eType, message });
            }
            else
            {
                this.lstEventLog.Items.Add(string.Format("EVENT: {0}, description: {1}", eType, message));
            }
		}

        delegate void LogEventDelegate2(AgEEventTypes eType);

		private void LogEvent(AgEEventTypes eType) {
            if (this.lstEventLog.InvokeRequired)
            {
                this.lstEventLog.BeginInvoke(new LogEventDelegate2(LogEvent), new object[] {eType});
            }
            else
            {
                this.lstEventLog.Items.Add(string.Format("EVENT: {0}", eType));
            }
		}

        delegate void RebuildScenObjectListDelegate();

		private void RebuildScenObjectList() 
		{
            if (lstScenarioObjects.InvokeRequired)
            {
                lstScenarioObjects.BeginInvoke(new RebuildScenObjectListDelegate(RebuildScenObjectList), new object[] {});
            }
            else
            {
			    this.lstScenarioObjects.BeginUpdate(); 

                this.lstScenarioObjects.Items.Clear();

                if (objModelRoot.CurrentScenario != null)
                {
                    foreach (AGI.STKObjects.IAgStkObject o in objModelRoot.CurrentScenario.Children)
                    {
                        this.lstScenarioObjects.Items.Add(string.Format("{0}/{1}", o.ClassName, o.InstanceName));
                    }
                }

                this.lstScenarioObjects.EndUpdate();
            }
		}

		#endregion

		private void Form1_Load(object sender, System.EventArgs e) {

			try 
			{
                
                try
                {
                    stkApplication = System.Runtime.InteropServices.Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
                }
                catch
                {
                    Guid clsID = typeof(AGI.Ui.Application.AgUiApplicationClass).GUID;
                    Type t = Type.GetTypeFromCLSID(clsID);
                    stkApplication = Activator.CreateInstance(t) as AGI.Ui.Application.AgUiApplication;
                    stkApplication.LoadPersonality("STK");
               } 

                objModelRoot = stkApplication.Personality2 as AGI.STKObjects.AgStkObjectRoot;

				/*
				 * Connect to the Object Model Events.
				 * 
				 */
                objModelRoot.OnAnimationFaster += objModelRoot_OnAnimationFaster;
				objModelRoot.OnAnimationPause += objModelRoot_OnAnimationPause;
				objModelRoot.OnAnimationPlayback += objModelRoot_OnAnimationPlayback;
				objModelRoot.OnAnimationRewind += objModelRoot_OnAnimationRewind;
                objModelRoot.OnAnimationSlower += objModelRoot_OnAnimationSlower;
                objModelRoot.OnAnimationStep += objModelRoot_OnAnimationStep;
                objModelRoot.OnAnimationStepBack += objModelRoot_OnAnimationStepBack;
				objModelRoot.OnAnimUpdate += objModelRoot_OnAnimUpdate;
				objModelRoot.OnLogMessage += objModelRoot_OnLogMessage;
                objModelRoot.OnPercentCompleteBegin += objModelRoot_OnPercentCompleteBegin;
                objModelRoot.OnPercentCompleteEnd += objModelRoot_OnPercentCompleteEnd;
                objModelRoot.OnPercentCompleteUpdate += objModelRoot_OnPercentCompleteUpdate;
                objModelRoot.OnScenarioBeforeClose += objModelRoot_OnScenarioBeforeClose;
                objModelRoot.OnScenarioBeforeSave += objModelRoot_OnScenarioBeforeSave;
				objModelRoot.OnScenarioClose += objModelRoot_OnScenarioClose;
				objModelRoot.OnScenarioLoad += objModelRoot_OnScenarioLoad;
				objModelRoot.OnScenarioNew += objModelRoot_OnScenarioNew;
				objModelRoot.OnScenarioSave += objModelRoot_OnScenarioSave;
				objModelRoot.OnStkObjectAdded += objModelRoot_OnStkObjectAdded;
				objModelRoot.OnStkObjectDeleted += objModelRoot_OnStkObjectDeleted;
                objModelRoot.OnStkObjectChanged += objModelRoot_OnStkObjectChanged;
				objModelRoot.OnStkObjectRenamed += objModelRoot_OnStkObjectRenamed;
			}
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
				this.Close();
			}
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

			if(objModelRoot == null) 
			{
				return;
			}
			try {
				/*
				 * Disconnect from the Object Model Events.
				 */
                objModelRoot.OnAnimationFaster -= objModelRoot_OnAnimationFaster;
				objModelRoot.OnAnimationPause -= objModelRoot_OnAnimationPause;
				objModelRoot.OnAnimationPlayback -= objModelRoot_OnAnimationPlayback;
				objModelRoot.OnAnimationRewind -= objModelRoot_OnAnimationRewind;
                objModelRoot.OnAnimationSlower -= objModelRoot_OnAnimationSlower;
                objModelRoot.OnAnimationStep -= objModelRoot_OnAnimationStep;
                objModelRoot.OnAnimationStepBack -= objModelRoot_OnAnimationStepBack;
				objModelRoot.OnAnimUpdate -= objModelRoot_OnAnimUpdate;
				objModelRoot.OnLogMessage -= objModelRoot_OnLogMessage;
                objModelRoot.OnPercentCompleteBegin -= objModelRoot_OnPercentCompleteBegin;
                objModelRoot.OnPercentCompleteEnd -= objModelRoot_OnPercentCompleteEnd;
                objModelRoot.OnPercentCompleteUpdate -= objModelRoot_OnPercentCompleteUpdate;
                objModelRoot.OnScenarioBeforeClose -= objModelRoot_OnScenarioBeforeClose;
                objModelRoot.OnScenarioBeforeSave -= objModelRoot_OnScenarioBeforeSave;
				objModelRoot.OnScenarioClose -= objModelRoot_OnScenarioClose;
				objModelRoot.OnScenarioLoad -= objModelRoot_OnScenarioLoad;
				objModelRoot.OnScenarioNew -= objModelRoot_OnScenarioNew;
				objModelRoot.OnScenarioSave -= objModelRoot_OnScenarioSave;
				objModelRoot.OnStkObjectAdded -= objModelRoot_OnStkObjectAdded;
				objModelRoot.OnStkObjectDeleted -= objModelRoot_OnStkObjectDeleted;
                objModelRoot.OnStkObjectChanged -= objModelRoot_OnStkObjectChanged;
				objModelRoot.OnStkObjectRenamed -= objModelRoot_OnStkObjectRenamed;

				/*
				 * Close the scenario
				 */
				objModelRoot.CloseScenario();
				/*
				 * Decrement a reference count of the runtime callable wrapper
				 */
				System.Runtime.InteropServices.Marshal.ReleaseComObject(objModelRoot);

				/*
				 * Release the STK Application object
				 */
				System.Runtime.InteropServices.Marshal.ReleaseComObject(stkApplication);

				stkApplication = null;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.ToString());
			}
		}

		#region Object Model Event Handlers

        /// <summary>
        /// Occurs whenever the animation speed is increased
        /// </summary>
        /// <param name="CurrentTime"></param>
        private void objModelRoot_OnAnimationFaster()
        {
            LogEvent(AgEEventTypes.eAnimationFaster);
        }

		/// <summary>
		/// Occurs whenever the animation is paused 
		/// </summary>
		/// <param name="CurrentTime"></param>
		private void objModelRoot_OnAnimationPause(double CurrentTime) 
		{
			LogEvent(AgEEventTypes.eAnimationPause, string.Format("CurrentTime: {0}", CurrentTime));
		}

		/// <summary>
		/// Occurs each animation cycle
		/// </summary>
		/// <param name="CurrentTime"></param>
		/// <param name="eAction"></param>
		/// <param name="eDirection"></param>
		private void objModelRoot_OnAnimationPlayback(double CurrentTime, AGI.STKObjects.AgEAnimationActions eAction, AGI.STKObjects.AgEAnimationDirections eDirection) 
		{
			LogEvent(AgEEventTypes.eAnimationPlayback, string.Format("CurrentTime: {0}, Direction: {1}", CurrentTime, eDirection));
		}

		/// <summary>
		/// Occurs when the animation is reset
		/// </summary>
		private void objModelRoot_OnAnimationRewind() 
		{
			LogEvent(AgEEventTypes.eAnimationRewind);
		}

        /// <summary>
        /// Occurs when the animation is stepped forward
        /// </summary>
        private void objModelRoot_OnAnimationStep(double CurrentTime)
        {
            LogEvent(AgEEventTypes.eAnimationStep, string.Format("CurrentTime: {0}", CurrentTime));
        }

        /// <summary>
        /// Occurs when the animation is stepped backward
        /// </summary>
        private void objModelRoot_OnAnimationStepBack(double CurrentTime)
        {
            LogEvent(AgEEventTypes.eAnimationStepBack, string.Format("CurrentTime: {0}", CurrentTime));
        }

        /// <summary>
        /// Occurs whenever the animation speed is decreased
        /// </summary>
        /// <param name="CurrentTime"></param>
        private void objModelRoot_OnAnimationSlower()
        {
            LogEvent(AgEEventTypes.eAnimationSlower);
        }

		/// <summary>
		/// Similar to the OnAnimationPlayback. Occurs each animation cycle
		/// </summary>
		/// <param name="TimeEpSec"></param>
		private void objModelRoot_OnAnimUpdate(double TimeEpSec) 
		{
			LogEvent(AgEEventTypes.eAnimationUpdate, string.Format("TimeEpSec: {0}", TimeEpSec));
		}

		/// <summary>
		/// Occurs when a new log message has been added to the message log 
		/// </summary>
		/// <param name="Message"></param>
		/// <param name="MsgType"></param>
		/// <param name="ErrorCode"></param>
		/// <param name="Filename"></param>
		/// <param name="LineNo"></param>
		/// <param name="DispID"></param>
		private void objModelRoot_OnLogMessage(string Message, AGI.STKUtil.AgELogMsgType MsgType, int ErrorCode, string Filename, int LineNo, AGI.STKUtil.AgELogMsgDispID DispID) 
		{
			LogEvent(AgEEventTypes.eLogMessage, string.Format("MsgType: {0}, ErrorCode: {1}, FileName: {2}, LineNo: {3}, MsgDispID: {4}, Message: {5}", 
				MsgType, ErrorCode, Filename, LineNo, DispID, Message));
		}

        /// <summary>
        /// Occurs when a lengthy operation is about to start
        /// </summary>
        private void objModelRoot_OnPercentCompleteBegin()
        {
            LogEvent(AgEEventTypes.ePercentCompleteBegin);
        }

        /// <summary>
        /// Occurs upon when a currently running lengthy operation is finished
        /// </summary>
        private void objModelRoot_OnPercentCompleteEnd()
        {
            LogEvent(AgEEventTypes.ePercentCompleteEnd);
        }

        /// <summary>
        /// Occurs upon changes to the status of a currently running lengthy operation 
        /// </summary>
        private void objModelRoot_OnPercentCompleteUpdate(IAgPctCmpltEventArgs pArgs)
        {
            LogEvent(AgEEventTypes.ePercentCompleteUpdate, string.Format("Message: {0}, PercentCompleted: {1}", pArgs.Message, pArgs.PercentCompleted));
        }

        /// <summary>
        /// Occurs before a current scenario is closed
        /// </summary>
        private void objModelRoot_OnScenarioBeforeClose()
        {
            LogEvent(AgEEventTypes.eScenarioBeforeClose);
        }

        /// <summary>
        /// Occurs before a current scenario is saved
        /// </summary>
        private void objModelRoot_OnScenarioBeforeSave(IAgScenarioBeforeSaveEventArgs pArgs)
        {
            LogEvent(AgEEventTypes.eScenarioBeforeSave, string.Format("Path: {0}", pArgs.Path));
        }

		/// <summary>
		/// Occurs when a current scenario is closed
		/// </summary>
		private void objModelRoot_OnScenarioClose() 
		{
			LogEvent(AgEEventTypes.eScenarioClose);
		}

		/// <summary>
		/// Occurs when existing scenario is loaded
		/// </summary>
		/// <param name="Path"></param>
		private void objModelRoot_OnScenarioLoad(string Path) 
		{
			LogEvent(AgEEventTypes.eScenarioLoad, string.Format("Path: {0}", Path));
			RebuildScenObjectList();
		}

		/// <summary>
		/// Occurs when a new scenario is created
		/// </summary>
		/// <param name="Path"></param>
		private void objModelRoot_OnScenarioNew(string Path) 
		{
			LogEvent(AgEEventTypes.eScenarioNew, string.Format("Path: {0}", Path));
		}

		/// <summary>
		/// Occurs when a current scenario is saved
		/// </summary>
		/// <param name="Path"></param>
		private void objModelRoot_OnScenarioSave(string Path) 
		{
			LogEvent(AgEEventTypes.eScenarioSave, string.Format("Path: {0}", Path));
		}

		/// <summary>
		/// Occurs when a new object is added to the scenario
		/// </summary>
		/// <param name="Sender"></param>
		private void objModelRoot_OnStkObjectAdded(object Sender) 
		{
			LogEvent(AgEEventTypes.eObjectAdded, string.Format("Sender: {0}", Sender));
			RebuildScenObjectList();
		}

		/// <summary>
		/// Occurs when object is removed from the scenario
		/// </summary>
		/// <param name="Sender"></param>
		private void objModelRoot_OnStkObjectDeleted(object Sender) 
		{
			LogEvent(AgEEventTypes.eObjectDeleted, string.Format("Sender: {0}", Sender));
			RebuildScenObjectList();
		}

        private void objModelRoot_OnStkObjectChanged(IAgStkObjectChangedEventArgs pArgs)
        {
            LogEvent(AgEEventTypes.eObjectChanged, string.Format("Sender: {0}", pArgs.Path));
            //RebuildScenObjectList();
        }

		/// <summary>
		/// Occurs when object is renamed
		/// </summary>
		/// <param name="Sender"></param>
		/// <param name="OldPath"></param>
		/// <param name="NewPath"></param>
		private void objModelRoot_OnStkObjectRenamed(object Sender, string OldPath, string NewPath) 
		{
			LogEvent(AgEEventTypes.eObjectRenamed, string.Format("Sender: {0}, OldPath: {1}, NewPath: {2}", Sender, OldPath, NewPath));
		}

		#endregion // Object Model Event Handlers

		#region User Interface Control Handlers

		private void btnNewScen_Click(object sender, System.EventArgs e) 
		{
			this.lstEventLog.Items.Clear();

            objModelRoot.CloseScenario();
			objModelRoot.NewScenario("EventDemo");

			this.btnNewScen.Enabled = false;

			this.btnAreaTarget.Enabled = true;
			this.btnCloseScen.Enabled = true;
			this.btnNewFacility.Enabled = true;
			this.btnNewSat.Enabled = true;
			this.btnUnloadObjects.Enabled = true;
			this.lstScenarioObjects.Enabled = true;
			this.btnPlay.Enabled = true;
			this.btnStop.Enabled = true;
			this.btnPlayBackward.Enabled = true;
			this.btnPause.Enabled = true;
            this.btnStep.Enabled = true;
            this.btnStepBack.Enabled = true;
            this.btnIncreaseStep.Enabled = true;
            this.btnDecreaseStep.Enabled = true;
		}

		private void btnNewSat_Click(object sender, System.EventArgs e) 
		{
			AGI.STKObjects.IAgSatellite sat = (AGI.STKObjects.IAgSatellite)objModelRoot.CurrentScenario.Children.New(AGI.STKObjects.AgESTKObjectType.eSatellite, "Satellite" + (autoIncrement).ToString());
			sat.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorTwoBody);
			AGI.STKObjects.IAgVePropagatorTwoBody twobody = (AGI.STKObjects.IAgVePropagatorTwoBody)sat.Propagator;
			IAgOrbitStateClassical classical = (IAgOrbitStateClassical)twobody.InitialState.Representation.ConvertTo(AGI.STKUtil.AgEOrbitStateType.eOrbitStateClassical);
			classical.Orientation.Inclination = new Random().Next(180);
			classical.SizeShapeType = AgEClassicalSizeShape.eSizeShapeRadius;
			IAgClassicalSizeShapeRadius radius = (IAgClassicalSizeShapeRadius)classical.SizeShape;
			radius.PerigeeRadius = new Random().Next(6500, 45000);
			radius.PerigeeRadius = new Random().Next(6500, 45000);
			twobody.InitialState.Representation.Assign(classical);
			twobody.Propagate();
			++autoIncrement;
		}

		private void btnNewFacility_Click(object sender, System.EventArgs e) 
		{
            AGI.STKObjects.IAgFacility facility = (AGI.STKObjects.IAgFacility)objModelRoot.CurrentScenario.Children.New(AGI.STKObjects.AgESTKObjectType.eFacility, "Facility" + (autoIncrement).ToString());

            IAgPlanetodetic planetodetic = facility.Position.ConvertTo(AgEPositionType.ePlanetodetic) as IAgPlanetodetic;
            // Pick a random location
            planetodetic.Lat = new Random().Next(-90, 90);
            planetodetic.Lon = new Random().Next(-180, 180);
            // Set facility position
            facility.Position.Assign(planetodetic);
			++autoIncrement;
		}

		private void btnAreaTarget_Click(object sender, System.EventArgs e) 
		{
            AGI.STKObjects.IAgAreaTarget target = (AGI.STKObjects.IAgAreaTarget)objModelRoot.CurrentScenario.Children.New(AGI.STKObjects.AgESTKObjectType.eAreaTarget, "AreaTarget" + (autoIncrement).ToString());

            target.AutoCentroid = false;
            IAgPlanetodetic planetodetic = target.Position.ConvertTo(AgEPositionType.ePlanetodetic) as IAgPlanetodetic;
            // Pick a random location
            planetodetic.Lat = new Random().Next(-90, 90);
            planetodetic.Lon = new Random().Next(-180, 180);
            // Set facility position
            target.Position.Assign(planetodetic);
			++autoIncrement;
		}

		private void btnCloseScen_Click(object sender, System.EventArgs e) 
		{
			objModelRoot.CloseScenario();

			this.btnNewScen.Enabled = true;

			this.btnAreaTarget.Enabled = false;
			this.btnCloseScen.Enabled = false;
			this.btnNewFacility.Enabled = false;
			this.btnNewSat.Enabled = false;
			this.btnUnloadObjects.Enabled = false;
			this.lstScenarioObjects.Enabled = false;

			this.btnPlay.Enabled = false;
			this.btnStop.Enabled = false;
			this.btnPlayBackward.Enabled = false;
			this.btnPause.Enabled = false;
            this.btnStep.Enabled = false;
            this.btnStepBack.Enabled = false;
            this.btnIncreaseStep.Enabled = false;
            this.btnDecreaseStep.Enabled = false;
		}

		private void btnPlay_Click(object sender, System.EventArgs e) 
		{
			objModelRoot.PlayForward();
		}

		private void btnPause_Click(object sender, System.EventArgs e) 
		{
			objModelRoot.Pause();
		}

		private void btnPlayBackward_Click(object sender, System.EventArgs e) 
		{
			objModelRoot.PlayBackward();
		}

		private void btnStop_Click(object sender, System.EventArgs e) 
		{
			objModelRoot.Rewind();
		}

		private void btnUnloadObjects_Click(object sender, System.EventArgs e) 
		{
			foreach(AGI.STKObjects.IAgStkObject o in objModelRoot.CurrentScenario.Children) 
			{
				objModelRoot.CurrentScenario.Children.Unload(o.ClassType, o.InstanceName);
			}
		}

        private void btnStep_Click(object sender, EventArgs e)
        {
            objModelRoot.StepForward();
        }

        private void btnStepBack_Click(object sender, EventArgs e)
        {
            objModelRoot.StepBackward();
        }

        private void btnIncreaseStep_Click(object sender, EventArgs e)
        {
            objModelRoot.Faster();
        }

        private void btnDecreaseStep_Click(object sender, EventArgs e)
        {
            objModelRoot.Slower();
        }

		#endregion
	}
}
