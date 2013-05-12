Imports System.Runtime.Serialization
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API
''' <summary>
'''   Races
''' </summary>
''' <remarks></remarks>
  <DataContract(Name := "race")>
  Public Enum eRace
  
  ''' <summary>
  '''   Protoss
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "protoss")> _
      <Notation("Protoss")>
    Protoss = 1
  
  ''' <summary>
  '''   Terran
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "terran")> _
      <Notation("Terran")>
    Terran = 2
  
  ''' <summary>
  '''   Zerg
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "zerg")> _
      <Notation("Zerg")>
    Zerg = 3
  
  ''' <summary>
  '''   Random
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "random")> _
      <Notation("Random")>
    Random
  End Enum
End Namespace