#Region "UsingDirectives"
Imports System.Windows.Forms
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace DisplayConditions
	Public Class TimeDisplayConditionCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("DisplayConditions\TimeDisplayConditionCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim globeOverlayFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/DisplayConditionExample.jp2").FullPath
            ExecuteSnippet(scene, root, globeOverlayFile)
        End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddTimeIntervalDisplayCondition", _
            "Draw a primitive based on a time interval", _
            "Graphics | DisplayConditions", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsTimeIntervalDisplayCondition" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("globeOverlayFile", "Location of the globe overlay file")> ByVal globeOverlayFile As String)
            Try
                '#Region "CodeSnippet"
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

                Dim overlay As IAgStkGraphicsGeospatialImageGlobeOverlay = manager.Initializers.GeospatialImageGlobeOverlay.InitializeWithString(globeOverlayFile)

                Dim start As IAgDate = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$startDate$The start date$", "30 May 2008 14:30:00.000"))
                Dim [end] As IAgDate = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$endDate$The end date$", "30 May 2008 15:00:00.000"))

                DirectCast(root.CurrentScenario, IAgScenario).Animation.StartTime = Double.Parse(start.Subtract("sec", 3600).Format("epSec"))

                Dim condition As IAgStkGraphicsTimeIntervalDisplayCondition = manager.Initializers.TimeIntervalDisplayCondition.InitializeWithTimes(start, [end])
                DirectCast(overlay, IAgStkGraphicsGlobeOverlay).DisplayCondition = TryCast(condition, IAgStkGraphicsDisplayCondition)

                scene.CentralBodies.Earth.Imagery.Add(DirectCast(overlay, IAgStkGraphicsGlobeImageOverlay))

                '#End Region

                m_Overlay = DirectCast(overlay, IAgStkGraphicsGlobeImageOverlay)

                OverlayHelper.AddTextBox("The overlay will be drawn on 5/30/2008 between " & vbCrLf & _
                                         "2:30:00 PM and 3:00:00 PM. " & vbCrLf & vbCrLf & _
                                         "This is implemented by assigning a " & vbCrLf & _
                                         "TimeIntervalDisplayCondition to the overlay's " & vbCrLf & _
                                         "DisplayCondition property.", manager)

                OverlayHelper.AddTimeOverlay(root)

                m_Start = start
                m_End = [end]
                OverlayHelper.TimeDisplay.AddInterval(Double.Parse(m_Start.Format("epSec").ToString()), Double.Parse(m_End.Format("epSec").ToString()))
            Catch
                MessageBox.Show("Could not create globe overlays.  Your video card may not support this feature.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
				'
				' Set-up the animation for this specific example
				'
				Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)

				animation.Pause()
				SetAnimationDefaults(root)

				animation.PlayForward()

				scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
				scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed
				Dim extent As Array = DirectCast(m_Overlay, IAgStkGraphicsGlobeOverlay).Extent
				scene.Camera.ViewExtent("Earth", extent)
				scene.Render()
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_Overlay IsNot Nothing Then
				DirectCast(root, IAgAnimation).Rewind()
				OverlayHelper.TimeDisplay.RemoveInterval(Double.Parse(m_Start.Format("epSec").ToString()), Double.Parse(m_End.Format("epSec").ToString()))
				OverlayHelper.RemoveTimeOverlay(DirectCast(root.CurrentScenario, IAgScenario).SceneManager)
				OverlayHelper.RemoveTextBox(DirectCast(root.CurrentScenario, IAgScenario).SceneManager)

				scene.CentralBodies.Earth.Imagery.Remove(m_Overlay)
				scene.Render()

				m_Overlay = Nothing
			End If
		End Sub

		Private m_Overlay As IAgStkGraphicsGlobeImageOverlay

		Private m_Start As IAgDate
		Private m_End As IAgDate
	End Class
End Namespace
