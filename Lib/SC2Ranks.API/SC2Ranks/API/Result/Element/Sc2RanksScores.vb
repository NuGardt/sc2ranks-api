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
  Public Class Sc2RanksScores
    '"scores": {
    '  "top": 11,
    '  "avg": 21,
    '  "sum": 31
    '}

    Private m_Top As String
    Private m_Avg As String
    Private m_Sum As String

    Public Sub New()
      Me.m_Top = Nothing
      Me.m_Avg = Nothing
      Me.m_Sum = Nothing
    End Sub

    <DataMember(name := "top")>
    Private Property iTop As String
      Get
        Return Me.m_Top
      End Get
      Set(ByVal Value As String)
        Me.m_Top = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Top As Nullable(Of Int32)
      Get
        Dim tTop As Int32
        If Int32.TryParse(Me.m_Top, tTop) Then
          Return tTop
        Else
          Return Nothing
        End If
      End Get
    End Property

    <DataMember(name := "avg")>
    Private Property iAverage As String
      Get
        Return Me.m_Avg
      End Get
      Set(ByVal Value As String)
        Me.m_Avg = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Average As Nullable(Of Int32)
      Get
        Dim tAvg As Int32
        If Int32.TryParse(Me.m_Avg, tAvg) Then
          Return tAvg
        Else
          Return Nothing
        End If
      End Get
    End Property

    <DataMember(name := "sum")>
    Private Property iSum As String
      Get
        Return Me.m_Sum
      End Get
      Set(ByVal Value As String)
        Me.m_Sum = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Sum As Nullable(Of Int32)
      Get
        Dim tSum As Int32
        If Int32.TryParse(Me.m_Sum, tSum) Then
          Return tSum
        Else
          Return Nothing
        End If
      End Get
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        If (Me.Top.HasValue) Then Call .AppendFormat("Top: {0}{1}", Me.Top.Value.ToString(), vbCrLf)
        If (Me.Average.HasValue) Then Call .AppendFormat("Average: {0}{1}", Me.Average.Value.ToString(), vbCrLf)
        If (Me.Sum.HasValue) Then Call .AppendFormat("Sum: {0}{1}", Me.Sum.Value.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace