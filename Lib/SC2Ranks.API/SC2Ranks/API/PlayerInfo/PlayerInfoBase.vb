Imports System.Runtime.Serialization
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API.PlayerInfo
''' <summary>
'''   Class containing basic player information.
''' </summary>
''' <remarks></remarks>
  <DataContract()>
  Public Class PlayerInfoBase
    Protected m_AchievementPoints As Integer
    Protected m_BattleNetID As Int32
    Protected m_CharacterCode As Nullable(Of Int16)
    Protected m_CharacterName As String
    Protected m_ID As Integer
    Protected m_Error As String
    Protected m_Portrait As PlayerInfoPortrait
    Protected m_RegionRaw As String
    Protected m_Tag As String
    Protected m_UpdatedAtRaw As String
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_AchievementPoints = Nothing
      Me.m_BattleNetID = Nothing
      Me.m_CharacterCode = Nothing
      Me.m_CharacterName = Nothing
      Me.m_ID = Nothing
      Me.m_Error = Nothing
      Me.m_Portrait = Nothing
      Me.m_RegionRaw = Nothing
      Me.m_Tag = Nothing
      Me.m_UpdatedAtRaw = Nothing
    End Sub

    Private Sub New(ByVal CharacterName As String,
                    ByVal Region As eRegion,
                    ByVal CharacterCode As Int16)
      Me.m_CharacterName = CharacterName
      Me.m_RegionRaw = Enums.RegionBuffer.GetValue(Region)
      Me.m_CharacterCode = CharacterCode
    End Sub

    Private Sub New(ByVal CharacterName As String,
                    ByVal Region As eRegion,
                    ByVal BattleNetID As Int32)
      Me.m_CharacterName = CharacterName
      Me.m_RegionRaw = Enums.RegionBuffer.GetValue(Region)
      Me.m_BattleNetID = BattleNetID
      Me.m_CharacterCode = Nothing
    End Sub
    
    ''' <summary>
    '''   Creates a search info basesd on character code. Not reliable when searching as character codes may not be set or are incorrect.
    ''' </summary>
    ''' <param name="Region">Region of the player. All may not be specified.</param>
    ''' <param name="CharacterName">Name of the character.</param>
    ''' <param name="CharacterCode">Character code. Not reliable when searching as character codes may not be set or are incorrect.</param>
    ''' <returns></returns>
    ''' <remarks>Not reliable when searching as character codes may not be set or are incorrect.</remarks>
    Public Shared Function CreateByCharacterCode(ByVal Region As eRegion,
                                                 ByVal CharacterName As String,
                                                 ByVal CharacterCode As Int16) As PlayerInfoBase
      Return New PlayerInfoBase(CharacterName := CharacterName, Region := Region, CharacterCode := CharacterCode)
    End Function
    
    ''' <summary>
    '''   Created a search info based on Battle.net ID.
    ''' </summary>
    ''' <param name="Region">Region of the player. All may not be specified.</param>
    ''' <param name="CharacterName">Name of the character.</param>
    ''' <param name="BattleNetID">Battle.net Unique Identifier.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateByBattleNetID(ByVal Region As eRegion,
                                               ByVal CharacterName As String,
                                               ByVal BattleNetID As Integer) As PlayerInfoBase
      Return New PlayerInfoBase(CharacterName := CharacterName, Region := Region, BattleNetID := BattleNetID)
    End Function

#Region "Properties"
    
    ''' <summary>
    '''   Achievement Points
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "achievement_points")>
    Public Property AchievementPoints() As Integer
      Get
        Return Me.m_AchievementPoints
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_AchievementPoints = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the Battle.net Identifier.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "bnet_id")>
    Public Property BattleNetID() As Int32
      Get
        Return Me.m_BattleNetID
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_BattleNetID = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the character code. Value maybe incorrect.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "character_code")>
    Public Property CharacterCode() As Nullable(Of Int16)
      Get
        Return Me.m_CharacterCode
      End Get
      Private Set(ByVal Value As Nullable(Of Int16))
        Me.m_CharacterCode = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the character name.
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
    
    ''' <summary>
    '''   Returns the SC2Ranks identifier.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "id", EmitDefaultValue := False)>
    Public Property ID() As Int32
      Get
        Return Me.m_ID
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_ID = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the error message if an error has occured.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "error", EmitDefaultValue := False)>
    Public Property [Error]() As String
      Get
        Return Me.m_Error
      End Get
      Private Set(ByVal Value As String)
        Me.m_Error = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns <c>True</c> if an error has occured, otherwise <c>False</c>.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property HasError As Boolean
      Get
        Return (Not String.IsNullOrEmpty(Me.m_Error))
      End Get
    End Property
    
    ''' <summary>
    '''   Portrait
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "portrait", EmitDefaultValue := False)>
    Public Property Portrait() As PlayerInfoPortrait
      Get
        Return Me.m_Portrait
      End Get
      Private Set(ByVal Value As PlayerInfoPortrait)
        Me.m_Portrait = Value
      End Set
    End Property

    <DataMember(Name := "region")>
    Protected Property RegionRaw() As String
      Get
        Return Me.m_RegionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RegionRaw = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the region of the player.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Region() As eRegion
      Get
        Return Enums.RegionBuffer.GetEnum(Me.RegionRaw)
      End Get
    End Property
    
    ''' <summary>
    '''   Returns the clan tag.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "tag", EmitDefaultValue := False)>
    Public Property Tag() As String
      Get
        Return Me.m_Tag
      End Get
      Private Set(ByVal Value As String)
        Me.m_Tag = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the character name with clan tag ("[Clan] Name")
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CharacterNameWithTag As String
      Get
        If String.IsNullOrEmpty(Me.m_Tag) Then
          Return Me.m_CharacterName
        Else
          Return String.Format("[{0}] {1}", Me.m_Tag, Me.m_CharacterName)
        End If
      End Get
    End Property

    <DataMember(Name := "updated_at")>
    Protected Property UpdatedAtRaw() As String
      Get
        Return Me.m_UpdatedAtRaw
      End Get
      Set(ByVal Value As String)
        Me.m_UpdatedAtRaw = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Updated At
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UpdatedAt() As DateTimeOffset
      Get
        If String.IsNullOrEmpty(Me.UpdatedAtRaw) Then
          Return Nothing
        Else
          Return DateTimeOffset.Parse(Me.UpdatedAtRaw)
        End If
      End Get
    End Property

#End Region
  End Class
End Namespace