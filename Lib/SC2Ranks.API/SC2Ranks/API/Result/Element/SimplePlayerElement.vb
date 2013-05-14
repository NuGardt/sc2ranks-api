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
Namespace SC2Ranks.API.Result.Element
''' <summary>
'''   Class containing simple player information.
''' </summary>
''' <remarks></remarks>
  Public Class SimplePlayerElement
    Inherits BaseResult

    Public BattleNetID As Int32
    Public CharacterCode As Nullable(Of Int16)
    Public CharacterName As String
    Public Region As eRegion

    Private Sub New(ByVal CharacterName As String,
                    ByVal Region As eRegion,
                    ByVal CharacterCode As Int16)
      Me.CharacterName = CharacterName
      Me.Region = Region
      Me.CharacterCode = CharacterCode
    End Sub

    Private Sub New(ByVal CharacterName As String,
                    ByVal Region As eRegion,
                    ByVal BattleNetID As Int32)
      Me.CharacterName = CharacterName
      Me.Region = Region
      Me.BattleNetID = BattleNetID
      Me.CharacterCode = Nothing
    End Sub
    
    ''' <summary>
    '''   Creates a search info basesd on character code.
    ''' </summary>
    ''' <param name="Region">Region of the player. All may not be specified.</param>
    ''' <param name="CharacterName">Name of the character.</param>
    ''' <param name="CharacterCode">Character code. Not reliable when searching as character codes may not be set or are incorrect.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete("Not reliable with character codes. SC2Ranks may have incorrect or no character codes. Blizzard no longer provides these codes publicly.")>
    Public Shared Function CreateByCharacterCode(ByVal Region As eRegion,
                                                 ByVal CharacterName As String,
                                                 ByVal CharacterCode As Int16) As SimplePlayerElement
      Return New SimplePlayerElement(CharacterName := CharacterName, Region := Region, CharacterCode := CharacterCode)
    End Function
    
    ''' <summary>
    '''   Created a search info based on Battle.net ID.
    ''' </summary>
    ''' <param name="Region">Region of the player. All may not be specified.</param>
    ''' <param name="CharacterName">Name of the character.</param>
    ''' <param name="BattleNetID">Battle.net Unique Identifier.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateByBattleNetID(ByVal Region As eRegion,
                                               ByVal CharacterName As String,
                                               ByVal BattleNetID As Integer) As SimplePlayerElement
      Return New SimplePlayerElement(CharacterName := CharacterName, Region := Region, BattleNetID := BattleNetID)
    End Function
  End Class
End Namespace