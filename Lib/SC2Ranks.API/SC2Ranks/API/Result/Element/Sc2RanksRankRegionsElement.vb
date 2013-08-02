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
  Public Class Sc2RanksRankRegionsElement
    '"rank_regions": {
    '  "global": {
    '    "long": "Global",
    '    "short": "Global"
    '  },
    '  "eu": {
    '    "long": "Europe",
    '    "short": "EU"
    '  },
    '  "sea": {
    '    "long": "Southeast Asia",
    '    "short": "SEA"
    '  },
    '  "cn": {
    '    "long": "China",
    '    "short": "CN"
    '  },
    '  "fea": {
    '    "long": "Korea / Taiwan",
    '    "short": "KR/TW"
    '  },
    '  "am": {
    '    "long": "Americas",
    '    "short": "AM"
    '  }
    '},

    Private m_Global As Sc2RanksNotationElement
    Private m_Eu As Sc2RanksNotationElement
    Private m_Sea As Sc2RanksNotationElement
    Private m_Cn As Sc2RanksNotationElement
    Private m_Fea As Sc2RanksNotationElement
    Private m_Am As Sc2RanksNotationElement

    Public Sub New()
      Me.m_Global = Nothing
      Me.m_Eu = Nothing
      Me.m_Sea = Nothing
      Me.m_Cn = Nothing
      Me.m_Fea = Nothing
      Me.m_Am = Nothing
    End Sub

    <DataMember(name := "global")>
    Public Property [Global] As Sc2RanksNotationElement
      Get
        Return Me.m_Global
      End Get
      Private Set(ByVal Value As Sc2RanksNotationElement)
        Me.m_Global = Value
      End Set
    End Property

    <DataMember(name := "eu")>
    Public Property Eu As Sc2RanksNotationElement
      Get
        Return Me.m_Eu
      End Get
      Private Set(ByVal Value As Sc2RanksNotationElement)
        Me.m_Eu = Value
      End Set
    End Property

    <DataMember(name := "sea")>
    Public Property Sea As Sc2RanksNotationElement
      Get
        Return Me.m_Sea
      End Get
      Private Set(ByVal Value As Sc2RanksNotationElement)
        Me.m_Sea = Value
      End Set
    End Property

    <DataMember(name := "cn")>
    Public Property Cn As Sc2RanksNotationElement
      Get
        Return Me.m_Cn
      End Get
      Private Set(ByVal Value As Sc2RanksNotationElement)
        Me.m_Cn = Value
      End Set
    End Property

    <DataMember(name := "fea")>
    Public Property Fea As Sc2RanksNotationElement
      Get
        Return Me.m_Fea
      End Get
      Private Set(ByVal Value As Sc2RanksNotationElement)
        Me.m_Fea = Value
      End Set
    End Property

    <DataMember(name := "am")>
    Public Property Am As Sc2RanksNotationElement
      Get
        Return Me.m_Am
      End Get
      Private Set(ByVal Value As Sc2RanksNotationElement)
        Me.m_Am = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("EU: {0}{1}", Me.Eu.ToString(), vbCrLf)
        Call .AppendFormat("SEA: {0}{1}", Me.Sea.ToString(), vbCrLf)
        Call .AppendFormat("CN: {0}{1}", Me.Cn.ToString(), vbCrLf)
        Call .AppendFormat("FEA: {0}{1}", Me.Fea.ToString(), vbCrLf)
        Call .AppendFormat("AM: {0}{1}", Me.Am.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace