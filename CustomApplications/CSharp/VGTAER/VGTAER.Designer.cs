namespace VGTAER
{
    partial class VGTAER
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VGTAER));
            this.STKControl3D = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Rewind = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rangeValueLabel = new System.Windows.Forms.Label();
            this.elevationValueLabel = new System.Windows.Forms.Label();
            this.azimuthValueLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // STKControl3D
            // 
            this.STKControl3D.Enabled = true;
            this.STKControl3D.Location = new System.Drawing.Point(0, 0);
            this.STKControl3D.Name = "STKControl3D";
            this.STKControl3D.Size = new System.Drawing.Size(540, 479);
            this.STKControl3D.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Rewind);
            this.groupBox1.Controls.Add(this.Pause);
            this.groupBox1.Controls.Add(this.Play);
            this.groupBox1.Location = new System.Drawing.Point(546, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 45);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Control";
            // 
            // Rewind
            // 
            this.Rewind.Location = new System.Drawing.Point(102, 16);
            this.Rewind.Name = "Rewind";
            this.Rewind.Size = new System.Drawing.Size(53, 23);
            this.Rewind.TabIndex = 16;
            this.Rewind.Text = "Rewind";
            this.Rewind.UseVisualStyleBackColor = true;
            this.Rewind.Click += new System.EventHandler(this.Rewind_Click);
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(49, 16);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(47, 23);
            this.Pause.TabIndex = 15;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // Play
            // 
            this.Play.Location = new System.Drawing.Point(8, 16);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(35, 23);
            this.Play.TabIndex = 14;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(554, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Azimuth";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(554, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Elevation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(554, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Range";
            // 
            // rangeValueLabel
            // 
            this.rangeValueLabel.AutoSize = true;
            this.rangeValueLabel.Location = new System.Drawing.Point(614, 134);
            this.rangeValueLabel.Name = "rangeValueLabel";
            this.rangeValueLabel.Size = new System.Drawing.Size(0, 13);
            this.rangeValueLabel.TabIndex = 24;
            // 
            // elevationValueLabel
            // 
            this.elevationValueLabel.AutoSize = true;
            this.elevationValueLabel.Location = new System.Drawing.Point(614, 111);
            this.elevationValueLabel.Name = "elevationValueLabel";
            this.elevationValueLabel.Size = new System.Drawing.Size(0, 13);
            this.elevationValueLabel.TabIndex = 23;
            // 
            // azimuthValueLabel
            // 
            this.azimuthValueLabel.AutoSize = true;
            this.azimuthValueLabel.Location = new System.Drawing.Point(614, 87);
            this.azimuthValueLabel.Name = "azimuthValueLabel";
            this.azimuthValueLabel.Size = new System.Drawing.Size(0, 13);
            this.azimuthValueLabel.TabIndex = 22;
            // 
            // VGTAER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 478);
            this.Controls.Add(this.rangeValueLabel);
            this.Controls.Add(this.elevationValueLabel);
            this.Controls.Add(this.azimuthValueLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.STKControl3D);
            this.Name = "VGTAER";
            this.Text = "VGT AER Example";
            this.Shown += new System.EventHandler(this.VgtAERCalculator_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VGTAER_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AGI.STKX.Controls.AxAgUiAxVOCntrl STKControl3D;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Rewind;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label rangeValueLabel;
        private System.Windows.Forms.Label elevationValueLabel;
        private System.Windows.Forms.Label azimuthValueLabel;
    }
}

