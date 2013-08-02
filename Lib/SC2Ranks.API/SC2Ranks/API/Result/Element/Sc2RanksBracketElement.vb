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
Imports NuGardt.SC2Ranks.Helper
Imports System.Text

Namespace SC2Ranks.API.Result.Element
  <DataContract()>
  Public Class Sc2RanksBracketElement
    '"1v1": {
    '  "bracket": 1,
    '  "random": false
    '},

    Private m_BracketRaw As Int16
    Private m_Random As Boolean

    Public Sub New()
      Me.m_BracketRaw = Nothing
      Me.m_Random = Nothing
    End Sub

    <DataMember(name := "bracket")>
    Private Property BracketRaw As Int16
      Get
        Return Me.m_BracketRaw
      End Get
      Set(ByVal Value As Int16)
        Me.m_BracketRaw = Value
      End Set
    End Property

    <DataMember(name := "random")>
    Private Property Random As Boolean
      Get
        Return Me.m_Random
      End Get
      Set(ByVal Value As Boolean)
        Me.m_Random = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Bracket As eSc2RanksBracket
      Get
        Return Enums.BracketRawToBracket(Me.m_BracketRaw, Me.m_Random)
      End Get
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Bracket: {0}{1}", Me.Bracket.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace