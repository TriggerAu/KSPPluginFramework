using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;

namespace PersistingData
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    class DictionaryTest : MonoBehaviourExtended
    {
        SettingsDict sTest = new SettingsDict("DictStore.cfg");
        internal override void Awake()
        {
            sTest.Save();
        }

    }


    class SettingsDict : ConfigNodeStorage
    {
        public SettingsDict(String FilePath) : base(FilePath) { 
                ListDict.Add("Key1","Value1");
                ListDict.Add("Key2","Value2");
                ListDict.Add("Key3","Value3");
        }

        [Persistent]
        internal List<String> ListString =new List<String>(){"String1","String2","String3" };

        [Persistent]
        internal Dictionary<String, String> ListDict = new Dictionary<string, string>();  



    }
}
