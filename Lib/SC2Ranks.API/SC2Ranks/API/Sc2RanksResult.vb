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
''' <summary>
'''   This class containt the result and extra informatiom such as cache properties and raw response.
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
  Public NotInheritable Class Sc2RanksResult(Of T As Class)
    Private m_Result As T
    Private m_FromCache As Boolean
    Private m_CacheDate As DateTime
    Private m_CacheDuration As TimeSpan
    Private m_ResponseRaw As String

    Public Sub New()
      Me.m_Result = Nothing
      Me.m_FromCache = Nothing
      Me.m_CacheDate = Nothing
      Me.m_CacheDuration = Nothing
      Me.m_ResponseRaw = Nothing
    End Sub

    Public Property Result() As T
      Get
        Return Me.m_Result
      End Get
      Friend Set(ByVal Value As T)
        Me.m_Result = Value
      End Set
    End Property

    Public Property FromCache As Boolean
      Get
        Return Me.m_FromCache
      End Get
      Friend Set(ByVal Value As Boolean)
        Me.m_FromCache = Value
      End Set
    End Property

    Public Property CacheDate As DateTime
      Get
        Return Me.m_CacheDate
      End Get
      Friend Set(ByVal Value As DateTime)
        Me.m_CacheDate = Value
      End Set
    End Property

    Public Property CacheDuration As TimeSpan
      Get
        Return Me.m_CacheDuration
      End Get
      Friend Set(ByVal Value As TimeSpan)
        Me.m_CacheDuration = Value
      End Set
    End Property

    Public Property ResponseRaw As String
      Get
        Return Me.m_ResponseRaw
      End Get
      Friend Set(ByVal Value As String)
        Me.m_ResponseRaw = Value
      End Set
    End Property
  End Class
End Namespace