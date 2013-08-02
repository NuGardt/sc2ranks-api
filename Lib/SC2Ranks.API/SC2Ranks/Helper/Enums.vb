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
Imports NuGardt.SC2Ranks.API
Imports System.Runtime.Serialization

Namespace SC2Ranks.Helper
  Public NotInheritable Class Enums
    'Public Shared CustomDivisionActionBuffer As EnumBuffer(Of eCustomDivisionAction, EnumMemberAttribute) = New EnumBuffer(Of eCustomDivisionAction, EnumMemberAttribute)
    'Public Shared SearchTypeBuffer As EnumBuffer(Of eSearchType, EnumMemberAttribute) = New EnumBuffer(Of eSearchType, EnumMemberAttribute)

    Public Shared MatchTypeBuffer As EnumBuffer(Of eSc2RanksMatchType, EnumMemberAttribute) = New EnumBuffer(Of eSc2RanksMatchType, EnumMemberAttribute)
    Public Shared RankRegionBuffer As EnumBuffer(Of eSc2RanksRankRegion, EnumMemberAttribute) = New EnumBuffer(Of eSc2RanksRankRegion, EnumMemberAttribute)
    Public Shared RegionBuffer As EnumBuffer(Of eSc2RanksRegion, EnumMemberAttribute) = New EnumBuffer(Of eSc2RanksRegion, EnumMemberAttribute)
    Public Shared RegionNotationBuffer As EnumBuffer(Of eSc2RanksRegion, NotationAttribute) = New EnumBuffer(Of eSc2RanksRegion, NotationAttribute)
    Public Shared BracketBuffer As EnumBuffer(Of eSc2RanksBracket, EnumMemberAttribute) = New EnumBuffer(Of eSc2RanksBracket, EnumMemberAttribute)
    Public Shared BracketNotationBuffer As EnumBuffer(Of eSc2RanksBracket, NotationAttribute) = New EnumBuffer(Of eSc2RanksBracket, NotationAttribute)
    Public Shared RacesBuffer As EnumBuffer(Of eSc2RanksRace, EnumMemberAttribute) = New EnumBuffer(Of eSc2RanksRace, EnumMemberAttribute)
    Public Shared LeaguesBuffer As EnumBuffer(Of eSc2RanksLeague, EnumMemberAttribute) = New EnumBuffer(Of eSc2RanksLeague, EnumMemberAttribute)
    Public Shared ExpansionBuffer As EnumBuffer(Of eSc2RanksExpansion, EnumMemberAttribute) = New EnumBuffer(Of eSc2RanksExpansion, EnumMemberAttribute)
    Public Shared ExpansionNotationBuffer As EnumBuffer(Of eSc2RanksExpansion, NotationAttribute) = New EnumBuffer(Of eSc2RanksExpansion, NotationAttribute)

    Private Sub New()
      '-
    End Sub

    Public Shared Function BracketRawToBracket(ByVal Bracket As Int16,
                                               ByVal Random As Boolean) As eSc2RanksBracket
      Dim Erg As eSc2RanksBracket

      Select Case Bracket
        Case 2
          Erg = eSc2RanksBracket._2V2
        Case 3
          Erg = eSc2RanksBracket._3V3
        Case 4
          Erg = eSc2RanksBracket._4V4
        Case Else
          Erg = eSc2RanksBracket._1V1
      End Select

      If Random Then Erg = Erg And eSc2RanksBracket.Random

      Return Erg
    End Function
  End Class
End Namespace
