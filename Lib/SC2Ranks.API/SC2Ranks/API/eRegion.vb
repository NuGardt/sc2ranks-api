Imports System.Runtime.Serialization
Imports com.NuGardt.SC2Ranks.Helper

Namespace SC2Ranks.API
''' <summary>
'''   Realm Regions
''' </summary>
''' <remarks></remarks>
  <DataContract(Name := "region")>
  Public Enum eRegion
  
  ''' <summary>
  '''   All Regions
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "all")>
      <Notation("All")>
    All
  
  ''' <summary>
  '''   Americas
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "am")>
      <Notation("Americas")>
    AM
  
  ''' <summary>
  '''   European Union
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "eu")>
      <Notation("Europe")>
    EU
  
  ''' <summary>
  '''   South-Korea
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "kr")>
      <Notation("Korea")>
    KR
  
  ''' <summary>
  '''   Taiwan
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "tw")>
      <Notation("Taiwan")>
    TW
  
  ''' <summary>
  '''   South-East-Asia
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "sea")>
      <Notation("South-East-Asia")>
    SEA
  
  ''' <summary>
  '''   China
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "cn")>
      <Notation("China")>
    CN
  
  ''' <summary>
  '''   United States
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "us")>
      <Notation("United States")>
    US

    <EnumMember(Value := "fea")>
    <Notation("FEA")>
    FEA
  
  ''' <summary>
  '''   Latin America
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "la")>
      <Notation("Latin America")>
    LA
  
  ''' <summary>
  '''   North-America
  ''' </summary>
  ''' <remarks></remarks>
    <EnumMember(Value := "na")>
      <Notation("North America")>
    NA
  End Enum
End Namespace