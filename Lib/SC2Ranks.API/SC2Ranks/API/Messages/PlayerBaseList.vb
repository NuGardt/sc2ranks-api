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

Namespace SC2Ranks.API.Messages
''' <summary>
'''   Class containing basic player information.
''' </summary>
''' <remarks></remarks>
  <CollectionDataContract()>
  Public Class PlayerBaseList
    Inherits Sc2RanksResult
    Implements IList(Of PlayerBase)

    Private ReadOnly m_List As New List(Of PlayerBase)

    Public Sub New()
      Call MyBase.New()

      Me.m_List = New List(Of PlayerBase)()
    End Sub

    Public Sub Add(ByVal Item As PlayerBase) Implements ICollection(Of PlayerBase).Add
      Call Me.m_List.Add(item)
    End Sub

    Public Sub Clear() Implements ICollection(Of PlayerBase).Clear
      Call Me.m_List.Clear()
    End Sub

    Public Function Contains(ByVal Item As PlayerBase) As Boolean Implements ICollection(Of PlayerBase).Contains
      Return Me.m_List.Contains(Item)
    End Function

    Public Sub CopyTo(ByVal Array() As PlayerBase,
                      ByVal ArrayIndex As Integer) Implements ICollection(Of PlayerBase).CopyTo
      Call Me.m_List.CopyTo(Array, ArrayIndex)
    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of PlayerBase).Count
      Get
        Return Me.m_List.Count
      End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of PlayerBase).IsReadOnly
      Get
        Return False
      End Get
    End Property

    Public Function Remove(ByVal Item As PlayerBase) As Boolean Implements ICollection(Of PlayerBase).Remove
      Return Me.m_List.Remove(Item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of PlayerBase) Implements IEnumerable(Of PlayerBase).GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function

    Public Function IndexOf(ByVal Item As PlayerBase) As Integer Implements IList(Of PlayerBase).IndexOf
      Return Me.m_List.IndexOf(item)
    End Function

    Public Sub Insert(ByVal Index As Integer,
                      ByVal Item As PlayerBase) Implements IList(Of PlayerBase).Insert
      Call Me.m_List.Insert(index, item)
    End Sub

    Default Public Property Item(ByVal Index As Integer) As PlayerBase Implements IList(Of PlayerBase).Item
      Get
        Return Me.m_List.Item(index)
      End Get
      Set(ByVal Value As PlayerBase)
        Me.m_List.Item(index) = Value
      End Set
    End Property

    Public Sub RemoveAt(ByVal Index As Integer) Implements IList(Of PlayerBase).RemoveAt
      Call Me.m_List.RemoveAt(index)
    End Sub

    Private Function iGetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
      Return Me.m_List.GetEnumerator()
    End Function
  End Class
End Namespace