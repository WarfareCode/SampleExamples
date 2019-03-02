//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: GraphForm.cs
//  DataProviders
//
//
//  This class uses the data generated in the DataProviders example and shows
//  the data graphically.
//
//--------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DataProviders
{
	/// <summary>
	/// Summary description for GraphForm.
	/// </summary>
	public partial class GraphForm : Form
	{
		private LineGraph lgraph;

		public GraphForm(string Title, string XAxisText, string YAxisText, ArrayList XAxis, ArrayList YAxis)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			lgraph = new LineGraph();

			lgraph.Title = Title;
			lgraph.XAxisText = XAxisText;
			lgraph.YAxisText = YAxisText;

			lgraph.Height = 400;
			lgraph.Width = 700;
			lgraph.XSlice = 50;
			lgraph.YSlice = 50;

			lgraph.TitleBackColor = Color.White;
			lgraph.TitleForeColor = Color.PaleVioletRed;
			lgraph.AxisTextColor = Color.DarkBlue;
			lgraph.BackroundColor = Color.White;

			lgraph.XAxis = XAxis;
			lgraph.YAxis = YAxis;

			lgraph.InitializeGraph();
			lgraph.CreateGraph(Color.PaleVioletRed);
			DataGraph.Image = lgraph.GetGraph();		
		}

		private void GraphForm_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
