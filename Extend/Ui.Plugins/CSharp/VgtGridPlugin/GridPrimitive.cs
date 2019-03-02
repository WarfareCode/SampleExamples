using System;
using AGI.STKGraphics;

namespace AGI.Grid
{
    class GridPrimitive
    {
        public GridPrimitive(AGI.STKObjects.IAgStkObject obj)
        {
            Object = obj;
            Origin = obj.Vgt.Points[DefaultOrigin];
            Axes = obj.Vgt.Axes[DefaultAxes];
        }
        public GridPrimitive(AGI.STKObjects.IAgStkObject obj, AGI.STKVgt.IAgCrdnPoint origin, AGI.STKVgt.IAgCrdnAxes axes)
        {
            Object = obj;
            Origin = origin;
            Axes = axes;
        }

        /// <summary>
        /// Creates a composite primitive of the grid from by combining the three configured GridPlanes.
        /// </summary>
        public IAgStkGraphicsPrimitive GetPrimitive(IAgStkGraphicsSceneManager mananger)
        {
            SetReferenceFrame();

            IAgStkGraphicsCompositePrimitive newComposite = mananger.Initializers.CompositePrimitive.Initialize();
            ((IAgStkGraphicsPrimitive)newComposite).ReferenceFrame = ReferenceFrame;

            newComposite.Add(XYPlane.GetPrimitive(ReferenceFrame, mananger));
            newComposite.Add(XZPlane.GetPrimitive(ReferenceFrame, mananger));
            newComposite.Add(YZPlane.GetPrimitive(ReferenceFrame, mananger));

            return (IAgStkGraphicsPrimitive)newComposite;
        }

        /// <summary>
        /// Uses the currently set origin and axes to load the reference from frome VGT if it exists
        /// or create a new VGT system if it does not.
        /// </summary>
        private void SetReferenceFrame()
        {
            string gridSystemName = "GridSystem" + ((AGI.STKVgt.IAgCrdn)Origin).Name + ((AGI.STKVgt.IAgCrdn)Axes).Name;

            if (!Object.Vgt.Systems.Contains(gridSystemName))
            {
                // Try to create the system
                if (Object.Vgt.Points.Contains(((AGI.STKVgt.IAgCrdn)Origin).Name)
                    && Object.Vgt.Axes.Contains(((AGI.STKVgt.IAgCrdn)Axes).Name))
                {
                    AGI.STKVgt.IAgCrdnSystemAssembled newSystem = Object.Vgt.Systems.Factory.Create(gridSystemName, string.Empty, AGI.STKVgt.AgECrdnSystemType.eCrdnSystemTypeAssembled) as AGI.STKVgt.IAgCrdnSystemAssembled;
                    newSystem.OriginPoint.SetPoint(Origin);
                    newSystem.ReferenceAxes.SetAxes(Axes);

                    ReferenceFrame = (AGI.STKVgt.IAgCrdnSystem)newSystem;
                }
                else
                {
                    throw new Exception("Could not create a system with the current orign and axes. Check to make sure they exist.");
                }
            }
            else
            {
                // Use the exisiting system
                ReferenceFrame = Object.Vgt.Systems[gridSystemName];
            }
        }

        public AGI.STKObjects.IAgStkObject Object 
        {
            get { return m_Object; }
            set
            {
                if (value != null && value.IsVgtSupported())
                    m_Object = value;
                else
                    throw new Exception("The STK Object specified does not support VGT.");
            } 
        }

        public AGI.STKVgt.IAgCrdnPoint Origin 
        {
            get { return m_Origin; }
            set
            {
                if (Object.Vgt.Points.Contains(((AGI.STKVgt.IAgCrdn)value).Name))
                    m_Origin = value;
                else
                    throw new Exception("Unable to set \"" + ((AGI.STKVgt.IAgCrdn)value).Name + "\" as the origin.\n" +
                        "Please make sure the point exists on " + Object.InstanceName + ".");
            }
        }

        public AGI.STKVgt.IAgCrdnAxes Axes
        {
            get { return m_Axes; }
            set
            {
                if (Object.Vgt.Axes.Contains(((AGI.STKVgt.IAgCrdn)value).Name))
                    m_Axes = value;
                else
                    throw new Exception("Unable to set \"" + ((AGI.STKVgt.IAgCrdn)value).Name + "\" as the axes.\n" +
                        "Please make sure the axes exist on " + Object.InstanceName + ".");
            }
        }

        public XYPlane XYPlane = new XYPlane();
        public XZPlane XZPlane = new XZPlane();
        public YZPlane YZPlane = new YZPlane();

        private AGI.STKVgt.IAgCrdnSystem ReferenceFrame { get; set; }

        private AGI.STKObjects.IAgStkObject m_Object;
        private AGI.STKVgt.IAgCrdnPoint m_Origin;
        private AGI.STKVgt.IAgCrdnAxes m_Axes;

        private const string DefaultOrigin = "Center";
        private const string DefaultAxes = "Body";
    }
}
