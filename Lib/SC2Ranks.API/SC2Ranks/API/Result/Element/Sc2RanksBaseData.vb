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
  Public Class Sc2RanksBaseData
    '{
    '  "leagues": {
    '    "all": "All",
    '    "grandmaster": "Grandmaster",
    '    "master": "Master",
    '    "diamond": "Diamond",
    '    "platinum": "Platinum",
    '    "gold": "Gold",
    '    "silver": "Silver",
    '    "bronze": "Bronze"
    '  },
    '  "regions": {
    '    "global": {
    '      "long": "Global",
    '      "short": "Global"
    '    },
    '    "us": {
    '      "long": "United States",
    '      "short": "US"
    '    },
    '    "tw": {
    '      "long": "Taiwan",
    '      "short": "TW"
    '    },
    '    "kr": {
    '      "long": "Korea",
    '      "short": "KR"
    '    },
    '    "la": {
    '      "long": "Latin America",
    '      "short": "LA"
    '    },
    '    "ru": {
    '      "long": "Russia",
    '      "short": "RU"
    '    }
    '  },
    '  "rank_regions": {
    '    "global": {
    '      "long": "Global",
    '      "short": "Global"
    '    },
    '    "eu": {
    '      "long": "Europe",
    '      "short": "EU"
    '    },
    '    "sea": {
    '      "long": "Southeast Asia",
    '      "short": "SEA"
    '    },
    '    "cn": {
    '      "long": "China",
    '      "short": "CN"
    '    },
    '    "fea": {
    '      "long": "Korea / Taiwan",
    '      "short": "KR/TW"
    '    },
    '    "am": {
    '      "long": "Americas",
    '      "short": "AM"
    '    }
    '  },
    '  "expansions": {
    '    "hots": {
    '      "long": "Heart of the Swarm",
    '      "short": "HoTS"
    '    },
    '    "wol": {
    '      "long": "Wings of Liberty",
    '      "short": "WoL"
    '    }
    '  },
    '  "races": "Random",
    '  "brackets": {
    '    "1v1": {
    '      "bracket": 1,
    '      "random": false
    '    },
    '    "2v2t": {
    '      "bracket": 2,
    '      "random": false
    '    },
    '    "3v3t": {
    '     "bracket": 3,
    '      "random": false
    '    },
    '    "4v4t": {
    '     "bracket": 4,
    '      "random": false
    '    },
    '    "2v2r": {
    '      "bracket": 2,
    '      "random": true
    '    },
    '    "3v3r": {
    '      "bracket": 3,
    '      "random": true
    '    },
    '    "4v4r": {
    '      "bracket": 4,
    '      "random": true
    '    }
    '  },
    '  "season": {
    '   "year": 2013,
    '    "number": 4
    '  }
    '}

    Private m_Leagues As Sc2RanksLeaguesDefinition
    Private m_Regions As Sc2RanksRegionsDefinition
    Private m_RankRegions As Sc2RanksRankRegionsDefinition
    Private m_Expansions As Sc2RanksExpansionsDefinition
    Private m_Races As Sc2RanksRacesDefinition
    Private m_Brackets As Sc2RanksBracketsDefinition
    Private m_Season As Sc2RanksSeason

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Leagues = Nothing
      Me.m_Regions = Nothing
      Me.m_RankRegions = Nothing
      Me.m_Expansions = Nothing
      Me.m_Races = Nothing
      Me.m_Brackets = Nothing
      Me.m_Season = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "leagues")>
    Public Property Leagues As Sc2RanksLeaguesDefinition
      Get
        Return Me.m_Leagues
      End Get
      Private Set(ByVal Value As Sc2RanksLeaguesDefinition)
        Me.m_Leagues = Value
      End Set
    End Property

    <DataMember(Name := "regions")>
    Public Property Regions As Sc2RanksRegionsDefinition
      Get
        Return Me.m_Regions
      End Get
      Private Set(ByVal Value As Sc2RanksRegionsDefinition)
        Me.m_Regions = Value
      End Set
    End Property

    <DataMember(Name := "rank_regions")>
    Public Property RankRegions As Sc2RanksRankRegionsDefinition
      Get
        Return Me.m_RankRegions
      End Get
      Private Set(ByVal Value As Sc2RanksRankRegionsDefinition)
        Me.m_RankRegions = Value
      End Set
    End Property

    <DataMember(Name := "expansions")>
    Public Property Expansions As Sc2RanksExpansionsDefinition
      Get
        Return Me.m_Expansions
      End Get
      Private Set(ByVal Value As Sc2RanksExpansionsDefinition)
        Me.m_Expansions = Value
      End Set
    End Property

    <DataMember(Name := "races")>
    Public Property Races As Sc2RanksRacesDefinition
      Get
        Return Me.m_Races
      End Get
      Private Set(ByVal Value As Sc2RanksRacesDefinition)
        Me.m_Races = Value
      End Set
    End Property

    <DataMember(Name := "brackets")>
    Public Property Brackets As Sc2RanksBracketsDefinition
      Get
        Return Me.m_Brackets
      End Get
      Private Set(ByVal Value As Sc2RanksBracketsDefinition)
        Me.m_Brackets = Value
      End Set
    End Property

    <DataMember(Name := "season")>
    Public Property Season As Sc2RanksSeason
      Get
        Return Me.m_Season
      End Get
      Private Set(ByVal Value As Sc2RanksSeason)
        Me.m_Season = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Leagues: {0}", Me.Leagues.ToString())
        Call .AppendFormat("Regions: {0}", Me.Regions.ToString())
        Call .AppendFormat("Rank Regions: {0}", Me.RankRegions.ToString())
        Call .AppendFormat("Expansions: {0}", Me.Expansions.ToString())
        Call .AppendFormat("Races: {0}", Me.Races.ToString())
        Call .AppendFormat("Brackets: {0}", Me.Brackets.ToString())
        Call .AppendFormat("Season: {0}", Me.Season.ToString())
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace