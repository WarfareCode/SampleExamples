using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKUtil;
using AGI.STKObjects;

namespace PolylineDrawing
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

            _root = new AGI.STKObjects.AgStkObjectRoot();
            NewScenario();
            IAgStkGraphicsSceneManager manager = ((IAgScenario)_root.CurrentScenario).SceneManager;

            IAgStkGraphicsGreatArcInterpolator interp = manager.Initializers.GreatArcInterpolator.Initialize();
            _editingPolyline = manager.Initializers.PolylinePrimitive.InitializeWithInterpolator((IAgStkGraphicsPositionInterpolator)interp);
            _editingPolyline.Width = 2;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)_editingPolyline);

            _editingPointBatch = manager.Initializers.PointBatchPrimitive.Initialize();
            _editingPointBatch.PixelSize = 4;
            manager.Primitives.Add((IAgStkGraphicsPrimitive)_editingPointBatch);

            _editingPoints = Array.CreateInstance(typeof(object), 0);

            AddInstructionsOverlay();
        }

        private void NewScenario()
        {
            try
            {
                _root.CloseScenario();
                _root.NewScenario("PointInPolygon");

                IAgUnitPrefsDimCollection dimensions = _root.UnitPreferences;
                dimensions.ResetUnits();
                dimensions.SetCurrentUnit("DateFormat", "UTCG");
                IAgScenario scene = (IAgScenario)_root.CurrentScenario;

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
                dimensions.SetCurrentUnit("latitudeUnit", "rad");
                dimensions.SetCurrentUnit("longitudeunit", "rad");
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

        private void SetEditingPrimitives(Array positions)
        {
            try
            {
                _editingPolyline.SetCartographic("Earth", ref positions);
            }
            catch (ArgumentException)
            {
                // There is no unique geodesic curve connecting the initial and final points.
            }

            _editingPointBatch.SetCartographic("Earth", ref positions);
        }

        private void axAgUiAxVOCntrl1_DblClick(object sender, EventArgs e)
        {
            //
            // If the user double clicks on the globe, a new point is 
            // added to the current polyline.
            //
            IAgStkGraphicsSceneManager manager = ((IAgScenario)_root.CurrentScenario).SceneManager;

            object[] windowCoords = new object[2];
            windowCoords[0] = _lastMousePosition.X;
            windowCoords[1] = _lastMousePosition.Y;
            Array position = (Array)windowCoords;
            Array clickedPosition = null;
            if (manager.Scenes.Count > 0)
            {
                try
                {
                    clickedPosition = manager.Scenes[0].Camera.WindowToCartographic("Earth", ref position);
                }
                catch (ArgumentException)
                {
                    // The mouse position does not intersect the Earth. 
                    return;
                }
            }

            if (clickedPosition == null)
                return;

            object[] arrEditingPoints = (object[])_editingPoints;
            Array.Resize(ref arrEditingPoints, _editingPoints.Length + 3);
            _editingPoints = (Array)arrEditingPoints;

            clickedPosition.CopyTo(_editingPoints, _editingPoints.Length - 3);

            SetEditingPrimitives(_editingPoints);

            if (manager.Scenes.Count > 0)
            {
                manager.Scenes[0].Render();
            }
        }

        private void axAgUiAxVOCntrl1_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            _lastMousePosition = new Point(e.x, e.y);

            //
            // When the mouse moves, draw a line from the polyline's last
            // position to the position on the globe under the cursor.
            //
            IAgStkGraphicsSceneManager manager = ((IAgScenario)_root.CurrentScenario).SceneManager;

            object[] windowCoords = new object[2];
            windowCoords[0] = e.x;
            windowCoords[1] = e.y;
            Array position = (Array)windowCoords;
            Array clickedPosition = null;
            if (manager.Scenes.Count > 0)
            {
                try
                {
                    clickedPosition = manager.Scenes[0].Camera.WindowToCartographic("Earth", ref position);
                }
                catch (ArgumentException)
                {
                    // The mouse is not over the globe. 
                    return;
                }
            }

            if (clickedPosition == null)
                return;

            Array inProgressPoints = Array.CreateInstance(typeof(object), _editingPoints.Length + 3);
            Array.Copy(_editingPoints, inProgressPoints, _editingPoints.Length);

            clickedPosition.CopyTo(inProgressPoints, inProgressPoints.Length - 3);
            SetEditingPrimitives(inProgressPoints);

            if (manager.Scenes.Count > 0)
            {
                manager.Scenes[0].Render();
            }
        }

        private void axAgUiAxVOCntrl1_KeyUpEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_KeyUpEvent e)
        {
            //
            // Pressing space bar creates a new polyline using the
            // points the user just clicked.
            //
            if (e.keyCode == (short)Keys.Space)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)_root.CurrentScenario).SceneManager;
                if (_editingPoints.Length > 1)
                {
                    IAgStkGraphicsGreatArcInterpolator interp = manager.Initializers.GreatArcInterpolator.Initialize();
                    IAgStkGraphicsPolylinePrimitive newPolyline = 
                        manager.Initializers.PolylinePrimitive.InitializeWithInterpolatorAndSetHint(
                        (IAgStkGraphicsPositionInterpolator)interp, 
                        AgEStkGraphicsSetHint.eStkGraphicsSetHintInfrequent);
                    ((IAgStkGraphicsPrimitive)newPolyline).Color = ((IAgStkGraphicsPrimitive)_editingPolyline).Color;
                    newPolyline.Width = 2;
                    newPolyline.SetCartographic("Earth", ref _editingPoints);
                    manager.Primitives.Add((IAgStkGraphicsPrimitive)newPolyline);
                }

                //
                // Clear list and primitives used for drawing the current polyline
                // so user can draw a fresh one.
                //
                object[] arrEditingPoints = (object[])_editingPoints;
                Array.Resize(ref arrEditingPoints, 0);
                _editingPoints = (Array)arrEditingPoints;
                SetEditingPrimitives(_editingPoints);

                //
                // Assign a random color to the next polyline
                //
                Random r = new Random();
                ((IAgStkGraphicsPrimitive)_editingPolyline).Color = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));

                if (manager.Scenes.Count > 0)
                {
                    manager.Scenes[0].Render();
                }
            }
        }

        private void AddInstructionsOverlay()
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)_root.CurrentScenario).SceneManager;

            IAgStkGraphicsRendererTexture2D texture = manager.Textures.LoadFromStringUri("Instructions.bmp");
            IAgStkGraphicsTextureScreenOverlay textureOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYTexture(10, 10, texture);
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)textureOverlay;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft;
            overlay.BorderSize = 1;

            IAgStkGraphicsScreenOverlayCollectionBase baseOverlays = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays;
            baseOverlays.Add((IAgStkGraphicsScreenOverlay)textureOverlay);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_root.CurrentScenario != null)
                _root.CloseScenario();
            axAgUiAxVOCntrl1 = null;
        }

        private IAgStkGraphicsPolylinePrimitive _editingPolyline;
        private IAgStkGraphicsPointBatchPrimitive _editingPointBatch;
        private Array _editingPoints;

        private Point _lastMousePosition;

        private AgStkObjectRoot _root;
    }
}
