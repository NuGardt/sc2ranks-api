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
  Public Class Sc2RanksLeaguesDefinition
    '"leagues": {
    '  "all": "All",
    '  "grandmaster": "Grandmaster",
    '  "master": "Master",
    '  "diamond": "Diamond",
    '  "platinum": "Platinum",
    '  "gold": "Gold",
    '  "silver": "Silver",
    '  "bronze": "Bronze"
    '},

    Private m_All As String
    Private m_Grandmaster As String
    Private m_Master As String
    Private m_Diamond As String
    Private m_Platinum As String
    Private m_Gold As String
    Private m_Silver As String
    Private m_Bronze As String

    Public Sub New()
      Me.m_All = Nothing
      Me.m_Grandmaster = Nothing
      Me.m_Master = Nothing
      Me.m_Diamond = Nothing
      Me.m_Platinum = Nothing
      Me.m_Gold = Nothing
      Me.m_Silver = Nothing
      Me.m_Bronze = Nothing
    End Sub

    <DataMember(name := "all")>
    Public Property All As String
      Get
        Return Me.m_All
      End Get
      Private Set(ByVal Value As String)
        Me.m_All = Value
      End Set
    End Property

    <DataMember(name := "grandmaster")>
    Public Property Grandmaster As String
      Get
        Return Me.m_Grandmaster
      End Get
      Private Set(ByVal Value As String)
        Me.m_Grandmaster = Value
      End Set
    End Property

    <DataMember(name := "master")>
    Public Property Master As String
      Get
        Return Me.m_Master
      End Get
      Private Set(ByVal Value As String)
        Me.m_Master = Value
      End Set
    End Property

    <DataMember(name := "diamond")>
    Public Property Diamond As String
      Get
        Return Me.m_Diamond
      End Get
      Private Set(ByVal Value As String)
        Me.m_Diamond = Value
      End Set
    End Property

    <DataMember(name := "platinum")>
    Public Property Platinum As String
      Get
        Return Me.m_Platinum
      End Get
      Private Set(ByVal Value As String)
        Me.m_Platinum = Value
      End Set
    End Property

    <DataMember(name := "gold")>
    Public Property Gold As String
      Get
        Return Me.m_Gold
      End Get
      Private Set(ByVal Value As String)
        Me.m_Gold = Value
      End Set
    End Property

    <DataMember(name := "silver")>
    Public Property Silver As String
      Get
        Return Me.m_Silver
      End Get
      Private Set(ByVal Value As String)
        Me.m_Silver = Value
      End Set
    End Property

    <DataMember(name := "bronze")>
    Public Property Bronze As String
      Get
        Return Me.m_Bronze
      End Get
      Private Set(ByVal Value As String)
        Me.m_Bronze = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("All: {0}{1}", Me.All.ToString(), vbCrLf)
        Call .AppendFormat("Grandmaster: {0}{1}", Me.Grandmaster.ToString(), vbCrLf)
        Call .AppendFormat("Master: {0}{1}", Me.Master.ToString(), vbCrLf)
        Call .AppendFormat("Diamond: {0}{1}", Me.Diamond.ToString(), vbCrLf)
        Call .AppendFormat("Platinum: {0}{1}", Me.Platinum.ToString(), vbCrLf)
        Call .AppendFormat("Gold: {0}{1}", Me.Gold.ToString(), vbCrLf)
        Call .AppendFormat("Silver: {0}{1}", Me.Silver.ToString(), vbCrLf)
        Call .AppendFormat("Bronze: {0}{1}", Me.Bronze.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace