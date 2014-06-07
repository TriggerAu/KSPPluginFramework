KSPPluginFramework
================
The KSP Plugin Framework was developed as part of a rewrite of Kerbal Alarm Clock and KSP Alternate Resource Panel to provide some common code that could be reused with little or no modification.

It then became a project unto itself :)

This project contains a number of helper classes which include code to help you quickly build a plugin for [Kerbal Space Program](http://www.kerbalspaceprogram.com/)

The code can be used as is or as reference so that you can write your own

**Forum Thread:** [KSP Plugin Framework](http://forum.kerbalspaceprogram.com/threads/66503-KSP-Plugin-Framework)  
**Author:** [TriggerAu](https://github.com/TriggerAu)


###History
####Version 1.2 - 8 June 2014  
- Added version tags to files 
- Added tooltip timeout fix

####Version 1.1 - 6 Apr 2014
- Updated the framework with feedback issues
- Restructured project examples files
- Dropdown example

####Version 1.0 - 22 Jan 2014
Feature complete set of stuff
- **MonoBehaviourExtended** - Adds some bits to the base MonoBehaviour class - logging, repeating background function, definitions of Unity Events
- **MonoBehaviourWindow** - Adds to the above with all the bits to draw a window, Contains code for - Visibilty, Screen Clamping, Tooltips, Wrappers
- **SkinsLibrary** - A way to change the whole guis skin in one go for your code as well as easily set up new skins.
- **ConfigNodeStorage** - This class (and docco) helps with being able to have a "settings" object that you can easily save/load as required, and also some examples of how ot serialise complex objects
