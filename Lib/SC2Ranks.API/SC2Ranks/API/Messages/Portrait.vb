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

Namespace SC2Ranks.API.Messages
''' <summary>
'''   Class containing portrait information.
''' </summary>
''' <remarks></remarks>
  <DataContract(Name := "portrait")>
  Public Class Portrait
    Private m_IconID As Int16
    Private m_Row As Int16
    Private m_Column As Int16
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Private Sub New()
      Me.m_IconID = Nothing
      Me.m_Row = Nothing
      Me.m_Column = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "icon_id")>
    Public Property IconID() As Int16
      Get
        Return Me.m_IconID
      End Get
      Private Set(ByVal Value As Int16)
        Me.m_IconID = Value
      End Set
    End Property

    <DataMember(Name := "row")>
    Public Property Row() As Int16
      Get
        Return Me.m_Row
      End Get
      Private Set(ByVal Value As Int16)
        Me.m_Row = Value
      End Set
    End Property

    <DataMember(Name := "column")>
    Public Property Column() As Int16
      Get
        Return Me.m_Column
      End Get
      Private Set(ByVal Value As Int16)
        Me.m_Column = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Icon ID: {0}{1}", Me.IconID.ToString(), vbCrLf)
        Call .AppendFormat("Row: {0}{1}", Me.Row.ToString(), vbCrLf)
        Call .AppendFormat("Column: {0}{1}", Me.Column.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace