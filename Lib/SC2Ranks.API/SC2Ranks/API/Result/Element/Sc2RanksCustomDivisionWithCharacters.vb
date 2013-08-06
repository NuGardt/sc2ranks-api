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
  Public Class Sc2RanksCustomDivisionWithCharacters
    Inherits Sc2RanksCustomDivision

    Private m_Characters() As Sc2RanksCharacterExtended

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Characters = Nothing
    End Sub

#Region "Properties"

    <DataMember(name := "characters")>
    Public Property Characters As Sc2RanksCharacterExtended()
      Get
        Return Me.m_Characters
      End Get
      Private Set(ByVal Value As Sc2RanksCharacterExtended())
        Me.m_Characters = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Characters: {0}", vbCrLf)
        If (Me.Characters IsNot Nothing) Then
          Dim dMax As Int32 = Me.Characters.Count - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Character (#{0}): {1}{2}", i.ToString(), Me.Characters(i).ToString, vbCrLf)
          Next i
        End If
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace