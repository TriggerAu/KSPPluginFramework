using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;
using KSPPluginFramework;

namespace PersistingData
{
    [KSPAddon(KSPAddon.Startup.MainMenu,false)]
    class ConfigNodeExample :MonoBehaviourExtended
    {
        SettingsCN settings = new SettingsCN();

        internal override void Awake()
        {
            String FilePath = System.IO.Path.Combine(
                System.Reflection.Assembly.GetExecutingAssembly().Location, 
                "Settings.cfg").Replace("\\", "/");

            ConfigNode cnLoad = new ConfigNode();
            cnLoad = ConfigNode.Load(FilePath);
            ConfigNode.LoadObjectFromConfig(settings, cnLoad);

            settings.TestString = "Hello again";

            ConfigNode cnToPrint = new ConfigNode("settings");
            cnToPrint= ConfigNode.CreateConfigFromObject(settings);
            LogFormatted(cnToPrint.ToString());

            cnToPrint.Save(FilePath);
        }
    }
    
    class SettingsCN:IPersistenceLoad,IPersistenceSave
    {
        [Persistent] internal String TestString = "Fred";
        [Persistent] internal Boolean TestBoolean = true;

        void IPersistenceLoad.PersistenceLoad() { }
        void IPersistenceSave.PersistenceSave() { }
    }


    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    class ConfigNodeStorageExample : MonoBehaviourExtended
    {
        SettingsCNS settings = new SettingsCNS("Settings2.cfg");
        internal override void Awake()
        {
            settings.Load();
            settings.TestString = "hello again";
            LogFormatted(settings.AsConfigNode.ToString());
            settings.Save();
        }
    }

    class SettingsCNS:ConfigNodeStorage
    {
        public SettingsCNS(String FilePath) : base(FilePath) { }
        [Persistent] internal String TestString = "Fred";
        [Persistent] internal Boolean TestBoolean = true;
    }





}
