#Region "UsingDirectives"
Imports AGI.STKGraphics
Imports AGI.STKObjects
#End Region

Namespace Camera
	Class CameraFixedFrameCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Camera\CameraFixedFrameCodeSnippet.vb")
		End Sub

		' Name        
		' Description 
		' Category    
		' References  
		' Namespaces  
		' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddAPanelOverlay", _
            "Change view mode to use Earth's fixed frame", _
            "Graphics | Camera", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsScene" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            OverlayHelper.AddTextBox("By default, the camera is in the Earth's inertial frame.  " & vbCrLf & _
                                     "During animation, the globe will rotate.  In this example, " & vbCrLf & _
                                     "the camera is changed to the Earth's fixed frame, so the " & vbCrLf & _
                                     "camera does not move relative to the Earth during animation.", DirectCast(root.CurrentScenario, IAgScenario).SceneManager)
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			'
			' Set-up the animation for this specific example
			'
			Dim animationControl As IAgAnimation = DirectCast(root, IAgAnimation)
			Dim animationSettings As IAgScAnimation = DirectCast(root.CurrentScenario, IAgScenario).Animation

			animationControl.Pause()
			SetAnimationDefaults(root)
			animationSettings.AnimStepValue = 60.0
			animationControl.PlayForward()

			scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
			scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

			'#Region "CodeSnippet"
            scene.Camera.ViewCentralBody(attachID("$centralBodyName$Name of the Central Body to view$", "Earth"), attachID("$axes$The axes for the camera to use$", root.VgtRoot.WellKnownAxes.Earth.Fixed))
			'#End Region
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			scene.Camera.ViewCentralBody("Earth", root.VgtRoot.WellKnownAxes.Earth.Inertial)

			OverlayHelper.RemoveTextBox(DirectCast(root.CurrentScenario, IAgScenario).SceneManager)
			scene.Render()
			DirectCast(root, IAgAnimation).Rewind()
		End Sub
	End Class
End Namespace
