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
  Public Class Sc2RanksExpansionsDefinition
    '"expansions": {
    '  "hots": {
    '    "long": "Heart of the Swarm",
    '    "short": "HoTS"
    '  },
    '  "wol": {
    '    "long": "Wings of Liberty",
    '    "short": "WoL"
    '  }
    '},

    Private m_Wol As Sc2RanksNotation
    Private m_Hots As Sc2RanksNotation

    Public Sub New()
      Me.m_Wol = Nothing
      Me.m_Hots = Nothing
    End Sub

    <DataMember(name := "wol")>
    Public Property Wol As Sc2RanksNotation
      Get
        Return Me.m_Wol
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_Wol = Value
      End Set
    End Property

    <DataMember(name := "hots")>
    Public Property Hots As Sc2RanksNotation
      Get
        Return Me.m_Hots
      End Get
      Private Set(ByVal Value As Sc2RanksNotation)
        Me.m_Hots = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("WoL: {0}{1}", Me.Wol.ToString(), vbCrLf)
        Call .AppendFormat("HotS: {0}{1}", Me.Hots.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace