Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports AGI.STKUtil
Imports AGI.STKObjects
Imports AGI.STKVgt
Imports AGI.STKGraphics

    Public NotInheritable Class STKUtil
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Reads an STK area target file (*.at) and returns the points defining
        ''' the area target's boundary as a list of Cartographic points.
        ''' </summary>
    Public Shared Function ReadAreaTargetCartographic(ByVal fileName As String) As Array
        '
        ' Open the file and read everything between "BEGIN PolygonPoints"
        ' and "END PolygonPoints"
        '
        Dim areaTarget As String = File.ReadAllText(fileName)
        Dim startToken As String = "BEGIN PolygonPoints"
        Dim points As String = areaTarget.Substring(areaTarget.IndexOf(startToken, StringComparison.Ordinal) + startToken.Length)
        points = points.Substring(0, points.IndexOf("END PolygonPoints", StringComparison.Ordinal))

        Dim splitPoints As String() = points.Split(New Char() {ControlChars.Tab, ControlChars.Lf, ControlChars.Cr}, StringSplitOptions.RemoveEmptyEntries)

        Dim targetPoints As Array = Array.CreateInstance(GetType(Object), splitPoints.Length)
        For i As Integer = 0 To splitPoints.Length - 1 Step 3
            '
            ' Each line is [Latitude][Longitude][Altitude].  In the file,
            ' latitude and longitude are in degrees and altitude is in
            ' meters.
            '
            Dim latitude As Double = Double.Parse(splitPoints(i), CultureInfo.InvariantCulture)
            Dim longitude As Double = Double.Parse(splitPoints(i + 1), CultureInfo.InvariantCulture)
            Dim altitude As Double = Double.Parse(splitPoints(i + 2), CultureInfo.InvariantCulture)

            targetPoints.SetValue(latitude, i)
            targetPoints.SetValue(longitude, i + 1)

            targetPoints.SetValue(altitude, i + 2)
        Next

        Return targetPoints
    End Function

    ''' <summary>
    ''' Reads an STK area target file (*.at) and returns the points defining
    ''' the area target's boundary as a list Cartesian points in the
    ''' earth's fixed frame.
    ''' This method assumes the file exists, that it is a valid area target 
    ''' file, and the area target is on earth.
    ''' </summary>
    Public Shared Function ReadAreaTargetPoints(ByVal fileName As String, ByVal root As AgStkObjectRoot) As Array
        '
        ' Open the file and read everything between "BEGIN PolygonPoints"
        ' and "END PolygonPoints"
        '
        Dim areaTarget As String = File.ReadAllText(fileName)
        Dim startToken As String = "BEGIN PolygonPoints"
        Dim points As String = areaTarget.Substring(areaTarget.IndexOf(startToken, StringComparison.Ordinal) + startToken.Length)
        points = points.Substring(0, points.IndexOf("END PolygonPoints", StringComparison.Ordinal))

        Dim splitPoints As String() = points.Split(New Char() {ControlChars.Tab, ControlChars.Lf, ControlChars.Cr}, StringSplitOptions.RemoveEmptyEntries)
        Dim targetPoints As Array = Array.CreateInstance(GetType(Object), splitPoints.Length)
        For i As Integer = 0 To splitPoints.Length - 1 Step 3
            '
            ' Each line is [Latitude][Longitude][Altitude].  In the file,
            ' latitude and longitude are in degrees and altitude is in
            ' meters.
            '
            Dim latitude As Double = Double.Parse(splitPoints(i), CultureInfo.InvariantCulture)
            Dim longitude As Double = Double.Parse(splitPoints(i + 1), CultureInfo.InvariantCulture)
            Dim altitude As Double = Double.Parse(splitPoints(i + 2), CultureInfo.InvariantCulture)
            Dim pos As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
            pos.AssignPlanetodetic(latitude, longitude, altitude)

            pos.QueryCartesianArray().CopyTo(targetPoints, i)
        Next

        Return targetPoints
    End Function

    Public Shared Function ReadLineTargetPoints(ByVal fileName As String, ByVal root As AgStkObjectRoot) As Array
        Dim areaTarget As String = File.ReadAllText(fileName)
        Dim startToken As String = "BEGIN PolylinePoints"
        Dim points As String = areaTarget.Substring(areaTarget.IndexOf(startToken, StringComparison.Ordinal) + startToken.Length)
        points = points.Substring(0, points.IndexOf("END PolylinePoints", StringComparison.Ordinal))

        Dim splitPoints As String() = points.Split(New Char() {ControlChars.Tab, ControlChars.Lf, ControlChars.Cr, " "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim targetPoints As Array = Array.CreateInstance(GetType(Object), splitPoints.Length)
        For i As Integer = 0 To splitPoints.Length - 1 Step 3
            Dim longitude As Double = Double.Parse(splitPoints(i + 1), CultureInfo.InvariantCulture)
            Dim latitude As Double = Double.Parse(splitPoints(i), CultureInfo.InvariantCulture)
            Dim altitude As Double = Double.Parse(splitPoints(i + 2), CultureInfo.InvariantCulture)
            Dim pos As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
            pos.AssignPlanetodetic(latitude, longitude, altitude)

            pos.QueryCartesianArray().CopyTo(targetPoints, i)
        Next

        Return targetPoints
    End Function
    End Class
