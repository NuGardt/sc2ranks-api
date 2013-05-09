Imports System.Runtime.Serialization

Namespace SC2Ranks.API
  ''' <summary>
  ''' Action for manage division.
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract(Name := "action")>
  Public Enum eCustomDivisionAction
    ''' <summary>
    ''' Add one or more players.
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "add")>
    Add

    ''' <summary>
    ''' Remove one or more players.
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "remove")>
    Remove
  End Enum
End Namespace