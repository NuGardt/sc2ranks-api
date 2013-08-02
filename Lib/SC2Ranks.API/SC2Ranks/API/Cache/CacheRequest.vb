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

Namespace SC2Ranks.API.Cache
  ''' <summary>
  ''' This class manages the cache storage.
  ''' </summary>
  ''' <remarks></remarks>
  Public Class CacheRequest
    Private ReadOnly Lock As ReaderWriterLock
    Private ReadOnly RequestsByRequestString As IDictionary(Of Guid, CacheRequestEntry)

    Public Sub New()
      Me.Lock = New ReaderWriterLock()

      Me.RequestsByRequestString = New SortedDictionary(Of Guid, CacheRequestEntry)
    End Sub

    Public Function GetResponse(ByVal Request As String,
                                ByVal Post As String,
                                <Out()> ByRef Expires As Nullable(Of DateTime)) As String
      Expires = Nothing
      Dim Erg As String
      Dim Entry As CacheRequestEntry = Nothing

      Call Me.Lock.AcquireReaderLock(- 1)

      If (Not String.IsNullOrEmpty(Request)) AndAlso Me.RequestsByRequestString.TryGetValue(CacheRequestEntry.GetHash(Request, Post), Entry) AndAlso (Not Entry.IsExpired) Then
        Expires = Entry.Expires

        Erg = Entry.Response
      Else
        Erg = Nothing
      End If

      Call Me.Lock.ReleaseReaderLock()

      Return Erg
    End Function

    Public Sub AddResponse(ByVal URL As String,
                           ByVal POST As String,
                           ByVal Response As String,
                           ByVal CacheDuration As TimeSpan)
      Dim Entry As CacheRequestEntry

      If (CacheDuration.TotalSeconds >= 0) AndAlso (Not String.IsNullOrEmpty(URL)) AndAlso (Not String.IsNullOrEmpty(Response)) Then

        Entry = New CacheRequestEntry(URL, POST, Response, DateTime.UtcNow.Add(CacheDuration))

        Call Me.Lock.AcquireWriterLock(- 1)

        If Me.RequestsByRequestString.ContainsKey(Entry.Hash) Then Call Me.RequestsByRequestString.Remove(Entry.Hash)

        Call Me.RequestsByRequestString.Add(Entry.Hash, Entry)

        Call Me.Lock.ReleaseWriterLock()
      End If
    End Sub

    Public Sub PruneExpired()
      Dim ToRemoveRequests As New List(Of Guid)

      Call Me.Lock.AcquireWriterLock(- 1)

      With Me.RequestsByRequestString.Values.GetEnumerator()
        Call .Reset()

        Do While .MoveNext()
          If .Current.IsExpired Then Call ToRemoveRequests.Add(.Current.Hash)
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
      ElseIf (Not Stream.CanRead) Then
        Ex = New Exception("Stream must be readable.")
      Else
        Try
          Dim Entries() As CacheRequestEntry
          Dim Entry As CacheRequestEntry

          Serializer = New DataContractJsonSerializer(GetType(CacheRequestEntry()))

          'Deserialize
          Entries = DirectCast(Serializer.ReadObject(Stream), CacheRequestEntry())

          Call Me.Lock.AcquireWriterLock(- 1)

          Call Me.RequestsByRequestString.Clear()

          Dim dMax As Integer = Entries.Length - 1
          For d As Integer = 0 To dMax
            Entry = Entries(d)

            If (Not Entry.IsExpired) AndAlso (Not Me.RequestsByRequestString.ContainsKey(Entry.Hash)) Then Call Me.RequestsByRequestString.Add(Entry.Hash, Entry)
          Next d

          Call Me.Lock.ReleaseWriterLock()
        Catch iEx As Exception
          Ex = iEx
        End Try
      End If

      Return Ex
    End Function

    Public Function Write(ByVal Stream As Stream) As Exception
      Dim Ex As Exception = Nothing
      Dim Serializer As DataContractJsonSerializer

      If (Stream Is Nothing) Then
        Ex = New ArgumentNullException("Stream")
      ElseIf (Not Stream.CanWrite) Then
        Ex = New Exception("Stream must be writable.")
      Else
        Try
          Dim Entries() As CacheRequestEntry

          Serializer = New DataContractJsonSerializer(GetType(CacheRequestEntry()))

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