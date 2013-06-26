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
Imports NuGardt.SC2Ranks.Helper
Imports System.Text

Namespace SC2Ranks.API.Result.Element
  ''' <summary>
  ''' Class containing information about a team in a division.
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract()>
  Public Class DivisionElement
    Private m_Bracket As Integer
    Private m_Division As String
    Private m_DivisionID As Int32
    Private m_DivisionRank As Int32
    Private m_ExpansionRaw As Integer
    Private m_IsRandom As Boolean
    Private m_LeagueRaw As String
    Private m_Losses As Integer
    Private m_Members As TeamMateElement()
    Private m_Points As Integer
    Private m_Ratio As Double
    Private m_Wins As Integer

    ''' <summary>
    ''' Construct.
    ''' </summary>
    ''' <remarks>Should not instantiate from outside.</remarks>
    Protected Sub New()
      Me.m_Bracket = Nothing
      Me.m_Division = Nothing
      Me.m_DivisionID = Nothing
      Me.m_DivisionRank = Nothing
      Me.m_ExpansionRaw = Nothing
      Me.m_IsRandom = Nothing
      Me.m_LeagueRaw = Nothing
      Me.m_Losses = Nothing
      Me.m_Members = Nothing
      Me.m_Points = Nothing
      Me.m_Ratio = Nothing
      Me.m_Wins = Nothing
    End Sub

#Region "Properties"

    <DataMember(Name := "bracket")>
    Protected Property BracketRaw() As Int32
      Get
        Return Me.m_Bracket
      End Get
      Set(ByVal Value As Int32)
        Me.m_Bracket = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the bracket of the team.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Bracket As eBracket
      Get
        Dim tBracket As eBracket

        Select Case Me.m_Bracket
          Case 2
            tBracket = eBracket._2V2
          Case 3
            tBracket = eBracket._3V3
          Case 4
            tBracket = eBracket._4V4
          Case Else
            tBracket = eBracket._1V1
        End Select

        If Me.m_IsRandom Then tBracket = tBracket Or eBracket.Random

        Return tBracket
      End Get
    End Property

    ''' <summary>
    ''' Returns the name of the division.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "division")>
    Public Property Division() As String
      Get
        Return Me.m_Division
      End Get
      Private Set(ByVal Value As String)
        Me.m_Division = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the division identifier.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "division_id", IsRequired := False, EmitDefaultValue := False)>
    Public Property DivisionID() As Int32
      Get
        Return Me.m_DivisionID
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_DivisionID = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the division rank.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "division_rank")>
    Public Property DivisionRank() As Int32
      Get
        Return Me.m_DivisionRank
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_DivisionRank = Value
      End Set
    End Property

    <DataMember(Name := "expansion")>
    Protected Property ExpansionRaw() As Int32
      Get
        Return Me.m_ExpansionRaw
      End Get
      Set(ByVal Value As Int32)
        Me.m_ExpansionRaw = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the expansion level of the division.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Expansion() As eExpansion
      Get
        Return Enums.ExpansionBuffer.GetEnum(ExpansionRaw.ToString)
      End Get
    End Property

    <DataMember(Name := "is_random")>
    Protected Property IsRandom() As Boolean
      Get
        Return Me.m_IsRandom
      End Get
      Set(ByVal Value As Boolean)
        Me.m_IsRandom = Value
      End Set
    End Property

    <DataMember(Name := "league")>
    Protected Property LeagueRaw() As String
      Get
        Return Me.m_LeagueRaw
      End Get
      Set(ByVal Value As String)
        Me.m_LeagueRaw = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the league of the division.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property League() As eLeague
      Get
        Return Enums.LeaguesBuffer.GetEnum(LeagueRaw)
      End Get
    End Property

    ''' <summary>
    ''' Return the number of losses in a division.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "losses")>
    Public Property Losses() As Int32
      Get
        Return Me.m_Losses
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Losses = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the members of the division.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "members", IsRequired := False, EmitDefaultValue := False)>
    Public Property Members() As TeamMateElement()
      Get
        Return Me.m_Members
      End Get
      Private Set(ByVal Value As TeamMateElement())
        Me.m_Members = Value
      End Set
    End Property

    ''' <summary>
    ''' Return the number of points in a division.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "points")>
    Public Property Points() As Int32
      Get
        Return Me.m_Points
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Points = Value
      End Set
    End Property

    ''' <summary>
    ''' Return the win/loss ratio in a division
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "ratio")>
    Public Property Ratio() As Double
      Get
        Return Me.m_Ratio
      End Get
      Private Set(ByVal Value As Double)
        Me.m_Ratio = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the number of wins in a division.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "wins")>
    Public Property Wins() As Int32
      Get
        Return Me.m_Wins
      End Get
      Private Set(ByVal Value As Int32)
        Me.m_Wins = Value
      End Set
    End Property

#End Region

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Bracket: {0}{1}", Me.Bracket.ToString(), vbCrLf)
        Call .AppendFormat("Division: {0}{1}", Me.Division, vbCrLf)
        Call .AppendFormat("Division ID: {0}{1}", Me.DivisionID.ToString(), vbCrLf)
        Call .AppendFormat("Division Rank: {0}{1}", Me.DivisionRank.ToString(), vbCrLf)
        Call .AppendFormat("Expansion: {0}{1}", Me.Expansion.ToString(), vbCrLf)
        Call .AppendFormat("League: {0}{1}", Me.League.ToString(), vbCrLf)
        Call .AppendFormat("Losses: {0}{1}", Me.Losses, vbCrLf)
        If (Me.Members IsNot Nothing) Then
          Dim dMax As Int32 = Me.Members.Length - 1
          For i As Int32 = 0 To dMax
            Call .AppendFormat("Member (#{0}: {1}{2}", i.ToString(), Me.Members(i).ToString(), vbCrLf)
          Next i
        End If
        Call .AppendFormat("Points: {0}{1}", Me.Points, vbCrLf)
        Call .AppendFormat("Ratio: {0}{1}", Me.Ratio.ToString(), vbCrLf)
        Call .AppendFormat("Wins: {0}{1}", Me.Wins, vbCrLf)
      End With

      Return SB.ToString
    End Function
  End Class
End Namespace
