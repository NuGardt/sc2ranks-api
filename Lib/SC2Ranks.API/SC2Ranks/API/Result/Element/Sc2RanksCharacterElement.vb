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
  Public Class Sc2RanksCharacterElement
    '{
    '  "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '  "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '  "url": "http://www.sc2ranks.com/character/us/4161722/frozz/hots/1v1",
    '  "race": "zerg",
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
    Private m_RaceRaw As String
    Private m_RegionRaw As String
    Private m_BattleNetID As Int64
    Private m_Name As String
    Private m_Clan As Sc2RanksClanElement

    Public Sub New()
      Me.m_ReplayUrl = Nothing
      Me.m_VodUrl = Nothing
      Me.m_Url = Nothing
      Me.m_RaceRaw = Nothing
      Me.m_RegionRaw = Nothing
      Me.m_BattleNetID = Nothing
      Me.m_Name = Nothing
      Me.m_Clan = Nothing
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

    <DataMember(name := "race")>
    Private Property RaceRaw As String
      Get
        Return Me.m_RaceRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RaceRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Race As eSc2RanksRace
      Get
        Return Enums.RacesBuffer.GetEnum(Me.m_RaceRaw)
      End Get
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

    <DataMember(name := "clan", EmitDefaultValue := False)>
    Public Property Clan As Sc2RanksClanElement
      Get
        Return Me.m_Clan
      End Get
      Private Set(ByVal Value As Sc2RanksClanElement)
        Me.m_Clan = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Replay URL: {0}{1}", Me.ReplayUrl, vbCrLf)
        Call .AppendFormat("VoD URL: {0}{1}", Me.VodUrl, vbCrLf)
        Call .AppendFormat("URL: {0}{1}", Me.Url, vbCrLf)
        Call .AppendFormat("Race: {0}{1}", Me.Race.ToString(), vbCrLf)
        Call .AppendFormat("Region: {0}{1}", Me.Region.ToString(), vbCrLf)
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
        Call .AppendFormat("Name: {0}{1}", Me.Name, vbCrLf)
        If (Me.Clan IsNot Nothing) Then Call .AppendFormat("Clan: {0}{1}", Me.Clan.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace