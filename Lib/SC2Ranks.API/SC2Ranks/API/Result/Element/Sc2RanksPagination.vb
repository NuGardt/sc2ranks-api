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
  Public Class Sc2RanksPagination
    '"pagination": {
    '  "limit": 10,
    '  "offset": 0,
    '  "total": 5
    '}

    Private m_Limit As Int32
    Private m_Offset As Int32
    Private m_Total As Int32

    Public Sub New()
      Me.m_Limit = Nothing
      Me.m_Offset = Nothing
      Me.m_Total = Nothing
    End Sub

    <DataMember(name := "limit")>
    Public Property Limit As Int32
      Get
        Return Me.m_Limit
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Limit = Value
      End Set
    End Property

    <DataMember(name := "offset")>
    Public Property Offset As Int32
      Get
        Return Me.m_Offset
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Offset = Value
      End Set
    End Property

    <DataMember(name := "total")>
    Public Property Total As Int32
      Get
        Return Me.m_Total
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Total = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Limit: {0}{1}", Me.Limit.ToString(), vbCrLf)
        Call .AppendFormat("Offset: {0}{1}", Me.Offset.ToString(), vbCrLf)
        Call .AppendFormat("Total: {0}{1}", Me.Total.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace