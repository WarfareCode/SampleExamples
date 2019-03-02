using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HohmannTransfer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public String ReportData
        {
            get { return this.richTextBox1.Text; }
            set { this.richTextBox1.Text = value; }
        }

        public String FormTitle
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

    }
}