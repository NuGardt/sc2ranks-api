Imports System.Runtime.Serialization
Imports System.Text

Namespace SC2Ranks.API.Messages
''' <summary>
'''   Class conatining information of found players.
''' </summary>
''' <remarks></remarks>
  <DataContract()>
  Public Class SearchPlayer
    Private m_BattleNetID As Integer
    Private m_CharacterName As String
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Private Sub New()
      Me.m_BattleNetID = Nothing
      Me.m_CharacterName = Nothing
    End Sub

#Region "Properties"
    
    ''' <summary>
    '''   Returns the Battle.net identifier.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "bnet_id")>
    Protected Property BattleNetID() As Int32
      Get
        Return Me.m_BattleNetID
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_BattleNetID = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Return the character name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "name")>
    Public Property CharacterName() As String
      Get
        Return Me.m_CharacterName
      End Get
      Private Set(ByVal Value As String)
        Me.m_CharacterName = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Character Name: {0}{1}", Me.CharacterName, vbCrLf)
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace
