namespace PolylineDrawing
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
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.SuspendLayout();
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(616, 410);
            this.axAgUiAxVOCntrl1.TabIndex = 0;
            this.axAgUiAxVOCntrl1.MouseMoveEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEventHandler(this.axAgUiAxVOCntrl1_MouseMoveEvent);
            this.axAgUiAxVOCntrl1.DblClick += new System.EventHandler(this.axAgUiAxVOCntrl1_DblClick);
            this.axAgUiAxVOCntrl1.KeyUpEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyUpEventHandler(this.axAgUiAxVOCntrl1_KeyUpEvent);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 410);
            this.Controls.Add(this.axAgUiAxVOCntrl1);
            this.Name = "Form1";
            this.Text = "Polyline Drawing";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
    }
}

