Imports System.Runtime.Serialization
Imports com.NuGardt.SC2Ranks.API
Imports System.Xml
Imports System.IO

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

    'Public Shared Function TryParse(ByVal cmd As CommandLine,
    '                                ByRef Config As IConfig,
    '                                Optional ByRef Ex As System.Exception = Nothing) As Boolean
    '  Ex = Nothing

    '  If cmd Is Nothing Then
    '    Ex = New ArgumentNullException("cmd")
    '  Else
    '    Dim sTemp As String = Nothing
    '    Dim tDivisionID As Integer

    '    If cmd.GetValues("config", sTemp) AndAlso Read(sTemp.Trim, Config) Then
    '      Config.ConfigPath = sTemp.Trim
    '    Else
    '      If Not Read(DefaultConfigPath, Config) Then Config = New Config
    '      Config.ConfigPath = DefaultConfigPath
    '    End If

    '  Return (Ex Is Nothing)
    'End Function

    Private Shared Function Read(ByVal Path As String,
                                 ByRef Config As Config,
                                 Optional ByRef Ex As Exception = Nothing) As Boolean
      Ex = Nothing
      Config = Nothing

      If String.IsNullOrEmpty(Path) Then
        Ex = New ArgumentNullException("Path")
      ElseIf Not File.Exists(Path) Then
        Ex = New FileNotFoundException(Path)
      Else
        Try
          Dim Stream As Stream
          Dim XMLReader As XmlReader
          Dim iConfig As Config = Nothing

          Stream = New FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read)

          XMLReader = XMLReader.Create(Stream)

          If FromXml(XMLReader, iConfig, Ex) Then Config = iConfig

          With Stream
            Call .Close()
            Call .Dispose()
          End With
        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return (Ex Is Nothing)
    End Function

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