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

    Private m_Top As Int32
    Private m_Avg As Int32
    Private m_Sum As Int32

    Public Sub New()
      Me.m_Top = Nothing
      Me.m_Avg = Nothing
      Me.m_Sum = Nothing
    End Sub

    <DataMember(name := "top")>
    Public Property Top As Int32
      Get
        Return Me.m_Top
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Top = Value
      End Set
    End Property

    <DataMember(name := "avg")>
    Public Property Average As Int32
      Get
        Return Me.m_Avg
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Avg = Value
      End Set
    End Property

    <DataMember(name := "sum")>
    Public Property Sum As Int32
      Get
        Return Me.m_Sum
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Sum = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Top: {0}{1}", Me.Top.ToString(), vbCrLf)
        Call .AppendFormat("Average: {0}{1}", Me.Average.ToString(), vbCrLf)
        Call .AppendFormat("Sum: {0}{1}", Me.Sum.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace