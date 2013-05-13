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
  <DataContract()>
  Friend Class RequestCacheEntry
    Private m_Request As String
    Private m_Response As String
    Private m_CacheDate As Date
    Private m_CacheDuration As TimeSpan

    Private Sub New()
      '-
    End Sub

    Public Sub New(ByVal Request As String,
                   ByVal Response As String,
                   ByVal CacheDate As Date,
                   ByVal CacheDuration As TimeSpan)
      Me.m_Request = Request
      Me.m_Response = Response
      Me.m_CacheDate = CacheDate
      Me.m_CacheDuration = CacheDuration
    End Sub

    Public ReadOnly Property IsExpired As Boolean
      Get
        Return (DateTime.UtcNow > (Me.m_CacheDate + Me.m_CacheDuration))
      End Get
    End Property

    <DataMember(Name := "request")>
    Public Property Request() As String
      Get
        Return Me.m_Request
      End Get
      Private Set(ByVal Value As String)
        Me.m_Request = Value
      End Set
    End Property

    <DataMember(Name := "response")>
    Public Property Response() As String
      Get
        Return Me.m_Response
      End Get
      Private Set(ByVal Value As String)
        Me.m_Response = Value
      End Set
    End Property

    <DataMember(Name := "cache_date")>
    Public Property CacheDate() As DateTime
      Get
        Return Me.m_CacheDate
      End Get
      Private Set(ByVal Value As DateTime)
        Me.m_CacheDate = Value
      End Set
    End Property

    <DataMember(Name := "cache_duration")>
    Public Property CacheDuration() As TimeSpan
      Get
        Return Me.m_CacheDuration
      End Get
      Private Set(ByVal Value As TimeSpan)
        Me.m_CacheDuration = Value
      End Set
    End Property
  End Class
End Namespace