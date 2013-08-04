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
  Public Class Sc2RanksCustomDivisionManageListResult
    Inherits Sc2RanksBaseResult
    Implements IList(Of Sc2RanksCustomDivisionManageElement)

    '[
    '  {
    '    "error": "invalid",
    '    "bnet_id": "1",
    '    "region": "invalid"
    '  },
    '  {
    '    "error": "invalid",
    '    "bnet_id": "invalid",
    '    "region": "us"
    '  },
    '  {
    '    "error": "added",
    '    "bnet_id": 4161722,
    '    "region": "us"
    '  },
    '  {
    '    "error": "already_added",
    '    "bnet_id": 1234,
    '    "region": "us"
    '  }
    ']

    Private ReadOnly m_List As New List(Of Sc2RanksCustomDivisionManageElement)

    Public Sub New()
      Call MyBase.New()

      Me.m_List = New List(Of Sc2RanksCustomDivisionManageElement)()
    End Sub

    Public Sub Add(ByVal Item As Sc2RanksCustomDivisionManageElement) Implements ICollection(Of Sc2RanksCustomDivisionManageElement).Add
      Call Me.m_List.Add(Item)
    End Sub

    Public Sub Clear() Implements ICollection(Of Sc2RanksCustomDivisionManageElement).Clear
      Call Me.m_List.Clear()
    End Sub

    Public Function Contains(ByVal Item As Sc2RanksCustomDivisionManageElement) As Boolean Implements ICollection(Of Sc2RanksCustomDivisionManageElement).Contains
      Return Me.m_List.Contains(Item)
    End Function

    Public Sub CopyTo(ByVal Array() As Sc2RanksCustomDivisionManageElement,
                      ByVal ArrayIndex As Integer) Implements ICollection(Of Sc2RanksCustomDivisionManageElement).CopyTo
      Call Me.m_List.CopyTo(Array, ArrayIndex)
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of Sc2RanksCustomDivisionManageElement).Count
      Get
        Return Me.m_List.Count
      End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Sc2RanksCustomDivisionManageElement).IsReadOnly
      Get
        Return False
      End Get
    End Property

    Public Function Remove(ByVal Item As Sc2RanksCustomDivisionManageElement) As Boolean Implements ICollection(Of Sc2RanksCustomDivisionManageElement).Remove
      Return Me.m_List.Remove(Item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of Sc2RanksCustomDivisionManageElement) Implements IEnumerable(Of Sc2RanksCustomDivisionManageElement).GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Function IndexOf(ByVal Item As Sc2RanksCustomDivisionManageElement) As Integer Implements IList(Of Sc2RanksCustomDivisionManageElement).IndexOf
      Return Me.m_List.IndexOf(Item)
    End Function

    Public Sub Insert(ByVal Index As Integer,
                      ByVal Item As Sc2RanksCustomDivisionManageElement) Implements IList(Of Sc2RanksCustomDivisionManageElement).Insert
      Call Me.m_List.Insert(Index, Item)
    End Sub

    Default Public Property Item(ByVal Index As Integer) As Sc2RanksCustomDivisionManageElement Implements IList(Of Sc2RanksCustomDivisionManageElement).Item
      Get
        Return Me.m_List.Item(Index)
      End Get
      Set(ByVal Value As Sc2RanksCustomDivisionManageElement)
        Me.m_List.Item(Index) = Value
      End Set
    End Property

    Public Sub RemoveAt(ByVal Index As Integer) Implements IList(Of Sc2RanksCustomDivisionManageElement).RemoveAt
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
            Call .AppendFormat("Custom Division Manage (#{0}): {1}{2}", i.ToString(), Me.m_List.Item(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace