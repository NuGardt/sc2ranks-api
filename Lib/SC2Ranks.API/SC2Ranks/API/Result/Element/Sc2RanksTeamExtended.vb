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
  Public Class Sc2RanksTeamExtended
    Inherits Sc2RanksTeamBasic

    '{
    '  "url": "http://www.sc2ranks.com/team/am/11004161722/frozz",
    '  "rank_region": "am",
    '  "expansion": "hots",
    '  "league": "grandmaster",
    '  "last_game_at": 1375307693,
    '  "bracket": 1,
    '  "random": false,
    '  "points": 437,
    '  "wins": 20,
    '  "losses": 33,
    '  "win_ratio": 0.3773,
    '  "division": {
    '    "id": "51fad92d4970cf8401000006",
    '    "rank": 199
    '  },
    '  "rankings": {
    '    "world": 1,
    '    "region": 1
    '  },
    '  "characters": [
    '    {
    '      "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '      "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '      "url": "http://www.sc2ranks.com/character/us/4161722/frozz/hots/1v1",
    '      "race": "zerg",
    '      "region": "us",
    '      "bnet_id": 4161722,
    '      "name": "frozz",
    '      "clan": {
    '        "url": "http://www.sc2ranks.com/clan/us/Monty",
    '        "tag": "Monty"
    '      }
    '  }
    '},

    Private m_Characters() As Sc2RanksCharacterBasic

    Public Sub New()
      Me.m_Characters = Nothing
    End Sub

    <DataMember(name := "characters")>
    Public Property Characters As Sc2RanksCharacterBasic()
      Get
        Return Me.m_Characters
      End Get
      Private Set(ByVal Value As Sc2RanksCharacterBasic())
        Me.m_Characters = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .Append(MyBase.ToString())
        Call .AppendFormat("Characters: {0}", vbCrLf)
        If (Me.m_Characters IsNot Nothing) Then
          Dim dMax As Int32 = Me.m_Characters.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Character (#{0}): {1}{2}", i.ToString(), Me.m_Characters(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace