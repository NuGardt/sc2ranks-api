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

Namespace SC2Ranks.API
  <DataContract()>
  Public Class Sc2RanksBulkCharacter
    Public Region As eSc2RanksRegion
    Public BattleNetID As Int64

    Public Sub New()
      Me.Region = Nothing
      Me.BattleNetID = Nothing
    End Sub

    Public Sub New(ByVal Region As eSc2RanksRegion,
                   ByVal BattleNetID As Int64)
      Me.Region = Region
      Me.BattleNetID = BattleNetID
    End Sub

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Region: {0}{1}", Me.Region.ToString(), vbCrLf)
        Call .AppendFormat("Battle.net ID: {0}{1}", Me.BattleNetID.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace