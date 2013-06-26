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
Imports NuGardt.SC2Ranks.API.Result.Element
Imports System.Text

Namespace SC2Ranks.API.Result
  ''' <summary>
  ''' Class containing the result of a player search.
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract()>
  Public Class SearchPlayerResult
    Inherits BaseResult

    Private m_Characters() As SearchPlayer
    Private m_Total As Int32

    ''' <summary>
    ''' Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Private Sub New()
      Me.m_Characters = Nothing
      Me.m_Total = Nothing
    End Sub

#Region "Properties"

    ''' <summary>
    ''' Returns the players found.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Only cotains a maximum of 10 players. Call using ResultOffset to get the next batch of players.</remarks>
    <DataMember(Name := "characters", IsRequired := False, EmitDefaultValue := False)>
    Public Property Members() As SearchPlayer()
      Get
        Return Me.m_Characters
      End Get
      Private Set(ByVal Value As SearchPlayer())
        Me.m_Characters = Value
      End Set
    End Property

    ''' <summary>
    ''' Return the number of players found.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "total")>
    Public Property Total() As Int32
      Get
        Return Me.m_Total
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Total = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        If (Me.Members IsNot Nothing) Then
          Dim dMax As Int32 = Me.Members.Length - 1
          For d As Int32 = 0 To dMax
            Call .AppendFormat("Characters (#{0}): {1}{2}", d.ToString(), Me.Members(d).ToString(), vbCrLf)
          Next d
        End If
        Call .AppendFormat("Total: {0}{1}", Me.Total.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace
