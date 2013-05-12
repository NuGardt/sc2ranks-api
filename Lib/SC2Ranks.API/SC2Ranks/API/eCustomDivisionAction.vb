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
'''   Action for manage division.
''' </summary>
''' <remarks></remarks>
  <DataContract(Name := "action")>
  Public Enum eCustomDivisionAction
  
  ''' <summary>
  '''   Add one or more players.
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "add")>
    Add
  
  ''' <summary>
  '''   Remove one or more players.
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "remove")>
    Remove
  End Enum
End Namespace