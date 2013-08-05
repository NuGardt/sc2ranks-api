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
  Public Class Sc2RanksCharacterTeamListElement
    '{
    '  "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '  "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '  "url": "http://www.sc2ranks.com/character/us/4161722/frozz",
    '  "achievement_points": 0,
    '  "updated_at": 1359844061,
    '  "swarm_levels": {
    '    "zerg": 0,
    '    "protoss": 0,
    '    "terran": 0
    '  },
    '  "region": "us",
    '  "bnet_id": 4161722,
    '  "name": "frozz",
    '  "teams": [
    '    {
    '      "url": "http://www.sc2ranks.com/team/am/11004161722/",
    '      "rank_region": "am",
    '      "expansion": "hots",
    '      "league": "grandmaster",
    '      "last_game_at": 1375396061,
    '      "bracket": 1,
    '      "random": false,
    '      "points": 437,
    '      "wins": 20,
    '      "losses": 33,
    '      "win_ratio": 0.3773,
    '      "division": {
    '        "id": "51fc325d4970cf3cfa000005",
    '        "rank": 199
    '      },
    '      "rankings": {
    '        "world": 1,
    '        "region": 1
    '      }
    '    },
    '    {
    '      "url": "http://www.sc2ranks.com/team/am/11004161722/",
    '      "rank_region": "am",
    '      "expansion": "hots",
    '      "league": "grandmaster",
    '      "last_game_at": 1375396061,
    '      "bracket": 1,
    '      "random": false,
    '      "points": 437,
    '      "wins": 20,
    '      "losses": 33,
    '      "win_ratio": 0.3773,
    '      "division": {
    '        "id": "51fc325d4970cf3cfa000005",
    '        "rank": 199
    '      },
    '      "rankings": {
    '        "world": 1,
    '        "region": 1
    '      }
    '    }
    '  ]
    '}

    Private m_ReplayUrl As String
    Private m_VodUrl As String
    Private m_Url As String
    Private m_AchievementPoints As Int32
    Private m_UpdatedAt As Int64
    Private m_SwarmLevels As Sc2RanksSwarmLevelElement
    Private m_RegionRaw As String
    Private m_BattleNetID As Int64
    Private m_Name As String
    Private m_Clan As Sc2RanksClanElement
    Private m_Teams As Sc2RanksTeamElement()

    Public Sub New()
      Me.m_ReplayUrl = Nothing
      Me.m_VodUrl = Nothing
      Me.m_Url = Nothing
      Me.m_AchievementPoints = Nothing
      Me.m_UpdatedAt = Nothing
      Me.m_SwarmLevels = Nothing
      Me.m_RegionRaw = Nothing
      Me.m_BattleNetID = Nothing
      Me.m_Name = Nothing
      Me.m_Clan = Nothing
      Me.m_Teams = Nothing
    End Sub

    <DataMember(name := "replay_url")>
    Public Property ReplayUrl As String
      Get
        Return Me.m_ReplayUrl
      End Get
      Private Set(ByVal Value As String)
        Me.m_ReplayUrl = Value
      End Set
    End Property

    <DataMember(name := "vod_url")>
    Public Property VodUrl As String
      Get
        Return Me.m_VodUrl
      End Get
      Private Set(ByVal Value As String)
        Me.m_VodUrl = Value
      End Set
    End Property

    <DataMember(name := "url")>
    Public Property Url As String
      Get
        Return Me.m_Url
      End Get
      Private Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(name := "achievement_points")>
    Public Property AchievementPoints As Int32
      Get
        Return Me.m_AchievementPoints
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_AchievementPoints = Value
      End Set
    End Property

    <DataMember(name := "updated_at")>
    Public Property UpdatedAtUnixTime As Int64
      Get
        Return Me.m_UpdatedAt
      End Get
      Private Set(ByVal Value As Int64)
        Me.m_UpdatedAt = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property UpdatedAt As DateTime
      Get
        Return New DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Me.m_UpdatedAt)
      End Get
    End Property

    <DataMember(name := "swarm_levels")>
    Public Property SwarmLevels As Sc2RanksSwarmLevelElement
      Get
        Return Me.m_SwarmLevels
      End Get
      Private Set(ByVal Value As Sc2RanksSwarmLevelElement)
        Me.m_SwarmLevels = Value
      End Set
    End Property

    <DataMember(name := "region")>
    Private Property RegionRaw As String
      Get
        Return Me.m_RegionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RegionRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Region As eSc2RanksRegion
      Get
        Return Enums.RegionBuffer.GetEnum(Me.m_RegionRaw)
      End Get
    End Property

    <DataMember(name := "bnet_id")>
    Public Property BattleNetID As Int64
      Get
        Return Me.m_BattleNetID
      End Get
      Private Set(ByVal Value As Int64)
        Me.m_BattleNetID = Value
      End Set
    End Property

    <DataMember(name := "name")>
    Public Property Name As String
      Get
        Return Me.m_Name
      End Get
      Private Set(ByVal Value As String)
        Me.m_Name = Value
      End Set
    End Property

    <DataMember(name := "clan")>
    Public Property Clan As Sc2RanksClanElement
      Get
        Return Me.m_Clan
      End Get
      Private Set(ByVal Value As Sc2RanksClanElement)
        Me.m_Clan = Value
      End Set
    End Property

    <DataMember(name := "teams")>
    Public Property Teams As Sc2RanksTeamElement()
      Get
        Return Me.m_Teams
      End Get
      Private Set(ByVal Value As Sc2RanksTeamElement())
        Me.m_Teams = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Replay URL: {0}{1}", Me.ReplayUrl, vbCrLf)
        Call .AppendFormat("VoD URL: {0}{1}", Me.VodUrl, vbCrLf)
        Call .AppendFormat("URL: {0}{1}", Me.Url, vbCrLf)
        Call .AppendFormat("Achievement Points: {0}{1}", Me.AchievementPoints.ToString(), vbCrLf)
        Call .AppendFormat("Updated At: {0}{1}", Me.UpdatedAt.ToString(), vbCrLf)
        If (Me.SwarmLevels IsNot Nothing) Then Call .AppendFormat("Swarm Levels: {0}{1}", Me.SwarmLevels.ToString(), vbCrLf)
        Call .AppendFormat("Region: {0}{1}", Me.Region.ToString(), vbCrLf)
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
        Call .AppendFormat("Name: {0}{1}", Me.Name, vbCrLf)
        If (Me.Clan IsNot Nothing) Then Call .AppendFormat("Clan: {0}{1}", Me.Clan.ToString(), vbCrLf)
        Call .AppendFormat("Teams: {0}", vbCrLf)
        If (Me.Teams IsNot Nothing) Then
          Dim dMax As Int32 = Me.Teams.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Team (#{0}): {1}{2}", i.ToString(), Me.Teams(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace