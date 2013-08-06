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
Imports System.Text

Namespace SC2Ranks.API.Result.Element
  <DataContract()>
  Public Class Sc2RanksCustomDivision
    '{
    '  "id": "51fc3c3b4970cf407e000004",
    '  "url": "http://www.sc2ranks.com/cdiv/51fc3c3b4970cf407e000004/foo",
    '  "name": "foo",
    '  "description": "bar",
    '  "members": 2,
    '  "public": false
    '}

    Private m_ID As String
    Private m_Url As String
    Private m_Name As String
    Private m_Description As String
    Private m_Members As Int32
    Private m_Public As Boolean

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_ID = Nothing
      Me.m_Url = Nothing
      Me.m_Name = Nothing
      Me.m_Description = Nothing
      Me.m_Members = Nothing
      Me.m_Public = Nothing
    End Sub

#Region "Properties"

    <DataMember(name := "id")>
    Public Property ID As String
      Get
        Return Me.m_ID
      End Get
      Private Set(ByVal Value As String)
        Me.m_ID = Value
      End Set
    End Property

    <DataMember(name := "url")>
    Public Property Url As String
      Get
        Return Me.m_Url
      End Get
      Private Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(name := "name")>
    Public Property Name As String
      Get
        Return Me.m_Name
      End Get
      Private Set(ByVal Value As String)
        Me.m_Name = Value
      End Set
    End Property

    <DataMember(name := "description")>
    Public Property Description As String
      Get
        Return Me.m_Description
      End Get
      Private Set(ByVal Value As String)
        Me.m_Description = Value
      End Set
    End Property

    <DataMember(name := "members")>
    Public Property MemberCount As Int32
      Get
        Return Me.m_Members
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Members = Value
      End Set
    End Property

    <DataMember(name := "public")>
    Public Property [Public] As Boolean
      Get
        Return Me.m_Public
      End Get
      Private Set(ByVal Value As Boolean)
        Me.m_Public = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("ID: {0}{1}", Me.ID, vbCrLf)
        Call .AppendFormat("URL: {0}{1}", Me.Url, vbCrLf)
        Call .AppendFormat("Name: {0}{1}", Me.Name, vbCrLf)
        Call .AppendFormat("Description: {0}{1}", Me.Description, vbCrLf)
        Call .AppendFormat("Members: {0}{1}", Me.MemberCount.ToString(), vbCrLf)
        Call .AppendFormat("Public: {0}{1}", Me.Public.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace