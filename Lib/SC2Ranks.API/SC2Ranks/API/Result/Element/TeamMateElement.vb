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
Imports com.NuGardt.SC2Ranks.Helper
Imports System.Text

Namespace SC2Ranks.API.Result.Element
''' <summary>
'''   Class containing information for a team mate.
''' </summary>
''' <remarks></remarks>
  <DataContract(Name := "member")>
  Public Class TeamMateElement
    Protected m_BattleNetID As Int32
    Protected m_CharacterCode As Nullable(Of Int16)
    Protected m_CharacterName As String
    Private m_FavouriteRaceRaw As String
    Protected m_RegionRaw As String
    Private m_Tag As String
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_BattleNetID = Nothing
      Me.m_CharacterCode = Nothing
      Me.m_CharacterName = Nothing
      Me.m_FavouriteRaceRaw = Nothing
      Me.m_RegionRaw = Nothing
      Me.m_Tag = Nothing
    End Sub

#Region "Properties"
    
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

    <DataMember(Name := "fav_race")>
    Protected Property FavouriteRaceRaw() As String
      Get
        Return Me.m_FavouriteRaceRaw
      End Get
      Set(ByVal Value As String)
        Me.m_FavouriteRaceRaw = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Favourite Race.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FavouriteRace() As eRace
      Get
        Return Enums.RacesBuffer.GetEnum(FavouriteRaceRaw)
      End Get
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

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
        Call .AppendFormat("Character Code: {0}{1}", Me.CharacterCode.ToString(), vbCrLf)
        Call .AppendFormat("Character Name: {0}{1}", Me.CharacterName, vbCrLf)
        Call .AppendFormat("Favourite Race: {0}{1}", Me.FavouriteRace, vbCrLf)
        Call .AppendFormat("Tag: {0}{1}", Me.Tag, vbCrLf)
        Call .AppendFormat("Region: {0}{1}", Me.Region.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace