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
  Public Class Sc2RanksClanExtendedWithCharacters
    Inherits Sc2RanksClanExtended
    '{
    '  "url": "http://www.sc2ranks.com/clan/am/Monty",
    '  "region": "am",
    '  "tag": "Monty",
    '  "members": 1,
    '  "updated_at": 1370210805,
    '  "scores": {
    '    "top": 11,
    '    "avg": 21,
    '    "sum": 31
    '  },
    '  "characters": [
    '    {
    '      "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '      "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '      "url": "http://www.sc2ranks.com/character/us/4161722/frozz",
    '      "achievement_points": 0,
    '      "updated_at": 1359842805,
    '      "swarm_levels": {
    '        "zerg": 0,
    '        "protoss": 0,
    '        "terran": 0
    '      },
    '      "region": "us",
    '      "bnet_id": 4161722,
    '      "name": "frozz",
    '      "clan": {
    '        "url": "http://www.sc2ranks.com/clan/us/Monty",
    '        "tag": "Monty"
    '      }
    '    },
    '    {
    '      "replay_url": "http://www.sc2ranks.com/character/us/1234/frozz/replays",
    '      "vod_url": "http://www.sc2ranks.com/character/us/1234/frozz/vods",
    '      "url": "http://www.sc2ranks.com/character/us/1234/frozz",
    '      "achievement_points": 0,
    '      "updated_at": 1359842805,
    '      "swarm_levels": {
    '        "zerg": 0,
    '        "protoss": 0,
    '        "terran": 0
    '      },
    '      "region": "us",
    '      "bnet_id": 1234,
    '      "name": "frozz",
    '      "clan": {
    '        "url": "http://www.sc2ranks.com/clan/us/Monty",
    '        "tag": "Monty"
    '      }
    '    }
    '  ],
    '  "pagination": {
    '    "limit": 10,
    '    "offset": 0,
    '    "total": 2
    '  }
    '}

    Private m_Characters() As Sc2RanksCharacterExtended
    Private m_Pagination As Sc2RanksPagination

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Characters = Nothing
      Me.m_Pagination = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "characters")>
    Public Property Characters As Sc2RanksCharacterExtended()
      Get
        Return Me.m_Characters
      End Get
      Private Set(ByVal Value As Sc2RanksCharacterExtended())
        Me.m_Characters = Value
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

        Call .AppendFormat("Characters: {0}", vbCrLf)
        If Me.Characters IsNot Nothing Then
          Dim dMax As Int32 = Me.Characters.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Character (#{0}): {1}{2}", i.ToString(), Me.Characters(i).ToString, vbCrLf)
          Next i
        End If
        Call .AppendFormat("Pagination: {0}{1}", Me.Pagination, vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace