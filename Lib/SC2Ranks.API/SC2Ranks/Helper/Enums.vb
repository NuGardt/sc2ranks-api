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
Imports com.NuGardt.SC2Ranks.API
Imports System.Runtime.Serialization

Namespace SC2Ranks.Helper
  Public NotInheritable Class Enums
    Public Shared CustomDivisionActionBuffer As EnumBuffer(Of eCustomDivisionAction, EnumMemberAttribute) = New EnumBuffer(Of eCustomDivisionAction, EnumMemberAttribute)
    Public Shared SearchTypeBuffer As EnumBuffer(Of eSearchType, EnumMemberAttribute) = New EnumBuffer(Of eSearchType, EnumMemberAttribute)
    Public Shared RegionBuffer As EnumBuffer(Of eRegion, EnumMemberAttribute) = New EnumBuffer(Of eRegion, EnumMemberAttribute)
    Public Shared RegionNotationBuffer As EnumBuffer(Of eRegion, NotationAttribute) = New EnumBuffer(Of eRegion, NotationAttribute)
    Public Shared BracketBuffer As EnumBuffer(Of eBracket, EnumMemberAttribute) = New EnumBuffer(Of eBracket, EnumMemberAttribute)
    Public Shared BracketNotationBuffer As EnumBuffer(Of eBracket, NotationAttribute) = New EnumBuffer(Of eBracket, NotationAttribute)
    Public Shared RacesBuffer As EnumBuffer(Of eRace, EnumMemberAttribute) = New EnumBuffer(Of eRace, EnumMemberAttribute)
    Public Shared LeaguesBuffer As EnumBuffer(Of eLeague, EnumMemberAttribute) = New EnumBuffer(Of eLeague, EnumMemberAttribute)
    Public Shared ExpansionBuffer As EnumBuffer(Of eExpansion, EnumMemberAttribute) = New EnumBuffer(Of eExpansion, EnumMemberAttribute)
    Public Shared ExpansionNotationBuffer As EnumBuffer(Of eExpansion, NotationAttribute) = New EnumBuffer(Of eExpansion, NotationAttribute)

    Private Sub New()
      '-
    End Sub
  End Class
End Namespace
