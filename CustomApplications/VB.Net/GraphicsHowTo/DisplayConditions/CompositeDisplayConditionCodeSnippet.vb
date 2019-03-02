Imports System.Drawing
Imports System.Windows.Forms
#Region "UsingDirectives"
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
Imports AGI.STKVgt
#End Region

Namespace DisplayConditions
	Class CompositeDisplayConditionCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("DisplayConditions\CompositeDisplayConditionCodeSnippet.vb")
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim modelFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath
            Dim position As Array = New Object() {29.98, -90.25, 0.0}
            Dim origin As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
            origin.AssignPlanetodetic(CDbl(position.GetValue(0)), CDbl(position.GetValue(1)), CDbl(position.GetValue(2)))
            Dim axes As IAgCrdnAxesFixed = CreateAxes(root, "Earth", origin)

            ExecuteSnippet(scene, root, modelFile, axes)
        End Sub

        ' Name        
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID         
        <AGI.CodeSnippets.CodeSnippet( _
            "AddACompositeDisplayCondition", _
            "Draw a primitive based on multiple conditions", _
            "Graphics | DisplayConditions", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsCompositeDisplayCondition" _
            )> _
        Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("modelFile", "Location of the model file")> ByVal modelFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("Axes", "An axes used to orient the model")> ByVal axes As IAgCrdnAxesFixed)
            OverlayHelper.AddTextBox("The primitive will be drawn on 5/30/2008 between 2:00:00" & vbCrLf & _
                                     "PM and 2:30:00 PM and between 3:00:00 PM and 3:30:00 PM." & vbCrLf & vbCrLf & _
                                     "Two TimeIntervalDisplayConditions are created and added to a" & vbCrLf & _
                                     "CompositeDisplayCondition, which is assigned to the model's " & vbCrLf & _
                                     "DisplayCondition property. The composite is set to use the " & vbCrLf & _
                                     "logical or operator so the primitive is shown if the current" & vbCrLf & _
                                     "animation time is within one of the time intervals. " & vbCrLf & vbCrLf & _
                                     "Note - the display conditions that make up a composite " & vbCrLf & _
                                     "display condition need not be of the same type.", DirectCast(root.CurrentScenario, IAgScenario).SceneManager)

            OverlayHelper.AddTimeOverlay(root)


            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim position As Array = New Object() {attachID("$lat$Model latitude$", 29.98), attachID("$lon$Model longitude$", -90.25), attachID("$alt$Model altitude$", 0.0)}

            Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
                modelFile)
            model.SetPositionCartographic(attachID("$planetName$Name of the planet to place the model$", "Earth"), position)
            model.Scale = Math.Pow(10, attachID("$scale$Scale of the model$", 1.5))

            Dim start1 As IAgDate = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$startDate1$The first start date$", "30 May 2008 14:00:00.000"))
            Dim end1 As IAgDate = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$endDate1$The first end date$", "30 May 2008 14:30:00.000"))
            DirectCast(root, IAgAnimation).CurrentTime = Double.Parse(start1.Format("epSec"))
            Dim start2 As IAgDate = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$startDate2$The second start date$", "30 May 2008 15:00:00.000"))
            Dim end2 As IAgDate = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$endDate2$The second end date$", "30 May 2008 15:30:00.000"))

            Dim time1 As IAgStkGraphicsTimeIntervalDisplayCondition = manager.Initializers.TimeIntervalDisplayCondition.InitializeWithTimes(start1, end1)
            Dim time2 As IAgStkGraphicsTimeIntervalDisplayCondition = manager.Initializers.TimeIntervalDisplayCondition.InitializeWithTimes(start2, end2)
            Dim composite As IAgStkGraphicsCompositeDisplayCondition = manager.Initializers.CompositeDisplayCondition.Initialize()

            composite.Add(DirectCast(time1, IAgStkGraphicsDisplayCondition))
            composite.Add(DirectCast(time2, IAgStkGraphicsDisplayCondition))
            composite.LogicOperation = AgEStkGraphicsBinaryLogicOperation.eStkGraphicsBinaryLogicOperationOr
            DirectCast(model, IAgStkGraphicsPrimitive).DisplayCondition = TryCast(composite, IAgStkGraphicsDisplayCondition)

            Dim result As IAgCrdnAxesFindInAxesResult = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(DirectCast(root.CurrentScenario, IAgScenario).Epoch, DirectCast(axes, IAgCrdnAxes))
            model.Orientation = result.Orientation

            manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))
            '#End Region

            m_Start1 = Double.Parse(start1.Format("epSec").ToString())
            m_End1 = Double.Parse(end1.Format("epSec").ToString())
            m_Start2 = Double.Parse(start2.Format("epSec").ToString())
            m_End2 = Double.Parse(end2.Format("epSec").ToString())
            OverlayHelper.TimeDisplay.AddInterval(m_Start1, m_End1)
            OverlayHelper.TimeDisplay.AddInterval(m_Start2, m_End2)

            m_Primitive = DirectCast(model, IAgStkGraphicsPrimitive)
        End Sub

        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            '
            ' Set-up the animation for this specific example
            '
            Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
            animation.Pause()
            SetAnimationDefaults(root)
            animation.PlayForward()

            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere)
            scene.Render()
        End Sub

        Public Overrides Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            DirectCast(root, IAgAnimation).Rewind()
            OverlayHelper.TimeDisplay.RemoveInterval(m_Start1, m_End1)
            OverlayHelper.TimeDisplay.RemoveInterval(m_Start2, m_End2)
            OverlayHelper.RemoveTimeOverlay(manager)
            OverlayHelper.RemoveTextBox(manager)

            manager.Primitives.Remove(m_Primitive)
            scene.Render()

            m_Primitive = Nothing
        End Sub

        Private m_Primitive As IAgStkGraphicsPrimitive

        Private m_Start1 As Double
        Private m_End1 As Double
        Private m_Start2 As Double
        Private m_End2 As Double


    End Class
End Namespace
