Imports System.Runtime.Serialization
Imports System.Text

Namespace SC2Ranks.API.PlayerInfo
  ''' <summary>
  ''' Class containing extended player information.
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract()>
  Public Class PlayerInfoExtended
    Inherits PlayerInfoBase

    Private m_Teams As PlayerInfoTeam()

    ''' <summary>
    ''' Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Private Sub New()
      Call MyBase.New()

      Me.m_Teams = Nothing
    End Sub

#Region "Properties"

    ''' <summary>
    ''' Teams
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "teams", IsRequired := False, EmitDefaultValue := False)>
    Public Property Teams() As PlayerInfoTeam()
      Get
        Return Me.m_Teams
      End Get
      Private Set(ByVal Value As PlayerInfoTeam())
        Me.m_Teams = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendLine(MyBase.ToString())

        If (Me.Teams IsNot Nothing) Then
          Dim dMax As Int32 = Me.Teams.Length - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Team (#{0}): {1}{2}", i.ToString(), Me.Teams(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace
