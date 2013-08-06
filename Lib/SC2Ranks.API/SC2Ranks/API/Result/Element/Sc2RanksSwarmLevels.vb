
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
  Public Class Sc2RanksSwarmLevels
    '"swarm_levels": {
    '  "zerg": 0,
    '  "protoss": 0,
    '  "terran": 0
    '},

    Private m_Zerg As Int32
    Private m_Protoss As Int32
    Private m_Terran As Int32

    Public Sub New()
      Me.m_Zerg = Nothing
      Me.m_Protoss = Nothing
      Me.m_Terran = Nothing
    End Sub

    <DataMember(name := "zerg")>
    Public Property Zerg As Int32
      Get
        Return Me.m_Zerg
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Zerg = Value
      End Set
    End Property

    <DataMember(name := "protoss")>
    Public Property Protoss As Int32
      Get
        Return Me.m_Protoss
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Protoss = Value
      End Set
    End Property

    <DataMember(name := "terran")>
    Public Property Terran As Int32
      Get
        Return Me.m_Terran
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Terran = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Zerg: {0}{1}", Me.Zerg.ToString(), vbCrLf)
        Call .AppendFormat("Protoss: {0}{1}", Me.Protoss.ToString(), vbCrLf)
        Call .AppendFormat("Terran: {0}{1}", Me.Terran.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace