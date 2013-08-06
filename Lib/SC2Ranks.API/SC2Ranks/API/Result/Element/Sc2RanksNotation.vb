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
  Public Class Sc2RanksNotation
    '"kr": {
    '  "long": "Korea",
    '  "short": "KR"
    '},
    Private m_Long As String
    Private m_Short As String

    Public Sub New()
      Me.m_Long = Nothing
      Me.m_Short = Nothing
    End Sub

    <DataMember(name := "long")>
    Public Property [Long] As String
      Get
        Return Me.m_Long
      End Get
      Private Set(ByVal Value As String)
        Me.m_Long = Value
      End Set
    End Property

    <DataMember(name := "short")>
    Public Property [Short] As String
      Get
        Return Me.m_Short
      End Get
      Private Set(ByVal Value As String)
        Me.m_Short = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Long: {0}{1}", Me.Long, vbCrLf)
        Call .AppendFormat("Short: {0}{1}", Me.Short, vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace