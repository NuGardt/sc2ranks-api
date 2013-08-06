Imports System.Net
Imports NuGardt.API.Helper.JSON
Imports NuGardt.SC2Ranks.API.Result.Element
Imports System.Runtime.Serialization

Namespace SC2Ranks.API.Result
  Public Class Sc2RanksGetCharacterListResult
    Inherits Sc2RanksCharacterExtendedList
    Implements ISc2RanksBaseResult

    Private m_Error As String
    Private m_CacheExpires As Nullable(Of DateTime)
    Private m_ResponseRaw As String
    Private m_CreditsLeft As Int32
    Private m_CreditsUsed As Int32

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
      Call MyBase.New()

      Me.m_Error = Nothing

      Me.m_CreditsLeft = Nothing
      Me.m_CreditsUsed = Nothing
    End Sub

#Region "Properties"

    ''' <summary>
    ''' Returns the error message if an error has occured.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember(Name := "error", EmitDefaultValue := False)>
    Public Property [Error]() As String
      Get
        Return Me.m_Error
      End Get
      Private Set(ByVal Value As String)
        Me.m_Error = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns <c>True</c> if an error has occured, otherwise <c>False</c>.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <IgnoreDataMember()>
    Public ReadOnly Property HasError As Boolean
      Get
        Return (Not String.IsNullOrEmpty(Me.m_Error))
      End Get
    End Property

    <IgnoreDataMember()>
    Public Property CreditsLeft As Int32
      Get
        Return Me.m_CreditsLeft
      End Get
      Friend Set(ByVal Value As Int32)
        Me.m_CreditsLeft = Value
      End Set
    End Property

    <IgnoreDataMember()>
    Public Property CreditsUsed As Int32
      Get
        Return Me.m_CreditsUsed
      End Get
      Friend Set(ByVal Value As Int32)
        Me.m_CreditsUsed = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the date when the cached data expires. Only available when caching is enabled.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <IgnoreDataMember()>
    Public Property CacheExpires As Nullable(Of DateTime) Implements IBaseResult.CacheExpires
      Get
        Return Me.m_CacheExpires
      End Get
      Friend Set(ByVal Value As Nullable(Of DateTime))
        Me.m_CacheExpires = Value
      End Set
    End Property

    ''' <summary>
    ''' Returns the raw response from the server.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <IgnoreDataMember()>
    Public Property ResponseRaw As String Implements IBaseResult.ResponseRaw
      Get
        Return Me.m_ResponseRaw
      End Get
      Friend Set(ByVal Value As String)
        Me.m_ResponseRaw = Value
      End Set
    End Property

#End Region

#Region "Sub ReadHeader"

    Private Sub ReadHeader(Headers As WebHeaderCollection) Implements IBaseResult.ReadHeader
      If (Headers IsNot Nothing) Then
        Call Int32.TryParse(Headers.Get(Sc2RanksService.HeaderCreditsLeft), Me.m_CreditsLeft)
        Call Int32.TryParse(Headers.Get(Sc2RanksService.HeaderCreditsUsed), Me.m_CreditsUsed)
      End If
    End Sub

#End Region
  End Class
End Namespace