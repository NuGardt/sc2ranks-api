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

Namespace SC2Ranks.API
  ''' <summary>
  ''' Races
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract(Name := "race")>
  Public Enum eSc2RanksRace

    ''' <summary>
    ''' Protoss
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "protoss")>
    <Notation("Protoss")>
    Protoss = 1

    ''' <summary>
    ''' Terran
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "terran")>
    <Notation("Terran")>
    Terran = 2

    ''' <summary>
    ''' Zerg
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "zerg")>
    <Notation("Zerg")>
    Zerg = 3

    ''' <summary>
    ''' Random
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "random")>
    <Notation("Random")>
    Random
  End Enum
End Namespace