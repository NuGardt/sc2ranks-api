Imports com.NuGardt.SC2Ranks.Helper
Imports System.Runtime.Serialization

Namespace SC2Ranks.API
  ''' <summary>
  ''' Bracket.
  ''' </summary>
  ''' <remarks></remarks>
    Public Enum eBracket
    ''' <summary>
    ''' Random flag.
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("R")>
  Random = 1

    ''' <summary>
    ''' 1v1
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("1v1")>
      <EnumMember(Value:="1")>
  _1V1 = 2

    ''' <summary>
    ''' 2v2
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("2v2")>
      <EnumMember(Value:="2")>
  _2V2 = 4

    ''' <summary>
    ''' 2v2 Random
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("2v2R")>
  _2V2R = _2V2 Or Random

    ''' <summary>
    ''' 3v3
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("3v3")>
      <EnumMember(Value:="3")>
  _3V3 = 8

    ''' <summary>
    ''' 3v3 Random
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("3v3R")>
  _3V3R = _3V3 Or Random

    ''' <summary>
    ''' 4v4
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("4v4")>
      <EnumMember(Value:="4")>
  _4V4 = 16

    ''' <summary>
    ''' 4v4Random
    ''' </summary>
    ''' <remarks></remarks>
    <Notation("4v4R")>
  _4V4R = _4V4 Or Random
  End Enum
End Namespace