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
  Public Class Sc2RanksBracketsElement
    '"brackets": {
    '  "1v1": {
    '    "bracket": 1,
    '    "random": false
    '  },
    '  "2v2t": {
    '    "bracket": 2,
    '    "random": false
    '  },
    '  "3v3t": {
    '    "bracket": 3,
    '    "random": false
    '  },
    '  "4v4t": {
    '    "bracket": 4,
    '    "random": false
    '  },
    '  "2v2r": {
    '    "bracket": 2,
    '    "random": true
    '  },
    '  "3v3r": {
    '    "bracket": 3,
    '    "random": true
    '  },
    '  "4v4r": {
    '    "bracket": 4,
    '    "random": true
    '  }
    '},

    Private m_1V1 As Sc2RanksBracketElement
    Private m_2V2T As Sc2RanksBracketElement
    Private m_3V3T As Sc2RanksBracketElement
    Private m_4V4T As Sc2RanksBracketElement
    Private m_2V2R As Sc2RanksBracketElement
    Private m_3V3R As Sc2RanksBracketElement
    Private m_4V4R As Sc2RanksBracketElement

    Public Sub New()
      Me.m_1V1 = Nothing
      Me.m_2V2T = Nothing
      Me.m_3V3T = Nothing
      Me.m_4V4T = Nothing
      Me.m_2V2R = Nothing
      Me.m_3V3R = Nothing
      Me.m_4V4R = Nothing
    End Sub

    <DataMember(name := "1v1")>
    Public Property _1V1 As Sc2RanksBracketElement
      Get
        Return Me.m_1V1
      End Get
      Private Set(ByVal Value As Sc2RanksBracketElement)
        Me.m_1V1 = Value
      End Set
    End Property

    <DataMember(name := "2v2t")>
    Public Property _2V2T As Sc2RanksBracketElement
      Get
        Return Me.m_2V2T
      End Get
      Private Set(ByVal Value As Sc2RanksBracketElement)
        Me.m_2V2T = Value
      End Set
    End Property

    <DataMember(name := "3v3t")>
    Public Property _3V3T As Sc2RanksBracketElement
      Get
        Return Me.m_3V3T
      End Get
      Private Set(ByVal Value As Sc2RanksBracketElement)
        Me.m_3V3T = Value
      End Set
    End Property

    <DataMember(name := "4v4t")>
    Public Property _4V4T As Sc2RanksBracketElement
      Get
        Return Me.m_4V4T
      End Get
      Private Set(ByVal Value As Sc2RanksBracketElement)
        Me.m_4V4T = Value
      End Set
    End Property

    <DataMember(name := "2v2r")>
    Public Property _2V2R As Sc2RanksBracketElement
      Get
        Return Me.m_2V2R
      End Get
      Private Set(ByVal Value As Sc2RanksBracketElement)
        Me.m_2V2R = Value
      End Set
    End Property

    <DataMember(name := "3v3r")>
    Public Property _3V3R As Sc2RanksBracketElement
      Get
        Return Me.m_3V3R
      End Get
      Private Set(ByVal Value As Sc2RanksBracketElement)
        Me.m_3V3R = Value
      End Set
    End Property

    <DataMember(name := "4v4r")>
    Public Property _4V4R As Sc2RanksBracketElement
      Get
        Return Me.m_4V4R
      End Get
      Private Set(ByVal Value As Sc2RanksBracketElement)
        Me.m_4V4R = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("1V1: {0}{1}", Me._1V1.ToString(), vbCrLf)
        Call .AppendFormat("2V2T: {0}{1}", Me._2V2T.ToString(), vbCrLf)
        Call .AppendFormat("3V3T: {0}{1}", Me._3V3T.ToString(), vbCrLf)
        Call .AppendFormat("4V4T: {0}{1}", Me._4V4T.ToString(), vbCrLf)
        Call .AppendFormat("2V2R: {0}{1}", Me._2V2R.ToString(), vbCrLf)
        Call .AppendFormat("3V3R: {0}{1}", Me._3V3R.ToString(), vbCrLf)
        Call .AppendFormat("4V4R: {0}{1}", Me._4V4R.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace