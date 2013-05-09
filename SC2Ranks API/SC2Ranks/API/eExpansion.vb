Imports System.Runtime.Serialization
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API
  ''' <summary>
  ''' Expansion
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract(Name := "expansion")>
  Public Enum eExpansion
    ''' <summary>
    ''' Wings of Liberty
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "0")>
      <Notation("Wings of Liberty")>
    WoL = 1

    ''' <summary>
    ''' Heart of the Swarm
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "1")>
      <Notation("Heart of the Swarm")>
    HotS = 2

    ''' <summary>
    ''' Legacy of the Void
    ''' </summary>
    ''' <remarks></remarks>
      <EnumMember(Value := "2")>
      <Notation("Legacy of the Void")>
    LotV = 3
  End Enum
End Namespace