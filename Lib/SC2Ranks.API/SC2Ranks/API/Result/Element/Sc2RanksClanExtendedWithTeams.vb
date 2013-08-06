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
Imports System.Text

Namespace SC2Ranks.API.Result.Element
  <DataContract()>
  Public Class Sc2RanksClanExtendedWithTeams
    Inherits Sc2RanksClanExtended
    '{
    '  "url": "http://www.sc2ranks.com/clan/am/Monty",
    '  "region": "am",
    '  "tag": "Monty",
    '  "members": 1,
    '  "updated_at": 1370210433,
    '  "scores": {
    '    "top": 11,
    '    "avg": 21,
    '    "sum": 31
    '  },
    '  "teams": [
    '    {
    '      "url": "http://www.sc2ranks.com/team/am/11004161722/frozz",
    '      "rank_region": "am",
    '      "expansion": "hots",
    '      "league": "grandmaster",
    '      "last_game_at": 1375394433,
    '      "bracket": 1,
    '      "random": false,
    '      "points": 437,
    '      "wins": 20,
    '      "losses": 33,
    '      "win_ratio": 0.3773,
    '      "division": {
    '        "id": "51fc2c014970cf3b1c000006",
    '        "rank": 199
    '      },
    '      "rankings": {
    '        "world": 1,
    '        "region": 1
    '      },
    '      "characters": [
    '        {
    '          "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '          "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '          "url": "http://www.sc2ranks.com/character/us/4161722/frozz/hots/1v1",
    '          "race": "zerg",
    '          "region": "us",
    '          "bnet_id": 4161722,
    '          "name": "frozz",
    '          "clan": {
    '            "url": "http://www.sc2ranks.com/clan/us/Monty",
    '            "tag": "Monty"
    '          }
    '        }
    '      ]
    '    },
    '    {
    '      "url": "http://www.sc2ranks.com/team/am/11004161722/frozz",
    '      "rank_region": "am",
    '      "expansion": "hots",
    '      "league": "grandmaster",
    '      "last_game_at": 1375394433,
    '      "bracket": 1,
    '      "random": false,
    '      "points": 437,
    '      "wins": 20,
    '      "losses": 33,
    '      "win_ratio": 0.3773,
    '      "division": {
    '        "id": "51fc2c014970cf3b1c000006",
    '        "rank": 199
    '      },
    '      "rankings": {
    '        "world": 1,
    '        "region": 1
    '      },
    '      "characters": [
    '        {
    '          "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '          "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '          "url": "http://www.sc2ranks.com/character/us/4161722/frozz/hots/1v1",
    '          "race": "zerg",
    '          "region": "us",
    '          "bnet_id": 4161722,
    '          "name": "frozz",
    '          "clan": {
    '            "url": "http://www.sc2ranks.com/clan/us/Monty",
    '            "tag": "Monty"
    '          }
    '        }
    '      ]
    '    }
    '  ],
    '  "pagination": {
    '    "limit": 10,
    '    "offset": 0,
    '    "total": 2
    '  }
    '}

    Private m_Teams() As Sc2RanksTeamExtended
    Private m_Pagination As Sc2RanksPagination

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Teams = Nothing
      Me.m_Pagination = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "teams")>
    Public Property Teams As Sc2RanksTeamExtended()
      Get
        Return Me.m_Teams
      End Get
      Private Set(ByVal Value As Sc2RanksTeamExtended())
        Me.m_Teams = Value
      End Set
    End Property

    <DataMember(Name := "pagination")>
    Public Property Pagination As Sc2RanksPagination
      Get
        Return Me.m_Pagination
      End Get
      Private Set(ByVal Value As Sc2RanksPagination)
        Me.m_Pagination = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat(MyBase.ToString())

        Call .AppendFormat("Teams: {0}", vbCrLf)
        If Me.Teams IsNot Nothing Then
          Dim dMax As Int32 = Me.Teams.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Team (#{0}): {1}{2}", i.ToString(), Me.Teams(i).ToString, vbCrLf)
          Next i
        End If
        Call .AppendFormat("Pagination: {0}{1}", Me.Pagination, vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace