namespace MoonMissionWithBPlaneTargeting
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.axAgUiAxVOCntrl2 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.axAgUiAx2DCntrl1 = new AGI.STKX.Controls.AxAgUiAx2DCntrl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(830, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create Scenario";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(830, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Create Planets";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(830, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(148, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Spacecraft Graphics";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(830, 99);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(148, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "2D Graphics Window";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(830, 128);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(148, 37);
            this.button5.TabIndex = 7;
            this.button5.Text = "3D Graphics Window: Earth-Centered";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(830, 171);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(148, 37);
            this.button6.TabIndex = 8;
            this.button6.Text = "3D Graphics Window: Moon-Centered";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(830, 214);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(148, 23);
            this.button7.TabIndex = 9;
            this.button7.Text = "MCS Setup";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(830, 243);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(148, 36);
            this.button8.TabIndex = 10;
            this.button8.Text = "Trans-Lunar Injection: First Guess";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Enabled = false;
            this.button9.Location = new System.Drawing.Point(830, 285);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(148, 48);
            this.button9.TabIndex = 11;
            this.button9.Text = "Set up the targeter to calculate launch and coast times";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(830, 339);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(148, 23);
            this.button10.TabIndex = 12;
            this.button10.Text = "Run the targeter";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Enabled = false;
            this.button11.Location = new System.Drawing.Point(830, 368);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(148, 41);
            this.button11.TabIndex = 13;
            this.button11.Text = "Setting up the Targeter to Target on the B_Plane";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Enabled = false;
            this.button12.Location = new System.Drawing.Point(830, 415);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(148, 23);
            this.button12.TabIndex = 14;
            this.button12.Text = "Drawing the B-Plane";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.Enabled = false;
            this.button13.Location = new System.Drawing.Point(830, 444);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(148, 35);
            this.button13.TabIndex = 15;
            this.button13.Text = "Running the Targeter to Achieve B_Plane Params";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button14.Enabled = false;
            this.button14.Location = new System.Drawing.Point(830, 485);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(148, 36);
            this.button14.TabIndex = 16;
            this.button14.Text = "Targeting Altitude and Inclination";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button15.Enabled = false;
            this.button15.Location = new System.Drawing.Point(830, 527);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(148, 23);
            this.button15.TabIndex = 17;
            this.button15.Text = "Approaching the moon";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button16.Enabled = false;
            this.button16.Location = new System.Drawing.Point(830, 557);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(148, 23);
            this.button16.TabIndex = 18;
            this.button16.Text = "Lunar Orbit Insertion";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button17.Location = new System.Drawing.Point(864, 629);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 19;
            this.button17.Text = "Play";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button18.Location = new System.Drawing.Point(864, 687);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(75, 23);
            this.button18.TabIndex = 20;
            this.button18.Text = "Rewind";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button19.Location = new System.Drawing.Point(830, 658);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(66, 23);
            this.button19.TabIndex = 21;
            this.button19.Text = "Slower";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button20.Location = new System.Drawing.Point(910, 658);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(68, 23);
            this.button20.TabIndex = 22;
            this.button20.Text = "Faster";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAxVOCntrl1.Picture")));
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(410, 440);
            this.axAgUiAxVOCntrl1.TabIndex = 23;
            // 
            // axAgUiAxVOCntrl2
            // 
            this.axAgUiAxVOCntrl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAxVOCntrl2.Location = new System.Drawing.Point(410, 0);
            this.axAgUiAxVOCntrl2.Name = "axAgUiAxVOCntrl2";
            this.axAgUiAxVOCntrl2.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAxVOCntrl2.Picture")));
            this.axAgUiAxVOCntrl2.Size = new System.Drawing.Size(410, 440);
            this.axAgUiAxVOCntrl2.TabIndex = 24;
            // 
            // axAgUiAx2DCntrl1
            // 
            this.axAgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.axAgUiAx2DCntrl1.Location = new System.Drawing.Point(0, 440);
            this.axAgUiAx2DCntrl1.Name = "axAgUiAx2DCntrl1";
            this.axAgUiAx2DCntrl1.PanModeEnabled = true;
            this.axAgUiAx2DCntrl1.Picture = ((System.Drawing.Image)(resources.GetObject("axAgUiAx2DCntrl1.Picture")));
            this.axAgUiAx2DCntrl1.Size = new System.Drawing.Size(820, 357);
            this.axAgUiAx2DCntrl1.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 809);
            this.Controls.Add(this.axAgUiAx2DCntrl1);
            this.Controls.Add(this.axAgUiAxVOCntrl2);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Controls.Add(this.button20);
            this.Controls.Add(this.button19);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl2;
        private AGI.STKX.Controls.AxAgUiAx2DCntrl axAgUiAx2DCntrl1;
    }
}

