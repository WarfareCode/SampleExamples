using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;

namespace DataProviders
{
	public class LineGraph
	{
		private int m_Width = 700;						
		private int m_Height = 400;						
		private ArrayList m_XAxis;						
		private ArrayList m_YAxis;						
		private Color m_graphColor = Color.Red;			
		private float m_XSlice = 1;						
		private float m_YSlice = 1;						
		private Graphics objGraphics;
		private Bitmap objBitmap;
		private string m_XAxisText = "X-Axis";
		private string m_YAxisText = "Y-Axis";
		private string m_Title = "Line Graph";
		private Color m_TitleBackColor = Color.Cyan;
		private Color m_TitleForeColor = Color.DarkGreen;
		private Color m_BackroundColor = Color.White;
		private String m_AxisTextFont = "Verdana";
		private Color m_AxisTextColor = Color.DarkBlue;

		public int Width
		{
			get { return m_Width;}
			set 
			{ 
				if ( value > 200)
					m_Width = value;
			}
		}

		
		public int Height
		{
			get { return m_Height;}
			set 
			{ 
				if ( value > 200)
					m_Height = value;
			}
		}

		public ArrayList XAxis
		{
			set 
			{ 
				m_XAxis = value;
			}
			get { return m_XAxis;}
		}

		public ArrayList YAxis
		{
			set { m_YAxis = value;}
			get { return m_YAxis;}
		}

		public Color GraphColor
		{
			set { m_graphColor = value;}
			get { return m_graphColor;}
		}

		public float XSlice
		{
			set { m_XSlice = value;}
			get { return m_XSlice;}
		}

		public float YSlice
		{
			set { m_YSlice = value;}
			get { return m_YSlice;}
		}

		public string XAxisText
		{
			get { return m_XAxisText;}
			set { m_XAxisText = value;}
		}
		
		public string YAxisText
		{
			get { return m_YAxisText;}
			set { m_YAxisText = value;}
		}

		public string Title
		{
			get { return m_Title;}
			set { m_Title = value;}
		}

		public Color TitleBackColor
		{
			get { return m_TitleBackColor;}
			set { m_TitleBackColor = value;}
		}

		public Color TitleForeColor
		{
			get { return m_TitleForeColor;}
			set { m_TitleForeColor = value;}
		}

		public Color BackroundColor
		{
			get { return m_BackroundColor;}
			set { m_BackroundColor = value;}
		}

		public string AxisTextFont
		{
			get { return m_AxisTextFont;}
			set { m_AxisTextFont = value;}
		}

		public Color AxisTextColor
		{
			get { return m_AxisTextColor;}
			set { m_AxisTextColor = value;}
		}
		
		public LineGraph()
		{
		}

		public void InitializeGraph()
		{
			objBitmap = new Bitmap(Width,Height);
			objGraphics = Graphics.FromImage(objBitmap);
			objGraphics.FillRectangle(new SolidBrush(m_BackroundColor),0,0,Width,Height);
			objGraphics.DrawLine(new Pen(new SolidBrush(Color.Black),2),100,Height - 100,Width - 100,Height - 100);
			objGraphics.DrawLine(new Pen(new SolidBrush(Color.Black),2),100,Height - 100,100,10);
			objGraphics.DrawString("0",new Font("Courier New",10),new SolidBrush(Color.White),100,Height - 90);
			SetAxisText(ref objGraphics);
			CreateTitle(ref objGraphics);
		}

		public void CreateGraph(Color _GraphColor)
		{
			GraphColor = _GraphColor;
			SetPixels(ref objGraphics);
		}

		public Bitmap GetGraph()
		{
			SetXAxis(ref objGraphics,XSlice);
			SetYAxis(ref objGraphics,YSlice);
			return objBitmap;
		}

		private void PlotGraph(ref Graphics objGraphics,float x1,float y1,float x2,float y2)
		{
			objGraphics.DrawLine(new Pen(new SolidBrush(GraphColor),2),x1 + 100, (Height - 100) - y1 ,x2 + 100,(Height - 100) - y2  );
		}

		private  void SetXAxis(ref Graphics objGraphics,float iSlices)
		{
			float x1 = 100,y1 = Height - 110,x2 = 100,y2 = Height - 90;
			int iCount = 0;
			int iSliceCount = 1;
			for(int iIndex = 0;iIndex <= Width - 200;iIndex += 10)
			{
				if(iCount == 5)
				{
					objGraphics.DrawLine(new Pen(new SolidBrush(Color.Black)),
						x1+iIndex,y1,x2+iIndex,y2);
					objGraphics.DrawString(Convert.ToString(iSlices * iSliceCount),new Font("verdana",8),new SolidBrush(Color.White),
						x1 + iIndex - 10,y2);
					iCount = 0;
					iSliceCount++;
				}
				else
				{
					objGraphics.DrawLine(new Pen(new SolidBrush(Color.Goldenrod)),
						x1+iIndex,y1+5,x2+iIndex,y2-5);
				}
				iCount++;
			}
		}

		private void SetYAxis(ref Graphics objGraphics,float iSlices)
		{
			int x1 = 95; 
			int y1 = Height - 110;
			int x2 = 105;
			int y2 = Height - 110;
			int iCount = 1;
			int iSliceCount = 1;

			for(int iIndex = 0;iIndex<Height - 200;iIndex+=10)
			{
				if(iCount == 5)
				{
					objGraphics.DrawLine(new Pen(new SolidBrush(Color.Black)),
						x1 - 5, y1 - iIndex,x2 + 5,y2 - iIndex);
					objGraphics.DrawString(Convert.ToString(iSlices * iSliceCount),new Font("verdana",8),new SolidBrush(Color.White),
						60,y1 - iIndex );
					iCount = 0;
					iSliceCount++;
				}
				else
				{
					objGraphics.DrawLine(new Pen(new SolidBrush(Color.Goldenrod)),
						x1, (y1 - iIndex),x2,(y2 - iIndex));
				}
				iCount ++;
			}

		}

		private void SetPixels(ref Graphics objGraphics)
		{
			if((XAxis.Count > 0) && (YAxis.Count > 0))
			{
				float X1 = float.Parse(XAxis[0].ToString());
				float Y1 = float.Parse(YAxis[0].ToString());

				if(XAxis.Count == YAxis.Count)
				{
					for(int iXaxis = 0,iYaxis =0;(iXaxis < XAxis.Count - 1 && iYaxis < YAxis.Count - 1);iXaxis++,iYaxis++)
					{
						PlotGraph(ref objGraphics,X1,Y1,float.Parse(XAxis[iXaxis + 1].ToString()),float.Parse(YAxis[iYaxis + 1].ToString()));
						X1 = float.Parse(XAxis[iXaxis + 1].ToString());
						Y1 = float.Parse(YAxis[iYaxis + 1].ToString());
					}
				}
			}
		}

		private void SetAxisText(ref Graphics objGraphics)
		{
			objGraphics.DrawString(XAxisText,new Font("Courier New",10),new SolidBrush(m_AxisTextColor),
				Width / 2 - 50,Height - 50);

			int X = 30;
			int Y = (Height / 2 ) - 50;
			for(int iIndex = 0;iIndex < YAxisText.Length;iIndex++)
			{
				objGraphics.DrawString(YAxisText[iIndex].ToString(),new Font("Courier New",10),new SolidBrush(m_AxisTextColor),
					X,Y);
				Y += 10;
			}
		}

		private void CreateTitle(ref Graphics objGraphics)
		{
			objGraphics.FillRectangle(new SolidBrush(TitleBackColor),Height - 100,20,Height - 200,20);
			Rectangle rect = new Rectangle(Height - 100,20,Height - 200,20);
			objGraphics.DrawString(Title,new Font("Verdana",10),new SolidBrush(TitleForeColor),rect);
		}
	}	
}
