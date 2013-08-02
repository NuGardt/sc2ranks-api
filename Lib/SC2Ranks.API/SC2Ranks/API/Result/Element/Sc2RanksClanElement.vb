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
  Public Class Sc2RanksClanElement
    '"clan": {
    '  "url": "http://www.sc2ranks.com/clan/us/Monty",
    '  "tag": "Monty"
    '}

    Private m_Url As String
    Private m_Tag As String

    Public Sub New()
      Me.m_Url = Nothing
      Me.m_Tag = Nothing
    End Sub

    <DataMember(name := "url")>
    Public Property Url As String
      Get
        Return Me.m_Url
      End Get
      Private Set(ByVal Value As String)
        Me.m_Url = Value
      End Set
    End Property

    <DataMember(name := "tag")>
    Public Property Tag As String
      Get
        Return Me.m_Tag
      End Get
      Private Set(ByVal Value As String)
        Me.m_Tag = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("URL: {0}{1}", Me.Url, vbCrLf)
        Call .AppendFormat("Tag: {0}{1}", Me.Tag, vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace