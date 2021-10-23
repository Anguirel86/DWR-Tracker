# DWR Tracker

Auto tracker for the [Dragon Warrior Randomizer](https://github.com/mcgrew/dwrandomizer)

![DWR Tracker screenshot](https://github.com/anguirel86/DWR-Tracker/raw/master/dwr-tracker.PNG)

## Requirements

* Windows
* Lua enabled emulator
  * Bizhawk
  * FCEUX
  * Mesen

## Features

* Stat tracking
* Equipment tracking
* Quest item tracking
* Dungeon/town map display
* Enemy info while in battle

The following features are available only if a ROM is read in through "File -> Read Rom"
* Automated overworld mapping
* Enemy ability info  
  
## Usage

This project is in an **alpha** state. You need to use the tracker via the following steps.

1. Double-click the "DWR Tracker.exe" file
2. Open you emulator 
3. Launch the Dragon Warrior Randomizer seed you wish to play
4. Open a new Lua script window in the emulator 
5. Run the autotracker.lua script packaged with the tracker
6. The emulator and tracker will connect and "Connection established" will be displayed at the bottom of the tracker window
7. Select "File -> Read Rom" and select your ROM file if overworld mapping and enemy ability info is desired 

## Known Issues

* Switching ROMs without pausing the tracker lua script can break the connection and can force a restart of the tracker to resume autotracking.
* Mesen doesn't like to reconnect after a connection is broken.

## TODO 

* Implement clickable icons so the tracker can be used as a manual tracker.
* Ability to hide/toggle the info/map panel.


