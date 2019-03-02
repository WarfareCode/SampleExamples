namespace DataProviders
{
    partial class GraphForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.ListBox DataBox;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.PictureBox DataGraph;

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
            this.DataBox = new System.Windows.Forms.ListBox();
            this.DataGraph = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // DataBox
            // 
            this.DataBox.Location = new System.Drawing.Point(32, 24);
            this.DataBox.Name = "DataBox";
            this.DataBox.Size = new System.Drawing.Size(248, 264);
            this.DataBox.TabIndex = 0;
            // 
            // DataGraph
            // 
            this.DataGraph.Location = new System.Drawing.Point(0, 0);
            this.DataGraph.Name = "DataGraph";
            this.DataGraph.Size = new System.Drawing.Size(672, 400);
            this.DataGraph.TabIndex = 1;
            this.DataGraph.TabStop = false;
            // 
            // GraphForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(648, 390);
            this.Controls.Add(this.DataGraph);
            this.Name = "GraphForm";
            this.Text = "Abstract Graph";
            this.Load += new System.EventHandler(this.GraphForm_Load);
            this.ResumeLayout(false);

        }
        #endregion
    }
}