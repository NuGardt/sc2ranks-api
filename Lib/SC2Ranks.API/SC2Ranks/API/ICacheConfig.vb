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
Namespace SC2Ranks.API
  Public Interface ICacheConfig
    Property GetBaseDataCacheDuration As TimeSpan

    Property GetCharacterCacheDuration As TimeSpan

    Property GetCharacterTeamsListCacheDuration As TimeSpan

    Property SearchCharacterTeamListCacheDuration As TimeSpan

    Property GetCharacterListCacheDuration As TimeSpan

    Property GetCharacterTeamListCacheDuration As TimeSpan

    Property GetClanCacheDuration As TimeSpan

    Property GetClanCharacterListCacheDuration As TimeSpan

    Property GetClanTeamListCacheDuration As TimeSpan

    Property GetRankingsTopCacheDuration As TimeSpan

    Property GetDivisionsTopCacheDuration As TimeSpan

    Property GetDivisionCacheDuration As TimeSpan

    Property GetDivisionTeamsTopCacheDuration As TimeSpan

    Property GetCustomDivisionsCacheDuration As TimeSpan

    Property GetCustomDivisionCacheDuration As TimeSpan

    Property GetCustomDivisionTeamListCacheDuration As TimeSpan

    Property GetCustomDivisionCharacterListCacheDuration As TimeSpan

    Property CustomDivisionAddCacheDuration As TimeSpan

    Property CustomDivisionRemoveCacheDuration As TimeSpan
  End Interface
End Namespace