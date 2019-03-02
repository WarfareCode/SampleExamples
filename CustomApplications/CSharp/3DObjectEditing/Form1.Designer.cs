namespace _DObjectEditing
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
            this.editCancel = new System.Windows.Forms.Button();
            this.editApply = new System.Windows.Forms.Button();
            this.editOk = new System.Windows.Forms.Button();
            this.editStart = new System.Windows.Forms.Button();
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.SuspendLayout();
            // 
            // editCancel
            // 
            this.editCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editCancel.Enabled = false;
            this.editCancel.Image = global::_DObjectEditing.Properties.Resources.editCancel;
            this.editCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editCancel.Location = new System.Drawing.Point(343, 440);
            this.editCancel.Name = "editCancel";
            this.editCancel.Size = new System.Drawing.Size(82, 25);
            this.editCancel.TabIndex = 4;
            this.editCancel.Text = "Edit Cancel";
            this.editCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editCancel.UseVisualStyleBackColor = true;
            this.editCancel.Click += new System.EventHandler(this.editCancel_Click);
            // 
            // editApply
            // 
            this.editApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editApply.Enabled = false;
            this.editApply.Image = global::_DObjectEditing.Properties.Resources.editApply;
            this.editApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editApply.Location = new System.Drawing.Point(255, 440);
            this.editApply.Name = "editApply";
            this.editApply.Size = new System.Drawing.Size(82, 25);
            this.editApply.TabIndex = 3;
            this.editApply.Text = "Edit Apply";
            this.editApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editApply.UseVisualStyleBackColor = true;
            this.editApply.Click += new System.EventHandler(this.editApply_Click);
            // 
            // editOk
            // 
            this.editOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editOk.Enabled = false;
            this.editOk.Image = global::_DObjectEditing.Properties.Resources.editApply;
            this.editOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editOk.Location = new System.Drawing.Point(167, 440);
            this.editOk.Name = "editOk";
            this.editOk.Size = new System.Drawing.Size(82, 25);
            this.editOk.TabIndex = 2;
            this.editOk.Text = "Edit Ok";
            this.editOk.UseVisualStyleBackColor = true;
            this.editOk.Click += new System.EventHandler(this.editOk_Click);
            // 
            // editStart
            // 
            this.editStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editStart.Image = global::_DObjectEditing.Properties.Resources.editStart;
            this.editStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.editStart.Location = new System.Drawing.Point(79, 440);
            this.editStart.Name = "editStart";
            this.editStart.Size = new System.Drawing.Size(82, 25);
            this.editStart.TabIndex = 1;
            this.editStart.Text = "Edit Start";
            this.editStart.UseVisualStyleBackColor = true;
            this.editStart.Click += new System.EventHandler(this.editStart_Click);
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(12, 12);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(481, 422);
            this.axAgUiAxVOCntrl1.TabIndex = 0;
            this.axAgUiAxVOCntrl1.OnObjectEditingStart += new AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingStartEventHandler(this.axAgUiAxVOCntrl1_OnObjectEditingStart);
            this.axAgUiAxVOCntrl1.OnObjectEditingCancel += new AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingCancelEventHandler(this.axAgUiAxVOCntrl1_OnObjectEditingCancel);
            this.axAgUiAxVOCntrl1.OnObjectEditingApply += new AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingApplyEventHandler(this.axAgUiAxVOCntrl1_OnObjectEditingApply);
            this.axAgUiAxVOCntrl1.OnObjectEditingStop += new AxAGI.STKX.IAgUiAxVOCntrlEvents_OnObjectEditingStopEventHandler(this.axAgUiAxVOCntrl1_OnObjectEditingStop);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 471);
            this.Controls.Add(this.editCancel);
            this.Controls.Add(this.editApply);
            this.Controls.Add(this.editOk);
            this.Controls.Add(this.editStart);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "3D Object Editing Sample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private System.Windows.Forms.Button editStart;
        private System.Windows.Forms.Button editOk;
        private System.Windows.Forms.Button editApply;
        private System.Windows.Forms.Button editCancel;
    }
}

