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
Imports NuGardt.SC2Ranks.API.Result.Element
Imports System.Text

Namespace SC2Ranks.API.Result
  <DataContract()>
  Public Class Sc2RanksDataResult
    Inherits Sc2RanksBaseResult
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

    Private m_Leagues As Sc2RanksLeaguesElement
    Private m_Regions As Sc2RanksRegionsElement
    Private m_RankRegions As Sc2RanksRankRegionsElement
    Private m_Expansions As Sc2RanksExpansionsElement
    'Private m_Races As Sc2RanksRacesElement
    Private m_Brackets As Sc2RanksBracketsElement
    Private m_Season As Sc2RanksSeasonElement

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Leagues = Nothing
      Me.m_Regions = Nothing
      Me.m_RankRegions = Nothing
      Me.m_Expansions = Nothing
      'Me.m_Races = Nothing
      Me.m_Brackets = Nothing
      Me.m_Season = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "leagues")>
    Private Property Leagues As Sc2RanksLeaguesElement
      Get
        Return Me.m_Leagues
      End Get
      Set(ByVal Value As Sc2RanksLeaguesElement)
        Me.m_Leagues = Value
      End Set
    End Property

    <DataMember(Name := "regions")>
    Private Property Regions As Sc2RanksRegionsElement
      Get
        Return Me.m_Regions
      End Get
      Set(ByVal Value As Sc2RanksRegionsElement)
        Me.m_Regions = Value
      End Set
    End Property

    <DataMember(Name := "rank_regions")>
    Private Property RankRegions As Sc2RanksRankRegionsElement
      Get
        Return Me.m_RankRegions
      End Get
      Set(ByVal Value As Sc2RanksRankRegionsElement)
        Me.m_RankRegions = Value
      End Set
    End Property

    <DataMember(Name := "expansions")>
    Private Property Expansions As Sc2RanksExpansionsElement
      Get
        Return Me.m_Expansions
      End Get
      Set(ByVal Value As Sc2RanksExpansionsElement)
        Me.m_Expansions = Value
      End Set
    End Property

    <DataMember(Name := "brackets")>
    Private Property Brackets As Sc2RanksBracketsElement
      Get
        Return Me.m_Brackets
      End Get
      Set(ByVal Value As Sc2RanksBracketsElement)
        Me.m_Brackets = Value
      End Set
    End Property

    <DataMember(Name := "season")>
    Private Property Season As Sc2RanksSeasonElement
      Get
        Return Me.m_Season
      End Get
      Set(ByVal Value As Sc2RanksSeasonElement)
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
        'Call .AppendFormat("Races: {0}", Me.Races.ToString())
        Call .AppendFormat("Brackets: {0}", Me.Brackets.ToString())
        Call .AppendFormat("Season: {0}", Me.Season.ToString())
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace