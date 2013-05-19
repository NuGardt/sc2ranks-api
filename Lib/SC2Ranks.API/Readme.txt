=NuGardt SC2Ranks API v1.2013.05.18=
 Author: Kevin 'OomJan' Gardthausen
 Last Update: 2013-05-18

 Visual Basic .NET 4.0 implementation of the SC2Ranks API with optional request caching. Project includes test project. Runs under Mono (v3) framework.


==Open source==
 Download the source at http://http://code.google.com/p/nugardt-sc2ranks-api/ or via Subversion (SVN): svn checkout http://nugardt-sc2ranks-api.googlecode.com/svn/trunk/ nugardt-sc2ranks-api-read-only

 Written with Microsoft Visual Studio 2010 in .NET Framework 4.0.


==Support==
 * Home Page: http://www.nugardt.com/open-source/sc2ranks-api/
 * Code: http://http://code.google.com/p/nugardt-sc2ranks-api/
 * E-mail: kevin@nugardt.com
 * Twitter: http://www.twitter.com/oomjan34
 * SC2Ranks: http://www.sc2ranks.com/


==Usage==
Library is thread safe. Original library is signed by NuGardt (http://www.nugardt.com/certificates/).

===Creating an instance===
{{{
Dim RankService As Sc2RanksService
Dim CacheStream As System.IO.Stream
Dim Ex As System.Exception 

Ex = Sc2RanksService.CreateInstance(AppKey:="myAppKey", CacheStream:=CacheStream, Instance:=RankService, IgnoreFaultCacheStream:=False)
}}}

Creates an instance of the Sc2RanksService. It takes three parameters.
  * AppKey: The app key. This is required my SC2Ranks. Use your domain name. eg. mysite.com. May not be nothing.
  * CacheStream: Can be nothing. Read/write stream for cache data.
  * Instance: Reference. Contains the instance if Ex is Nothing.
  * IgnoreFaultCacheStream: Optional, default False. True: If the cache stream is not readable not error will be returned. False: If the cache stream is not readable an error will be returned.

Returns System.Exception if unsuccessful.

===Calling a method (Sync)===
{{{
Dim Ex As System.Exception 

Ex = RankService.GetBasePlayerByBattleNetID(Region:=eRegion.EU, CharacterName:="OomJan", BattleNetID:=1770249, IgnoreCache:=False, Result:=ResultInstance)
}}}

Calls RankService.GetBasePlayerByBattleNetID. Takes multiple parameters depending on method. IgnoreCache and Result are always present.
  * IgnoreCache: Optional, default False. If cache data is available then the cached data will be used and SC2Ranks will not be contacted.
  * Result: Reference. Contains the result if Ex is Nothing.

Returns System.Exception if unsuccessful.

===Calling a method (Async)===
{{{
Dim AsyncResult As System.IAsyncResult

AsyncResult = RankService.GetBasePlayerByBattleNetIDBegin(Key:="MyAsyncKey", Region:=eRegion.EU, CharacterName:="OomJan", BattleNetID:=1770249 IgnoreCache:=False, Callback:=AddressOf MyCallback)

Private Sub MyCallback(ByVal Result As IAsyncResult)
  Dim Ex As Exception
  Dim Response As GetBasePlayerResult = Nothing
  Dim Key As Object = Nothing

  Ex = RankService.GetBasePlayerByBattleNetIDEnd(Result:=Result, Key:=Key, Response:=Response)
  '...
End Sub
}}}

Calls RankService.GetBasePlayerByBattleNetIDBegin without waiting for the result. When the result is available the callback will be called.
  * Key: Can be nothing. Use it for tracking calls.
  * Callback: Will be called when the result is available.


==License==
 NuGardt SC2Ranks API
 Copyright (C) 2011-2013 NuGardt Software
 http://www.nugardt.com

 This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

 This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

 You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.