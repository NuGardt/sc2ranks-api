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
  Public Class Sc2RanksCustomDivisionManageElement
    '{
    '  "error": "invalid",
    '  "bnet_id": "invalid",
    '  "region": "us"
    '},

    Private m_Error As String
    Private m_BattleNetID As Int64
    Private m_RegionRaw As String

    Public Sub New()
      Me.m_Error = Nothing
      Me.m_BattleNetID = Nothing
      Me.m_RegionRaw = Nothing
    End Sub

    <DataMember(name := "error")>
    Public Property [Error] As String
      Get
        Return Me.m_Error
      End Get
      Private Set(ByVal Value As String)
        Me.m_Error = Value
      End Set
    End Property

    <DataMember(name := "bnet_id")>
    Public Property BattleNetID As Int64
      Get
        Return Me.m_BattleNetID
      End Get
      Private Set(ByVal Value As Int64)
        Me.m_BattleNetID = Value
      End Set
    End Property

    <DataMember(name := "region")>
    Private Property RegionRaw As String
      Get
        Return Me.m_RegionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RegionRaw = Value
      End Set
    End Property

    Public ReadOnly Property Region() As eSc2RanksRegion
      Get
        Return Enums.RegionBuffer.GetEnum(Me.m_RegionRaw)
      End Get
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Error: {0}{1}", Me.Error, vbCrLf)
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
        Call .AppendFormat("Region: {0}{1}", Me.Region.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace