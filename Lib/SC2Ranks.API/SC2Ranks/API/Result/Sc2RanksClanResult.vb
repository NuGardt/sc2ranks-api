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
Imports NuGardt.SC2Ranks.API.Result.Element
Imports System.Text

Namespace SC2Ranks.API.Result
  <DataContract()>
  Public Class Sc2RanksClanResult
    Inherits Sc2RanksBaseResult
    '{
    '  "url": "http://www.sc2ranks.com/clan/am/Monty",
    '  "region": "am",
    '  "tag": "Monty",
    '  "members": 1,
    '  "updated_at": 1370209265,
    '  "scores": {
    '    "top": 11,
    '    "avg": 21,
    '    "sum": 31
    '  }
    '}

    Private m_Url As String
    Private m_RegionRaw As String
    Private m_Tag As String
    Private m_Description As String
    Private m_Members As Int32
    Private m_UpdatedAt As Int64
    Private m_Scores As Sc2RanksScoreElement

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Url = Nothing
      Me.m_RegionRaw = Nothing
      Me.m_Tag = Nothing
      Me.m_Description = Nothing
      Me.m_Members = Nothing
      Me.m_UpdatedAt = Nothing
      Me.m_Scores = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "url")>
    Public Property Url As String
      Get
        Return Me.m_Url
      End Get
      Private Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(Name := "region")>
    Private Property RegionRaw As String
      Get
        Return Me.m_RegionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RegionRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Region() As eSc2RanksRegion
      Get
        Return Enums.RegionBuffer.GetEnum(Me.m_RegionRaw)
      End Get
    End Property

    <DataMember(Name := "tag")>
    Public Property Tag As String
      Get
        Return Me.m_Tag
      End Get
      Private Set(ByVal Value As String)
        Me.m_Tag = Value
      End Set
    End Property

    <DataMember(Name := "description")>
    Public Property Description As String
      Get
        Return Me.m_Description
      End Get
      Private Set(ByVal Value As String)
        Me.m_Description = Value
      End Set
    End Property

    <DataMember(Name := "members")>
    Public Property MemberCount As Int32
      Get
        Return Me.m_Members
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Members = Value
      End Set
    End Property

    <DataMember(Name := "updated_at")>
    Public Property UpdatedAtUnixTime As Int64
      Get
        Return Me.m_UpdatedAt
      End Get
      Private Set(ByVal Value As Int64)
        Me.m_UpdatedAt = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property UpdatedAt As DateTime
      Get
        Return New DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Me.m_UpdatedAt)
      End Get
    End Property

    <DataMember(Name := "scores")>
    Public Property Scores As Sc2RanksScoreElement
      Get
        Return Me.m_Scores
      End Get
      Private Set(ByVal Value As Sc2RanksScoreElement)
        Me.m_Scores = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("URL: {0}", Me.Url)
        Call .AppendFormat("Region: {0}", Me.Region.ToString())
        Call .AppendFormat("Tag: {0}", Me.Tag)
        Call .AppendFormat("Description: {0}", Me.Description)
        Call .AppendFormat("Member Count: {0}", Me.MemberCount.ToString())
        Call .AppendFormat("Updated At: {0}", Me.UpdatedAt.ToString())
        If (Me.Scores IsNot Nothing) Then Call .AppendFormat("Scores: {0}", Me.Scores.ToString())
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace