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
  Public Class Sc2RanksRegionsDefinition
    '"regions": {
    '  "global": {
    '    "long": "Global",
    '    "short": "Global"
    '  },
    '  "us": {
    '    "long": "United States",
    '    "short": "US"
    '  },
    '  "tw": {
    '    "long": "Taiwan",
    '    "short": "TW"
    '  },
    '  "kr": {
    '    "long": "Korea",
    '    "short": "KR"
    '  },
    '  "la": {
    '    "long": "Latin America",
    '    "short": "LA"
    '  },
    '  "ru": {
    '    "long": "Russia",
    '    "short": "RU"
    '  }
    '},

    Private m_Global As Sc2RanksNotation
    Private m_Us As Sc2RanksNotation
    Private m_Tw As Sc2RanksNotation
    Private m_Kr As Sc2RanksNotation
    Private m_La As Sc2RanksNotation
    Private m_Ru As Sc2RanksNotation

    Public Sub New()
      Me.m_Global = Nothing
      Me.m_Us = Nothing
      Me.m_Tw = Nothing
      Me.m_Kr = Nothing
      Me.m_La = Nothing
      Me.m_Ru = Nothing
    End Sub

    <DataMember(name := "global")>
    Public Property [Global] As Sc2RanksNotation
      Get
        Return Me.m_Global
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_Global = Value
      End Set
    End Property

    <DataMember(name := "us")>
    Public Property Us As Sc2RanksNotation
      Get
        Return Me.m_Us
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_Us = Value
      End Set
    End Property

    <DataMember(name := "tw")>
    Public Property Tw As Sc2RanksNotation
      Get
        Return Me.m_Tw
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_Tw = Value
      End Set
    End Property

    <DataMember(name := "kr")>
    Public Property Kr As Sc2RanksNotation
      Get
        Return Me.m_Kr
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_Kr = Value
      End Set
    End Property

    <DataMember(name := "la")>
    Public Property La As Sc2RanksNotation
      Get
        Return Me.m_La
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_La = Value
      End Set
    End Property

    <DataMember(name := "ru")>
    Public Property Ru As Sc2RanksNotation
      Get
        Return Me.m_Ru
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_Ru = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Global: {0}{1}", Me.Global.ToString(), vbCrLf)
        Call .AppendFormat("US: {0}{1}", Me.Us.ToString(), vbCrLf)
        Call .AppendFormat("TW: {0}{1}", Me.Tw.ToString(), vbCrLf)
        Call .AppendFormat("KR: {0}{1}", Me.Kr.ToString(), vbCrLf)
        Call .AppendFormat("LA: {0}{1}", Me.La.ToString(), vbCrLf)
        Call .AppendFormat("RU: {0}{1}", Me.Ru.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace