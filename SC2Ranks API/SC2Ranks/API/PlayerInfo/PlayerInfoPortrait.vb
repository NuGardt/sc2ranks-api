Imports System.Runtime.Serialization
Imports System.Text

Namespace SC2Ranks.API.PlayerInfo
  ''' <summary>
  ''' Class containing portrait information.
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract(Name := "portrait")>
  Public Class PlayerInfoPortrait
    Private m_IconID As Int16
    Private m_Row As Int16
    Private m_Column As Int16

    ''' <summary>
    ''' Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Private Sub New()
      Me.m_IconID = Nothing
      Me.m_Row = Nothing
      Me.m_Column = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "icon_id")>
    Public Property IconID() As Int16
      Get
        Return Me.m_IconID
      End Get
      Private Set(ByVal Value As Int16)
        Me.m_IconID = Value
      End Set
    End Property

    <DataMember(Name := "row")>
    Public Property Row() As Int16
      Get
        Return Me.m_Row
      End Get
      Private Set(ByVal Value As Int16)
        Me.m_Row = Value
      End Set
    End Property

    <DataMember(Name := "column")>
    Public Property Column() As Int16
      Get
        Return Me.m_Column
      End Get
      Private Set(ByVal Value As Int16)
        Me.m_Column = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Icon ID: {0}{1}", Me.IconID.ToString(), vbCrLf)
        Call .AppendFormat("Row: {0}{1}", Me.Row.ToString(), vbCrLf)
        Call .AppendFormat("Column: {0}{1}", Me.Column.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace