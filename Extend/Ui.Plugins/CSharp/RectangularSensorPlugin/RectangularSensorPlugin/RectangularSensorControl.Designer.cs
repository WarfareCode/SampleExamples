namespace RectangularSensorPlugin
{
    partial class RectangularSensorControl
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
            this.rectangularSensorList = new System.Windows.Forms.ListBox();
            this.setProjectionPropertiesButton = new System.Windows.Forms.Button();
            this.addProjectionButton = new System.Windows.Forms.Button();
            this.activeProjectionList = new System.Windows.Forms.ListBox();
            this.removeProjectionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.editProjectionPropertiesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rectangularSensorList
            // 
            this.rectangularSensorList.FormattingEnabled = true;
            this.rectangularSensorList.HorizontalScrollbar = true;
            this.rectangularSensorList.Location = new System.Drawing.Point(24, 20);
            this.rectangularSensorList.Name = "rectangularSensorList";
            this.rectangularSensorList.Size = new System.Drawing.Size(237, 134);
            this.rectangularSensorList.TabIndex = 1;
            // 
            // setProjectionPropertiesButton
            // 
            this.setProjectionPropertiesButton.Location = new System.Drawing.Point(71, 160);
            this.setProjectionPropertiesButton.Name = "setProjectionPropertiesButton";
            this.setProjectionPropertiesButton.Size = new System.Drawing.Size(142, 23);
            this.setProjectionPropertiesButton.TabIndex = 3;
            this.setProjectionPropertiesButton.Text = "Set Projection Properties";
            this.setProjectionPropertiesButton.UseVisualStyleBackColor = true;
            this.setProjectionPropertiesButton.Click += new System.EventHandler(this.setProjectionPropertiesButton_Click);
            // 
            // addProjectionButton
            // 
            this.addProjectionButton.Location = new System.Drawing.Point(274, 53);
            this.addProjectionButton.Name = "addProjectionButton";
            this.addProjectionButton.Size = new System.Drawing.Size(125, 23);
            this.addProjectionButton.TabIndex = 7;
            this.addProjectionButton.Text = "Add Projection ->";
            this.addProjectionButton.UseVisualStyleBackColor = true;
            this.addProjectionButton.Click += new System.EventHandler(this.addProjectionButton_Click);
            // 
            // activeProjectionList
            // 
            this.activeProjectionList.FormattingEnabled = true;
            this.activeProjectionList.HorizontalScrollbar = true;
            this.activeProjectionList.Location = new System.Drawing.Point(410, 20);
            this.activeProjectionList.Name = "activeProjectionList";
            this.activeProjectionList.Size = new System.Drawing.Size(237, 134);
            this.activeProjectionList.TabIndex = 8;
            // 
            // removeProjectionButton
            // 
            this.removeProjectionButton.Location = new System.Drawing.Point(274, 82);
            this.removeProjectionButton.Name = "removeProjectionButton";
            this.removeProjectionButton.Size = new System.Drawing.Size(125, 23);
            this.removeProjectionButton.TabIndex = 9;
            this.removeProjectionButton.Text = "<- Remove Projection";
            this.removeProjectionButton.UseVisualStyleBackColor = true;
            this.removeProjectionButton.Click += new System.EventHandler(this.removeProjectionButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Available Sensors";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Active Projections";
            // 
            // editProjectionPropertiesButton
            // 
            this.editProjectionPropertiesButton.Location = new System.Drawing.Point(458, 160);
            this.editProjectionPropertiesButton.Name = "editProjectionPropertiesButton";
            this.editProjectionPropertiesButton.Size = new System.Drawing.Size(141, 23);
            this.editProjectionPropertiesButton.TabIndex = 13;
            this.editProjectionPropertiesButton.Text = "Edit Projection Properties";
            this.editProjectionPropertiesButton.UseVisualStyleBackColor = true;
            this.editProjectionPropertiesButton.Click += new System.EventHandler(this.editProjectionPropertiesButton_Click);
            // 
            // RectangularSensorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editProjectionPropertiesButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeProjectionButton);
            this.Controls.Add(this.activeProjectionList);
            this.Controls.Add(this.addProjectionButton);
            this.Controls.Add(this.setProjectionPropertiesButton);
            this.Controls.Add(this.rectangularSensorList);
            this.Name = "RectangularSensorControl";
            this.Size = new System.Drawing.Size(662, 190);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox rectangularSensorList;
        private System.Windows.Forms.Button setProjectionPropertiesButton;
        private System.Windows.Forms.Button addProjectionButton;
        private System.Windows.Forms.ListBox activeProjectionList;
        private System.Windows.Forms.Button removeProjectionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button editProjectionPropertiesButton;
    }
}
