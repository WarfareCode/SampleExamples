namespace VGTTutorial
{
    partial class VGT_Tutorial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VGT_Tutorial));
            this.Create_Angle = new System.Windows.Forms.Button();
            this.ActionsBox = new System.Windows.Forms.GroupBox();
            this.Show_Velocity_Vector = new System.Windows.Forms.Button();
            this.Create_Plane = new System.Windows.Forms.Button();
            this.Create_Axes = new System.Windows.Forms.Button();
            this.Close_Scenario = new System.Windows.Forms.Button();
            this.Create_Vector = new System.Windows.Forms.Button();
            this.Create_Scenario = new System.Windows.Forms.Button();
            this.Create_Satellite = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDistanceFromSatToFacility = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DescriptionBox = new System.Windows.Forms.GroupBox();
            this.MainLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_ResetAnim = new System.Windows.Forms.ToolStripButton();
            this.Button_StepBack = new System.Windows.Forms.ToolStripButton();
            this.Button_ReverseAnim = new System.Windows.Forms.ToolStripButton();
            this.Button_Pause = new System.Windows.Forms.ToolStripButton();
            this.Button_Play = new System.Windows.Forms.ToolStripButton();
            this.Button_StepAhead = new System.Windows.Forms.ToolStripButton();
            this.Button_SlowDown = new System.Windows.Forms.ToolStripButton();
            this.Button_SpeedUp = new System.Windows.Forms.ToolStripButton();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.ActionsBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DescriptionBox.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Create_Angle
            // 
            this.Create_Angle.AutoSize = true;
            this.Create_Angle.Enabled = false;
            this.Create_Angle.Location = new System.Drawing.Point(183, 73);
            this.Create_Angle.Name = "Create_Angle";
            this.Create_Angle.Size = new System.Drawing.Size(129, 23);
            this.Create_Angle.TabIndex = 7;
            this.Create_Angle.Text = "Create Angle";
            this.Create_Angle.UseVisualStyleBackColor = true;
            this.Create_Angle.Click += new System.EventHandler(this.CreateAngle);
            // 
            // ActionsBox
            // 
            this.ActionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ActionsBox.AutoSize = true;
            this.ActionsBox.Controls.Add(this.Show_Velocity_Vector);
            this.ActionsBox.Controls.Add(this.Create_Plane);
            this.ActionsBox.Controls.Add(this.Create_Axes);
            this.ActionsBox.Controls.Add(this.Close_Scenario);
            this.ActionsBox.Controls.Add(this.Create_Angle);
            this.ActionsBox.Controls.Add(this.Create_Vector);
            this.ActionsBox.Controls.Add(this.Create_Scenario);
            this.ActionsBox.Controls.Add(this.Create_Satellite);
            this.ActionsBox.Location = new System.Drawing.Point(533, 4);
            this.ActionsBox.MinimumSize = new System.Drawing.Size(337, 145);
            this.ActionsBox.Name = "ActionsBox";
            this.ActionsBox.Size = new System.Drawing.Size(337, 145);
            this.ActionsBox.TabIndex = 2;
            this.ActionsBox.TabStop = false;
            this.ActionsBox.Text = "Actions";
            // 
            // Show_Velocity_Vector
            // 
            this.Show_Velocity_Vector.AutoSize = true;
            this.Show_Velocity_Vector.Enabled = false;
            this.Show_Velocity_Vector.Location = new System.Drawing.Point(32, 101);
            this.Show_Velocity_Vector.Name = "Show_Velocity_Vector";
            this.Show_Velocity_Vector.Size = new System.Drawing.Size(129, 23);
            this.Show_Velocity_Vector.TabIndex = 14;
            this.Show_Velocity_Vector.Text = "Show Velocity Vector";
            this.Show_Velocity_Vector.UseVisualStyleBackColor = true;
            this.Show_Velocity_Vector.Click += new System.EventHandler(this.ShowVelocityVector);
            // 
            // Create_Plane
            // 
            this.Create_Plane.AutoSize = true;
            this.Create_Plane.Enabled = false;
            this.Create_Plane.Location = new System.Drawing.Point(183, 45);
            this.Create_Plane.Name = "Create_Plane";
            this.Create_Plane.Size = new System.Drawing.Size(129, 23);
            this.Create_Plane.TabIndex = 12;
            this.Create_Plane.Text = "Create Plane";
            this.Create_Plane.UseVisualStyleBackColor = true;
            this.Create_Plane.Click += new System.EventHandler(this.CreatePlane);
            // 
            // Create_Axes
            // 
            this.Create_Axes.AutoSize = true;
            this.Create_Axes.Enabled = false;
            this.Create_Axes.Location = new System.Drawing.Point(183, 17);
            this.Create_Axes.Name = "Create_Axes";
            this.Create_Axes.Size = new System.Drawing.Size(129, 23);
            this.Create_Axes.TabIndex = 11;
            this.Create_Axes.Text = "Create Axes";
            this.Create_Axes.UseVisualStyleBackColor = true;
            this.Create_Axes.Click += new System.EventHandler(this.CreateAxes);
            // 
            // Close_Scenario
            // 
            this.Close_Scenario.AutoSize = true;
            this.Close_Scenario.Enabled = false;
            this.Close_Scenario.Location = new System.Drawing.Point(183, 101);
            this.Close_Scenario.Name = "Close_Scenario";
            this.Close_Scenario.Size = new System.Drawing.Size(129, 23);
            this.Close_Scenario.TabIndex = 10;
            this.Close_Scenario.Text = "Exit";
            this.Close_Scenario.UseVisualStyleBackColor = true;
            this.Close_Scenario.Click += new System.EventHandler(this.Unload);
            // 
            // Create_Vector
            // 
            this.Create_Vector.AutoSize = true;
            this.Create_Vector.Enabled = false;
            this.Create_Vector.Location = new System.Drawing.Point(32, 73);
            this.Create_Vector.Name = "Create_Vector";
            this.Create_Vector.Size = new System.Drawing.Size(129, 23);
            this.Create_Vector.TabIndex = 6;
            this.Create_Vector.Text = "Create Vector";
            this.Create_Vector.UseVisualStyleBackColor = true;
            this.Create_Vector.Click += new System.EventHandler(this.CreateVector);
            // 
            // Create_Scenario
            // 
            this.Create_Scenario.AutoSize = true;
            this.Create_Scenario.Location = new System.Drawing.Point(32, 17);
            this.Create_Scenario.Name = "Create_Scenario";
            this.Create_Scenario.Size = new System.Drawing.Size(129, 23);
            this.Create_Scenario.TabIndex = 0;
            this.Create_Scenario.Text = "New Scenario";
            this.Create_Scenario.UseVisualStyleBackColor = true;
            this.Create_Scenario.Click += new System.EventHandler(this.NewScenario);
            // 
            // Create_Satellite
            // 
            this.Create_Satellite.AutoSize = true;
            this.Create_Satellite.Enabled = false;
            this.Create_Satellite.Location = new System.Drawing.Point(32, 45);
            this.Create_Satellite.Name = "Create_Satellite";
            this.Create_Satellite.Size = new System.Drawing.Size(129, 23);
            this.Create_Satellite.TabIndex = 1;
            this.Create_Satellite.Text = "Create Satellite";
            this.Create_Satellite.UseVisualStyleBackColor = true;
            this.Create_Satellite.Click += new System.EventHandler(this.CreateSatellite);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(873, 155);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.DescriptionBox);
            this.panel1.Controls.Add(this.ActionsBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 456);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 155);
            this.panel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.txtDistanceFromSatToFacility);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(294, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 145);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information";
            // 
            // txtDistanceFromSatToFacility
            // 
            this.txtDistanceFromSatToFacility.Location = new System.Drawing.Point(10, 36);
            this.txtDistanceFromSatToFacility.Name = "txtDistanceFromSatToFacility";
            this.txtDistanceFromSatToFacility.ReadOnly = true;
            this.txtDistanceFromSatToFacility.Size = new System.Drawing.Size(211, 20);
            this.txtDistanceFromSatToFacility.TabIndex = 1;
            this.txtDistanceFromSatToFacility.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Distance from Satellite to Facility:";
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.DescriptionBox.Controls.Add(this.MainLabel);
            this.DescriptionBox.Location = new System.Drawing.Point(3, 4);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(284, 145);
            this.DescriptionBox.TabIndex = 3;
            this.DescriptionBox.TabStop = false;
            this.DescriptionBox.Text = "Description";
            // 
            // MainLabel
            // 
            this.MainLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLabel.Location = new System.Drawing.Point(3, 16);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(278, 126);
            this.MainLabel.TabIndex = 0;
            this.MainLabel.Text = "Txt to be filled in";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_ResetAnim,
            this.Button_StepBack,
            this.Button_ReverseAnim,
            this.Button_Pause,
            this.Button_Play,
            this.Button_StepAhead,
            this.Button_SlowDown,
            this.Button_SpeedUp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(873, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_ResetAnim
            // 
            this.Button_ResetAnim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_ResetAnim.Image = global::VGTTutorial.Properties.Resources.reset;
            this.Button_ResetAnim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_ResetAnim.Name = "Button_ResetAnim";
            this.Button_ResetAnim.Size = new System.Drawing.Size(23, 22);
            this.Button_ResetAnim.Text = "Reset";
            this.Button_ResetAnim.Click += new System.EventHandler(this.resetAnimButton_Click);
            // 
            // Button_StepBack
            // 
            this.Button_StepBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_StepBack.Image = global::VGTTutorial.Properties.Resources.stepbak;
            this.Button_StepBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_StepBack.Name = "Button_StepBack";
            this.Button_StepBack.Size = new System.Drawing.Size(23, 22);
            this.Button_StepBack.Text = "StepBackword";
            this.Button_StepBack.Click += new System.EventHandler(this.stepRevAnimButton_Click);
            // 
            // Button_ReverseAnim
            // 
            this.Button_ReverseAnim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_ReverseAnim.Image = global::VGTTutorial.Properties.Resources.playbak;
            this.Button_ReverseAnim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_ReverseAnim.Name = "Button_ReverseAnim";
            this.Button_ReverseAnim.Size = new System.Drawing.Size(23, 22);
            this.Button_ReverseAnim.Text = "PlayReverse";
            this.Button_ReverseAnim.Click += new System.EventHandler(this.startRevAnimButton_Click);
            // 
            // Button_Pause
            // 
            this.Button_Pause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_Pause.Image = global::VGTTutorial.Properties.Resources.pause;
            this.Button_Pause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Pause.Name = "Button_Pause";
            this.Button_Pause.Size = new System.Drawing.Size(23, 22);
            this.Button_Pause.Text = "Pause";
            this.Button_Pause.Click += new System.EventHandler(this.pauseAnimButton_Click);
            // 
            // Button_Play
            // 
            this.Button_Play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_Play.Image = global::VGTTutorial.Properties.Resources.play;
            this.Button_Play.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Play.Name = "Button_Play";
            this.Button_Play.Size = new System.Drawing.Size(23, 22);
            this.Button_Play.Text = "Play";
            this.Button_Play.Click += new System.EventHandler(this.startFwdAnimButton_Click);
            // 
            // Button_StepAhead
            // 
            this.Button_StepAhead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_StepAhead.Image = global::VGTTutorial.Properties.Resources.step;
            this.Button_StepAhead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_StepAhead.Name = "Button_StepAhead";
            this.Button_StepAhead.Size = new System.Drawing.Size(23, 22);
            this.Button_StepAhead.Text = "StepForward";
            this.Button_StepAhead.Click += new System.EventHandler(this.stepFwdAnimButton_Click);
            // 
            // Button_SlowDown
            // 
            this.Button_SlowDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_SlowDown.Image = global::VGTTutorial.Properties.Resources.slowdown;
            this.Button_SlowDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_SlowDown.Name = "Button_SlowDown";
            this.Button_SlowDown.Size = new System.Drawing.Size(23, 22);
            this.Button_SlowDown.Text = "Decrease Speed";
            this.Button_SlowDown.Click += new System.EventHandler(this.decTimeStepAnimButton_Click);
            // 
            // Button_SpeedUp
            // 
            this.Button_SpeedUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_SpeedUp.Image = global::VGTTutorial.Properties.Resources.speedup;
            this.Button_SpeedUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_SpeedUp.Name = "Button_SpeedUp";
            this.Button_SpeedUp.Size = new System.Drawing.Size(23, 22);
            this.Button_SpeedUp.Text = "Increase Speed";
            this.Button_SpeedUp.Click += new System.EventHandler(this.incTimeStepAnimButton_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 25);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(873, 431);
            this.axAgUiAxVOCntrl1.TabIndex = 5;
            // 
            // VGT_Tutorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(873, 611);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "VGT_Tutorial";
            this.Text = "VGT Tutorial";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VGT_Tutorial_FormClosing);
            this.ActionsBox.ResumeLayout(false);
            this.ActionsBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DescriptionBox.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ActionsBox;
        private System.Windows.Forms.Button Create_Scenario;
        private System.Windows.Forms.Button Create_Satellite;
        private System.Windows.Forms.Button Close_Scenario;
        private System.Windows.Forms.Button Create_Vector;
        private System.Windows.Forms.Button Create_Angle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button Create_Plane;
        private System.Windows.Forms.Button Create_Axes;
        private System.Windows.Forms.GroupBox DescriptionBox;
        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.ToolStripButton Button_Play;
        private System.Windows.Forms.ToolStripButton Button_Pause;
        private System.Windows.Forms.ToolStripButton Button_ReverseAnim;
        private System.Windows.Forms.ToolStripButton Button_ResetAnim;
        private System.Windows.Forms.ToolStripButton Button_SpeedUp;
        private System.Windows.Forms.ToolStripButton Button_SlowDown;
        private System.Windows.Forms.ToolStripButton Button_StepAhead;
        private System.Windows.Forms.ToolStripButton Button_StepBack;
        private System.Windows.Forms.Button Show_Velocity_Vector;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDistanceFromSatToFacility;

    }
}

