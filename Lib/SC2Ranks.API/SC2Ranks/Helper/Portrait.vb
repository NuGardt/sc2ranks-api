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
Imports System.Runtime.InteropServices
Imports NuGardt.SC2Ranks.API.Result.Element

Namespace SC2Ranks.Helper
  Public Module Portrait
    Public Function GetPortraitDetails(ByVal PortraitInfo As Sc2RanksPortrait,
                                       <Out()> ByRef Result As ePortrait) As Exception
      Result = Nothing

      If (PortraitInfo Is Nothing) Then
        Return New NullReferenceException("PortraitInfo")
      Else
        With PortraitInfo
          Return GetPortraitDetails(.File, .X, .Y, Result)
        End With
      End If
    End Function

    Public Function GetPortraitDetails(ByVal File As Int32,
                                       ByVal RowX As Int32,
                                       ByVal ColumnY As Int32,
                                       <Out()> ByRef Result As ePortrait) As Exception
      Result = ePortrait.Unknown

      Select Case File
        Case 0
          Select Case RowX
            Case 0
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Kachinsky
                Case 1
                  Result = ePortrait.Cade
                Case 2
                  Result = ePortrait.Thatcher
                Case 3
                  Result = ePortrait.Hall
                Case 4
                  Result = ePortrait.TigerMarine
                Case 5
                  Result = ePortrait.PandaMarine
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 1
              Select Case ColumnY
                Case 0
                  Result = ePortrait.GeneralWarfield
                Case 1
                  Result = ePortrait.JimRaynor
                Case 2
                  Result = ePortrait.ArcturusMengsk
                Case 3
                  Result = ePortrait.SarahKerrigan
                Case 4
                  Result = ePortrait.KateLockwell
                Case 5
                  Result = ePortrait.RorySwann
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 2
              Select Case ColumnY
                Case 0
                  Result = ePortrait.EgonStetmann
                Case 1
                  Result = ePortrait.Hill
                Case 2
                  Result = ePortrait.Adjutant
                Case 3
                  Result = ePortrait.DrArielHanson
                Case 4
                  Result = ePortrait.GabrielTosh
                Case 5
                  Result = ePortrait.MattHorner
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 3
              Select Case ColumnY
                Case 0
                  Result = ePortrait.TychusFindlay
                Case 1
                  Result = ePortrait.Zeratul
                Case 2
                  Result = ePortrait.ValerianMengsk
                Case 3
                  Result = ePortrait.Spectre
                Case 4
                  Result = ePortrait.RaynorMarine
                Case 5
                  Result = ePortrait.TaurenMarine
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 4
              Select Case ColumnY
                Case 0
                  Result = ePortrait.NightElfBanshee
                Case 1
                  Result = ePortrait.DiabloMarine
                Case 2
                  Result = ePortrait.SCV
                Case 3
                  Result = ePortrait.Firebat
                Case 4
                  Result = ePortrait.Vulture
                Case 5
                  Result = ePortrait.Hellion
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 5
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Medic
                Case 1
                  Result = ePortrait.SpartanCompany
                Case 2
                  Result = ePortrait.Wraith
                Case 3
                  Result = ePortrait.Diamondback
                Case 4
                  Result = ePortrait.Probe
                Case 5
                  Result = ePortrait.Scout
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case Else
              Return New Exception("Unknown row.")
          End Select
        Case 1
          Select Case RowX
            Case 0
              Select Case ColumnY
                Case 0
                  Result = ePortrait.TaurenMarineKorea
                Case 1
                  Result = ePortrait.NightElfBansheeKorea
                Case 2
                  Result = ePortrait.DiabloMarineKorea
                Case 3
                  Result = ePortrait.WorgenMarineKorea
                Case 4
                  Result = ePortrait.GoblinMarineKorea
                Case 5
                  Result = ePortrait.PanTerranMarine
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 1
              Select Case ColumnY
                Case 0
                  Result = ePortrait.WizardTemplar
                Case 1
                  Result = ePortrait.TyraelMarine
                Case 2
                  Result = ePortrait.WitchDoctorZergling
                Case 3
                  Result = ePortrait.StarCraftMaster
                Case 4
                  Result = ePortrait.NightElfTemplar
                Case 5
                  Result = ePortrait.InfestedOrc
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 2
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Unknown 'ToDo: Missing Portrait Information (Collectors/Special Event) (Marine)
                Case 1
                  Result = ePortrait.OverlordOfTerror
                Case 2
                  Result = ePortrait.BansheeQueen
                Case 3
                  Result = ePortrait.InfestedPanTerran
                Case 4
                  Result = ePortrait.PrinceValerian
                Case 5
                  Result = ePortrait.Zagara
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 3
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Lasarra
                Case 1
                  Result = ePortrait.Dehaka
                Case 2
                  Result = ePortrait.InfestedStukov
                Case 3
                  Result = ePortrait.MiraHorner
                Case 4
                  Result = ePortrait.PrimalQueen
                Case 5
                  Result = ePortrait.Izsha
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 4
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Abathur
                Case 1
                  Result = ePortrait.GhostKerrigan
                Case 2
                  Result = ePortrait.Zurvan
                Case 3
                  Result = ePortrait.Narud
                Case 4
                  Result = ePortrait.SnakeMarine
                Case 5
                  Result = ePortrait.Year15Terran
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 5
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Year15Protoss
                Case 1
                  Result = ePortrait.Year15Zerg
                Case 2
                  Result = ePortrait.SpawningPoolParty
                Case 3
                  Result = ePortrait.Unknown ' Not set
                Case 4
                  Result = ePortrait.Unknown ' Not set
                Case 5
                  Result = ePortrait.Unknown ' Not set
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case Else
              Return New Exception("Unknown row.")
          End Select
        Case 2
          Select Case RowX
            Case 0
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Ghost
                Case 1
                  Result = ePortrait.Thor
                Case 2
                  Result = ePortrait.Battlecruiser
                Case 3
                  Result = ePortrait.Nova
                Case 4
                  Result = ePortrait.Zealot
                Case 5
                  Result = ePortrait.Stalker
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 1
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Phoenix
                Case 1
                  Result = ePortrait.Immortal
                Case 2
                  Result = ePortrait.VoidRay
                Case 3
                  Result = ePortrait.Colossus
                Case 4
                  Result = ePortrait.Carrier
                Case 5
                  Result = ePortrait.Tassadar
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 2
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Reaper
                Case 1
                  Result = ePortrait.Sentry
                Case 2
                  Result = ePortrait.Overseer
                Case 3
                  Result = ePortrait.Viking
                Case 4
                  Result = ePortrait.HighTemplar
                Case 5
                  Result = ePortrait.Mutalisk
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 3
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Banshee
                Case 1
                  Result = ePortrait.HybridDestroyer
                Case 2
                  Result = ePortrait.DarkVoice
                Case 3
                  Result = ePortrait.Urubu
                Case 4
                  Result = ePortrait.Lyote
                Case 5
                  Result = ePortrait.Automaton2000
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 4
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Orlan
                Case 1
                  Result = ePortrait.WolfMarine
                Case 2
                  Result = ePortrait.MurlocMarine
                Case 3
                  Result = ePortrait.WorgenMarine
                Case 4
                  Result = ePortrait.GoblinMarine
                Case 5
                  Result = ePortrait.ZealotChef
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 5
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Stank
                Case 1
                  Result = ePortrait.Ornatus
                Case 2
                  Result = ePortrait.FacebookCorpsMembers
                Case 3
                  Result = ePortrait.LionMarines
                Case 4
                  Result = ePortrait.Dragons
                Case 5
                  Result = ePortrait.RaynorMarineChina
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case Else
              Return New Exception("Unknown row.")
          End Select
        Case 3
          Select Case RowX
            Case 0
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Urun
                Case 1
                  Result = ePortrait.Nyon
                Case 2
                  Result = ePortrait.Executor
                Case 3
                  Result = ePortrait.Mohandar
                Case 4
                  Result = ePortrait.Selendis
                Case 5
                  Result = ePortrait.Artanis
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 1
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Drone
                Case 1
                  Result = ePortrait.InfestedColonist
                Case 2
                  Result = ePortrait.InfestedMarine
                Case 3
                  Result = ePortrait.Corruptor
                Case 4
                  Result = ePortrait.Abberation
                Case 5
                  Result = ePortrait.BroodLord
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 2
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Overmind
                Case 1
                  Result = ePortrait.Leviathan
                Case 2
                  Result = ePortrait.Overlord
                Case 3
                  Result = ePortrait.HydraliskMarine
                Case 4
                  Result = ePortrait.ZerataiDarkTemplar
                Case 5
                  Result = ePortrait.Goliath
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 3
              Select Case ColumnY
                Case 0
                  Result = ePortrait.LenassaDarkTemplar
                Case 1
                  Result = ePortrait.MiraHan
                Case 2
                  Result = ePortrait.Archon
                Case 3
                  Result = ePortrait.HybridReaver
                Case 4
                  Result = ePortrait.Predator
                Case 5
                  Result = ePortrait.Unknown 'ToDo: Missing Portrait Information (Collectors/Special Event) (Marine)
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 4
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Zergling
                Case 1
                  Result = ePortrait.Roach
                Case 2
                  Result = ePortrait.Baneling
                Case 3
                  Result = ePortrait.Hydralisk
                Case 4
                  Result = ePortrait.Queen
                Case 5
                  Result = ePortrait.Infestor
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 5
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Ultralisk
                Case 1
                  Result = ePortrait.QueenOfBlades
                Case 2
                  Result = ePortrait.Marine
                Case 3
                  Result = ePortrait.Marauder
                Case 4
                  Result = ePortrait.Medivac
                Case 5
                  Result = ePortrait.SiegeTank
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case Else
              Return New Exception("Unknown row.")
          End Select
          Select Case RowX
            Case 0
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Level3Zealot
                Case 1
                  Result = ePortrait.Level8Sentry
                Case 2
                  Result = ePortrait.Level5Stalker
                Case 3
                  Result = ePortrait.Level11Immortal
                Case 4
                  Result = ePortrait.Level14Oracle
                Case 5
                  Result = ePortrait.Level17HighTemplar
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 1
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Level21Tempest
                Case 1
                  Result = ePortrait.Level23Colossus
                Case 2
                  Result = ePortrait.Level27Carrier
                Case 3
                  Result = ePortrait.Level29Zeratul
                Case 4
                  Result = ePortrait.Level3Marine
                Case 5
                  Result = ePortrait.Level5Marauder
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 2
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Level8Hellbat
                Case 1
                  Result = ePortrait.Level11WidowMine
                Case 2
                  Result = ePortrait.Level14Medivac
                Case 3
                  Result = ePortrait.Level17Banshee
                Case 4
                  Result = ePortrait.Level21Ghost
                Case 5
                  Result = ePortrait.Level23Thor
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 3
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Level27Battlecruiser
                Case 1
                  Result = ePortrait.Level29Raynor
                Case 2
                  Result = ePortrait.Level11Locust
                Case 3
                  Result = ePortrait.Level3Zergling
                Case 4
                  Result = ePortrait.Level5Roach
                Case 5
                  Result = ePortrait.Level8Hydralisk
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 4
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Level14SwarmHost
                Case 1
                  Result = ePortrait.Level17Infestor
                Case 2
                  Result = ePortrait.Level21Viper
                Case 3
                  Result = ePortrait.Level23BroodLord
                Case 4
                  Result = ePortrait.Level27Ultralisk
                Case 5
                  Result = ePortrait.Level29Kerrigan
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case 5
              Select Case ColumnY
                Case 0
                  Result = ePortrait.Beta30Tempest
                Case 1
                  Result = ePortrait.Beta30Warhound
                Case 2
                  Result = ePortrait.Beta30Viper
                Case 3
                  Result = ePortrait.Unknown ' Not set
                Case 4
                  Result = ePortrait.Unknown ' Not set
                Case 5
                  Result = ePortrait.Unknown ' Not set
                Case Else
                  Return New Exception("Unknown column.")
              End Select
            Case Else
              Return New Exception("Unknown row.")
          End Select
        Case Else
          Return New Exception("Unknown file.")
      End Select

      Return Nothing
    End Function
  End Module
End Namespace