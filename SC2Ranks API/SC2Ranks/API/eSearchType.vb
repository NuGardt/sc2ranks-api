Imports System.Runtime.Serialization

Namespace SC2Ranks.API
  ''' <summary>
  ''' Search Types
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract(Name := "search_type")>
  Public Enum eSearchType
    ''' <summary>
    ''' Exact search
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "exact")>
    Exact

    ''' <summary>
    ''' Contains name anywhere
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "contains")>
    Contains

    ''' <summary>
    ''' Start with name
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "starts")>
    StartsWith

    ''' <summary>
    ''' Ends with name
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "ends")>
    EndsWith
  End Enum
End Namespace