Imports com.NuGardt.SC2Ranks.API
Imports System.Runtime.Serialization

Namespace SC2Ranks.Helper
  Friend NotInheritable Class Enums
    Public Shared CustomDivisionActionBuffer As EnumBuffer(Of eCustomDivisionAction, EnumMemberAttribute) = New EnumBuffer(Of eCustomDivisionAction, EnumMemberAttribute)
    Public Shared SearchTypeBuffer As EnumBuffer(Of eSearchType, EnumMemberAttribute) = New EnumBuffer(Of eSearchType, EnumMemberAttribute)
    Public Shared RegionBuffer As EnumBuffer(Of eRegion, EnumMemberAttribute) = New EnumBuffer(Of eRegion, EnumMemberAttribute)
    Public Shared BracketBuffer As EnumBuffer(Of eBracket, EnumMemberAttribute) = New EnumBuffer(Of eBracket, EnumMemberAttribute)
    Public Shared RacesBuffer As EnumBuffer(Of eRace, EnumMemberAttribute) = New EnumBuffer(Of eRace, EnumMemberAttribute)
    Public Shared LeaguesBuffer As EnumBuffer(Of eLeague, EnumMemberAttribute) = New EnumBuffer(Of eLeague, EnumMemberAttribute)
    Public Shared ExpansionBuffer As EnumBuffer(Of eExpansion, EnumMemberAttribute) = New EnumBuffer(Of eExpansion, EnumMemberAttribute)

    Private Sub New()
      '-
    End Sub
  End Class
End Namespace
