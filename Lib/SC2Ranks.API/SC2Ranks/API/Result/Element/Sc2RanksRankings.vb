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
  Public Class Sc2RanksRankings
    '"rankings": {
    '  "world": 1,
    '  "region": 1
    '},

    Private m_World As Int32
    Private m_Region As Int32

    Public Sub New()
      Me.m_World = Nothing
      Me.m_Region = Nothing
    End Sub

    <DataMember(name := "world")>
    Public Property World As Int32
      Get
        Return Me.m_World
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_World = Value
      End Set
    End Property

    <DataMember(name := "region")>
    Public Property Region As Int32
      Get
        Return Me.m_Region
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Region = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("World: {0}{1}", Me.World.ToString(), vbCrLf)
        Call .AppendFormat("Region: {0}{1}", Me.Region.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace