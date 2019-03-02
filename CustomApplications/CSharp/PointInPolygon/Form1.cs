using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

namespace PointInPolygon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Refresh();

            m_Root = new AGI.STKObjects.AgStkObjectRoot();
            NewScenario();
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;

            //
            // Read an STK area target file and use the boundary to draw a filled
            // polygon on the globe.
            //
            Array points = ReadAreaTargetPoints("Italy_2.at");

            IAgStkGraphicsSurfaceTriangulatorResult result = manager.Initializers.SurfacePolygonTriangulator.Compute("Earth", ref points);

            _polygon = manager.Initializers.TriangleMeshPrimitive.InitializeWithSetHint(AgEStkGraphicsSetHint.eStkGraphicsSetHintInfrequent);
            ((IAgStkGraphicsPrimitive)_polygon).Color = Color.White;
            _polygon.SetTriangulator((IAgStkGraphicsTriangulatorResult)result);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)_polygon);

            _outline = manager.Initializers.PolylinePrimitive.InitializeWithHint(AgEStkGraphicsSetHint.eStkGraphicsSetHintInfrequent);
            ((IAgStkGraphicsPrimitive)_outline).Color = Color.White;
            _outline.SetWithSurfaceTriangulatorResult(result);
            manager.Primitives.Add((IAgStkGraphicsPrimitive)_outline);

            View((IAgStkGraphicsPrimitive)_polygon);

            //
            // This object is constructed once then used for a point in polygon
            // test every time the mouse moves.
            //
            _pointInPolygonTest = new PointInPolygonTest(points, m_Root);

            AddInstructionsOverlay();
        }

        private void axAgUiAxVOCntrl1_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            bool pointInPolygon = false;

            object[] windowCoords = new object[2];
            windowCoords[0] = e.x;
            windowCoords[1] = e.y;
            Array position = (Array)windowCoords;
            Array clickedCartographicPosition = null;

            if (manager.Scenes.Count > 0)
            {
                try
                {
                    // WindowToCartographic returns a one-dimensional array 
                    // containing three elements, where the element at 
                    // the index 0 is Latitude (rad), the element at the 
                    // index 1 is Longitude (rad), and the last element
                    // is the altitude (m).
                    clickedCartographicPosition = manager.Scenes[0].Camera.WindowToCartographic("Earth", ref position);
                }
                catch (ArgumentException)
                {
                    // The mouse position does not intersect the Earth.
                    return;
                }
            }

            //
            // Test if the position on the globe under the mouse is inside the polygon.
            //
            double x = 0, y = 0, z = 0;
            IAgPosition tempPos = m_Root.ConversionUtility.NewPositionOnCB("Earth");
            if (clickedCartographicPosition != null)
            {
                double latitudeInRadians = (double)clickedCartographicPosition.GetValue(0);
                double longitudeInRadians = (double)clickedCartographicPosition.GetValue(1);
                double altitudeInMeters = (double)clickedCartographicPosition.GetValue(2);
                tempPos.AssignPlanetocentric(latitudeInRadians, longitudeInRadians, altitudeInMeters);
            }
            tempPos.QueryCartesian(out x, out y, out z);
            Vector3D clickedPosition = new Vector3D(x, y, z);

            pointInPolygon = _pointInPolygonTest.Test(clickedPosition);

            //
            // Highlight the polygon and outline if the mouse is inside it.
            //
            IAgStkGraphicsPrimitive polygonPrimitive = (IAgStkGraphicsPrimitive)_polygon;
            IAgStkGraphicsPrimitive outlinePrimitive = (IAgStkGraphicsPrimitive)_outline;
            if (pointInPolygon)
            {
                polygonPrimitive.Color = Color.Yellow;
                outlinePrimitive.Color = Color.Yellow;
            }
            else
            {
                polygonPrimitive.Color = Color.White;
                outlinePrimitive.Color = Color.White;
            }

            if (manager.Scenes.Count > 0)
            {
                manager.Scenes[0].Render();
            }
        }

        private void NewScenario()
        {
            try
            {
                m_Root.CloseScenario();
                m_Root.NewScenario("PointInPolygon");

                IAgUnitPrefsDimCollection dimensions = m_Root.UnitPreferences;
                dimensions.ResetUnits();
                dimensions.SetCurrentUnit("DateFormat", "UTCG");
                IAgScenario scene = (IAgScenario)m_Root.CurrentScenario;

                scene.StartTime = "1 Jul 2002 00:00:00.00";
                scene.StopTime = "1 Jul 2002 04:00:00.00";
                scene.Epoch = "1 Jul 2002 00:00:00.00";

                dimensions.SetCurrentUnit("DistanceUnit", "m");
                dimensions.SetCurrentUnit("TimeUnit", "sec");
                dimensions.SetCurrentUnit("AngleUnit", "rad");
                dimensions.SetCurrentUnit("MassUnit", "kg");
                dimensions.SetCurrentUnit("PowerUnit", "dbw");
                dimensions.SetCurrentUnit("FrequencyUnit", "ghz");
                dimensions.SetCurrentUnit("SmallDistanceUnit", "m");
                dimensions.SetCurrentUnit("LatitudeUnit", "rad");
                dimensions.SetCurrentUnit("Longitudeunit", "rad");
                dimensions.SetCurrentUnit("DurationUnit", "HMS");
                dimensions.SetCurrentUnit("Temperature", "K");
                dimensions.SetCurrentUnit("SmallTimeUnit", "sec");
                dimensions.SetCurrentUnit("RatioUnit", "db");
                dimensions.SetCurrentUnit("rcsUnit", "dbsm");
                dimensions.SetCurrentUnit("DopplerVelocityUnit", "m/s");
                dimensions.SetCurrentUnit("Percent", "unitValue");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private Array ReadAreaTargetPoints(String fileName)
        {
            //
            // Open the file and read everything between "BEGIN PolygonPoints"
            // and "END PolygonPoints"
            //
            String areaTarget = File.ReadAllText(fileName);
            String startToken = "BEGIN PolygonPoints";
            String points = areaTarget.Substring(areaTarget.IndexOf(startToken, StringComparison.Ordinal) + startToken.Length);
            points = points.Substring(0, points.IndexOf("END PolygonPoints", StringComparison.Ordinal));

            String[] splitPoints = points.Split(new char[] { '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Array targetPoints = Array.CreateInstance(typeof(object), splitPoints.Length);

            for (int i = 0; i < splitPoints.Length; i += 3)
            {
                //
                // Each line is [Latitude][Longitude][Altitude].  In the file,
                // latitude and longitude are in degrees and altitude is in
                // meters.
                //
                double latitude = DegreesToRadians(Convert.ToDouble(splitPoints[i], CultureInfo.InvariantCulture));
                double longitude = DegreesToRadians(Convert.ToDouble(splitPoints[i + 1], CultureInfo.InvariantCulture));
                double altitude = Convert.ToDouble(splitPoints[i + 2], CultureInfo.InvariantCulture);

                IAgPosition position = m_Root.ConversionUtility.NewPositionOnCB("Earth");
                position.AssignPlanetodetic(latitude, longitude, altitude);

                double x = 0, y = 0, z = 0;
                position.QueryCartesian(out x, out y, out z);

                targetPoints.SetValue(x, i);
                targetPoints.SetValue(y, i + 1);
                targetPoints.SetValue(z, i + 2);
            }

            return targetPoints;
        }

        private void AddInstructionsOverlay()
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;

            IAgStkGraphicsRendererTexture2D texture = manager.Textures.LoadFromStringUri("Instructions.bmp");
            IAgStkGraphicsTextureScreenOverlay textureOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYTexture(10, 10, texture);
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)textureOverlay;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft;
            overlay.BorderSize = 1;

            IAgStkGraphicsScreenOverlayCollectionBase baseOverlays = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays;
            baseOverlays.Add((IAgStkGraphicsScreenOverlay)textureOverlay);
        }

        private void View(IAgStkGraphicsPrimitive primitive)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            IAgStkGraphicsBoundingSphere bounds = primitive.BoundingSphere;

            IAgCrdnProvider provider = m_Root.CentralBodies["Earth"].Vgt;
            IAgCrdnPointFixedInSystem @fixed = (IAgCrdnPointFixedInSystem)
                provider.Points.Factory.Create(string.Format("{0}_{1}", 0, 0), string.Empty, AgECrdnPointType.eCrdnPointTypeFixedInSystem);

            @fixed.FixedPoint.AssignCartesian((double)bounds.Center.GetValue(0), 
                (double)bounds.Center.GetValue(1), (double)bounds.Center.GetValue(2));
            @fixed.Reference.SetSystem(provider.WellKnownSystems.Earth.Fixed);

            IAgCrdnAxesOnSurface axes = (IAgCrdnAxesOnSurface)
                provider.Axes.Factory.Create(string.Format("{0}_{1}", 1, 0), string.Empty, AgECrdnAxesType.eCrdnAxesTypeOnSurface); 
            axes.ReferencePoint.SetPoint((IAgCrdnPoint)@fixed);
            axes.CentralBody.SetPath("Earth");

            IAgCrdnAxesFixed eastNorthUp = (IAgCrdnAxesFixed)
                provider.Axes.Factory.Create(string.Format("{0}_{1}", 1, 1), string.Empty, AgECrdnAxesType.eCrdnAxesTypeFixed); 
            eastNorthUp.ReferenceAxes.SetAxes((IAgCrdnAxes)axes);
            eastNorthUp.FixedOrientation.AssignEulerAngles(AGI.STKUtil.AgEEulerOrientationSequence.e321, Math.PI / 2, 0, 0);

            IAgCrdnAxes referenceAxes = ((IAgCrdnAxesFixed)eastNorthUp).ReferenceAxes.GetAxes();
            IAgCrdnAxesOnSurface onSurface = (IAgCrdnAxesOnSurface)referenceAxes;

            double azimuth = -1.5707963;
            double elevation = -0.52356;
            double range = 0.0;
            if (manager.Scenes.Count > 0)
            {
                range = manager.Scenes[0].Camera.DistancePerRadius * bounds.Radius;
            }

            double radial = range * Math.Cos(elevation);
            Array offset = new object[] {radial * Math.Cos(azimuth), radial * Math.Sin(azimuth), -range * Math.Sin(elevation)};

            if (manager.Scenes.Count > 0)
            {
                manager.Scenes[0].Camera.ViewOffset((IAgCrdnAxes)eastNorthUp, onSurface.ReferencePoint.GetPoint(), ref offset);
                manager.Scenes[0].Render();
            }
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Root.CurrentScenario != null)
                m_Root.CloseScenario();
            axAgUiAxVOCntrl1 = null;
        }

        private IAgStkGraphicsTriangleMeshPrimitive _polygon;
        private IAgStkGraphicsPolylinePrimitive _outline;
        private PointInPolygonTest _pointInPolygonTest;

        private AgStkObjectRoot m_Root;
    }
}
