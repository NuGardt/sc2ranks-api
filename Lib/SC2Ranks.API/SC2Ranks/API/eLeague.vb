Imports System.Runtime.Serialization
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API
''' <summary>
'''   Leagues
''' </summary>
''' <remarks></remarks>
  <DataContract(Name := "league")>
  Public Enum eLeague
  
  ''' <summary>
  '''   Bronze league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "bronze")> _
      <Notation("Bronze")>
    Bronze = 1
  
  ''' <summary>
  '''   Silver league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "silver")> _
      <Notation("Silver")>
    Silver = 2
  
  ''' <summary>
  '''   Gold league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "gold")> _
      <Notation("Gold")>
    Gold = 3
  
  ''' <summary>
  '''   Platinum league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "platinum")> _
      <Notation("Platinum")>
    Platinum = 4
  
  ''' <summary>
  '''   Diamond league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "diamond")> _
      <Notation("Diamond")>
    Diamond = 5
  
  ''' <summary>
  '''   Master league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "master")> _
      <Notation("Master")>
    Master = 6
  
  ''' <summary>
  '''   Grandmaster league
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "grandmaster")> _
      <Notation("Grandmaster")>
    GrandMaster = 7
  End Enum
End Namespace