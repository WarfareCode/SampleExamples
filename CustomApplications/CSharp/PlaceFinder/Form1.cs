using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;


namespace PlaceFinder
{
	/// <summary>
    /// STK/X Place Finder demo uses the http://www.geonames.org web service to create
	/// places in 3D 
	/// </summary>
	public partial class PlaceFinderForm : Form
	{
        #region Member Variables
        private AGI.STKObjects.AgStkObjectRoot stkRootObject = null;
        private System.Windows.Forms.ContextMenu contextMenu;

        private int button = 0;
        private AGI.STKX.AgEShiftValues shift = 0;
        private Point p;
        private int nMouseMove = 0;
        private int missNb=0;

        private int placeIndex = 0;

        private AGI.STKObjects.AgStkObjectRoot stkRoot
        {
            get
            {
                if (stkRootObject == null)
                {
                    stkRootObject = new AGI.STKObjects.AgStkObjectRoot();
                }
                return stkRootObject;
            }
        }

        #endregion

        #region Constructor

		public PlaceFinderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Create an empty scenario
			//
			try
			{
                Restart(null, null);
			} 
			catch (Exception e)
			{
                ShowMessage("The command failed : " + e.Message);
			}


            //
            // Set-up context menu
            //
            this.contextMenu = new System.Windows.Forms.ContextMenu(); 
            this.contextMenu.MenuItems.Add("Add nearest place", new EventHandler(this.AddNearestPlace));
            this.contextMenu.MenuItems.Add("Delete", new EventHandler(this.DeletePlace));
            this.contextMenu.MenuItems.Add("-");
            this.contextMenu.MenuItems.Add("Restart", new EventHandler(this.Restart));

            //
            // Layout the controls on the form
            //
            ReLayout();
        }


        #endregion

        #region main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            AGI.STKX.AgSTKXApplication STKXApp = null;
            try
            {
                STKXApp = new AGI.STKX.AgSTKXApplication();

                if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl))
                {
                    MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (System.Runtime.InteropServices.COMException exception)
            {
                if (exception.ErrorCode == unchecked((int)0x80040154))
                {
                    string errorMessage = "Could not instantiate AgSTKXApplication.";
                    errorMessage += Environment.NewLine;
                    errorMessage += Environment.NewLine;
                    errorMessage += "Check that STK or STK Engine 64-bit is installed on this machine.";

                    MessageBox.Show(errorMessage, "STK Engine Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    throw;
                }
            }
            if (STKXApp != null)
            {
                Application.Run(new PlaceFinderForm());
            }
        }
        #endregion

        #region Menu commands

        private void AddNearestPlace(object sender, System.EventArgs e)
        {
            AGI.STKX.AgPickInfoData pickInfoData = axAgUiAxVOCntrl1.PickInfo(p.X, p.Y);
            if ((pickInfoData.ObjPath == "") && (pickInfoData.IsLatLonAltValid))
            {
                LonLatPt pt = new LonLatPt();
                pt.Lat = pickInfoData.Lat;
                pt.Lon = pickInfoData.Lon;

                Thread newThread = new Thread(new ParameterizedThreadStart(this.ThreadProc));
                newThread.Name = "PlaceFinderThread";
                newThread.SetApartmentState(ApartmentState.STA);
                newThread.IsBackground = true;
                newThread.Start(pt);
            }
        }

        private delegate void ShowMessageDelegate(string message);

        private void ShowMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ShowMessageDelegate(ShowMessage), message);
            }
            else
            {
                MessageBox.Show(this, message, "PlaceFinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ThreadProc(object llaObj)
        {
            try
            {
                // Use http://www.geonames.org/export/web-services.html#findNearbyPlaceName
                LonLatPt pt = (LonLatPt)llaObj;
                string url = string.Format("http://api.geonames.org/findNearbyPlaceName?lat={0}&lng={1}&username=demo", pt.Lat, pt.Lon);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    // Extract the results from the XML response
                    string xmlString = reader.ReadToEnd();

                    using (XmlReader xmlReader = XmlReader.Create(new StringReader(xmlString)))
                    {
                        XPathDocument document = new XPathDocument(xmlReader);
                        XPathNavigator navigator = document.CreateNavigator();

                        XPathNavigator geonamesNode = navigator.SelectSingleNode("/geonames");
                        XPathNavigator statusNode = navigator.SelectSingleNode("//status");
                        XPathNavigator nameNode = navigator.SelectSingleNode("//name");
                        XPathNavigator latNode = navigator.SelectSingleNode("//lat");
                        XPathNavigator lngNode = navigator.SelectSingleNode("//lng");
                        XPathNavigator countryNameNode = navigator.SelectSingleNode("//countryName");

                        if ((statusNode == null) && (nameNode != null) && (latNode != null) && (lngNode != null))
                        {
                            this.BeginInvoke(new MethodInvoker(delegate()
                            {
                                try
                                {
                                    stkRoot.BeginUpdate();
                                    AGI.STKObjects.IAgPlace place = (AGI.STKObjects.IAgPlace)stkRoot.CurrentScenario.Children.New(AGI.STKObjects.AgESTKObjectType.ePlace, string.Format("Place{0}", placeIndex++));
                                    place.Position.AssignPlanetodetic(Double.Parse(latNode.InnerXml), Double.Parse(lngNode.InnerXml), 0);
                                    AGI.STKObjects.IAgStkObject stkObject = (AGI.STKObjects.IAgStkObject)place;
                                    place.Graphics.UseInstNameLabel = false;
                                    place.Graphics.LabelName = nameNode.InnerXml;
                                    if (countryNameNode != null)
                                    {
                                        stkObject.ShortDescription = nameNode.InnerXml + ", " + countryNameNode.InnerXml;
                                    }
                                    else
                                    {
                                        stkObject.ShortDescription = nameNode.InnerXml;
                                    }
                                    stkRoot.EndUpdate();
                                }
                                catch
                                {
                                }
                            }));
                        }
                        else if ((statusNode != null) && statusNode.HasAttributes)
                        {
                            string messageFromWebSvc = statusNode.GetAttribute("message", string.Empty);
                            string msg;
                            if (messageFromWebSvc.Contains("hourly limit"))
                            {
                                msg = "This example is using a free/demo web service from http://www.geonames.org that has hourly limits. Please try again at the start of the next hour." + Environment.NewLine + Environment.NewLine + "Message returned from the web service:" + Environment.NewLine + Environment.NewLine + "[" + messageFromWebSvc + "]";
                            }
                            else
                            {
                                msg = "This example is using a free/demo web service from http://www.geonames.org that might not be available. You might want to retry at a later time." + Environment.NewLine + Environment.NewLine + "Message returned from the web service:" + Environment.NewLine + Environment.NewLine + "[" + messageFromWebSvc + "]";
                            }
                            ShowMessage(msg);
                        }
                        else if (!geonamesNode.HasChildren)
                        {
                            this.ShowMessage("No place found nearby");
                        }
                        else
                        {
                            ShowMessage("This example is using a free/demo web service from http://www.geonames.org that is not currently available. You might want to retry at a later time.");
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                ShowMessage(string.Format("Check your network connectivity.\r\nException: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format("Web service invocation failed.\r\nException: {0}", ex.Message));
            }
        }

        private void DeletePlace(object sender, System.EventArgs e)
        {
            AGI.STKX.AgPickInfoData pickInfoData = axAgUiAxVOCntrl1.PickInfo(p.X, p.Y);
            if (pickInfoData.ObjPath != "")
            {
		        AGI.STKObjects.IAgStkObject obj = stkRoot.GetObjectFromPath(pickInfoData.ObjPath);
                stkRoot.CurrentScenario.Children.Unload(obj.ClassType, obj.InstanceName);
				
            }
        }
        private void Restart(object sender, System.EventArgs e)
        {
            if (stkRoot.CurrentScenario != null)
            {
                stkRoot.CloseScenario();
            }
		    stkRoot.NewScenario("PlaceFinderDemo");
        }
        #endregion

        #region Form events
        
        private void OnFormResize(object sender, System.EventArgs e)
        {
            ReLayout();
        }

        #endregion

        #region Utilities
        
        private void ReLayout()
        {
            this.axAgUiAxVOCntrl1.Left=0;
            this.axAgUiAxVOCntrl1.Top=0;
            this.axAgUiAxVOCntrl1.Width=this.ClientSize.Width;
            this.axAgUiAxVOCntrl1.Height=this.ClientSize.Height-this.statusBar.Height;

            this.statusBar.Left=0;
            this.statusBar.Top=this.ClientSize.Height-this.statusBar.Height;
            this.statusBar.Width=this.ClientSize.Width;
        }
        #endregion

        #region STK/X VO Control events

        private void OnVOCntrlMouseDown(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
        {
            button = e.button;
            shift = (AGI.STKX.AgEShiftValues) e.shift;
            p = new Point(e.x, e.y);
            nMouseMove = 0;
        }

        private void OnVOCntrlClick(object sender, System.EventArgs e)
        {
            if ((button == 2) && (nMouseMove < 5))
            {
                // right click

                AGI.STKX.IAgPickInfoData pickInfoData = axAgUiAxVOCntrl1.PickInfo(p.X, p.Y);
                contextMenu.MenuItems[1].Enabled = ((pickInfoData.ObjPath != ""));

                contextMenu.Show(this, p);
            }
        }

        private void OnVOCntrlMouseMove(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            ++nMouseMove;

            axAgUiAxVOCntrl1.PickInfo(e.x, e.y);

            AGI.STKX.IAgPickInfoData pickInfoData = axAgUiAxVOCntrl1.PickInfo(e.x, e.y);
            if (pickInfoData.ObjPath != "")
            {
                AGI.STKObjects.IAgStkObject obj = stkRoot.GetObjectFromPath(pickInfoData.ObjPath);
         
                statusBar.Text = obj.ShortDescription;

                missNb=0;
            }
            else
            {
                ++missNb;
                if (missNb > 20)
                {
                    statusBar.Text = "";
                }
            }
        }
        #endregion

        private void PlaceFinderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }
    }
    class LonLatPt
    {
        public double Lat;
        public double Lon;
    }
}
