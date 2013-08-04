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

Namespace SC2Ranks.API.Result
  <DataContract()>
  Public Class Sc2RanksDivisionListResult
    Inherits Sc2RanksBaseResult
    Implements IList(Of Sc2RanksDivisionResult)

    '[
    '  {
    '    "id": "51fc38734970cf3f98000006",
    '    "url": "http://www.sc2ranks.com/division/am/192413001/grandmaster",
    '    "name": "Grandmaster",
    '    "rank_region": "am",
    '    "expansion": "hots",
    '    "league": "grandmaster",
    '    "bracket": 1,
    '    "random": false,
    '    "characters": 5,
    '    "avg_points": 50.0,
    '    "avg_wins": 12.5,
    '    "avg_losses": 1.93,
    '    "avg_games": 20.3,
    '    "avg_win_ratio": 0.5913,
    '    "updated_at": 1375480419
    '  }
    ']

    Private ReadOnly m_List As New List(Of Sc2RanksDivisionResult)

    Public Sub New()
      Call MyBase.New()

      Me.m_List = New List(Of Sc2RanksDivisionResult)()
    End Sub

    Public Sub Add(ByVal Item As Sc2RanksDivisionResult) Implements ICollection(Of Sc2RanksDivisionResult).Add
      Call Me.m_List.Add(Item)
    End Sub

    Public Sub Clear() Implements ICollection(Of Sc2RanksDivisionResult).Clear
      Call Me.m_List.Clear()
    End Sub

    Public Function Contains(ByVal Item As Sc2RanksDivisionResult) As Boolean Implements ICollection(Of Sc2RanksDivisionResult).Contains
      Return Me.m_List.Contains(Item)
    End Function

    Public Sub CopyTo(ByVal Array() As Sc2RanksDivisionResult,
                      ByVal ArrayIndex As Integer) Implements ICollection(Of Sc2RanksDivisionResult).CopyTo
      Call Me.m_List.CopyTo(Array, ArrayIndex)
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of Sc2RanksDivisionResult).Count
      Get
        Return Me.m_List.Count
      End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Sc2RanksDivisionResult).IsReadOnly
      Get
        Return False
      End Get
    End Property

    Public Function Remove(ByVal Item As Sc2RanksDivisionResult) As Boolean Implements ICollection(Of Sc2RanksDivisionResult).Remove
      Return Me.m_List.Remove(Item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of Sc2RanksDivisionResult) Implements IEnumerable(Of Sc2RanksDivisionResult).GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Function IndexOf(ByVal Item As Sc2RanksDivisionResult) As Integer Implements IList(Of Sc2RanksDivisionResult).IndexOf
      Return Me.m_List.IndexOf(Item)
    End Function

    Public Sub Insert(ByVal Index As Integer,
                      ByVal Item As Sc2RanksDivisionResult) Implements IList(Of Sc2RanksDivisionResult).Insert
      Call Me.m_List.Insert(Index, Item)
    End Sub

    Default Public Property Item(ByVal Index As Integer) As Sc2RanksDivisionResult Implements IList(Of Sc2RanksDivisionResult).Item
      Get
        Return Me.m_List.Item(Index)
      End Get
      Set(ByVal Value As Sc2RanksDivisionResult)
        Me.m_List.Item(Index) = Value
      End Set
    End Property

    Public Sub RemoveAt(ByVal Index As Integer) Implements IList(Of Sc2RanksDivisionResult).RemoveAt
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
          Call .AppendFormat("Division (#{0}): {1}{2}", i.ToString(), Me.m_List.Item(i).ToString, vbCrLf)
        Next i
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace