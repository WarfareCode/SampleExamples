Imports System.IO
Imports System.Text

''' <summary>
''' Reads #region sections from a source file and returns 
''' the using directives and code snippets.
''' </summary>
Public Class CodeSnippetReader
    Public Sub New(ByVal filePath As String)
        m_filePath = filePath
    End Sub

    ''' <summary>
    ''' Returns the source code in the first '#region CodeSnippets' 
    ''' section.  The string is cleaned up to display nicely in a 
    ''' rich text box.
    ''' </summary>
    Public ReadOnly Property Code() As String
        Get
            If m_codeSnippet Is Nothing Then
                m_codeSnippet = GetRegion("CodeSnippet")
            End If

            Return m_codeSnippet
        End Get
    End Property

    ''' <summary>
    ''' Returns the source code in the first '#region UsingDirectives'
    ''' section.  The string is cleaned up to display nicely in a 
    ''' rich text box.
    ''' </summary>
    Public ReadOnly Property UsingDirectives() As String
        Get
            If m_usingDirectives Is Nothing Then
                m_usingDirectives = GetRegion("UsingDirectives")
            End If

            Return m_usingDirectives
        End Get
    End Property

    ''' <summary>
    ''' Returns the filename that contains the code snippet.
    ''' </summary>
    Public ReadOnly Property FileName() As String
        Get
            Return Path.GetFileName(m_filePath)
        End Get
    End Property

    ''' <summary>
    ''' Returns the filepath that contains the code snippet.
    ''' </summary>
    Public ReadOnly Property FilePath() As String
        Get
            Return m_filePath
        End Get
    End Property

    ''' <summary>
    ''' Loads the source file, if it is not already loaded, and extracts
    ''' the source code in the first region defined by the input string.
    ''' </summary>
    Private Function GetRegion(ByVal region As String) As String
        If m_fileContent Is Nothing Then
            m_fileContent = File.ReadAllLines(m_filePath)
        End If

        Dim result As New StringBuilder()

        ' Find all occurrences of the specified region.

        Dim startRegionToken As String = "#Region """ & region & """"
        Const endRegionToken As String = "#End Region"

        Dim insideRegion As Boolean = False
        Dim spacesToRemove As Integer = -1
        For Each line As String In m_fileContent
            If line.Contains(startRegionToken) Then
                ' Start reading code snippet.
                insideRegion = True
                spacesToRemove = -1
                Continue For
            End If

            If line.Contains(endRegionToken) Then
                ' Stop reading code snippet.
                insideRegion = False
                Continue For
            End If

            If Not insideRegion Then
                Continue For
            End If

            If spacesToRemove < 0 Then
                ' Compute number of spaces before code on the first non-blank line.  All following lines
                ' will have this many spaces removed from them to left-align the code in the rich text box.                    
                Dim isBlankLine As Boolean = True
                Dim spaces As Integer = 0
                For Each c As Char In line
                    If c = " "c OrElse c = ControlChars.Tab Then
                        spaces += 1
                    Else
                        isBlankLine = False
                        Exit For
                    End If
                Next

                If isBlankLine Then
                    Continue For
                End If

                spacesToRemove = spaces
            End If

            ' Remove spaces from the front of the line, if the line is long enough.
            Dim lineOfCode As String = line
            If lineOfCode.Length > spacesToRemove Then
                lineOfCode = lineOfCode.Substring(spacesToRemove)
            End If

            result.Append(lineOfCode)
            result.Append(ControlChars.Lf)
        Next

        Return result.ToString()
    End Function

    Private ReadOnly m_filePath As String
    Private m_fileContent As String()
    Private m_codeSnippet As String
    Private m_usingDirectives As String
End Class
