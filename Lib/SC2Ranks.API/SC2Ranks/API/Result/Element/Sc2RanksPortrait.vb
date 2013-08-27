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
  Public Class Sc2RanksPortrait
    'portrait: {
    '  "file": 1,
    '  "x": -2,
    '  "y": -5
    '}

    Private m_File As Int32
    Private m_X As Int32
    Private m_Y As Int32

    Public Sub New()
      Me.m_File = Nothing
      Me.m_X = Nothing
    End Sub

    <DataMember(name := "file")>
    Public Property File As Int32
      Get
        Return Me.m_File
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_File = Value
      End Set
    End Property

    <DataMember(name := "x")>
    Public Property X As Int32
      Get
        Return Me.m_X
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_X = Value
      End Set
    End Property

    <DataMember(name := "y")>
    Public Property Y As Int32
      Get
        Return Me.m_Y
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Y = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("File: {0}{1}", Me.File, vbCrLf)
        Call .AppendFormat("X: {0}{1}", Me.X.ToString, vbCrLf)
        Call .AppendFormat("Y: {0}{1}", Me.Y.ToString, vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace