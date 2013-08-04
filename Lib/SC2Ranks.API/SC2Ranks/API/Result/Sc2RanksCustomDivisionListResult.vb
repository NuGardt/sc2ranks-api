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
  Public Class Sc2RanksCustomDivisionListResult
    Inherits Sc2RanksBaseResult
    Implements IList(Of Sc2RanksCustomDivisionResult)

    '[
    '  {
    '    "id": "51fc3c3b4970cf407e000004",
    '    "url": "http://www.sc2ranks.com/cdiv/51fc3c3b4970cf407e000004/foo",
    '    "name": "foo",
    '    "description": "bar",
    '    "members": 2,
    '    "public": false
    '  }
    ']

    Private ReadOnly m_List As New List(Of Sc2RanksCustomDivisionResult)

    Public Sub New()
      Call MyBase.New()

      Me.m_List = New List(Of Sc2RanksCustomDivisionResult)()
    End Sub

    Public Sub Add(ByVal Item As Sc2RanksCustomDivisionResult) Implements ICollection(Of Sc2RanksCustomDivisionResult).Add
      Call Me.m_List.Add(Item)
    End Sub

    Public Sub Clear() Implements ICollection(Of Sc2RanksCustomDivisionResult).Clear
      Call Me.m_List.Clear()
    End Sub

    Public Function Contains(ByVal Item As Sc2RanksCustomDivisionResult) As Boolean Implements ICollection(Of Sc2RanksCustomDivisionResult).Contains
      Return Me.m_List.Contains(Item)
    End Function

    Public Sub CopyTo(ByVal Array() As Sc2RanksCustomDivisionResult,
                      ByVal ArrayIndex As Integer) Implements ICollection(Of Sc2RanksCustomDivisionResult).CopyTo
      Call Me.m_List.CopyTo(Array, ArrayIndex)
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of Sc2RanksCustomDivisionResult).Count
      Get
        Return Me.m_List.Count
      End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Sc2RanksCustomDivisionResult).IsReadOnly
      Get
        Return False
      End Get
    End Property

    Public Function Remove(ByVal Item As Sc2RanksCustomDivisionResult) As Boolean Implements ICollection(Of Sc2RanksCustomDivisionResult).Remove
      Return Me.m_List.Remove(Item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of Sc2RanksCustomDivisionResult) Implements IEnumerable(Of Sc2RanksCustomDivisionResult).GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Function IndexOf(ByVal Item As Sc2RanksCustomDivisionResult) As Integer Implements IList(Of Sc2RanksCustomDivisionResult).IndexOf
      Return Me.m_List.IndexOf(Item)
    End Function

    Public Sub Insert(ByVal Index As Integer,
                      ByVal Item As Sc2RanksCustomDivisionResult) Implements IList(Of Sc2RanksCustomDivisionResult).Insert
      Call Me.m_List.Insert(Index, Item)
    End Sub

    Default Public Property Item(ByVal Index As Integer) As Sc2RanksCustomDivisionResult Implements IList(Of Sc2RanksCustomDivisionResult).Item
      Get
        Return Me.m_List.Item(Index)
      End Get
      Set(ByVal Value As Sc2RanksCustomDivisionResult)
        Me.m_List.Item(Index) = Value
      End Set
    End Property

    Public Sub RemoveAt(ByVal Index As Integer) Implements IList(Of Sc2RanksCustomDivisionResult).RemoveAt
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
            Call .AppendFormat("Custom Division (#{0}): {1}{2}", i.ToString(), Me.m_List.Item(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace