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
Imports System.Security.Cryptography
Imports System.Text

Namespace SC2Ranks.API.Cache
  <DataContract()>
  Friend Class CacheRequestEntry
    Private Shared ReadOnly MD5 As New MD5CryptoServiceProvider

    Private m_Url As String
    Private m_Post As String

    Private m_Response As String

    Private m_Expires As Date

    Private m_Hash As Guid

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()
      Me.m_Url = Nothing
      Me.m_Post = Nothing

      Me.m_Response = Nothing

      Me.m_Expires = Nothing
    End Sub

    Public Sub New(ByVal Url As String,
                   ByVal Post As String,
                   ByVal Response As String,
                   ByVal Expires As Date)
      Me.m_Url = Url
      Me.m_Post = Post

      Me.m_Response = Response

      Me.m_Expires = Expires

      Me.m_Hash = GetHash(Url, Post)
    End Sub

    Public Shared Function GetHash(ByVal Url As String,
                                   ByVal Post As String) As Guid
      Return New Guid(MD5.ComputeHash(Encoding.UTF8.GetBytes(Url + Post)))
    End Function

#Region "Properties"

    <DataMember(Name := "hash")>
    Public Property Hash As Guid
      Get
        Return Me.m_Hash
      End Get
      Private Set(ByVal Value As Guid)
        Me.m_Hash = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public ReadOnly Property IsExpired As Boolean
      Get
        Return (DateTime.UtcNow > Me.m_Expires)
      End Get
    End Property

    <DataMember(Name := "url")>
    Public Property URL() As String
      Get
        Return Me.m_Url
      End Get
      Private Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(Name := "post")>
    Public Property Post() As String
      Get
        Return Me.m_Post
      End Get
      Private Set(ByVal Value As String)
        Me.m_Post = Value
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

    <DataMember(Name := "expires")>
    Public Property Expires() As DateTime
      Get
        Return Me.m_Expires
      End Get
      Private Set(ByVal Value As DateTime)
        Me.m_Expires = Value
      End Set
    End Property

#End Region
  End Class
End Namespace