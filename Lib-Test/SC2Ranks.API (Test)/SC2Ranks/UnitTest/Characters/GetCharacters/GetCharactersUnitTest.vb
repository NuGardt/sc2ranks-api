Imports NuGardt.SC2Ranks.API
Imports NuGardt.UnitTest
Imports NuGardt.SC2Ranks.API.Result

Namespace SC2Ranks.UnitTest.Characters.GetCharacters
  Public Class GetCharactersUnitTest
    Implements IUnitTestCase

    Private OnCompletion As AsyncCallback
    Private m_Result As String

    Private Service As Sc2RanksService
    Private Ex As Exception

    Public Sub Initialize() Implements IUnitTestCase.Initialize
      Me.m_Result = Nothing

      Me.Ex = Sc2RanksService.CreateInstance(My.Resources.ApiKey, Nothing, Nothing, Service)
    End Sub

    Public Sub Start(ByVal OnCompletion As AsyncCallback,
                     Optional Report As IUnitTestCase.procReport = Nothing) Implements IUnitTestCase.Start
      Me.OnCompletion = OnCompletion

      If (Me.Ex Is Nothing) Then
        Dim Response As Sc2RanksCharacterListResult = Nothing

        Dim Characters As New List(Of Sc2RanksBulkCharacter)
        Call Characters.Add(New Sc2RanksBulkCharacter([Const].Region, [Const].BattleNetID))

        Me.Ex = Me.Service.GetCharacters(Characters, Response)

        If (Ex Is Nothing) Then
          If Response.HasError Then
            Me.Ex = New Exception(Response.Error)
          Else
            Me.m_Result = Helper.CheckResult(Of Sc2RanksCharacterListResult)("GetCharacters", Me.Ex, Response)
          End If
        End If
      End If

      Call Me.OnCompletion.Invoke(Nothing)
    End Sub

    Public Function Abort() As Boolean Implements IUnitTestCase.Abort
      Return False
    End Function

    Public Sub [End](Optional Result As IAsyncResult = Nothing) Implements IUnitTestCase.[End]
      '-
    End Sub

    Public Sub Dispose() Implements IUnitTestCase.Dispose
      Call Me.Service.Dispose()
    End Sub

    Public ReadOnly Property Successfull As Boolean Implements IUnitTestCase.Successfull
      Get
        Return (Me.Ex Is Nothing)
      End Get
    End Property

    Public ReadOnly Property IsRunning As Boolean Implements IUnitTestCase.IsRunning
      Get
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property Name As String Implements IUnitTestCase.Name
      Get
        Return "SC2Ranks API: GetCharacters"
      End Get
    End Property

    Public ReadOnly Property Result As String Implements IUnitTestCase.Result
      Get
        If (Not String.IsNullOrEmpty(Me.m_Result)) Then
          Return Me.m_Result
        ElseIf (Me.Ex IsNot Nothing) Then
          Return Me.Ex.ToString()
        Else
          Return Nothing
        End If
      End Get
    End Property
  End Class
End Namespace