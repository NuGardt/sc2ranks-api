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
Imports System.Threading
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Runtime.Serialization.Json

Namespace SC2Ranks.API
  Public Class RequestCache
    Private ReadOnly Lock As ReaderWriterLock
    Private ReadOnly RequestsByRequestString As IDictionary(Of String, RequestCacheEntry)

    Public Sub New()
      Me.Lock = New ReaderWriterLock()

      Me.RequestsByRequestString = New SortedDictionary(Of String, RequestCacheEntry)(StringComparer.InvariantCultureIgnoreCase)
    End Sub

    Public Function GetResponse(ByVal Request As String,
                                <Out()> ByRef CacheDate As DateTime,
                                <Out()> ByRef CacheDuration As TimeSpan) As String
      CacheDate = Nothing
      CacheDuration = Nothing
      Dim Erg As String
      Dim Entry As RequestCacheEntry = Nothing

      Call Me.Lock.AcquireReaderLock(- 1)

      If (Not String.IsNullOrEmpty(Request)) AndAlso Me.RequestsByRequestString.TryGetValue(Request, Entry) AndAlso (Not Entry.IsExpired) Then
        CacheDate = Entry.CacheDate
        CacheDuration = Entry.CacheDuration

        Erg = Entry.Response
      Else
        Erg = Nothing
      End If

      Call Me.Lock.ReleaseReaderLock()

      Return Erg
    End Function

    Public Sub AddResponse(ByVal Request As String,
                           ByVal Response As String,
                           ByVal CacheDuration As TimeSpan)
      If (CacheDuration.TotalSeconds >= 0) AndAlso (Not String.IsNullOrEmpty(Request)) AndAlso (Not String.IsNullOrEmpty(Response)) Then

        Call Me.Lock.AcquireWriterLock(- 1)

        If Me.RequestsByRequestString.ContainsKey(Request) Then Call Me.RequestsByRequestString.Remove(Request)

        Call Me.RequestsByRequestString.Add(Request, New RequestCacheEntry(Request, Response, DateTime.UtcNow, CacheDuration))

        Call Me.Lock.ReleaseWriterLock()
      End If
    End Sub

    Public Sub PruneExpired()
      Dim ToRemoveRequests As New List(Of String)

      Call Me.Lock.AcquireWriterLock(- 1)

      With Me.RequestsByRequestString.Values.GetEnumerator()
        Call .Reset()

        Do While .MoveNext()
          If .Current.IsExpired Then Call ToRemoveRequests.Add(.Current.Request)
        Loop

        Call .Dispose()
      End With

      With ToRemoveRequests.GetEnumerator()
        Do While .MoveNext()
          Call Me.RequestsByRequestString.Remove(.Current)
        Loop

        Call .Dispose()
      End With

      Call Me.Lock.ReleaseWriterLock()
    End Sub

    Public Sub Clear()
      Call Me.Lock.AcquireWriterLock(- 1)

      Call Me.RequestsByRequestString.Clear()

      Call Me.Lock.ReleaseWriterLock()
    End Sub

    Public Function Read(ByVal Stream As Stream) As Exception
      Dim Ex As Exception = Nothing
      Dim Serializer As DataContractJsonSerializer

      If (Stream Is Nothing) Then
        Ex = New ArgumentNullException("Stream")
      ElseIf (Not Stream.CanRead) OrElse (Not Stream.CanSeek) OrElse (Not Stream.CanWrite) Then
        Ex = New Exception("Stream must be readable, writable and seekable.")
      Else
        Try
          Dim Entries() As RequestCacheEntry
          Dim Entry As RequestCacheEntry

          Serializer = New DataContractJsonSerializer(GetType(RequestCacheEntry()))

          'Deserialize
          Entries = DirectCast(Serializer.ReadObject(Stream), RequestCacheEntry())

          Call Me.Lock.AcquireWriterLock(- 1)

          Call Me.RequestsByRequestString.Clear()

          Dim dMax As Integer = Entries.Length - 1
          For d As Integer = 0 To dMax
            Entry = Entries(d)

            If (Not Entry.IsExpired) AndAlso (Not Me.RequestsByRequestString.ContainsKey(Entry.Request)) Then Call Me.RequestsByRequestString.Add(Entry.Request, Entry)
          Next d

          Call Me.Lock.ReleaseWriterLock()
        Catch iEx As Exception
          Call Trace.WriteLine(iEx)
        End Try
      End If

      Return Ex
    End Function

    Public Function Write(ByVal Stream As Stream) As Exception
      Dim Ex As Exception = Nothing
      Dim Serializer As DataContractJsonSerializer

      If (Stream Is Nothing) Then
        Ex = New ArgumentNullException("Stream")
      ElseIf (Not Stream.CanRead) OrElse (Not Stream.CanSeek) OrElse (Not Stream.CanWrite) Then
        Ex = New Exception("Stream must be readable, writable and seekable.")
      Else
        Try
          Dim Entries() As RequestCacheEntry

          Serializer = New DataContractJsonSerializer(GetType(RequestCacheEntry()))

          Call Me.Lock.AcquireReaderLock(- 1)

          Entries = Me.RequestsByRequestString.Values.ToArray()

          Call Me.Lock.ReleaseReaderLock()

          Stream.Position = 0
          Call Serializer.WriteObject(Stream, Entries)

          Call Stream.Flush()
        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return Ex
    End Function
  End Class
End Namespace