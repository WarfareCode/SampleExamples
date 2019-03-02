Option Strict On
Option Explicit On

Imports AGI.STKX


Namespace My

    Partial Friend Class MyApplication

        Dim STKXApp As AgSTKXApplication = Nothing

        Private Sub MyApplication_Startup( _
            ByVal sender As Object, _
            ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs _
        ) Handles Me.Startup

            Try

                STKXApp = New AGI.STKX.AgSTKXApplication()
                If (Not STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl)) Then
                    MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If

            Catch exception As System.Runtime.InteropServices.COMException

                If (exception.ErrorCode = &H80040154) Then

                    Dim errorMessage As String = "Could not instantiate AgSTKXApplication."
                    errorMessage += Environment.NewLine
                    errorMessage += Environment.NewLine
                    errorMessage += "You are trying to run in the x64 configuration. Check that STK Engine 64-bit is installed on this machine."

                    MessageBox.Show(errorMessage, "STK Engine Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)

                Else
                    Throw
                End If

            End Try

            If STKXApp Is Nothing Then
                e.Cancel = True
            End If

        End Sub

    End Class
End Namespace