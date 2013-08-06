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
Imports NuGardt.SC2Ranks.Helper
Imports System.Text

Namespace SC2Ranks.API.Result.Element
  <DataContract()>
  Public Class Sc2RanksCharacterExtended
    '{
    '  "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '  "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '  "url": "http://www.sc2ranks.com/character/us/4161722/frozz",
    '  "achievement_points": 0,
    '  "updated_at": 1359755280,
    '  "swarm_levels": {
    '    "zerg": 0,
    '    "protoss": 0,
    '    "terran": 0
    '  },
    '  "region": "us",
    '  "bnet_id": 4161722,
    '  "name": "frozz",
    '  "clan": {
    '    "url": "http://www.sc2ranks.com/clan/us/Monty",
    '    "tag": "Monty"
    '  }
    '}

    Private m_ReplayUrl As String
    Private m_VodUrl As String
    Private m_Url As String
    Private m_AchievementPoints As Int32
    Private m_UpdatedAt As Int64
    Private m_SwarmLevels As Sc2RanksSwarmLevels
    Private m_RegionRaw As String
    Private m_BattleNetID As Int64
    Private m_Name As String
    Private m_Clan As Sc2RanksClanBasic

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Url = Nothing
      Me.m_ReplayUrl = Nothing
      Me.m_VodUrl = Nothing
      Me.m_AchievementPoints = Nothing
      Me.m_UpdatedAt = Nothing
      Me.m_SwarmLevels = Nothing
      Me.m_RegionRaw = Nothing
      Me.m_BattleNetID = Nothing
      Me.m_Name = Nothing
      Me.m_Clan = Nothing
    End Sub

#Region "Properties"

    <DataMember(name := "url")>
    Public Property Url As String
      Get
        Return Me.m_Url
      End Get
      Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(name := "replay_url")>
    Public Property ReplayUrl As String
      Get
        Return Me.m_ReplayUrl
      End Get
      Set(ByVal Value As String)
        Me.m_ReplayUrl = Value
      End Set
    End Property

    <DataMember(name := "vod_url")>
    Public Property VodUrl As String
      Get
        Return Me.m_VodUrl
      End Get
      Set(ByVal Value As String)
        Me.m_VodUrl = Value
      End Set
    End Property

    <DataMember(name := "achievement_points")>
    Public Property AchievementPoints As Int32
      Get
        Return Me.m_AchievementPoints
      End Get
      Set(ByVal Value As Int32)
        Me.m_AchievementPoints = Value
      End Set
    End Property

    <DataMember(name := "updated_at")>
    Public Property UpdatedAtUnixTime As Int64
      Get
        Return Me.m_UpdatedAt
      End Get
      Set(ByVal Value As Int64)
        Me.m_UpdatedAt = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public Property UpdatedAt As DateTime
      Get
        Return New DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Me.m_UpdatedAt)
      End Get
      Set(ByVal Value As DateTime)
        Me.m_UpdatedAt = Convert.ToInt64((Value - (New DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds)
      End Set
    End Property

    <DataMember(name := "swarm_levels")>
    Public Property SwarmLevels As Sc2RanksSwarmLevels
      Get
        Return Me.m_SwarmLevels
      End Get
      Set(ByVal Value As Sc2RanksSwarmLevels)
        Me.m_SwarmLevels = Value
      End Set
    End Property

    <DataMember(name := "region")>
    Protected Property RegionRaw As String
      Get
        Return Me.m_RegionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RegionRaw = Value
      End Set
    End Property

    '''' <summary>
    '''' Returns the region of the character.
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    <IgnoreDataMember()>
    Public ReadOnly Property Region() As eSc2RanksRegion
      Get
        Return Enums.RegionBuffer.GetEnum(Me.m_RegionRaw)
      End Get
    End Property

    <DataMember(name := "bnet_id")>
    Public Property BattleNetID As Int64
      Get
        Return Me.m_BattleNetID
      End Get
      Set(ByVal Value As Int64)
        Me.m_BattleNetID = Value
      End Set
    End Property

    <DataMember(name := "name")>
    Public Property Name As String
      Get
        Return Me.m_Name
      End Get
      Set(ByVal Value As String)
        Me.m_Name = Value
      End Set
    End Property

    <DataMember(name := "clan", EmitDefaultValue := False)>
    Public Property Clan As Sc2RanksClanBasic
      Get
        Return Me.m_Clan
      End Get
      Set(ByVal Value As Sc2RanksClanBasic)
        Me.m_Clan = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("URL: {0}{1}", Me.Url, vbCrLf)
        Call .AppendFormat("Replay URL: {0}{1}", Me.ReplayUrl, vbCrLf)
        Call .AppendFormat("VoD URL: {0}{1}", Me.VodUrl, vbCrLf)
        Call .AppendFormat("Achievement Points: {0}{1}", Me.AchievementPoints.ToString(), vbCrLf)
        Call .AppendFormat("Updated At: {0}{1}", Me.UpdatedAt.ToString(), vbCrLf)
        If (Me.SwarmLevels IsNot Nothing) Then Call .AppendFormat("Swarm Levels: {0}{1}", Me.SwarmLevels.ToString(), vbCrLf)
        Call .AppendFormat("Region: {0}{1}", Me.Region, vbCrLf)
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
        Call .AppendFormat("Name: {0}{1}", Me.Name, vbCrLf)
        If (Me.Clan IsNot Nothing) Then Call .AppendFormat("Clan: {0}{1}", Me.Clan.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace