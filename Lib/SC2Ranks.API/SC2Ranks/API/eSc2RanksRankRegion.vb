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
  ''' Rank Regions.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract(Name := "rank_region")>
  Public Enum eSc2RanksRankRegion
    ''' <summary>
    ''' All Regions
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "global")>
    <Notation("Global")>
    [Global]

    ''' <summary>
    ''' European Union
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "eu")>
    <Notation("Europe")>
    EU

    ''' <summary>
    ''' South-East-Asia
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "sea")>
    <Notation("South-East-Asia")>
    SEA

    ''' <summary>
    ''' China
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "cn")>
    <Notation("China")>
    CN

    ''' <summary>
    ''' Korea/Taiwan
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "fea")>
    <Notation("Korea/Taiwan")>
    Fea

    ''' <summary>
    ''' Americas
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "am")>
    <Notation("Americas")>
    AM
  End Enum
End Namespace