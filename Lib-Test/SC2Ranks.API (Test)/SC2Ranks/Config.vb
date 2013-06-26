' NuGardt SC2Ranks API Test
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
Imports NuGardt.SC2Ranks.API
Imports System.Xml

Namespace SC2Ranks
  <DataContract()>
  Friend Class Config
    ''' <summary>
    '''   Causes the program to pause on error or data mismatch
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "PauseOnMismatchOrError")>
    Public PauseOnMismatchOrError As Boolean

    ''' <summary>
    '''   Verbose. Output JSON response.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "OutputJson")>
    Public OutputJson As Boolean

    ''' <summary>
    '''   Shows contents of parsed data.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "OutputClass")>
    Public OutputClass As Boolean

    ''' <summary>
    '''   Carries out request synchronously.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "DoSyncTest")>
    Public DoSyncTest As Boolean

    ''' <summary>
    '''   Carries out request asynchronously
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "DoAsyncTest")>
    Public DoAsyncTest As Boolean

    ''' <summary>
    '''   Test methods that are not recommended to use anymore.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "DoObsoleteMethods")>
    Public DoObsoleteMethods As Boolean

    <DataMember(Name := "IgnoreCache")>
    Public IgnoreCache As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    <DataMember(Name := "TestGetBasePlayerByBattleNetID")>
    Public TestGetBasePlayerByBattleNetID As Boolean

    ''' <summary>
    '''   Tested: 2013-05-12 PASS
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestGetBasePlayerByCharacterCode")>
    Public TestGetBasePlayerByCharacterCode As Boolean

    ''' <summary>
    '''   Tested: 2013-05-12 PASS
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestGetBaseTeamByBattleNetID")>
    Public TestGetBaseTeamByBattleNetID As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    <DataMember(Name := "TestGetBaseTeamByCharacterCode")>
    Public TestGetBaseTeamByCharacterCode As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    <DataMember(Name := "TestGetCustomDivision")>
    Public TestGetCustomDivision As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    <DataMember(Name := "TestGetTeamByBattleNetID")>
    Public TestGetTeamByBattleNetID As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    <DataMember(Name := "TestGetTeamByCharacterCode")>
    Public TestGetTeamByCharacterCode As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    <DataMember(Name := "TestGetBasePlayers")>
    Public TestGetBasePlayers As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-12 PASS</remarks>
    <DataMember(Name := "TestSearchBaseCharacter")>
    Public TestSearchBaseCharacter As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-13 FAIL: 404 Missing on SC2Ranks side?</remarks>
    <DataMember(Name := "TestManageCustomDivision")>
    Public TestManageCustomDivision As Boolean

    ''' <summary>
    ''' </summary>
    ''' <remarks>Tested: 2013-05-13 PASS</remarks>
    <DataMember(Name := "TestGetBonusPools")>
    Public TestGetBonusPools As Boolean

    ''' <summary>
    '''   Region of player.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestRegion")>
    Public TestRegion As eRegion

    ''' <summary>
    '''   Character name of player
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestCharacterName")>
    Public TestCharacterName As String

    ''' <summary>
    '''   Character code. Should not be used anymore.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestCharacterCode")>
    Public TestCharacterCode As Integer

    ''' <summary>
    '''   Battle.net identifier
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestBattleNetID")>
    Public TestBattleNetID As Int32

    ''' <summary>
    '''   SC2Ranks custom division identifier
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestCustomDivisionID")>
    Public TestCustomDivisionID As Int32

    ''' <summary>
    '''   SC2Ranks custom division password
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "TestCustomDivisionPassword")>
    Public TestCustomDivisionPassword As String

    Public Sub New()
      Me.PauseOnMismatchOrError = True
      Me.OutputJson = False
      Me.OutputClass = False
      Me.DoSyncTest = True
      Me.DoAsyncTest = False
      Me.DoObsoleteMethods = True
      Me.IgnoreCache = False
      Me.TestGetBasePlayerByBattleNetID = True
      Me.TestGetBasePlayerByCharacterCode = True
      Me.TestGetBaseTeamByBattleNetID = True
      Me.TestGetBaseTeamByCharacterCode = True
      Me.TestGetCustomDivision = True
      Me.TestGetTeamByBattleNetID = True
      Me.TestGetTeamByCharacterCode = True
      Me.TestGetBasePlayers = True
      Me.TestSearchBaseCharacter = True
      Me.TestManageCustomDivision = False
      Me.TestGetBonusPools = True
      Me.TestRegion = eRegion.EU
      Me.TestCharacterName = "OomJan"
      Me.TestCharacterCode = 0
      Me.TestBattleNetID = 1770249
      Me.TestCustomDivisionID = 7085
      Me.TestCustomDivisionPassword = "secret"
    End Sub

    Public Function ToXml(ByVal Stream As XmlWriter,
                          Optional ByRef Ex As Exception = Nothing) As Boolean
      Ex = Nothing

      If Stream Is Nothing Then
        Ex = New ArgumentNullException("Stream")
      Else
        Try
          Dim Serilizer As DataContractSerializer

          Serilizer = New DataContractSerializer(Me.GetType)
          Call Serilizer.WriteObject(Stream, Me)

        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return (Ex Is Nothing)
    End Function

    Public Shared Function FromXml(ByVal Stream As XmlReader,
                                   ByRef Config As Config,
                                   Optional ByRef Ex As Exception = Nothing) As Boolean
      Ex = Nothing
      Config = Nothing

      If Stream Is Nothing Then
        Ex = New ArgumentNullException("Stream")
      Else
        Try
          Dim Serializer As DataContractSerializer

          Serializer = New DataContractSerializer(GetType(Config))
          Config = DirectCast(Serializer.ReadObject(Stream), Config)

        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return (Ex Is Nothing)
    End Function
  End Class
End Namespace