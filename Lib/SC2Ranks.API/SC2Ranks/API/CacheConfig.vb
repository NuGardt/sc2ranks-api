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
  Public Class CacheConfig
    Implements ICacheConfig

    Public Shared ReadOnly DefaultGetBaseDataCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetBaseDataCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCharacterCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCharacterCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCharacterTeamsListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCharacterTeamsListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultSearchCharacterTeamListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultSearchCharacterTeamListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCharacterListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCharacterListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCharacterTeamListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCharacterTeamListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetClanCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetClanCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetClanCharacterListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetClanCharacterListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetClanTeamListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetClanTeamListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetRankingsTopCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetRankingsTopCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetDivisionsTopCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetDivisionsTopCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetDivisionCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetDivisionCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetDivisionTeamsTopCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetDivisionTeamsTopCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCustomDivisionsCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCustomDivisionsCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCustomDivisionCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCustomDivisionCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCustomDivisionTeamListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCustomDivisionTeamListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultGetCustomDivisionCharacterListCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultGetCustomDivisionCharacterListCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultCustomDivisionAddCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultCustomDivisionAddCacheDurationString As String = "02:00:00"
    Public Shared ReadOnly DefaultCustomDivisionRemoveCacheDuration As TimeSpan = TimeSpan.FromHours(2)
    Public Const DefaultCustomDivisionRemoveCacheDurationString As String = "02:00:00"

    Private m_GetBaseDataCacheDuration As TimeSpan
    Private m_GetCharacterCacheDuration As TimeSpan
    Private m_GetCharacterTeamsListCacheDuration As TimeSpan
    Private m_SearchCharacterTeamListCacheDuration As TimeSpan
    Private m_GetCharacterListCacheDuration As TimeSpan
    Private m_GetCharacterTeamListCacheDuration As TimeSpan
    Private m_GetClanCacheDuration As TimeSpan
    Private m_GetClanCharacterListCacheDuration As TimeSpan
    Private m_GetClanTeamListCacheDuration As TimeSpan
    Private m_GetRankingsTopCacheDuration As TimeSpan
    Private m_GetDivisionsTopCacheDuration As TimeSpan
    Private m_GetDivisionCacheDuration As TimeSpan
    Private m_GetDivisionTeamsTopCacheDuration As TimeSpan
    Private m_GetCustomDivisionsCacheDuration As TimeSpan
    Private m_GetCustomDivisionCacheDuration As TimeSpan
    Private m_GetCustomDivisionTeamListCacheDuration As TimeSpan
    Private m_GetCustomDivisionCharacterListCacheDuration As TimeSpan
    Private m_CustomDivisionAddCacheDuration As TimeSpan
    Private m_CustomDivisionRemoveCacheDuration As TimeSpan

    Public Sub New()
      Me.m_GetBaseDataCacheDuration = DefaultGetBaseDataCacheDuration
      Me.m_GetCharacterCacheDuration = DefaultGetCharacterCacheDuration
      Me.m_GetCharacterTeamsListCacheDuration = DefaultGetCharacterTeamsListCacheDuration
      Me.m_SearchCharacterTeamListCacheDuration = DefaultSearchCharacterTeamListCacheDuration
      Me.m_GetCharacterListCacheDuration = DefaultGetCharacterListCacheDuration
      Me.m_GetCharacterTeamListCacheDuration = DefaultGetCharacterTeamListCacheDuration
      Me.m_GetClanCacheDuration = DefaultGetClanCacheDuration
      Me.m_GetClanCharacterListCacheDuration = DefaultGetClanCharacterListCacheDuration
      Me.m_GetClanTeamListCacheDuration = DefaultGetClanTeamListCacheDuration
      Me.m_GetRankingsTopCacheDuration = DefaultGetRankingsTopCacheDuration
      Me.m_GetDivisionsTopCacheDuration = DefaultGetDivisionsTopCacheDuration
      Me.m_GetDivisionCacheDuration = DefaultGetDivisionCacheDuration
      Me.m_GetDivisionTeamsTopCacheDuration = DefaultGetDivisionTeamsTopCacheDuration
      Me.m_GetCustomDivisionsCacheDuration = DefaultGetCustomDivisionsCacheDuration
      Me.m_GetCustomDivisionCacheDuration = DefaultGetCustomDivisionCacheDuration
      Me.m_GetCustomDivisionTeamListCacheDuration = DefaultGetCustomDivisionTeamListCacheDuration
      Me.m_GetCustomDivisionCharacterListCacheDuration = DefaultGetCustomDivisionCharacterListCacheDuration
      Me.m_CustomDivisionAddCacheDuration = DefaultCustomDivisionAddCacheDuration
      Me.m_CustomDivisionRemoveCacheDuration = DefaultCustomDivisionRemoveCacheDuration
    End Sub

#Region "Properties"

    Public Property GetBaseDataCacheDuration As TimeSpan Implements ICacheConfig.GetBaseDataCacheDuration
      Get
        Return Me.m_GetBaseDataCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBaseDataCacheDuration = Value
      End Set
    End Property

    Public Property GetCharacterCacheDuration As TimeSpan Implements ICacheConfig.GetCharacterCacheDuration
      Get
        Return Me.m_GetCharacterCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCharacterCacheDuration = Value
      End Set
    End Property

    Public Property GetCharacterTeamsListCacheDuration As TimeSpan Implements ICacheConfig.GetCharacterTeamsListCacheDuration
      Get
        Return Me.m_GetCharacterTeamsListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCharacterTeamsListCacheDuration = Value
      End Set
    End Property

    Public Property SearchCharacterTeamListCacheDuration As TimeSpan Implements ICacheConfig.SearchCharacterTeamListCacheDuration
      Get
        Return Me.m_SearchCharacterTeamListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_SearchCharacterTeamListCacheDuration = Value
      End Set
    End Property

    Public Property GetCharacterListCacheDuration As TimeSpan Implements ICacheConfig.GetCharacterListCacheDuration
      Get
        Return Me.m_GetCharacterListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCharacterListCacheDuration = Value
      End Set
    End Property

    Public Property GetCharacterTeamListCacheDuration As TimeSpan Implements ICacheConfig.GetCharacterTeamListCacheDuration
      Get
        Return Me.m_GetCharacterTeamListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCharacterTeamListCacheDuration = Value
      End Set
    End Property

    Public Property GetClanCacheDuration As TimeSpan Implements ICacheConfig.GetClanCacheDuration
      Get
        Return Me.m_GetClanCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetClanCacheDuration = Value
      End Set
    End Property

    Public Property GetClanCharacterListCacheDuration As TimeSpan Implements ICacheConfig.GetClanCharacterListCacheDuration
      Get
        Return Me.m_GetClanCharacterListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetClanCharacterListCacheDuration = Value
      End Set
    End Property

    Public Property GetClanTeamListCacheDuration As TimeSpan Implements ICacheConfig.GetClanTeamListCacheDuration
      Get
        Return Me.m_GetClanTeamListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetClanTeamListCacheDuration = Value
      End Set
    End Property

    Public Property GetRankingsTopCacheDuration As TimeSpan Implements ICacheConfig.GetRankingsTopCacheDuration
      Get
        Return Me.m_GetRankingsTopCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetRankingsTopCacheDuration = Value
      End Set
    End Property

    Public Property GetDivisionsTopCacheDuration As TimeSpan Implements ICacheConfig.GetDivisionsTopCacheDuration
      Get
        Return Me.m_GetDivisionsTopCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetDivisionsTopCacheDuration = Value
      End Set
    End Property

    Public Property GetDivisionCacheDuration As TimeSpan Implements ICacheConfig.GetDivisionCacheDuration
      Get
        Return Me.m_GetDivisionCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetDivisionCacheDuration = Value
      End Set
    End Property

    Public Property GetDivisionTeamsTopCacheDuration As TimeSpan Implements ICacheConfig.GetDivisionTeamsTopCacheDuration
      Get
        Return Me.m_GetDivisionTeamsTopCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetDivisionTeamsTopCacheDuration = Value
      End Set
    End Property

    Public Property GetCustomDivisionsCacheDuration As TimeSpan Implements ICacheConfig.GetCustomDivisionsCacheDuration
      Get
        Return Me.m_GetCustomDivisionsCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCustomDivisionsCacheDuration = Value
      End Set
    End Property

    Public Property GetCustomDivisionCacheDuration As TimeSpan Implements ICacheConfig.GetCustomDivisionCacheDuration
      Get
        Return Me.m_GetCustomDivisionCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCustomDivisionCacheDuration = Value
      End Set
    End Property

    Public Property GetCustomDivisionTeamListCacheDuration As TimeSpan Implements ICacheConfig.GetCustomDivisionTeamListCacheDuration
      Get
        Return Me.m_GetCustomDivisionTeamListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCustomDivisionTeamListCacheDuration = Value
      End Set
    End Property

    Public Property GetCustomDivisionCharacterListCacheDuration As TimeSpan Implements ICacheConfig.GetCustomDivisionCharacterListCacheDuration
      Get
        Return Me.m_GetCustomDivisionCharacterListCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetCustomDivisionCharacterListCacheDuration = Value
      End Set
    End Property

    Public Property CustomDivisionAddCacheDuration As TimeSpan Implements ICacheConfig.CustomDivisionAddCacheDuration
      Get
        Return Me.m_CustomDivisionAddCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_CustomDivisionAddCacheDuration = Value
      End Set
    End Property

    Public Property CustomDivisionRemoveCacheDuration As TimeSpan Implements ICacheConfig.CustomDivisionRemoveCacheDuration
      Get
        Return Me.m_CustomDivisionRemoveCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_CustomDivisionRemoveCacheDuration = Value
      End Set
    End Property

#End Region
  End Class
End Namespace