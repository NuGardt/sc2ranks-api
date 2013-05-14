' NuGardt SC2Ranks API
' Copyright (C) 2011-2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Imports System.Runtime.Serialization
Imports com.NuGardt.SC2Ranks.API.Result.Element
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API.Result
''' <summary>
'''   Class containing basic player information.
''' </summary>
''' <remarks></remarks>
  <DataContract()>
  Public Class GetBasePlayerResult
    Inherits BaseResult

    Protected m_AchievementPoints As Integer
    Protected m_BattleNetID As Int32
    Protected m_CharacterCode As Nullable(Of Int16)
    Protected m_CharacterName As String
    Protected m_ID As Integer
    Protected m_Portrait As PortraitElement
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
    '''   Portrait
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "portrait", EmitDefaultValue := False)>
    Public Property Portrait() As PortraitElement
      Get
        Return Me.m_Portrait
      End Get
      Private Set(ByVal Value As PortraitElement)
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