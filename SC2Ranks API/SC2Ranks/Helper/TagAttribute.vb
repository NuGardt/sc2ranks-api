Namespace SC2Ranks.Helper
  ''' <summary>
  ''' Attriobute for field tag.
  ''' </summary>
  ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Field, AllowMultiple := False, Inherited := False)>
  Public Class TagAttribute
    Inherits Attribute

    Public ReadOnly Value As String

    Public Sub New(ByVal Value As String)
      Me.Value = Value
    End Sub

    Public Overrides Function ToString() As String
      Return Me.Value
    End Function
  End Class
End Namespace