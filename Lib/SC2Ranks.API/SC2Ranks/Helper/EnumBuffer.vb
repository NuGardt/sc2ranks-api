' NuGardt SC2Ranks API
' Copyright (C) 2011-2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace SC2Ranks.Helper
  ''' <summary>
  ''' This class buffers an enumartion marked with <see cref="TagAttribute">TagAttribute</see>.
  ''' </summary>
  ''' <typeparam name="TEnum"></typeparam>
  ''' <remarks></remarks>
    <DebuggerStepThrough()>
  Public NotInheritable Class EnumBuffer(Of TEnum, TAttribute As Attribute)

    Public Delegate Function procParseCustomAttribute(ByVal Attribute As Attribute) As String

    Private ReadOnly ValueToEnum As IDictionary(Of String, TEnum)
    Private ReadOnly EnumToValue As IDictionary(Of TEnum, String)
    Private ReadOnly ParseCustomAttribute As procParseCustomAttribute

    ''' <summary>
    ''' Construct.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal ParseCustomAttribute As procParseCustomAttribute = Nothing)
      Me.ParseCustomAttribute = ParseCustomAttribute

      Me.ValueToEnum = New SortedDictionary(Of String, TEnum)(StringComparer.InvariantCultureIgnoreCase)
      Me.EnumToValue = New SortedDictionary(Of TEnum, String)

      Dim Ex As Exception = Nothing
      If (Not Me.ToDictionary(Me.ValueToEnum, Me.EnumToValue, Ex)) Then Call Trace.WriteLine(Ex)
    End Sub

    ''' <summary>
    ''' Returns a list of all enumerations.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToValueList() As ICollection(Of String)
      Return Me.ValueToEnum.Keys
    End Function

    ''' <summary>
    ''' Returns a list of all enumerations.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToEnumList() As ICollection(Of TEnum)
      Return Me.EnumToValue.Keys()
    End Function

    ''' <summary>
    ''' Returns the tag for an enumeration.
    ''' </summary>
    ''' <param name="Tag">The tag.</param>
    ''' <param name="Instance">
    ''' Contains the Enumeration, if the tag could be matched to an enumeration, otherwise <c>Nothing</c>.
    ''' </param>
    ''' <param name="Ex">
    ''' Optional. Contains the error message if one occured, otherwise <c>Nothing</c>.
    ''' </param>
    ''' <returns>
    ''' Returns <c>True</c>, if the tag was matched to an enumeration, otherwise <c>False</c>.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function GetEnum(ByVal Tag As String,
                            ByRef Instance As TEnum,
                            Optional ByRef Ex As Exception = Nothing) As Boolean
      Ex = Nothing
      Instance = Nothing

      If Not Me.ValueToEnum.TryGetValue(Tag, Instance) Then Ex = New KeyNotFoundException("Tag")

      Return (Ex Is Nothing)
    End Function

    ''' <summary>
    ''' Returns the tag for an enumeration.
    ''' </summary>
    ''' <param name="Tag">The tag.</param>
    ''' <param name="Ex">
    ''' Optional. Contains the error message if one occured, otherwise <c>Nothing</c>.
    ''' </param>
    ''' <returns>
    ''' Returns <c>True</c>, if the tag was matched to an enumeration, otherwise <c>False</c>.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function GetEnum(ByVal Tag As String,
                            Optional ByRef Ex As Exception = Nothing) As TEnum
      Ex = Nothing
      Dim Erg As TEnum = Nothing

      If (Not String.IsNullOrEmpty(Tag)) AndAlso (Not Me.ValueToEnum.TryGetValue(Tag, Erg)) Then Ex = New KeyNotFoundException("Tag")

      Return Erg
    End Function

    ''' <summary>
    ''' Returns the enumeration for a tag.
    ''' </summary>
    ''' <param name="Enum">The enumartion.</param>
    ''' <param name="Instance">
    ''' Contains the tag, if the enumartion could be matched to a tag, otherwise <c>False</c>.
    ''' </param>
    ''' <param name="Ex">
    ''' Optional. Contains the error message if one occured, otherwise <c>Nothing</c>.
    ''' </param>
    ''' <returns>
    ''' Return <c>True</c>, if the enumration was matched to a tag, otherwise <c>False</c>.
    ''' </returns>
    ''' <remarks>
    ''' Returns <c>True</c>, if the process complete without errors, otherwiese <c>False</c>.
    ''' </remarks>
    Public Function GetValue(ByVal [Enum] As TEnum,
                             ByRef Instance As String,
                             Optional ByRef Ex As Exception = Nothing) As Boolean
      Ex = Nothing
      Instance = Nothing

      If Not Me.EnumToValue.TryGetValue([Enum], Instance) Then Ex = New KeyNotFoundException("Enum")

      Return (Ex Is Nothing)
    End Function

    ''' <summary>
    ''' Returns the enumeration for a tag.
    ''' </summary>
    ''' <param name="Enum">The enumartion.</param>
    ''' <param name="Ex">
    ''' Optional. Contains the error message if one occured, otherwise <c>Nothing</c>.
    ''' </param>
    ''' <returns>
    ''' Return <c>True</c>, if the enumration was matched to a tag, otherwise <c>False</c>.
    ''' </returns>
    ''' <remarks>
    ''' Returns <c>True</c>, if the process complete without errors, otherwiese <c>False</c>.
    ''' </remarks>
    Public Function GetValue(ByVal [Enum] As TEnum,
                             Optional ByRef Ex As Exception = Nothing) As String
      Ex = Nothing
      Dim Erg As String = Nothing

      If Not Me.EnumToValue.TryGetValue([Enum], Erg) Then Ex = New KeyNotFoundException("Enum")

      Return Erg
    End Function

    Private Function ToDictionary(ByRef DictValueToEnum As IDictionary(Of String, TEnum),
                                  ByRef DictEnumToValue As IDictionary(Of TEnum, String),
                                  Optional ByRef Ex As Exception = Nothing) As Boolean
      Ex = Nothing
      Dim [Enum] As Type = GetType(TEnum)
      Dim cKey As TEnum
      Dim AttrList() As Object
      Dim Value As String

      If DictValueToEnum Is Nothing Then
        Ex = New ArgumentNullException("DictValueToEnum")
      ElseIf DictEnumToValue Is Nothing Then
        Ex = New ArgumentNullException("DictEnumToValue")
      Else
        Try
          Call DictValueToEnum.Clear()
          Call DictEnumToValue.Clear()

          With System.Enum.GetValues([Enum]).GetEnumerator
            Call .Reset()

            Do While .MoveNext
              cKey = DirectCast(.Current, TEnum)
              AttrList = [Enum].GetField(cKey.ToString).GetCustomAttributes(False)
              If AttrList IsNot Nothing Then
                With AttrList.GetEnumerator
                  Call .Reset()

                  Do While .MoveNext
                    If TypeOf .Current Is TAttribute Then
                      Value = ParseAttributeValue(DirectCast(.Current, Attribute))

                      Call DictValueToEnum.Add(Value, cKey)
                      Call DictEnumToValue.Add(cKey, Value)

                      Exit Do
                    End If
                  Loop
                End With
              End If
            Loop
          End With
        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return (Ex Is Nothing)
    End Function

    Private Function ParseAttributeValue(ByVal Attribute As Attribute) As String
      Dim Erg As String = Nothing

      If Attribute IsNot Nothing Then
        If TypeOf Attribute Is EnumMemberAttribute Then
          Erg = DirectCast(Attribute, EnumMemberAttribute).Value
        ElseIf TypeOf Attribute Is DataMemberAttribute Then
          Erg = DirectCast(Attribute, DataMemberAttribute).Name
        ElseIf TypeOf Attribute Is NotationAttribute Then
          Erg = DirectCast(Attribute, NotationAttribute).Value
        ElseIf TypeOf Attribute Is TagAttribute Then
          Erg = DirectCast(Attribute, TagAttribute).Value
        ElseIf (Me.ParseCustomAttribute IsNot Nothing) Then
          Erg = Me.ParseCustomAttribute.Invoke(Attribute)
        Else
          Erg = Attribute.ToString
        End If
      End If

      Return Erg
    End Function
  End Class
End Namespace