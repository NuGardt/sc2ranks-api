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
Imports NuGardt.SC2Ranks.Helper
Imports System.Runtime.Serialization

Namespace SC2Ranks.API
  ''' <summary>
  ''' Bracket.
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum eSc2RanksBracket

    ''' <summary>
    ''' Random flag.
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("R")>
    Random = 1

    ''' <summary>
    ''' 1v1
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("1v1")>
    <EnumMember(Value := "1v1")>
    _1V1 = 2

    ''' <summary>
    ''' 2v2 Team
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("2v2")>
    <EnumMember(Value := "2v2t")>
    _2V2T = 4

    ''' <summary>
    ''' 2v2 Random Team
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("2v2R")>
    <EnumMember(Value := "2v2r")>
    _2V2R = _2V2T Or Random

    ''' <summary>
    ''' 3v3 Team
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("3v3T")>
    <EnumMember(Value := "3v3t")>
    _3V3T = 8

    ''' <summary>
    ''' 3v3 Random Team
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("3v3R")>
    <EnumMember(Value := "3v3r")>
    _3V3R = _3V3T Or Random

    ''' <summary>
    ''' 4v4 Team
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("4v4T")>
    <EnumMember(Value := "4v4t")>
    _4V4T = 16

    ''' <summary>
    ''' 4v4 Random Team
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("4v4R")>
    <EnumMember(Value := "4v4r")>
    _4V4R = _4V4T Or Random
  End Enum
End Namespace