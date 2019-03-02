Imports System.Collections.Generic
Imports System.Text
Imports AGI.STKVgt
Imports AGI.STKUtil
Imports AGI.STKGraphics
Imports AGI.STKObjects

Public NotInheritable Class ViewHelper
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Changes the view of a scene such that the camera's field of view encompasses the specified bounding sphere.
    ''' </summary>
    Public Shared Sub ViewBoundingSphere(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot, ByVal centralBody As String, ByVal sphere As IAgStkGraphicsBoundingSphere)
        ViewBoundingSphere(scene, root, centralBody, sphere, -90, 30)
    End Sub
    Public Shared Sub ViewBoundingSphere(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot, ByVal centralBody As String, ByVal sphere As IAgStkGraphicsBoundingSphere, ByVal azimuthAngle As Double, ByVal elevationAngle As Double)
        Dim referencePoint As IAgCrdnPoint = VgtHelper.CreatePoint(root.CentralBodies(centralBody).Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem)

        Dim centerArray As Array = sphere.Center
        DirectCast(referencePoint, IAgCrdnPointFixedInSystem).FixedPoint.AssignCartesian(CDbl(centerArray.GetValue(0)), CDbl(centerArray.GetValue(1)), CDbl(centerArray.GetValue(2)))

        DirectCast(referencePoint, IAgCrdnPointFixedInSystem).Reference.SetSystem(root.VgtRoot.WellKnownSystems.Earth.Fixed)

        Dim boundingSphereCenter As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
        boundingSphereCenter.AssignCartesian(CDbl(centerArray.GetValue(0)), CDbl(centerArray.GetValue(1)), CDbl(centerArray.GetValue(2)))

        Dim boundingSphereAxes As IAgCrdnAxes = TryCast(CodeSnippet.CreateAxes(root, centralBody, boundingSphereCenter), IAgCrdnAxes)

        Dim r As Double = scene.Camera.DistancePerRadius * sphere.Radius

        Dim displayUnit As String = root.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit")
        Dim internalUnit As String = "rad"
        Dim elevationAngleInRads As Double = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, elevationAngle)
        Dim azimuthAngleInRads As Double = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, azimuthAngle)

        Dim phi As Double = elevationAngleInRads
        Dim theta As Double = azimuthAngleInRads
        Dim offset As Array = New Object() {r * Math.Cos(phi) * Math.Cos(theta), r * Math.Cos(phi) * Math.Sin(theta), r * Math.Sin(phi)}

        scene.Camera.ViewOffset(boundingSphereAxes, referencePoint, offset)
    End Sub

    ''' <summary>
    ''' Change the view of a scene such that the camera's field of view encompasses the specified extent.
    ''' </summary>
    ''' <param name="extent">Extent as an Array of doubles in the order west, south, east, north.</param>
    Public Shared Sub ViewExtent(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot, ByVal centralBody As String, ByVal extent As Array, ByVal azimuthAngle As Double, ByVal elevationAngle As Double)
        Dim west As Double, south As Double, east As Double, north As Double
        west = CDbl(extent.GetValue(0))
        south = CDbl(extent.GetValue(1))
        east = CDbl(extent.GetValue(2))
        north = CDbl(extent.GetValue(3))

        ViewExtent(scene, root, centralBody, west, south, east, _
         north, azimuthAngle, elevationAngle)
    End Sub
    Public Shared Sub ViewExtent(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot, ByVal centralBody As String, ByVal west As Double, ByVal south As Double, ByVal east As Double, _
                                 ByVal north As Double, ByVal azimuthAngle As Double, ByVal elevationAngle As Double)
        scene.Camera.ViewRectangularExtent(centralBody, west, south, east, north)

        Dim offset As IAgCartesian3Vector = root.ConversionUtility.NewCartesian3Vector()
        Dim r As Double = scene.Camera.Distance

        Dim displayUnit As String = root.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit")
        Dim internalUnit As String = "rad"
        Dim elevationAngleInRads As Double = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, elevationAngle)
        Dim azimuthAngleInRads As Double = root.ConversionUtility.ConvertQuantity("AngleUnit", displayUnit, internalUnit, azimuthAngle)

        Dim phi As Double = elevationAngleInRads
        Dim theta As Double = azimuthAngleInRads

        offset.Set(r * Math.Cos(phi) * Math.Cos(theta), r * Math.Cos(phi) * Math.Sin(theta), r * Math.Sin(phi))

        Dim newCameraPosition As Array = New Object() _
        { _
            CDbl(scene.Camera.ReferencePoint.GetValue(0)) + offset.X, _
            CDbl(scene.Camera.ReferencePoint.GetValue(1)) + offset.Y, _
            CDbl(scene.Camera.ReferencePoint.GetValue(2)) + offset.Z _
        }

        scene.Camera.Position = newCameraPosition
    End Sub
End Class
