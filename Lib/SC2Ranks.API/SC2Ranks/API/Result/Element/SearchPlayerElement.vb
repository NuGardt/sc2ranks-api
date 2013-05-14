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
''' <summary>
'''   Class conatining information of found players.
''' </summary>
''' <remarks></remarks>
  <DataContract()>
  Public Class SearchPlayer
    Private m_BattleNetID As Integer
    Private m_CharacterName As String
    
    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Private Sub New()
      Me.m_BattleNetID = Nothing
      Me.m_CharacterName = Nothing
    End Sub

#Region "Properties"
    
    ''' <summary>
    '''   Returns the Battle.net identifier.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "bnet_id")>
    Protected Property BattleNetID() As Int32
      Get
        Return Me.m_BattleNetID
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_BattleNetID = Value
      End Set
    End Property
    
    ''' <summary>
    '''   Return the character name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "name")>
    Public Property CharacterName() As String
      Get
        Return Me.m_CharacterName
      End Get
      Private Set(ByVal Value As String)
        Me.m_CharacterName = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Character Name: {0}{1}", Me.CharacterName, vbCrLf)
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace
