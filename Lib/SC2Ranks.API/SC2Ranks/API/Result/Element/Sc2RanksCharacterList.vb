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
Imports System.Text

Namespace SC2Ranks.API.Result.Element
  <DataContract()>
  Public Class Sc2RanksCharacterExtendedList
    Implements IList(Of Sc2RanksCharacterExtended)

    '[
    '  {
    '    "error": "invalid",
    '    "bnet_id": "1",
    '    "region": "apple"
    '  },
    '  {
    '    "error": "invalid",
    '    "bnet_id": "gqer3",
    '    "region": "us"
    '  },
    '  {
    '    "replay_url": "http://www.sc2ranks.com/character/us/1234/frozz/replays",
    '    "vod_url": "http://www.sc2ranks.com/character/us/1234/frozz/vods",
    '    "url": "http://www.sc2ranks.com/character/us/1234/frozz",
    '    "achievement_points": 0,
    '    "updated_at": 1359758537,
    '    "swarm_levels": {
    '      "zerg": 0,
    '      "protoss": 0,
    '      "terran": 0
    '    },
    '    "region": "us",
    '    "bnet_id": 1234,
    '    "name": "frozz",
    '    "clan": {
    '      "url": "http://www.sc2ranks.com/clan/us/Monty",
    '      "tag": "Monty"
    '    }
    '  }
    ']

    Private ReadOnly m_List As New List(Of Sc2RanksCharacterExtended)

    Public Sub New()
      Call MyBase.New()

      Me.m_List = New List(Of Sc2RanksCharacterExtended)()
    End Sub

    Public Sub Add(ByVal Item As Sc2RanksCharacterExtended) Implements ICollection(Of Sc2RanksCharacterExtended).Add
      Call Me.m_List.Add(Item)
    End Sub

    Public Sub Clear() Implements ICollection(Of Sc2RanksCharacterExtended).Clear
      Call Me.m_List.Clear()
    End Sub

    Public Function Contains(ByVal Item As Sc2RanksCharacterExtended) As Boolean Implements ICollection(Of Sc2RanksCharacterExtended).Contains
      Return Me.m_List.Contains(Item)
    End Function

    Public Sub CopyTo(ByVal Array() As Sc2RanksCharacterExtended,
                      ByVal ArrayIndex As Int32) Implements ICollection(Of Sc2RanksCharacterExtended).CopyTo
      Call Me.m_List.CopyTo(Array, ArrayIndex)
    End Sub

    Public ReadOnly Property Count As Int32 Implements ICollection(Of Sc2RanksCharacterExtended).Count
      Get
        Return Me.m_List.Count
      End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Sc2RanksCharacterExtended).IsReadOnly
      Get
        Return False
      End Get
    End Property

    Public Function Remove(ByVal Item As Sc2RanksCharacterExtended) As Boolean Implements ICollection(Of Sc2RanksCharacterExtended).Remove
      Return Me.m_List.Remove(Item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of Sc2RanksCharacterExtended) Implements IEnumerable(Of Sc2RanksCharacterExtended).GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Function IndexOf(ByVal Item As Sc2RanksCharacterExtended) As Int32 Implements IList(Of Sc2RanksCharacterExtended).IndexOf
      Return Me.m_List.IndexOf(Item)
    End Function

    Public Sub Insert(ByVal Index As Int32,
                      ByVal Item As Sc2RanksCharacterExtended) Implements IList(Of Sc2RanksCharacterExtended).Insert
      Call Me.m_List.Insert(Index, Item)
    End Sub

    Default Public Property Item(ByVal Index As Int32) As Sc2RanksCharacterExtended Implements IList(Of Sc2RanksCharacterExtended).Item
      Get
        Return Me.m_List.Item(Index)
      End Get
      Set(ByVal Value As Sc2RanksCharacterExtended)
        Me.m_List.Item(Index) = Value
      End Set
    End Property

    Public Sub RemoveAt(ByVal Index As Int32) Implements IList(Of Sc2RanksCharacterExtended).RemoveAt
      Call Me.m_List.RemoveAt(Index)
    End Sub

    Private Function iGetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Dim dMax As Int32 = Me.m_List.Count - 1
        For i As Int32 = 0 To dMax
          Call .AppendFormat("Character (#{0}): {1}{2}", i.ToString(), Me.m_List.Item(i).ToString, vbCrLf)
        Next i
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace