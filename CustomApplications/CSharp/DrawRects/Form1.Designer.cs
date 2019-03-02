namespace DrawRects
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.BtnNewScen = new System.Windows.Forms.Button();
            this.BtnAddRect = new System.Windows.Forms.Button();
            this.BtnClearAll = new System.Windows.Forms.Button();
            this.BtnListAll = new System.Windows.Forms.Button();
            this.BtnListAllForEach = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnSelectColor = new System.Windows.Forms.Button();
            this.Combo1 = new System.Windows.Forms.ComboBox();
            this.Combo2 = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.axAgUiAxVOCntrl2 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnNewScen
            // 
            this.BtnNewScen.Location = new System.Drawing.Point(664, 16);
            this.BtnNewScen.Name = "BtnNewScen";
            this.BtnNewScen.Size = new System.Drawing.Size(104, 40);
            this.BtnNewScen.TabIndex = 1;
            this.BtnNewScen.Text = "New Scenario";
            this.BtnNewScen.Click += new System.EventHandler(this.BtnNewScen_Click);
            // 
            // BtnAddRect
            // 
            this.BtnAddRect.Location = new System.Drawing.Point(664, 72);
            this.BtnAddRect.Name = "BtnAddRect";
            this.BtnAddRect.Size = new System.Drawing.Size(104, 40);
            this.BtnAddRect.TabIndex = 2;
            this.BtnAddRect.Text = "Add Rect";
            this.BtnAddRect.Click += new System.EventHandler(this.BtnAddRect_Click);
            // 
            // BtnClearAll
            // 
            this.BtnClearAll.Location = new System.Drawing.Point(664, 128);
            this.BtnClearAll.Name = "BtnClearAll";
            this.BtnClearAll.Size = new System.Drawing.Size(104, 40);
            this.BtnClearAll.TabIndex = 3;
            this.BtnClearAll.Text = "Clear All";
            this.BtnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // BtnListAll
            // 
            this.BtnListAll.Location = new System.Drawing.Point(664, 184);
            this.BtnListAll.Name = "BtnListAll";
            this.BtnListAll.Size = new System.Drawing.Size(104, 40);
            this.BtnListAll.TabIndex = 4;
            this.BtnListAll.Text = "List All";
            this.BtnListAll.Click += new System.EventHandler(this.BtnListAll_Click);
            // 
            // BtnListAllForEach
            // 
            this.BtnListAllForEach.Location = new System.Drawing.Point(664, 240);
            this.BtnListAllForEach.Name = "BtnListAllForEach";
            this.BtnListAllForEach.Size = new System.Drawing.Size(104, 40);
            this.BtnListAllForEach.TabIndex = 5;
            this.BtnListAllForEach.Text = "List All (For Each)";
            this.BtnListAllForEach.Click += new System.EventHandler(this.BtnListAllForEach_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnSelectColor);
            this.groupBox1.Controls.Add(this.Combo1);
            this.groupBox1.Controls.Add(this.Combo2);
            this.groupBox1.Location = new System.Drawing.Point(664, 304);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(104, 128);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Line Style";
            // 
            // BtnSelectColor
            // 
            this.BtnSelectColor.Location = new System.Drawing.Point(8, 88);
            this.BtnSelectColor.Name = "BtnSelectColor";
            this.BtnSelectColor.Size = new System.Drawing.Size(88, 32);
            this.BtnSelectColor.TabIndex = 8;
            this.BtnSelectColor.Text = "Color";
            this.BtnSelectColor.Click += new System.EventHandler(this.BtnSelectColor_Click);
            // 
            // Combo1
            // 
            this.Combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo1.Location = new System.Drawing.Point(8, 24);
            this.Combo1.Name = "Combo1";
            this.Combo1.Size = new System.Drawing.Size(80, 21);
            this.Combo1.TabIndex = 0;
            // 
            // Combo2
            // 
            this.Combo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo2.Location = new System.Drawing.Point(8, 56);
            this.Combo2.Name = "Combo2";
            this.Combo2.Size = new System.Drawing.Size(80, 21);
            this.Combo2.TabIndex = 7;
            // 
            // axAgUiAxVOCntrl2
            // 
            this.axAgUiAxVOCntrl2.Enabled = true;
            this.axAgUiAxVOCntrl2.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl2.Name = "axAgUiAxVOCntrl2";
            this.axAgUiAxVOCntrl2.Size = new System.Drawing.Size(653, 589);
            this.axAgUiAxVOCntrl2.TabIndex = 7;
            this.axAgUiAxVOCntrl2.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.axAgUiAxVOCntrl2_MouseDownEvent);
            this.axAgUiAxVOCntrl2.MouseMoveEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEventHandler(this.axAgUiAxVOCntrl2_MouseMoveEvent);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(774, 595);
            this.Controls.Add(this.axAgUiAxVOCntrl2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnListAllForEach);
            this.Controls.Add(this.BtnListAll);
            this.Controls.Add(this.BtnClearAll);
            this.Controls.Add(this.BtnAddRect);
            this.Controls.Add(this.BtnNewScen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DrawRects";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button BtnNewScen;
        private System.Windows.Forms.Button BtnAddRect;
        private System.Windows.Forms.Button BtnClearAll;
        private System.Windows.Forms.Button BtnListAll;
        private System.Windows.Forms.Button BtnListAllForEach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox Combo2;
        private System.Windows.Forms.Button BtnSelectColor;
        private System.Windows.Forms.ComboBox Combo1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl2;
    }
}