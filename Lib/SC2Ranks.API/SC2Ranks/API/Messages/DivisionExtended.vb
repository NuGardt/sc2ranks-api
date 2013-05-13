﻿' NuGardt SC2Ranks API
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
Imports System.ComponentModel
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API.Messages
''' <summary>
'''   Class containing team information for the division.
''' </summary>
''' <remarks></remarks>
  <DataContract()>
  Public Class DivisionExtended
    Inherits Division

    Private m_ID As Integer
    Private m_FavouriteRaceRaw As String
    Private m_RegionRank As Integer
    Private m_UpdatedAtRaw As String
    Private m_WorldRank As Integer
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Call MyBase.New()

      Me.m_ID = Nothing
      Me.m_FavouriteRaceRaw = Nothing
      Me.m_RegionRank = Nothing
      Me.m_UpdatedAtRaw = Nothing
      Me.m_WorldRank = Nothing
    End Sub

#Region "Properties"
    
    ''' <summary>
    '''   Returns the SC2Ranks identifier.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "id", IsRequired := False, EmitDefaultValue := False)>
    <DefaultValue(0)>
    Public Property ID() As Integer
      Get
        Return Me.m_ID
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_ID = Value
      End Set
    End Property

    <DataMember(Name := "fav_race")>
    Protected Property FavouriteRaceRaw() As String
      Get
        Return Me.m_FavouriteRaceRaw
      End Get
      Set(ByVal Value As String)
        Me.m_FavouriteRaceRaw = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the favourite race of the player.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FavouriteRace() As eRace
      Get
        Return Enums.RacesBuffer.GetEnum(FavouriteRaceRaw)
      End Get
    End Property
    
    ''' <summary>
    '''   Returns the region rank of the current league.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "region_rank")>
    Public Property RegionRank() As Integer
      Get
        Return Me.m_RegionRank
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_RegionRank = Value
      End Set
    End Property

    <DataMember(Name := "updated_at")>
    Protected Property UpdatedAtRaw() As String
      Get
        Return Me.m_UpdatedAtRaw
      End Get
      Set(ByVal Value As String)
        Me.m_UpdatedAtRaw = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Return the last time the profile was updated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UpdatedAt() As DateTimeOffset
      Get
        Return DateTimeOffset.Parse(UpdatedAtRaw)
      End Get
    End Property
    
    ''' <summary>
    '''   Returns of the world rank in the current league.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "world_rank")>
    Public Property WorldRank() As Integer
      Get
        Return Me.m_WorldRank
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_WorldRank = Value
      End Set
    End Property

#End Region
  End Class
End Namespace