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
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API
''' <summary>
'''   Leagues
''' </summary>
''' <remarks></remarks>
  <DataContract(Name := "league")>
  Public Enum eLeague
  
  ''' <summary>
  '''   Bronze league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "bronze")> _
      <Notation("Bronze")>
    Bronze = 1
  
  ''' <summary>
  '''   Silver league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "silver")> _
      <Notation("Silver")>
    Silver = 2
  
  ''' <summary>
  '''   Gold league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "gold")> _
      <Notation("Gold")>
    Gold = 3
  
  ''' <summary>
  '''   Platinum league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "platinum")> _
      <Notation("Platinum")>
    Platinum = 4
  
  ''' <summary>
  '''   Diamond league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "diamond")> _
      <Notation("Diamond")>
    Diamond = 5
  
  ''' <summary>
  '''   Master league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "master")> _
      <Notation("Master")>
    Master = 6
  
  ''' <summary>
  '''   Grandmaster league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "grandmaster")> _
      <Notation("Grandmaster")>
    GrandMaster = 7
  End Enum
End Namespace