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

Namespace SC2Ranks.API.Messages
  <DataContract()>
  Public Class BonusPool
    Protected m_Fea As Integer
    Protected m_Us As Integer
    Protected m_Am As Integer
    Protected m_Ru As Integer
    Protected m_Sea As Integer
    Protected m_La As Integer
    Protected m_Cn As Integer
    Protected m_Tw As Integer
    Protected m_Eu As Integer
    Protected m_Kr As Integer
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Fea = Nothing
      Me.m_Us = Nothing
      Me.m_Am = Nothing
      Me.m_Ru = Nothing
      Me.m_Sea = Nothing
      Me.m_La = Nothing
      Me.m_Cn = Nothing
      Me.m_Tw = Nothing
      Me.m_Eu = Nothing
      Me.m_Kr = Nothing
    End Sub

#Region "Properties"
    
    ''' <summary>
    '''   FEA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "fea")>
    Public Property Fea() As Integer
      Get
        Return Me.m_Fea
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Fea = Value
      End Set
    End Property
    
    ''' <summary>
    '''   United States
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete()>
    <DataMember(Name := "us")>
    Public Property US() As Integer
      Get
        Return Me.m_Us
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Us = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Americas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "am")>
    Public Property Am() As Integer
      Get
        Return Me.m_Am
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Am = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Russia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete()>
    <DataMember(Name := "ru")>
    Public Property Ru() As Integer
      Get
        Return Me.m_Ru
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Ru = Value
      End Set
    End Property
    
    ''' <summary>
    '''   South-East-Asia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "sea")>
    Public Property Sea() As Integer
      Get
        Return Me.m_Sea
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Sea = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Latin America
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete()>
    <DataMember(Name := "la")>
    Public Property La() As Integer
      Get
        Return Me.m_La
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_La = Value
      End Set
    End Property
    
    ''' <summary>
    '''   China
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "cn")>
    Public Property Cn() As Integer
      Get
        Return Me.m_Cn
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Cn = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Taiwan
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "tw")>
    Public Property Tw() As Integer
      Get
        Return Me.m_Tw
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Tw = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Europe
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "eu")>
    Public Property Eu() As Integer
      Get
        Return Me.m_Eu
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Eu = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Korea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "kr")>
    Public Property Kr() As Integer
      Get
        Return Me.m_Kr
      End Get
      Private Set(ByVal Value As Integer)
        Me.m_Kr = Value
      End Set
    End Property

#End Region
  End Class
End Namespace