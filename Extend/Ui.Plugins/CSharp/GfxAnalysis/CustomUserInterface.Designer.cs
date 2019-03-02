namespace Agi.Ui.Plugins.CSharp.GfxAnalysis
{
    partial class CustomUserInterface
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.newScenarioBtn = new System.Windows.Forms.Button();
            this.newSatelliteBtn = new System.Windows.Forms.Button();
            this.newSensorBtn = new System.Windows.Forms.Button();
            this.computeBtn = new System.Windows.Forms.Button();
            this.closeScenarioBtn = new System.Windows.Forms.Button();
            this.gfxTypeCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // newScenarioBtn
            // 
            this.newScenarioBtn.Location = new System.Drawing.Point(3, 3);
            this.newScenarioBtn.Name = "newScenarioBtn";
            this.newScenarioBtn.Size = new System.Drawing.Size(87, 23);
            this.newScenarioBtn.TabIndex = 1;
            this.newScenarioBtn.Text = "New Scenario";
            this.newScenarioBtn.UseVisualStyleBackColor = true;
            this.newScenarioBtn.Click += new System.EventHandler(this.newScenarioBtn_Click);
            // 
            // newSatelliteBtn
            // 
            this.newSatelliteBtn.Location = new System.Drawing.Point(96, 3);
            this.newSatelliteBtn.Name = "newSatelliteBtn";
            this.newSatelliteBtn.Size = new System.Drawing.Size(86, 23);
            this.newSatelliteBtn.TabIndex = 2;
            this.newSatelliteBtn.Text = "New Satellite";
            this.newSatelliteBtn.UseVisualStyleBackColor = true;
            this.newSatelliteBtn.Click += new System.EventHandler(this.newSatelliteBtn_Click);
            // 
            // newSensorBtn
            // 
            this.newSensorBtn.Location = new System.Drawing.Point(188, 3);
            this.newSensorBtn.Name = "newSensorBtn";
            this.newSensorBtn.Size = new System.Drawing.Size(75, 23);
            this.newSensorBtn.TabIndex = 3;
            this.newSensorBtn.Text = "New Sensor";
            this.newSensorBtn.UseVisualStyleBackColor = true;
            this.newSensorBtn.Click += new System.EventHandler(this.newSensorBtn_Click);
            // 
            // computeBtn
            // 
            this.computeBtn.Location = new System.Drawing.Point(269, 3);
            this.computeBtn.Name = "computeBtn";
            this.computeBtn.Size = new System.Drawing.Size(75, 23);
            this.computeBtn.TabIndex = 4;
            this.computeBtn.Text = "Compute";
            this.computeBtn.UseVisualStyleBackColor = true;
            this.computeBtn.Click += new System.EventHandler(this.computeBtn_Click);
            // 
            // closeScenarioBtn
            // 
            this.closeScenarioBtn.Location = new System.Drawing.Point(350, 3);
            this.closeScenarioBtn.Name = "closeScenarioBtn";
            this.closeScenarioBtn.Size = new System.Drawing.Size(99, 23);
            this.closeScenarioBtn.TabIndex = 5;
            this.closeScenarioBtn.Text = "Close Scenario";
            this.closeScenarioBtn.UseVisualStyleBackColor = true;
            this.closeScenarioBtn.Click += new System.EventHandler(this.closeScenarioBtn_Click);
            // 
            // gfxTypeCombo
            // 
            this.gfxTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gfxTypeCombo.FormattingEnabled = true;
            this.gfxTypeCombo.Items.AddRange(new object[] {
            "Solar Panel Tool",
            "Area Tool",
            "Obscuration Tool",
            "AzElMask Tool"});
            this.gfxTypeCombo.Location = new System.Drawing.Point(3, 32);
            this.gfxTypeCombo.Name = "gfxTypeCombo";
            this.gfxTypeCombo.Size = new System.Drawing.Size(146, 21);
            this.gfxTypeCombo.TabIndex = 6;
            // 
            // CustomUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gfxTypeCombo);
            this.Controls.Add(this.closeScenarioBtn);
            this.Controls.Add(this.computeBtn);
            this.Controls.Add(this.newSensorBtn);
            this.Controls.Add(this.newSatelliteBtn);
            this.Controls.Add(this.newScenarioBtn);
            this.Name = "CustomUserInterface";
            this.Size = new System.Drawing.Size(985, 105);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newScenarioBtn;
        private System.Windows.Forms.Button newSatelliteBtn;
        private System.Windows.Forms.Button newSensorBtn;
        private System.Windows.Forms.Button computeBtn;
        private System.Windows.Forms.Button closeScenarioBtn;
        private System.Windows.Forms.ComboBox gfxTypeCombo;
    }
}
