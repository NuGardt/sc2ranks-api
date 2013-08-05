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
  <Obsolete()>
  Public Class CacheConfig
    Implements ICacheConfig

    Public Shared ReadOnly DefaultSearchBasePlayerCacheDuration As TimeSpan = TimeSpan.FromMinutes(30)
    Public Const DefaultSearchBasePlayerCacheDurationString As String = "00:30:00"
    Public Shared ReadOnly DefaultGetBasePlayerByCharacterCodeCacheDuration As TimeSpan = TimeSpan.FromHours(1)
    Public Const DefaultGetBasePlayerByCharacterCodeCacheDurationString As String = "01:00:00"
    Public Shared ReadOnly DefaultGetBasePlayerByBattleNetIDCacheDuration As TimeSpan = TimeSpan.FromHours(1)
    Public Const DefaultGetBasePlayerByBattleNetIDCacheDurationString As String = "01:00:00"
    Public Shared ReadOnly DefaultGetBaseTeamByCharacterCodeCacheDuration As TimeSpan = TimeSpan.FromHours(3)
    Public Const DefaultGetBaseTeamByCharacterCodeCacheDurationString As String = "03:00:00"
    Public Shared ReadOnly DefaultGetBaseTeamByBattleNetIDCacheDuration As TimeSpan = TimeSpan.FromHours(3)
    Public Const DefaultGetBaseTeamByBattleNetIDCacheDurationString As String = "03:00:00"
    Public Shared ReadOnly DefaultGetTeamByCharacterCodeCacheDuration As TimeSpan = TimeSpan.FromHours(6)
    Public Const DefaultGetTeamByCharacterCodeCacheDurationString As String = "06:00:00"
    Public Shared ReadOnly DefaultGetTeamByBattleNetIDCacheDuration As TimeSpan = TimeSpan.FromHours(6)
    Public Const DefaultGetTeamByBattleNetIDCacheDurationString As String = "06:00:00"
    Public Shared ReadOnly DefaultGetCustomDivisionCacheDuration As TimeSpan = TimeSpan.FromHours(12)
    Public Const DefaultGetCustomDivisionCacheDurationString As String = "12:00:00"
    Public Shared ReadOnly DefaultGetBasePlayersCharCacheDuration As TimeSpan = TimeSpan.FromHours(1)
    Public Const DefaultGetBasePlayersCharCacheDurationString As String = "01:00:00"
    Public Shared ReadOnly DefaultGetBasePlayersTeamCacheDuration As TimeSpan = TimeSpan.FromHours(3)
    Public Const DefaultGetBasePlayersTeamCacheDurationString As String = "03:00:00"
    Public Shared ReadOnly DefaultGetBonusPoolsCacheDuration As TimeSpan = TimeSpan.FromHours(0)
    Public Const DefaultGetBonusPoolsCacheDurationString As String = "00:00:00"

    Private m_SearchBasePlayerCacheDuration As TimeSpan
    Private m_GetBasePlayerByCharacterCodeCacheDuration As TimeSpan
    Private m_GetBasePlayerByBattleNetIDCacheDuration As TimeSpan
    Private m_GetBaseTeamByCharacterCodeCacheDuration As TimeSpan
    Private m_GetBaseTeamByBattleNetIDCacheDuration As TimeSpan
    Private m_GetTeamByCharacterCodeCacheDuration As TimeSpan
    Private m_GetTeamByBattleNetIDCacheDuration As TimeSpan
    Private m_GetCustomDivisionCacheDuration As TimeSpan
    Private m_GetBasePlayersCharCacheDuration As TimeSpan
    Private m_GetBasePlayersTeamCacheDuration As TimeSpan
    Private m_GetBonusPoolsCacheDuration As TimeSpan

    Public Sub New()
      Me.m_SearchBasePlayerCacheDuration = DefaultSearchBasePlayerCacheDuration
      Me.m_GetBasePlayerByCharacterCodeCacheDuration = DefaultGetBasePlayerByCharacterCodeCacheDuration
      Me.m_GetBasePlayerByBattleNetIDCacheDuration = DefaultGetBasePlayerByBattleNetIDCacheDuration
      Me.m_GetBaseTeamByCharacterCodeCacheDuration = DefaultGetBaseTeamByCharacterCodeCacheDuration
      Me.m_GetBaseTeamByBattleNetIDCacheDuration = DefaultGetBaseTeamByBattleNetIDCacheDuration
      Me.m_GetTeamByCharacterCodeCacheDuration = DefaultGetTeamByCharacterCodeCacheDuration
      Me.m_GetTeamByBattleNetIDCacheDuration = DefaultGetTeamByBattleNetIDCacheDuration
      Me.m_GetCustomDivisionCacheDuration = DefaultGetCustomDivisionCacheDuration
      Me.m_GetBasePlayersCharCacheDuration = DefaultGetBasePlayersCharCacheDuration
      Me.m_GetBasePlayersTeamCacheDuration = DefaultGetBasePlayersTeamCacheDuration
      Me.m_GetBonusPoolsCacheDuration = DefaultGetBonusPoolsCacheDuration
    End Sub

#Region "Properties"

    Public Property GetBasePlayerByBattleNetIDCacheDuration As TimeSpan Implements ICacheConfig.GetBasePlayerByBattleNetIDCacheDuration
      Get
        Return Me.m_GetBasePlayerByBattleNetIDCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBasePlayerByBattleNetIDCacheDuration = Value
      End Set
    End Property

    Public Property GetBasePlayerByCharacterCodeCacheDuration As TimeSpan Implements ICacheConfig.GetBasePlayerByCharacterCodeCacheDuration
      Get
        Return Me.m_GetBasePlayerByCharacterCodeCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBasePlayerByCharacterCodeCacheDuration = Value
      End Set
    End Property

    Public Property GetBasePlayersCharCacheDuration As TimeSpan Implements ICacheConfig.GetBasePlayersCharCacheDuration
      Get
        Return Me.m_GetBasePlayersCharCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBasePlayersCharCacheDuration = Value
      End Set
    End Property

    Public Property GetBasePlayersTeamCacheDuration As TimeSpan Implements ICacheConfig.GetBasePlayersTeamCacheDuration
      Get
        Return Me.m_GetBasePlayersTeamCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBasePlayersTeamCacheDuration = Value
      End Set
    End Property

    Public Property GetBaseTeamByCharacterCodeCacheDuration As TimeSpan Implements ICacheConfig.GetBaseTeamByCharacterCodeCacheDuration
      Get
        Return Me.m_GetBaseTeamByCharacterCodeCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBaseTeamByCharacterCodeCacheDuration = Value
      End Set
    End Property

    Public Property GetBaseTeamByBattleNetIDCacheDuration As TimeSpan Implements ICacheConfig.GetBaseTeamByBattleNetIDCacheDuration
      Get
        Return Me.m_GetBaseTeamByBattleNetIDCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBaseTeamByBattleNetIDCacheDuration = Value
      End Set
    End Property

    Public Property GetBonusPoolsCacheDuration As TimeSpan Implements ICacheConfig.GetBonusPoolsCacheDuration
      Get
        Return Me.m_GetBonusPoolsCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetBonusPoolsCacheDuration = Value
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

    Public Property GetTeamByBattleNetIDCacheDuration As TimeSpan Implements ICacheConfig.GetTeamByBattleNetIDCacheDuration
      Get
        Return Me.m_GetTeamByBattleNetIDCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetTeamByBattleNetIDCacheDuration = Value
      End Set
    End Property

    Public Property GetTeamByCharacterCodeCacheDuration As TimeSpan Implements ICacheConfig.GetTeamByCharacterCodeCacheDuration
      Get
        Return Me.m_GetTeamByCharacterCodeCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_GetTeamByCharacterCodeCacheDuration = Value
      End Set
    End Property

    Public Property SearchBasePlayerCacheDuration As TimeSpan Implements ICacheConfig.SearchBasePlayerCacheDuration
      Get
        Return Me.m_SearchBasePlayerCacheDuration
      End Get
      Set(ByVal Value As TimeSpan)
        Me.m_SearchBasePlayerCacheDuration = Value
      End Set
    End Property

#End Region
  End Class
End Namespace