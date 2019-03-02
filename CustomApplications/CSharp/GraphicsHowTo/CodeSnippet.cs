using System.IO;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using System;
using System.Collections.Generic;

namespace GraphicsHowTo
{
    public abstract class CodeSnippet
    {
        protected CodeSnippet(string filePath)
        {
            string codeSnippetsPath = Path.Combine(Application.StartupPath, @"CodeSnippets");
            string sourceFilePath = Path.Combine(codeSnippetsPath, filePath);
            m_reader = new CodeSnippetReader(sourceFilePath);
        }

        /// <summary>
        /// Executes the code snippet.
        /// </summary>
        public abstract void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root);

        /// <summary>
        /// Zooms to the area with the example.  For example, the Execute()
        /// method may turn on clouds and this method may zoom out so the
        /// entire globe and clouds are visible.
        /// </summary>
        public abstract void View(IAgStkGraphicsScene scene, AgStkObjectRoot root);

        /// <summary>
        /// Removes the effects of the code snippet.  For example,
        /// if Execute() turned on clouds, this method turns clouds off.
        /// </summary>
        public abstract void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root);

        /// <summary>
        /// Returns the source code for this snippet as a string.
        /// </summary>
        public string Code
        {
            get { return m_reader.Code; }
        }

        /// <summary>
        /// Returns the using directives required to run the example.
        /// </summary>
        public string UsingDirectives
        {
            get { return m_reader.UsingDirectives; }
        }

        /// <summary>
        /// Returns the filename that contains the code snippet.
        /// </summary>
        public string FileName
        {
            get { return m_reader.FileName; }
        }

        /// <summary>
        /// Returns the filepath that contains the code snippet.
        /// </summary>
        public string FilePath
        {
            get { return m_reader.FilePath; }
        }

        public IAgPosition CreatePosition(AgStkObjectRoot root, double lat, double lon, double alt)
        {
            IAgPosition pos = root.ConversionUtility.NewPositionOnEarth();
            pos.AssignPlanetodetic(lat, lon, alt);
            return pos;
        }

        public static IAgCrdnAxesFixed CreateAxes(AgStkObjectRoot root, string centralBodyName, IAgPosition pos)
        {
            IAgCrdnProvider provider = root.CentralBodies[centralBodyName].Vgt;
            IAgCrdnPointFixedInSystem @fixed = (IAgCrdnPointFixedInSystem)VgtHelper.CreatePoint(provider, AgECrdnPointType.eCrdnPointTypeFixedInSystem);

            double x, y, z;
            pos.QueryCartesian(out x, out y, out z);
            @fixed.FixedPoint.AssignCartesian(x, y, z);
            @fixed.Reference.SetSystem(provider.WellKnownSystems.Earth.Fixed);

            // Create a topocentric axes
            IAgCrdnAxesOnSurface axes = (IAgCrdnAxesOnSurface)VgtHelper.CreateAxes(provider, AgECrdnAxesType.eCrdnAxesTypeOnSurface);
            axes.ReferencePoint.SetPoint((IAgCrdnPoint)@fixed);
            axes.CentralBody.SetPath(centralBodyName);

            IAgCrdnAxesFixed eastNorthUp = (IAgCrdnAxesFixed)VgtHelper.CreateAxes(provider, AgECrdnAxesType.eCrdnAxesTypeFixed);
            eastNorthUp.ReferenceAxes.SetAxes((IAgCrdnAxes)axes);
            eastNorthUp.FixedOrientation.AssignEulerAngles(AGI.STKUtil.AgEEulerOrientationSequence.e321, 90, 0, 0);

            return eastNorthUp;
        }

        public static IAgCrdnSystem CreateSystem(AgStkObjectRoot root, string centralBodyName, IAgPosition pos, IAgCrdnAxesFixed axes)
        {
            IAgCrdnProvider provider = root.CentralBodies[centralBodyName].Vgt;

            double x, y, z;
            IAgCrdnPointFixedInSystem point = (IAgCrdnPointFixedInSystem)VgtHelper.CreatePoint(provider, AgECrdnPointType.eCrdnPointTypeFixedInSystem);
            pos.QueryCartesian(out x, out y, out z);
            point.FixedPoint.AssignCartesian(x, y, z);
            point.Reference.SetSystem(provider.Systems["Fixed"]);

            IAgCrdnSystemAssembled system = (IAgCrdnSystemAssembled)VgtHelper.CreateSystem(provider, AgECrdnSystemType.eCrdnSystemTypeAssembled);
            system.OriginPoint.SetPoint((IAgCrdnPoint)point);
            system.ReferenceAxes.SetAxes((IAgCrdnAxes)axes);
            return (IAgCrdnSystem)system;
        }

        /// <summary>
        /// Setup defaults for code snippets in howto:
        /// 2pm 5/30 - 5/31, time = start, step = 15, looped animation
        /// This time will give good lighting on the side of the globe where most of the examples will be.
        /// </summary>
        public static void SetAnimationDefaults(AgStkObjectRoot root)
        {
            IAgAnimation animationControl = (IAgAnimation)root;
            IAgScenario scenario = (IAgScenario)root.CurrentScenario;
            IAgScAnimation animationSettings = ((IAgScenario)root.CurrentScenario).Animation;

            animationControl.Rewind();
            animationSettings.AnimStepValue = 15.0;
            animationSettings.RefreshDelta = 0.01;
            animationSettings.RefreshDeltaType = AgEScRefreshDeltaType.eRefreshDelta;
            scenario.StartTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec"));
            scenario.StopTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "31 May 2008 14:00:00.000").Format("epSec"));
            animationSettings.StartTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec")); ;
            animationSettings.EnableAnimCycleTime = true;
            animationSettings.AnimCycleTime = double.Parse(root.ConversionUtility.NewDate("UTCG", "31 May 2008 14:00:00.000").Format("epSec"));
            animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime;
            animationControl.Rewind();
        }

        public static System.Drawing.Size MeasureString(string text, System.Drawing.Font font)
        {
            // Graphics.MeasureString() is more accurate than TextRenderer.MeasureText, but it requires a Graphics object
            using (System.Drawing.Graphics graphics = HowToForm.Instance.Control3D.CreateGraphics())
            {
                return graphics.MeasureString(text, font).ToSize();
            }
        }

        public static Array ConvertIListToArray(IList<Array> positions)
        {
            Array array = Array.CreateInstance(typeof(object), positions.Count * 3);
            for (int i = 0; i < positions.Count; ++i)
            {
                Array position = positions[i];
                position.CopyTo(array, i * 3);
            }
            return array;
        }

        public static Array ConvertIListToArray(params Array[] positions)
        {
            Array array = Array.CreateInstance(typeof(object), positions.Length * 3);
            for (int i = 0; i < positions.Length; ++i)
            {
                Array position = positions[i];
                position.CopyTo(array, i * 3);
            }
            return array;
        }

        private readonly CodeSnippetReader m_reader;
    }

    public static class VgtHelper
    {
        static int _counter = 0;

        public enum VgtTypes
        {
            Vector,
            System,
            Axes,
            Point
        }

        public static string GetTransientName(VgtTypes type)
        {
            return string.Format("{0}_{1}", type, _counter++);
        }

        public static IAgCrdnPoint CreatePoint(IAgCrdnProvider provider, AgECrdnPointType type)
        {
            return provider.Points.Factory.Create(GetTransientName(VgtHelper.VgtTypes.Point), string.Empty, type);
        }

        public static IAgCrdnAxes CreateAxes(IAgCrdnProvider provider, AgECrdnAxesType type)
        {
            return provider.Axes.Factory.Create(GetTransientName(VgtHelper.VgtTypes.Axes), string.Empty, type);
        }

        public static IAgCrdnSystem CreateSystem(IAgCrdnProvider provider, AgECrdnSystemType type)
        {
            return provider.Systems.Factory.Create(GetTransientName(VgtHelper.VgtTypes.System), string.Empty, type);
        }
    }
}

namespace AGI.CodeSnippets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Method)]
    public class CodeSnippet : Attribute
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class Parameter : Attribute
        {
            private string _id;
            public string ID
            {
                get { return _id; }
                set { _id = value; }
            }

            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _description;
            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }

            private string _paramType;
            public string ParamType
            {
                get { return _paramType; }
                set { _paramType = value; }
            }

            public Parameter(string id, string description)
            {
                _id = id;
                _description = description;
            }
            private string _eid;
            public string EID
            {
                get { return _eid; }
                set { _eid = value; }
            }
        };

        private List<Parameter> _parameters;
        public List<Parameter> Parameters
        {
            get { return _parameters; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        private string _references;
        public string References
        {
            get { return _references; }
            set { _references = value; }
        }

        private string _namespaces;
        public string Namespaces
        {
            get { return _namespaces; }
            set { _namespaces = value; }
        }

        private string _file;
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        private int _startLine;
        public int StartLine
        {
            get { return _startLine; }
            set { _startLine = value; }
        }

        private int _endLine;
        public int EndLine
        {
            get { return _endLine; }
            set { _endLine = value; }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _eid;
        public string EID
        {
            get { return _eid; }
            set { _eid = value; }
        }

        private string _language;
        public string Language
        {
            get
            {
                if (!String.IsNullOrEmpty(_language))
                    return _language;

                if (_file.EndsWith("cs"))
                {
                    _language = "CSharp";
                }
                else if (_file.EndsWith("vb"))
                {
                    _language = "VB";
                }

                return _language;
            }
        }

        public CodeSnippet(
            string name,
            string description,
            string category,
            string references,
            string namespaces,
            string eid
        )
        {
            _name = name;
            _description = description;
            _category = category;
            _references = references;
            _namespaces = namespaces;
            _eid = eid;
            _parameters = new List<Parameter>();
        }
    }
}