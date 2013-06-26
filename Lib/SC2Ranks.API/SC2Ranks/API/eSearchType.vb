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

Namespace SC2Ranks.API
  ''' <summary>
  ''' Search Types
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract(Name := "search_type")>
  Public Enum eSearchType

    ''' <summary>
    ''' Exact search
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "exact")>
    Exact

    ''' <summary>
    ''' Contains name anywhere
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "contains")>
    Contains

    ''' <summary>
    ''' Start with name
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "starts")>
    StartsWith

    ''' <summary>
    ''' Ends with name
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "ends")>
    EndsWith
  End Enum
End Namespace