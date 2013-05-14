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

Namespace SC2Ranks.API.Result
''' <summary>
'''   This class contains extra result data.
''' </summary>
''' <remarks></remarks>
  <DataContract()>
  Public MustInherit Class BaseResult
    Private m_Error As String
    Private m_CacheExpires As Nullable(Of DateTime)
    Private m_ResponseRaw As String
    
    ''' <summary>
    '''   Constructor.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
      Me.m_Error = Nothing
      Me.m_CacheExpires = Nothing
      Me.m_ResponseRaw = Nothing
    End Sub

#Region "Properties"
    
    ''' <summary>
    '''   Returns the error message if an error has occured.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "error", EmitDefaultValue := False)>
    Public Property [Error]() As String
      Get
        Return Me.m_Error
      End Get
      Private Set(ByVal Value As String)
        Me.m_Error = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns <c>True</c> if an error has occured, otherwise <c>False</c>.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <IgnoreDataMember()>
    Public ReadOnly Property HasError As Boolean
      Get
        Return (Not String.IsNullOrEmpty(Me.m_Error))
      End Get
    End Property
    
    ''' <summary>
    '''   Returns the date when the cached data expires. The cache is from the API and not SC2Ranks.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <IgnoreDataMember()>
    Public Property CacheExpires As Nullable(Of DateTime)
      Get
        Return Me.m_CacheExpires
      End Get
      Friend Set(ByVal Value As Nullable(Of DateTime))
        Me.m_CacheExpires = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Returns the raw response from the server. Usually in JSON format.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <IgnoreDataMember()>
    Public Property ResponseRaw As String
      Get
        Return Me.m_ResponseRaw
      End Get
      Friend Set(ByVal Value As String)
        Me.m_ResponseRaw = Value
      End Set
    End Property

#End Region
  End Class
End Namespace