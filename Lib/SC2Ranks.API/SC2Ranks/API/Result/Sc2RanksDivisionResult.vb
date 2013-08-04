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

Namespace SC2Ranks.API.Result
  <DataContract()>
  Public Class Sc2RanksDivisionResult
    Inherits Sc2RanksBaseResult

    '{
    '  "id": "51fc38734970cf3f98000006",
    '  "url": "http://www.sc2ranks.com/division/am/192413001/grandmaster",
    '  "name": "Grandmaster",
    '  "rank_region": "am",
    '  "expansion": "hots",
    '  "league": "grandmaster",
    '  "bracket": 1,
    '  "random": false,
    '  "characters": 5,
    '  "avg_points": 50.0,
    '  "avg_wins": 12.5,
    '  "avg_losses": 1.93,
    '  "avg_games": 20.3,
    '  "avg_win_ratio": 0.5913,
    '  "updated_at": 1375480419
    '}

    Private m_ID As String
    Private m_Url As String
    Private m_Name As String
    Private m_RankRegionRaw As String
    Private m_ExpansionRaw As String
    Private m_LeagueRaw As String
    Private m_BracketRaw As Int32
    Private m_Random As Boolean
    Private m_CharacterCount As Int32
    Private m_AveragePoints As Double
    Private m_AverageWins As Double
    Private m_AverageLosses As Double
    Private m_AverageGames As Double
    Private m_AverageWinRatio As Double
    Private m_UpdateAt As Int64

    'ToDO: Sc2RanksDivisionElement
    Public Sub New()
      Me.m_ID = Nothing
      Me.m_Url = Nothing
      Me.m_Name = Nothing
      Me.m_RankRegionRaw = Nothing
      Me.m_ExpansionRaw = Nothing
      Me.m_LeagueRaw = Nothing
      Me.m_BracketRaw = Nothing
      Me.m_Random = Nothing
      Me.m_CharacterCount = Nothing
      Me.m_AveragePoints = Nothing
      Me.m_AverageWins = Nothing
      Me.m_AverageLosses = Nothing
      Me.m_AverageGames = Nothing
      Me.m_AverageWinRatio = Nothing
      Me.m_UpdateAt = Nothing
    End Sub

    <DataMember(name := "id")>
    Public Property ID As String
      Get
        Return Me.m_ID
      End Get
      Private Set(ByVal Value As String)
        Me.m_ID = Value
      End Set
    End Property

    <DataMember(name := "url")>
    Public Property Url As String
      Get
        Return Me.m_Url
      End Get
      Private Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(name := "name")>
    Public Property Name As String
      Get
        Return Me.m_Name
      End Get
      Private Set(ByVal Value As String)
        Me.m_Name = Value
      End Set
    End Property

    <DataMember(name := "rank_region")>
    Private Property RankRegionRaw As String
      Get
        Return Me.m_RankRegionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RankRegionRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property RankRegion As eSc2RanksRankRegion
      Get
        Return Enums.RankRegionBuffer.GetEnum(Me.m_RankRegionRaw)
      End Get
    End Property

    <DataMember(name := "expansion")>
    Private Property ExpansionRaw As String
      Get
        Return Me.m_ExpansionRaw
      End Get
      Set(ByVal Value As String)
        Me.m_ExpansionRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property Expansion() As eSc2RanksExpansion
      Get
        Return Enums.ExpansionBuffer.GetEnum(Me.m_ExpansionRaw)
      End Get
    End Property

    <DataMember(name := "league")>
    Private Property LeagueRaw As String
      Get
        Return Me.m_LeagueRaw
      End Get
      Set(ByVal Value As String)
        Me.m_LeagueRaw = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property League As eSc2RanksLeague
      Get
        Return Enums.LeagueBuffer.GetEnum(Me.m_LeagueRaw)
      End Get
    End Property

    <DataMember(name := "bracket")>
    Private Property BracketRaw As Int32
      Get
        Return Me.m_BracketRaw
      End Get
      Set(ByVal Value As Int32)
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

    <DataMember(name := "characters")>
    Public Property CharacterCount As Int32
      Get
        Return Me.m_CharacterCount
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_CharacterCount = Value
      End Set
    End Property

    <DataMember(name := "avg_points")>
    Public Property AveragePoints As Double
      Get
        Return Me.m_AveragePoints
      End Get
      Private Set(ByVal Value As Double)
        Me.m_AveragePoints = Value
      End Set
    End Property

    <DataMember(name := "avg_wins")>
    Public Property AverageWins As Double
      Get
        Return Me.m_AverageWins
      End Get
      Private Set(ByVal Value As Double)
        Me.m_AverageWins = Value
      End Set
    End Property

    <DataMember(name := "avg_losses")>
    Public Property AverageLosses As Double
      Get
        Return Me.m_AverageLosses
      End Get
      Private Set(ByVal Value As Double)
        Me.m_AverageLosses = Value
      End Set
    End Property

    <DataMember(name := "avg_games")>
    Public Property AverageGames As Double
      Get
        Return Me.m_AverageGames
      End Get
      Private Set(ByVal Value As Double)
        Me.m_AverageGames = Value
      End Set
    End Property

    <DataMember(name := "avg_win_ratio")>
    Public Property AverageWinRatio As Double
      Get
        Return Me.m_AverageWinRatio
      End Get
      Private Set(ByVal Value As Double)
        Me.m_AverageWinRatio = Value
      End Set
    End Property

    <DataMember(name := "updated_at")>
    Public Property UpdateAtUnixTime As Int64
      Get
        Return Me.m_UpdateAt
      End Get
      Private Set(ByVal Value As Int64)
        Me.m_UpdateAt = Value
      End Set
    End Property

    Public ReadOnly Property UpdatedAt As DateTime
      Get
        Return New DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Me.m_UpdateAt)
      End Get
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("ID: {0}{1}", Me.ID, vbCrLf)
        Call .AppendFormat("URL: {0}{1}", Me.Url, vbCrLf)
        Call .AppendFormat("Name: {0}{1}", Me.Name, vbCrLf)
        Call .AppendFormat("Rank Region: {0}{1}", Me.RankRegion.ToString(), vbCrLf)
        Call .AppendFormat("Expansion: {0}{1}", Me.Expansion.ToString(), vbCrLf)
        Call .AppendFormat("League: {0}{1}", Me.League.ToString(), vbCrLf)
        Call .AppendFormat("Breacket: {0}{1}", Me.Bracket.ToString(), vbCrLf)
        Call .AppendFormat("Character Count: {0}{1}", Me.CharacterCount.ToString(), vbCrLf)
        Call .AppendFormat("Average Points: {0}{1}", Me.AveragePoints.ToString(), vbCrLf)
        Call .AppendFormat("Average Wins: {0}{1}", Me.AverageWins.ToString(), vbCrLf)
        Call .AppendFormat("Average Losses: {0}{1}", Me.AverageLosses.ToString(), vbCrLf)
        Call .AppendFormat("Average Games: {0}{1}", Me.AverageGames.ToString(), vbCrLf)
        Call .AppendFormat("Average Win Ratio: {0}{1}", Me.AverageWinRatio.ToString(), vbCrLf)
        Call .AppendFormat("Updated At: {0}{1}", Me.UpdatedAt.ToString(), vbCrLf)

      End With

      Return SB.ToString
    End Function
  End Class
End Namespace