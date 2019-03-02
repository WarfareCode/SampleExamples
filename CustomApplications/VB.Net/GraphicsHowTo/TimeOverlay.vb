Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Globalization
Imports AGI.STKObjects
Imports AGI.STKUtil

Public Class TimeOverlay
	Inherits StatusOverlay(Of Double)
	Public Sub New(root As AgStkObjectRoot)
        MyBase.New(True, Double.Parse(DirectCast(root.CurrentScenario, IAgScenario).StartTime.ToString()), Double.Parse(DirectCast(root.CurrentScenario, IAgScenario).StopTime.ToString()), _
                   String.Format(CultureInfo.InvariantCulture, "{0:MM/dd}", root.ConversionUtility.NewDate("epSec", DirectCast(root.CurrentScenario, IAgScenario).StartTime.ToString()).OLEDate), _
                   String.Format(CultureInfo.InvariantCulture, "{0:MM/dd}", root.ConversionUtility.NewDate("epSec", DirectCast(root.CurrentScenario, IAgScenario).StopTime.ToString()).OLEDate), _
                   DirectCast(root.CurrentScenario, IAgScenario).SceneManager)
        m_Root = root
        m_CurrentTime = root.ConversionUtility.NewDate("epSec", DirectCast(root.CurrentScenario, IAgScenario).StartTime.ToString())

        AddHandler root.OnAnimUpdate, AddressOf StkTimeChanged
    End Sub

    Private Sub StkTimeChanged(TimeEpSec As Double)
        m_CurrentTime = m_Root.ConversionUtility.NewDate("epSec", TimeEpSec.ToString())
        Update(Value, DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager)
    End Sub

    Public Overrides Function ValueTransform(value As Double) As Double
        Return value - Double.Parse(DirectCast(m_Root.CurrentScenario, IAgScenario).StartTime.ToString())
    End Function

    Public Overrides ReadOnly Property Value() As Double
        Get
            Return Double.Parse(m_CurrentTime.Format("epSec"))
        End Get
    End Property

    Public Overrides ReadOnly Property Text() As String
        Get
            Return String.Format(CultureInfo.InvariantCulture, "Current Time:" & vbLf & _
                                 "{0:MM/dd/yyyy}" & vbLf & "{0:hh:mm:ss tt}", m_CurrentTime.OLEDate)
        End Get
    End Property

    Friend Sub AddInterval(start As Double, [end] As Double)
        Indicator.AddInterval(ValueTransform(start), ValueTransform([end]), IndicatorStyle.Bar, DirectCast(m_Root.CurrentScenario, IAgScenario).SceneManager)
    End Sub

	Friend Sub RemoveInterval(start As Double, [end] As Double)
		Indicator.RemoveInterval(ValueTransform(start), ValueTransform([end]))
	End Sub

	Private m_Root As AgStkObjectRoot
	Private m_CurrentTime As IAgDate
End Class
