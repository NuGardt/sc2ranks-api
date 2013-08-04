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
  Public Class Sc2RanksSeasonElement
    '"season": {
    '  "year": 2013,
    '  "number": 4
    '}

    Private m_Year As Int32
    Private m_Number As Int32

    Public Sub New()
      Me.m_Year = Nothing
      Me.m_Number = Nothing
    End Sub

    <DataMember(name := "year")>
    Public Property Year As Int32
      Get
        Return Me.m_Year
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Year = Value
      End Set
    End Property

    <DataMember(name := "number")>
    Public Property Number As Int32
      Get
        Return Me.m_Number
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Number = Value
      End Set
    End Property

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Year: {0}{1}", Me.Year.ToString(), vbCrLf)
        Call .AppendFormat("Number: {0}{1}", Me.Number, vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace