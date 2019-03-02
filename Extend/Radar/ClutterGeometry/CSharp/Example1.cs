﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AGI.Radar.Plugin;
using AGI.Plugin;
using AGI.VectorGeometryTool.Plugin;
using AGI.STK.Plugin;
using Microsoft.Win32;

namespace Agi.Radar.ClutterGeometry.CSharp.Example
{
    // NOTE: Generate your own Guid using Microsoft's GuidGen.exe
    [Guid("EFA7EA9B-A905-4c7d-839A-901FFA05F581")]
    // NOTE: Create your own ProgId to match your plugin's namespace and name
    [ProgId("Agi.Radar.ClutterGeometry.CSharp.Example1")]
    // NOTE: Specify the ClassInterfaceType.None enumeration, so the custom COM Interface 
    // you created is used instead of an autogenerated COM Interface.
    [ClassInterface(ClassInterfaceType.None)]
    public class Example1 : IExample1, IAgStkRadarClutterGeometryPlugin, IAgUtPluginConfig
    {
        object m_attrScope;
        double m_patchArea;
        double m_offsetAngle;
        double m_halfPi;
        IAgStkPluginSite m_stkPluginSite;
        IAgCrdnPluginProvider m_vectorToolProvider;
        IAgCrdnPluginCalcProvider m_calcToolProvider;
        IAgStkRadarCBIntersectComputeParams m_cbIntersectComputeParams;

        public Example1()
        {
            PatchArea = 1.0;
            OffsetAngle = 0.01745329251994329576923690768489;
            m_halfPi = 1.5707963267948966192;
            m_cbIntersectComputeParams = new AgStkRadarCBIntersectComputeParams() as IAgStkRadarCBIntersectComputeParams;
        }

        #region IExample1 Members

        public double PatchArea
        {
            get
            {
                return m_patchArea;
            }
            set
            {
                m_patchArea = value;
            }
        }

        public double OffsetAngle
        {
            get
            {
                return m_offsetAngle;
            }
            set
            {
                m_offsetAngle = value;
            }
        }

        #endregion

        #region IAgUtPluginConfig Members

        public object GetPluginConfig(AGI.Attr.AgAttrBuilder pAttrBuilder)
        {
            if (m_attrScope == null)
            {
                m_attrScope = pAttrBuilder.NewScope();
                pAttrBuilder.AddQuantityDispatchProperty(m_attrScope, "PatchArea", "PatchArea", "PatchArea", "sqm", "sqm", 0);
                pAttrBuilder.AddQuantityDispatchProperty(m_attrScope, "OffsetAngle", "OffsetAngle", "OffsetAngle", "deg", "rad", 0);
            }
            return m_attrScope;
        }

        public void VerifyPluginConfig(AgUtPluginConfigVerifyResult pPluginCfgResult)
        {
            pPluginCfgResult.Result = true;
            pPluginCfgResult.Message = "Ok";
        }

        #endregion

        #region IAgStkRadarClutterGeometryPlugin Members

        public void Compute(IAgStkRadarClutterGeometryComputeParams computeParams)
        {
            IAgStkRadarLink radarLink = computeParams.RadarLink;
            IAgStkRadarLinkGeometry radarLinkGeom = radarLink.Geometry;
            IAgStkRadarPosVelProvider xmtRdrPosVel = radarLinkGeom.TransmitRadarPosVelProvider;

            CartVec xmtRdrPosCBF = new CartVec(xmtRdrPosVel.PositionCBFArray);

            m_cbIntersectComputeParams.SetBasePositionCBF(xmtRdrPosCBF.X, xmtRdrPosCBF.Y, xmtRdrPosCBF.Z);

            double sinOffset = Math.Sin(m_halfPi - OffsetAngle);
            double cosOffset = Math.Cos(m_halfPi - OffsetAngle);

            //==============================  First Point Start ======================================================
            CartVec pt1Cbf = new CartVec(xmtRdrPosVel.ConvertBodyCartesianToCBFCartesianArray(cosOffset, 0.0, sinOffset));
            m_cbIntersectComputeParams.SetDirectionCBF(pt1Cbf.X, pt1Cbf.Y, pt1Cbf.Z);

            IAgStkRadarCBIntersectComputeResult pIntersectResult1 = xmtRdrPosVel.ComputeCentralBodyIntersect(m_cbIntersectComputeParams);
            if (pIntersectResult1.IntersectionFound)
            {
                IAgStkRadarClutterPatch clutterPatch = computeParams.ClutterPatches.Add();

                CartVec intersectPt = new CartVec(pIntersectResult1.Intercept1CBFArray);
                clutterPatch.SetPositionCBF(intersectPt.X, intersectPt.Y, intersectPt.Z);
                clutterPatch.Area = PatchArea;
            }
            //==============================  Second Point Start ======================================================
            CartVec pt2Cbf = new CartVec(xmtRdrPosVel.ConvertBodyCartesianToCBFCartesianArray(-cosOffset, 0.0, sinOffset));
            m_cbIntersectComputeParams.SetDirectionCBF(pt2Cbf.X, pt2Cbf.Y, pt2Cbf.Z);

            IAgStkRadarCBIntersectComputeResult pIntersectResult2 = xmtRdrPosVel.ComputeCentralBodyIntersect(m_cbIntersectComputeParams);
            if (pIntersectResult2.IntersectionFound)
            {
                IAgStkRadarClutterPatch clutterPatch = computeParams.ClutterPatches.Add();

                CartVec intersectPt = new CartVec(pIntersectResult2.Intercept1CBFArray);
                clutterPatch.SetPositionCBF(intersectPt.X, intersectPt.Y, intersectPt.Z);
                clutterPatch.Area = PatchArea;
            }
            //==============================  Third Point Start ======================================================
            CartVec pt3Cbf = new CartVec(xmtRdrPosVel.ConvertBodyCartesianToCBFCartesianArray(0.0, cosOffset, sinOffset));
            m_cbIntersectComputeParams.SetDirectionCBF(pt3Cbf.X, pt3Cbf.Y, pt3Cbf.Z);

            IAgStkRadarCBIntersectComputeResult pIntersectResult3 = xmtRdrPosVel.ComputeCentralBodyIntersect(m_cbIntersectComputeParams);
            if (pIntersectResult3.IntersectionFound)
            {
                IAgStkRadarClutterPatch clutterPatch = computeParams.ClutterPatches.Add();

                CartVec intersectPt = new CartVec(pIntersectResult3.Intercept1CBFArray);
                clutterPatch.SetPositionCBF(intersectPt.X, intersectPt.Y, intersectPt.Z);
                clutterPatch.Area = PatchArea;
            }
            //==============================  Fourth Point Start ======================================================
            CartVec pt4Cbf = new CartVec(xmtRdrPosVel.ConvertBodyCartesianToCBFCartesianArray(0.0, -cosOffset, sinOffset));
            m_cbIntersectComputeParams.SetDirectionCBF(pt4Cbf.X, pt4Cbf.Y, pt4Cbf.Z);

            IAgStkRadarCBIntersectComputeResult pIntersectResult4 = xmtRdrPosVel.ComputeCentralBodyIntersect(m_cbIntersectComputeParams);
            if (pIntersectResult4.IntersectionFound)
            {
                IAgStkRadarClutterPatch clutterPatch = computeParams.ClutterPatches.Add();

                CartVec intersectPt = new CartVec(pIntersectResult4.Intercept1CBFArray);
                clutterPatch.SetPositionCBF(intersectPt.X, intersectPt.Y, intersectPt.Z);
                clutterPatch.Area = PatchArea;
            }
        }

        public void Free()
        {
        }

        public void Initialize(IAgUtPluginSite site)
        {
            m_stkPluginSite = site as IAgStkPluginSite;
            if (m_stkPluginSite != null)
            {
                m_vectorToolProvider = m_stkPluginSite.VectorToolProvider;
                m_calcToolProvider = m_stkPluginSite.CalcToolProvider;
            }
        }

        public void PostCompute()
        {
        }

        public bool PreCompute()
        {
            return true;
        }

        public void Register(IAgStkRadarClutterGeometryPluginRegInfo registrationInfo)
        {
            registrationInfo.ValidRadarSystems = AgEStkRadarValidSystems.eStkRadarAllSystems;
        }

        #endregion

        struct CartVec
        {
            double m_x;
            double m_y;
            double m_z;

            public CartVec(Array values)
            {
                m_x = (double)values.GetValue(0);
                m_y = (double)values.GetValue(1);
                m_z = (double)values.GetValue(2);
            }

            public double X { get { return m_x; } set { m_x = value; } }
            public double Y { get { return m_y; } set { m_y = value; } }
            public double Z { get { return m_z; } set { m_z = value; } }

            public static CartVec operator -(CartVec lhs, CartVec rhs)
            {
                CartVec result = new CartVec();
                result.X = lhs.X - rhs.X;
                result.Y = lhs.Y - rhs.Y;
                result.Z = lhs.Z - rhs.Z;
                return result;
            }

            public CartVec Cross(CartVec rhs)
            {
                CartVec result = new CartVec();
                result.X = m_y * rhs.Z - m_z * rhs.Y;
                result.Y = m_z * rhs.X - m_x * rhs.Z;
                result.Z = m_x * rhs.Y - m_y * rhs.X;
                return result;
            }

            public double Mag()
            {
                return Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);
            }

            public double Dot(CartVec rhs)
            {
                return m_x * rhs.X + m_y * rhs.Y + m_z * rhs.Z;
            }

            public static double AngleBetween(CartVec lhs, CartVec rhs)
            {
                CartVec cross = lhs.Cross(rhs);
                double sinTheta = cross.Mag();
                double cosTheta = lhs.Dot(rhs);
                return Math.Abs(Math.Atan2(sinTheta, cosTheta));
            }
        }

        #region Registration functions
        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        [ComRegisterFunction]
        [ComVisible(false)]
        public static void RegisterFunction(Type t)
        {
            RemoveOtherVersions(t);
        }

        /// <summary>
        /// Called when the assembly is unregistered for use from COM.
        /// </summary>
        /// <param name="t">The type exposed to COM.</param>
        [ComUnregisterFunctionAttribute]
        [ComVisible(false)]
        public static void UnregisterFunction(Type t)
        {
            // Do nothing.
        }

        /// <summary>
        /// Called when the assembly is registered for use from COM.
        /// Eliminates the other versions present in the registry for
        /// this type.
        /// </summary>
        /// <param name="t">The type being exposed to COM.</param>
        public static void RemoveOtherVersions(Type t)
        {
            try
            {
                using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
                {
                    StringBuilder guidString = new StringBuilder("{");
                    guidString.Append(t.GUID.ToString());
                    guidString.Append("}");
                    using (RegistryKey guidKey = clsidKey.OpenSubKey(guidString.ToString()))
                    {
                        if (guidKey != null)
                        {
                            using (RegistryKey inproc32Key = guidKey.OpenSubKey("InprocServer32", true))
                            {
                                if (inproc32Key != null)
                                {
                                    string currentVersion = t.Assembly.GetName().Version.ToString();
                                    string[] subKeyNames = inproc32Key.GetSubKeyNames();
                                    if (subKeyNames.Length > 1)
                                    {
                                        foreach (string subKeyName in subKeyNames)
                                        {
                                            if (subKeyName != currentVersion)
                                            {
                                                inproc32Key.DeleteSubKey(subKeyName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore all exceptions...
            }
        }
        #endregion
    }
}
