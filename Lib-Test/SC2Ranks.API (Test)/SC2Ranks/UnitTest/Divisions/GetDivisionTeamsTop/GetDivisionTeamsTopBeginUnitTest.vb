Imports NuGardt.SC2Ranks.API
Imports NuGardt.UnitTest
Imports NuGardt.SC2Ranks.API.Result

Namespace SC2Ranks.UnitTest.Divisions.GetDivisionTeamsTop
  Public Class GetDivisionTeamsTopBeginUnitTest
    Implements IUnitTestCase

    Private OnCompletion As AsyncCallback
    Private m_Result As String

    Private Service As Sc2RanksService
    Private Ex As Exception

    Public Sub Initialize() Implements IUnitTestCase.Initialize
      Me.m_Result = Nothing

      Me.Ex = Sc2RanksService.CreateInstance(My.Resources.ApiKey, Service)
    End Sub

    Public Sub Start(ByVal OnCompletion As AsyncCallback,
                     Optional Report As IUnitTestCase.procReport = Nothing) Implements IUnitTestCase.Start
      Me.OnCompletion = OnCompletion

      If (Ex IsNot Nothing) Then
        Call Me.OnCompletion.Invoke(Nothing)
      Else
        Call Me.Service.GetDivisionTeamsTopBegin(Nothing, [Const].DivisionID, EndCallback)
      End If
    End Sub

    Private ReadOnly EndCallback As AsyncCallback = AddressOf iEndCallback

    Private Sub iEndCallback(ByVal Result As IAsyncResult)
      Call OnCompletion.Invoke(Result)
    End Sub

    Public Function Abort() As Boolean Implements IUnitTestCase.Abort
      Return False
    End Function

    Public Sub [End](Optional Result As IAsyncResult = Nothing) Implements IUnitTestCase.[End]
      Dim Response As Sc2RanksGetDivisionTeamsTopResult = Nothing

      Me.Ex = Me.Service.GetDivisionTeamsTopEnd(Result, Nothing, Response)

      If (Ex Is Nothing) Then
        If Response.HasError Then
          Me.Ex = New Exception(Response.Error)
        Else
          Me.m_Result = Helper(Of Sc2RanksGetDivisionTeamsTopResult).CheckResult("GetDivisionTeamListBegin", Me.Ex, Response)
        End If
      End If
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
        Return "SC2Ranks API: GetDivisionTeamListBegin"
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

    Public ReadOnly Property Enabled As Boolean Implements IUnitTestCase.Enabled
      Get
        Return False
      End Get
    End Property

    Public ReadOnly Property GroupName As String Implements IUnitTestCase.GroupName
      Get
        Return "Async"
      End Get
    End Property
  End Class
End Namespace