Imports System.Drawing
Imports AGI.STKVgt
Imports AGI.STKObjects
Imports AGI.STKGraphics

Namespace Primitives
	''' <summary>
	''' Visualization for a reference frame.  Given a ReferenceFrame, this class
	''' creates a polyline and text batch primitive to visualize the reference
	''' frame's axes.
	''' </summary>
	Class ReferenceFrameGraphics
		Implements IDisposable
		Public Sub New(root As AgStkObjectRoot, referenceFrame As IAgCrdnSystem, axesLength As Double)
            Me.New(root, referenceFrame, axesLength, Color.Red)
		End Sub

        Public Sub New(ByVal root As AgStkObjectRoot, ByVal referenceFrame As IAgCrdnSystem, ByVal axesLength As Double, mainColor As Color)
            Me.New(root, referenceFrame, axesLength, mainColor, Color.White)
        End Sub

		Public Sub New(root As AgStkObjectRoot, referenceFrame As IAgCrdnSystem, axesLength As Double, mainColor As Color, outlineColor As Color)
            Me.New(root, referenceFrame, axesLength, mainColor, outlineColor, DirectCast(root.CurrentScenario, IAgScenario).SceneManager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline("MS Sans Serif", 24, AgEStkGraphicsFontStyle.eStkGraphicsFontStyleRegular, True))
		End Sub

        Public Sub New(ByVal root As AgStkObjectRoot, ByVal referenceFrame As IAgCrdnSystem, ByVal axesLength As Double, ByVal mainColor As Color, ByVal outlineColor As Color, ByVal font As IAgStkGraphicsGraphicsFont)
            manager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim colorArgb As Integer = mainColor.ToArgb()

            m_Lines = manager.Initializers.PolylinePrimitive.InitializeWithType(AgEStkGraphicsPolylineType.eStkGraphicsPolylineTypeLines)

            Dim lines As Array = New Object() _
            { _
                0, 0, 0, axesLength, 0, 0, _
                0, 0, 0, 0, axesLength, 0, _
                0, 0, 0, 0, 0, axesLength _
            }
            Dim lineColors As Array = New Object() _
            { _
                colorArgb, colorArgb, _
                colorArgb, colorArgb, _
                colorArgb, colorArgb _
            }
            m_Lines.SetWithColors(lines, lineColors)

            DirectCast(m_Lines, IAgStkGraphicsPrimitive).ReferenceFrame = referenceFrame
            m_Lines.Width = 2
            manager.Primitives.Add(DirectCast(m_Lines, IAgStkGraphicsPrimitive))

            m_Text = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font)
            Dim optionalParameters As IAgStkGraphicsTextBatchPrimitiveOptionalParameters = manager.Initializers.TextBatchPrimitiveOptionalParameters.Initialize()
            Dim textColors As Array = New Object() _
            { _
                colorArgb, _
                colorArgb, _
                colorArgb _
            }
            optionalParameters.SetColors(textColors)

            Dim textPositions As Array = New Object() _
            { _
                axesLength, 0, 0, _
                0, axesLength, 0, _
                0, 0, axesLength _
            }
            Dim text As Array = New Object() {"+X", "+Y", "+Z"}

            m_Text.SetWithOptionalParameters(textPositions, text, optionalParameters)

            m_Text.OutlineColor = outlineColor
            DirectCast(m_Text, IAgStkGraphicsPrimitive).ReferenceFrame = referenceFrame
            manager.Primitives.Add(DirectCast(m_Text, IAgStkGraphicsPrimitive))
        End Sub


		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		Protected Overrides Sub Finalize()
			Try
				Dispose(False)
			Finally
				MyBase.Finalize()
			End Try
		End Sub

		Protected Overridable Sub Dispose(disposing As Boolean)
			If disposing Then
				If m_Text IsNot Nothing Then
					manager.Primitives.Remove(DirectCast(m_Text, IAgStkGraphicsPrimitive))
					'm_Text.Dispose();
					m_Text = Nothing
				End If
				If m_Lines IsNot Nothing Then
					manager.Primitives.Remove(DirectCast(m_Lines, IAgStkGraphicsPrimitive))
					'm_Lines.Dispose();
					m_Lines = Nothing
				End If
			End If
		End Sub

		Private m_Lines As IAgStkGraphicsPolylinePrimitive
		Private m_Text As IAgStkGraphicsTextBatchPrimitive
		Private manager As IAgStkGraphicsSceneManager
	End Class
End Namespace
