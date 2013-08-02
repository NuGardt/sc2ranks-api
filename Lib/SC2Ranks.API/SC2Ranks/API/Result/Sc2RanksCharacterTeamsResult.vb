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
Imports System.Collections.Generic
Imports System.Collections
Imports NuGardt.SC2Ranks.API.Result.Element
Imports System.Text

Namespace SC2Ranks.API.Result
  <DataContract()>
  Public Class Sc2RanksCharacterTeamsResult
    Inherits Sc2RanksBaseResult
    Implements IList(Of Sc2RanksCharacterTeamElement)

    '[
    '  {
    '    "url": "http://www.sc2ranks.com/team/am/11004161722/frozz",
    '    "rank_region": "am",
    '    "expansion": "hots",
    '    "league": "grandmaster",
    '    "last_game_at": 1375307693,
    '    "bracket": 1,
    '    "random": false,
    '    "points": 437,
    '    "wins": 20,
    '    "losses": 33,
    '    "win_ratio": 0.3773,
    '    "division": {
    '      "id": "51fad92d4970cf8401000006",
    '      "rank": 199
    '    },
    '    "rankings": {
    '      "world": 1,
    '      "region": 1
    '    },
    '    "characters": [
    '      {
    '        "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '        "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '        "url": "http://www.sc2ranks.com/character/us/4161722/frozz/hots/1v1",
    '        "race": "zerg",
    '        "region": "us",
    '        "bnet_id": 4161722,
    '        "name": "frozz",
    '        "clan": {
    '          "url": "http://www.sc2ranks.com/clan/us/Monty",
    '          "tag": "Monty"
    '        }
    '      }
    '  },
    '    ]
    '  {
    '    "url": "http://www.sc2ranks.com/team/am/11004161722/frozz",
    '    "rank_region": "am",
    '    "expansion": "hots",
    '    "league": "grandmaster",
    '    "last_game_at": 1375307693,
    '    "bracket": 1,
    '    "random": false,
    '    "points": 437,
    '    "wins": 20,
    '    "losses": 33,
    '    "win_ratio": 0.3773,
    '    "division": {
    '      "id": "51fad92d4970cf8401000006",
    '      "rank": 199
    '    },
    '    "rankings": {
    '      "world": 1,
    '      "region": 1
    '    },
    '    "characters": [
    '      {
    '        "replay_url": "http://www.sc2ranks.com/character/us/4161722/frozz/replays",
    '        "vod_url": "http://www.sc2ranks.com/character/us/4161722/frozz/vods",
    '        "url": "http://www.sc2ranks.com/character/us/4161722/frozz/hots/1v1",
    '        "race": "zerg",
    '        "region": "us",
    '        "bnet_id": 4161722,
    '        "name": "frozz",
    '        "clan": {
    '          "url": "http://www.sc2ranks.com/clan/us/Monty",
    '          "tag": "Monty"
    '        }
    '      }
    '    ]
    '  }
    ']

    Private ReadOnly m_List As New List(Of Sc2RanksCharacterTeamElement)

    Public Sub New()
      Call MyBase.New()

      Me.m_List = New List(Of Sc2RanksCharacterTeamElement)()
    End Sub

    Public Sub Add(ByVal Item As Sc2RanksCharacterTeamElement) Implements ICollection(Of Sc2RanksCharacterTeamElement).Add
      Call Me.m_List.Add(Item)
    End Sub

    Public Sub Clear() Implements ICollection(Of Sc2RanksCharacterTeamElement).Clear
      Call Me.m_List.Clear()
    End Sub

    Public Function Contains(ByVal Item As Sc2RanksCharacterTeamElement) As Boolean Implements ICollection(Of Sc2RanksCharacterTeamElement).Contains
      Return Me.m_List.Contains(Item)
    End Function

    Public Sub CopyTo(ByVal Array() As Sc2RanksCharacterTeamElement,
                      ByVal ArrayIndex As Integer) Implements ICollection(Of Sc2RanksCharacterTeamElement).CopyTo
      Call Me.m_List.CopyTo(Array, ArrayIndex)
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of Sc2RanksCharacterTeamElement).Count
      Get
        Return Me.m_List.Count
      End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Sc2RanksCharacterTeamElement).IsReadOnly
      Get
        Return False
      End Get
    End Property

    Public Function Remove(ByVal Item As Sc2RanksCharacterTeamElement) As Boolean Implements ICollection(Of Sc2RanksCharacterTeamElement).Remove
      Return Me.m_List.Remove(Item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of Sc2RanksCharacterTeamElement) Implements IEnumerable(Of Sc2RanksCharacterTeamElement).GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Function IndexOf(ByVal Item As Sc2RanksCharacterTeamElement) As Integer Implements IList(Of Sc2RanksCharacterTeamElement).IndexOf
      Return Me.m_List.IndexOf(Item)
    End Function

    Public Sub Insert(ByVal Index As Integer,
                      ByVal Item As Sc2RanksCharacterTeamElement) Implements IList(Of Sc2RanksCharacterTeamElement).Insert
      Call Me.m_List.Insert(Index, Item)
    End Sub

    Default Public Property Item(ByVal Index As Integer) As Sc2RanksCharacterTeamElement Implements IList(Of Sc2RanksCharacterTeamElement).Item
      Get
        Return Me.m_List.Item(Index)
      End Get
      Set(ByVal Value As Sc2RanksCharacterTeamElement)
        Me.m_List.Item(Index) = Value
      End Set
    End Property

    Public Sub RemoveAt(ByVal Index As Integer) Implements IList(Of Sc2RanksCharacterTeamElement).RemoveAt
      Call Me.m_List.RemoveAt(Index)
    End Sub

    Private Function iGetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        If (Me.m_List IsNot Nothing) Then
          Dim dMax As Int32 = Me.m_List.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Character (#{0}): {1}{2}", i.ToString(), Me.m_List.Item(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace