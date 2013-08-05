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
  Public Class Sc2RanksCharacterDivisionElement
    '"division": {
    '  "id": "51fad92d4970cf8401000006",
    '  "rank": 199
    '},

    Private m_ID As String
    Private m_RankRaw As String

    Public Sub New()
      Me.m_ID = Nothing
      Me.m_RankRaw = Nothing
    End Sub

    <DataMember(name := "id")>
    Public Property ID As String
      Get
        Return Me.m_ID
      End Get
      Private Set(ByVal Value As String)
        Me.m_ID = Value
      End Set
    End Property

    'ToDo: Problem under Mono using Nullable(of Int32) ("rank":null) ("rank":16)
    <DataMember(name := "rank")>
    Protected Property RankRaw As String
      Get
        Return Me.m_RankRaw
      End Get
      Set(ByVal Value As String)
        Me.m_RankRaw = Value
      End Set
    End Property

    Public ReadOnly Property Rank() As Nullable(Of Int32)
      Get
        Dim tRank As Int32 = Nothing

        If Int32.TryParse(Me.m_RankRaw, tRank) Then
          Return tRank
        Else
          Return Nothing
        End If
      End Get
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("ID: {0}{1}", Me.ID.ToString(), vbCrLf)
        If Me.Rank.HasValue Then Call .AppendFormat("Rank: {0}{1}", Me.Rank.Value.ToString(), vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace