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
Namespace SC2Ranks.Helper
  Public Enum ePortrait
    ''' <summary>
    ''' Unknown
    ''' </summary>
    ''' <remarks></remarks>
    Unknown = 0

    ''' <summary>
    ''' Kachinsky
    ''' </summary>
    ''' <remarks></remarks>
    Kachinsky

    ''' <summary>
    ''' Cade
    ''' </summary>
    ''' <remarks></remarks>
    Cade

    ''' <summary>
    ''' Thatcher
    ''' </summary>
    ''' <remarks></remarks>
    Thatcher

    ''' <summary>
    ''' Hall
    ''' </summary>
    ''' <remarks></remarks>
    Hall

    ''' <summary>
    ''' Tiger Marine
    ''' </summary>
    ''' <remarks></remarks>
    TigerMarine

    ''' <summary>
    ''' Panda Marine
    ''' </summary>
    ''' <remarks></remarks>
    PandaMarine

    ''' <summary>
    ''' General Warfield
    ''' </summary>
    ''' <remarks></remarks>
    GeneralWarfield

    ''' <summary>
    ''' Jim Raynor
    ''' </summary>
    ''' <remarks></remarks>
    JimRaynor

    ''' <summary>
    ''' Arcturus Mengsk
    ''' </summary>
    ''' <remarks></remarks>
    ArcturusMengsk

    ''' <summary>
    ''' Sarah Kerrigan
    ''' </summary>
    ''' <remarks></remarks>
    SarahKerrigan

    ''' <summary>
    ''' Kate Lockwell
    ''' </summary>
    ''' <remarks></remarks>
    KateLockwell

    ''' <summary>
    ''' Rory Swann
    ''' </summary>
    ''' <remarks></remarks>
    RorySwann

    ''' <summary>
    ''' Egon Stetmann
    ''' </summary>
    ''' <remarks></remarks>
    EgonStetmann

    ''' <summary>
    ''' Hill
    ''' </summary>
    ''' <remarks></remarks>
    Hill

    ''' <summary>
    ''' Adjutant
    ''' </summary>
    ''' <remarks></remarks>
    Adjutant

    ''' <summary>
    ''' Dr. Ariel Hanson
    ''' </summary>
    ''' <remarks></remarks>
    DrArielHanson

    ''' <summary>
    ''' Gabriel Tosh
    ''' </summary>
    ''' <remarks></remarks>
    GabrielTosh

    ''' <summary>
    ''' Matt Horner
    ''' </summary>
    ''' <remarks></remarks>
    MattHorner

    ''' <summary>
    ''' Tychus Findlay
    ''' </summary>
    ''' <remarks></remarks>
    TychusFindlay

    ''' <summary>
    ''' Zeratul
    ''' </summary>
    ''' <remarks></remarks>
    Zeratul

    ''' <summary>
    ''' Valerian Mengsk
    ''' </summary>
    ''' <remarks></remarks>
    ValerianMengsk

    ''' <summary>
    ''' Spectre
    ''' </summary>
    ''' <remarks></remarks>
    Spectre

    ''' <summary>
    ''' Raynor Marine
    ''' </summary>
    ''' <remarks></remarks>
    RaynorMarine

    ''' <summary>
    ''' Tauren Marine
    ''' </summary>
    ''' <remarks></remarks>
    TaurenMarine

    ''' <summary>
    ''' Night Elf Banshee
    ''' </summary>
    ''' <remarks></remarks>
    NightElfBanshee

    ''' <summary>
    ''' Diablo Marine
    ''' </summary>
    ''' <remarks></remarks>
    DiabloMarine

    ''' <summary>
    ''' SCV
    ''' </summary>
    ''' <remarks></remarks>
    SCV

    ''' <summary>
    ''' Firebat
    ''' </summary>
    ''' <remarks></remarks>
    Firebat

    ''' <summary>
    ''' Vulture
    ''' </summary>
    ''' <remarks></remarks>
    Vulture

    ''' <summary>
    ''' Hellion
    ''' </summary>
    ''' <remarks></remarks>
    Hellion

    ''' <summary>
    ''' Medic
    ''' </summary>
    ''' <remarks></remarks>
    Medic

    ''' <summary>
    ''' Spartan Company
    ''' </summary>
    ''' <remarks></remarks>
    SpartanCompany

    ''' <summary>
    ''' Wraith
    ''' </summary>
    ''' <remarks></remarks>
    Wraith

    ''' <summary>
    ''' Diamondback
    ''' </summary>
    ''' <remarks></remarks>
    Diamondback

    ''' <summary>
    ''' Probe
    ''' </summary>
    ''' <remarks></remarks>
    Probe

    ''' <summary>
    ''' Scout
    ''' </summary>
    ''' <remarks></remarks>
    Scout

    ''' <summary>
    ''' TaurenMarine (Korea)
    ''' </summary>
    ''' <remarks></remarks>
    TaurenMarineKorea

    ''' <summary>
    ''' NightElfBanshee (Korea)
    ''' </summary>
    ''' <remarks></remarks>
    NightElfBansheeKorea

    ''' <summary>
    ''' DiabloMarine (Korea)
    ''' </summary>
    ''' <remarks></remarks>
    DiabloMarineKorea

    ''' <summary>
    ''' WorgenMarine (Korea)
    ''' </summary>
    ''' <remarks></remarks>
    WorgenMarineKorea

    ''' <summary>
    ''' GoblinMarine (Korea)
    ''' </summary>
    ''' <remarks></remarks>
    GoblinMarineKorea

    ''' <summary>
    ''' PanTerran Marine
    ''' </summary>
    ''' <remarks></remarks>
    PanTerranMarine

    ''' <summary>
    ''' Wizard Templar
    ''' </summary>
    ''' <remarks></remarks>
    WizardTemplar

    ''' <summary>
    ''' Tyrael Marine
    ''' </summary>
    ''' <remarks></remarks>
    TyraelMarine

    ''' <summary>
    ''' Witch Doctor Zergling
    ''' </summary>
    ''' <remarks></remarks>
    WitchDoctorZergling

    ''' <summary>
    ''' StarCraft II Master
    ''' </summary>
    ''' <remarks></remarks>
    StarCraftMaster

    ''' <summary>
    ''' Night Elf Templar
    ''' </summary>
    ''' <remarks></remarks>
    NightElfTemplar

    ''' <summary>
    ''' Infested Orc
    ''' </summary>
    ''' <remarks></remarks>
    InfestedOrc

    ''' <summary>
    ''' Overlord Of Terror
    ''' </summary>
    ''' <remarks></remarks>
    OverlordOfTerror

    ''' <summary>
    ''' Banshee Queen
    ''' </summary>
    ''' <remarks></remarks>
    BansheeQueen

    ''' <summary>
    ''' Infested PanTerran
    ''' </summary>
    ''' <remarks></remarks>
    InfestedPanTerran

    ''' <summary>
    ''' Prince Valerian
    ''' </summary>
    ''' <remarks></remarks>
    PrinceValerian

    ''' <summary>
    ''' Zagara
    ''' </summary>
    ''' <remarks></remarks>
    Zagara

    ''' <summary>
    ''' Lasarra
    ''' </summary>
    ''' <remarks></remarks>
    Lasarra

    ''' <summary>
    ''' Dehaka
    ''' </summary>
    ''' <remarks></remarks>
    Dehaka

    ''' <summary>
    ''' InfestedStukov
    ''' </summary>
    ''' <remarks></remarks>
    InfestedStukov

    ''' <summary>
    ''' Mira Horner
    ''' </summary>
    ''' <remarks></remarks>
    MiraHorner

    ''' <summary>
    ''' Primal Queen
    ''' </summary>
    ''' <remarks></remarks>
    PrimalQueen

    ''' <summary>
    ''' Izsha
    ''' </summary>
    ''' <remarks></remarks>
    Izsha

    ''' <summary>
    ''' Abathur
    ''' </summary>
    ''' <remarks></remarks>
    Abathur

    ''' <summary>
    ''' Ghost Kerrigan
    ''' </summary>
    ''' <remarks></remarks>
    GhostKerrigan

    ''' <summary>
    ''' Zurvan
    ''' </summary>
    ''' <remarks></remarks>
    Zurvan

    ''' <summary>
    ''' Narud
    ''' </summary>
    ''' <remarks></remarks>
    Narud

    ''' <summary>
    ''' Snake Marine
    ''' </summary>
    ''' <remarks></remarks>
    SnakeMarine

    ''' <summary>
    ''' 15 Year Terran
    ''' </summary>
    ''' <remarks></remarks>
    Year15Terran

    ''' <summary>
    ''' 15 Year Protoss
    ''' </summary>
    ''' <remarks></remarks>
    Year15Protoss

    ''' <summary>
    ''' 15 Year Zerg
    ''' </summary>
    ''' <remarks></remarks>
    Year15Zerg

    ''' <summary>
    ''' Spawning Pool Party
    ''' </summary>
    ''' <remarks></remarks>
    SpawningPoolParty

    ''' <summary>
    ''' Ghost
    ''' </summary>
    ''' <remarks></remarks>
    Ghost

    ''' <summary>
    ''' Thor
    ''' </summary>
    ''' <remarks></remarks>
    Thor

    ''' <summary>
    ''' Battlecruiser
    ''' </summary>
    ''' <remarks></remarks>
    Battlecruiser

    ''' <summary>
    ''' Nova
    ''' </summary>
    ''' <remarks></remarks>
    Nova

    ''' <summary>
    ''' Zealot
    ''' </summary>
    ''' <remarks></remarks>
    Zealot

    ''' <summary>
    ''' Stalker
    ''' </summary>
    ''' <remarks></remarks>
    Stalker

    ''' <summary>
    ''' Phoenix
    ''' </summary>
    ''' <remarks></remarks>
    Phoenix

    ''' <summary>
    ''' Immortal
    ''' </summary>
    ''' <remarks></remarks>
    Immortal

    ''' <summary>
    ''' Void Ray
    ''' </summary>
    ''' <remarks></remarks>
    VoidRay

    ''' <summary>
    ''' Colossus
    ''' </summary>
    ''' <remarks></remarks>
    Colossus

    ''' <summary>
    ''' Carrier
    ''' </summary>
    ''' <remarks></remarks>
    Carrier

    ''' <summary>
    ''' Tassadar
    ''' </summary>
    ''' <remarks></remarks>
    Tassadar

    ''' <summary>
    ''' Reaper
    ''' </summary>
    ''' <remarks></remarks>
    Reaper

    ''' <summary>
    ''' Sentry
    ''' </summary>
    ''' <remarks></remarks>
    Sentry

    ''' <summary>
    ''' Overseer
    ''' </summary>
    ''' <remarks></remarks>
    Overseer

    ''' <summary>
    ''' Viking
    ''' </summary>
    ''' <remarks></remarks>
    Viking

    ''' <summary>
    ''' High Templar
    ''' </summary>
    ''' <remarks></remarks>
    HighTemplar

    ''' <summary>
    ''' Mutalisk
    ''' </summary>
    ''' <remarks></remarks>
    Mutalisk

    ''' <summary>
    ''' Banshee
    ''' </summary>
    ''' <remarks></remarks>
    Banshee

    ''' <summary>
    ''' Hybrid Destroyer
    ''' </summary>
    ''' <remarks></remarks>
    HybridDestroyer

    ''' <summary>
    ''' Dark Voice
    ''' </summary>
    ''' <remarks></remarks>
    DarkVoice

    ''' <summary>
    ''' Urubu
    ''' </summary>
    ''' <remarks></remarks>
    Urubu

    ''' <summary>
    ''' Lyote
    ''' </summary>
    ''' <remarks></remarks>
    Lyote

    ''' <summary>
    ''' Automaton 2000
    ''' </summary>
    ''' <remarks></remarks>
    Automaton2000

    ''' <summary>
    ''' Orlan
    ''' </summary>
    ''' <remarks></remarks>
    Orlan

    ''' <summary>
    ''' Wolf Marine
    ''' </summary>
    ''' <remarks></remarks>
    WolfMarine

    ''' <summary>
    ''' Murloc Marine
    ''' </summary>
    ''' <remarks></remarks>
    MurlocMarine

    ''' <summary>
    ''' Worgen Marine
    ''' </summary>
    ''' <remarks></remarks>
    WorgenMarine

    ''' <summary>
    ''' Goblin Marine
    ''' </summary>
    ''' <remarks></remarks>
    GoblinMarine

    ''' <summary>
    ''' Zealot Chef
    ''' </summary>
    ''' <remarks></remarks>
    ZealotChef

    ''' <summary>
    ''' Stank
    ''' </summary>
    ''' <remarks></remarks>
    Stank

    ''' <summary>
    ''' Ornatus
    ''' </summary>
    ''' <remarks></remarks>
    Ornatus

    ''' <summary>
    ''' Facebook Corps Members
    ''' </summary>
    ''' <remarks></remarks>
    FacebookCorpsMembers

    ''' <summary>
    ''' Lion Marines
    ''' </summary>
    ''' <remarks></remarks>
    LionMarines

    ''' <summary>
    ''' Dragons
    ''' </summary>
    ''' <remarks></remarks>
    Dragons

    ''' <summary>
    ''' Raynor Marine (China)
    ''' </summary>
    ''' <remarks></remarks>
    RaynorMarineChina

    ''' <summary>
    ''' Urun
    ''' </summary>
    ''' <remarks></remarks>
    Urun

    ''' <summary>
    ''' Nyon
    ''' </summary>
    ''' <remarks></remarks>
    Nyon

    ''' <summary>
    ''' Executor
    ''' </summary>
    ''' <remarks></remarks>
    Executor

    ''' <summary>
    ''' Mohandar
    ''' </summary>
    ''' <remarks></remarks>
    Mohandar

    ''' <summary>
    ''' Selendis
    ''' </summary>
    ''' <remarks></remarks>
    Selendis

    ''' <summary>
    ''' Artanis
    ''' </summary>
    ''' <remarks></remarks>
    Artanis

    ''' <summary>
    ''' Drone
    ''' </summary>
    ''' <remarks></remarks>
    Drone

    ''' <summary>
    ''' Infested Colonist
    ''' </summary>
    ''' <remarks></remarks>
    InfestedColonist

    ''' <summary>
    ''' Infested Marine
    ''' </summary>
    ''' <remarks></remarks>
    InfestedMarine

    ''' <summary>
    ''' Corruptor
    ''' </summary>
    ''' <remarks></remarks>
    Corruptor

    ''' <summary>
    ''' Abberation
    ''' </summary>
    ''' <remarks></remarks>
    Abberation

    ''' <summary>
    ''' Brood Lord
    ''' </summary>
    ''' <remarks></remarks>
    BroodLord

    ''' <summary>
    ''' Overmind
    ''' </summary>
    ''' <remarks></remarks>
    Overmind

    ''' <summary>
    ''' Leviathan
    ''' </summary>
    ''' <remarks></remarks>
    Leviathan

    ''' <summary>
    ''' Overlord
    ''' </summary>
    ''' <remarks></remarks>
    Overlord

    ''' <summary>
    ''' Hydralisk Marine
    ''' </summary>
    ''' <remarks></remarks>
    HydraliskMarine

    ''' <summary>
    ''' Zer 'atai Dark Templar
    ''' </summary>
    ''' <remarks></remarks>
    ZerataiDarkTemplar

    ''' <summary>
    ''' Goliath
    ''' </summary>
    ''' <remarks></remarks>
    Goliath

    ''' <summary>
    ''' Lenassa Dark Templar
    ''' </summary>
    ''' <remarks></remarks>
    LenassaDarkTemplar

    ''' <summary>
    ''' Mira Han
    ''' </summary>
    ''' <remarks></remarks>
    MiraHan

    ''' <summary>
    ''' Archon
    ''' </summary>
    ''' <remarks></remarks>
    Archon

    ''' <summary>
    ''' Hybrid Reaver
    ''' </summary>
    ''' <remarks></remarks>
    HybridReaver

    ''' <summary>
    ''' Predator
    ''' </summary>
    ''' <remarks></remarks>
    Predator

    ''' <summary>
    ''' Zergling
    ''' </summary>
    ''' <remarks></remarks>
    Zergling

    ''' <summary>
    ''' Roach
    ''' </summary>
    ''' <remarks></remarks>
    Roach

    ''' <summary>
    ''' Baneling
    ''' </summary>
    ''' <remarks></remarks>
    Baneling

    ''' <summary>
    ''' Hydralisk
    ''' </summary>
    ''' <remarks></remarks>
    Hydralisk

    ''' <summary>
    ''' Queen
    ''' </summary>
    ''' <remarks></remarks>
    Queen

    ''' <summary>
    ''' Infestor
    ''' </summary>
    ''' <remarks></remarks>
    Infestor

    ''' <summary>
    ''' Ultralisk
    ''' </summary>
    ''' <remarks></remarks>
    Ultralisk

    ''' <summary>
    ''' Queen of Blades
    ''' </summary>
    ''' <remarks></remarks>
    QueenOfBlades

    ''' <summary>
    ''' Marine
    ''' </summary>
    ''' <remarks></remarks>
    Marine

    ''' <summary>
    ''' Marauder
    ''' </summary>
    ''' <remarks></remarks>
    Marauder

    ''' <summary>
    ''' Medivac
    ''' </summary>
    ''' <remarks></remarks>
    Medivac

    ''' <summary>
    ''' Siege Tank
    ''' </summary>
    ''' <remarks></remarks>
    SiegeTank

    ''' <summary>
    ''' Level 3 Zealot
    ''' </summary>
    ''' <remarks></remarks>
    Level3Zealot

    ''' <summary>
    ''' Level 8 Sentry
    ''' </summary>
    ''' <remarks></remarks>
    Level8Sentry

    ''' <summary>
    ''' Level 5 Stalker
    ''' </summary>
    ''' <remarks></remarks>
    Level5Stalker

    ''' <summary>
    ''' Level 11 Immortal
    ''' </summary>
    ''' <remarks></remarks>
    Level11Immortal

    ''' <summary>
    ''' Level 14 Oracle
    ''' </summary>
    ''' <remarks></remarks>
    Level14Oracle

    ''' <summary>
    ''' Level 17 High Templar
    ''' </summary>
    ''' <remarks></remarks>
    Level17HighTemplar

    ''' <summary>
    ''' Level 21 Tempest
    ''' </summary>
    ''' <remarks></remarks>
    Level21Tempest

    ''' <summary>
    ''' Level 23 Colossus
    ''' </summary>
    ''' <remarks></remarks>
    Level23Colossus

    ''' <summary>
    ''' Level 27 Carrier
    ''' </summary>
    ''' <remarks></remarks>
    Level27Carrier

    ''' <summary>
    ''' Level 29 Zeratul
    ''' </summary>
    ''' <remarks></remarks>
    Level29Zeratul

    ''' <summary>
    ''' Level 3 Marine
    ''' </summary>
    ''' <remarks></remarks>
    Level3Marine

    ''' <summary>
    ''' Level 5 Marauder
    ''' </summary>
    ''' <remarks></remarks>
    Level5Marauder

    ''' <summary>
    ''' Level 8 Hellbat
    ''' </summary>
    ''' <remarks></remarks>
    Level8Hellbat

    ''' <summary>
    ''' Level 11 Widow Mine
    ''' </summary>
    ''' <remarks></remarks>
    Level11WidowMine

    ''' <summary>
    ''' Level 14 Medivac
    ''' </summary>
    ''' <remarks></remarks>
    Level14Medivac

    ''' <summary>
    ''' Level 17 Banshee
    ''' </summary>
    ''' <remarks></remarks>
    Level17Banshee

    ''' <summary>
    ''' Level 21 Ghost
    ''' </summary>
    ''' <remarks></remarks>
    Level21Ghost

    ''' <summary>
    ''' Level 23 Thor
    ''' </summary>
    ''' <remarks></remarks>
    Level23Thor

    ''' <summary>
    ''' Level 27 Battlecruiser
    ''' </summary>
    ''' <remarks></remarks>
    Level27Battlecruiser

    ''' <summary>
    ''' Level 29 Raynor
    ''' </summary>
    ''' <remarks></remarks>
    Level29Raynor

    ''' <summary>
    ''' Level 11 Locust
    ''' </summary>
    ''' <remarks></remarks>
    Level11Locust

    ''' <summary>
    ''' Level 3 Zergling
    ''' </summary>
    ''' <remarks></remarks>
    Level3Zergling

    ''' <summary>
    ''' Level 5 Roach
    ''' </summary>
    ''' <remarks></remarks>
    Level5Roach

    ''' <summary>
    ''' Level 8 Hydralisk
    ''' </summary>
    ''' <remarks></remarks>
    Level8Hydralisk

    ''' <summary>
    ''' Level 14 Swarm Host
    ''' </summary>
    ''' <remarks></remarks>
    Level14SwarmHost

    ''' <summary>
    ''' Level 17 Infestor
    ''' </summary>
    ''' <remarks></remarks>
    Level17Infestor

    ''' <summary>
    ''' Level 21 Viper
    ''' </summary>
    ''' <remarks></remarks>
    Level21Viper

    ''' <summary>
    ''' Level 23 Brood Lord
    ''' </summary>
    ''' <remarks></remarks>
    Level23BroodLord

    ''' <summary>
    ''' Level 27 Ultralisk
    ''' </summary>
    ''' <remarks></remarks>
    Level27Ultralisk

    ''' <summary>
    ''' Level 29 Kerrigan
    ''' </summary>
    ''' <remarks></remarks>
    Level29Kerrigan

    ''' <summary>
    ''' Beta 30 Tempest
    ''' </summary>
    ''' <remarks></remarks>
    Beta30Tempest

    ''' <summary>
    ''' Beta 30 Warhound
    ''' </summary>
    ''' <remarks></remarks>
    Beta30Warhound

    ''' <summary>
    ''' Beta 30 Viper
    ''' </summary>
    ''' <remarks></remarks>
    Beta30Viper
  End Enum
End Namespace