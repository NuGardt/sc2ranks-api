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
  <DataContract()>
  Public Class Sc2RanksCustomDivisionTeamsResult
    Inherits Sc2RanksCustomDivisionResult

    Private m_Teams() As Sc2RanksTeamCharacterListElement

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Teams = Nothing
    End Sub

#Region "Properties"

    <DataMember(name := "teams")>
    Public Property Teams As Sc2RanksTeamCharacterListElement()
      Get
        Return Me.m_Teams
      End Get
      Private Set(ByVal Value As Sc2RanksTeamCharacterListElement())
        Me.m_Teams = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Teams: {0}", vbCrLf)
        If (Me.Teams IsNot Nothing) Then
          Dim dMax As Int32 = Me.Teams.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Team (#{0}): {1}{2}", i.ToString(), Me.Teams(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace