using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;

namespace PersistingDataErrors
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class PersistingDataErrors: MonoBehaviour
    {
        String AssemblyPath;

        //Objects to test storage with
        Storage sTest = new Storage();
        Storage2 sTest2 = new Storage2();
        Storage3 sTest3 = new Storage3();

        void Awake()
        {
            AssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).Replace("\\", "/");

            //Test the first object - which contains only a string
            UnityEngine.Debug.Log("PersistingDataErrors1:Saving Object...");
            try {
                ConfigNode cnToSave = ConfigNode.CreateConfigFromObject(sTest, new ConfigNode());
                cnToSave.Save(AssemblyPath + "/Test.cfg");
                UnityEngine.Debug.Log("PersistingDataErrors1:Saved!");
            }
            catch (Exception ex) { UnityEngine.Debug.Log("PersistingDataErrors1:Failed To Save-" + ex.Message); }
            //and load it back to show no errors
            UnityEngine.Debug.Log("PersistingDataErrors1:Loading Object...");
            try {
                ConfigNode cnToLoad = ConfigNode.Load(AssemblyPath + "/Test.cfg");
                ConfigNode.LoadObjectFromConfig(sTest, cnToLoad);
                UnityEngine.Debug.Log("PersistingDataErrors1:Loaded!");
            }
            catch (Exception ex) { UnityEngine.Debug.Log("PersistingDataErrors3:Failed To Load-" + ex.Message); }

            //Now the Vector3D - which will fail to save
            UnityEngine.Debug.Log("PersistingDataErrors2:Saving Object...");
            try {
                ConfigNode cnToSave = ConfigNode.CreateConfigFromObject(sTest2, new ConfigNode());   //ERROR OCCURS HERE
                cnToSave.Save(AssemblyPath + "/Test2.cfg");
                UnityEngine.Debug.Log("PersistingDataErrors2:Saved!");
            }
            catch (Exception ex) { UnityEngine.Debug.Log("PersistingDataErrors2:Failed To Save-" + ex.Message); }

            //Now the Vector2 which saves, but cannot be loaded
            UnityEngine.Debug.Log("PersistingDataErrors3:Saving Object...");
            try {
                ConfigNode cnToSave = ConfigNode.CreateConfigFromObject(sTest3, new ConfigNode());
                cnToSave.Save(AssemblyPath + "/Test3.cfg");
                UnityEngine.Debug.Log("PersistingDataErrors3:Saved!");
            }
            catch (Exception ex) { UnityEngine.Debug.Log("PersistingDataErrors3:Failed To Save-" + ex.Message); }
            //Load attempt is here
            UnityEngine.Debug.Log("PersistingDataErrors3:Loading Object...");
            try {
                ConfigNode cnToLoad = ConfigNode.Load(AssemblyPath + "/Test3.cfg");
                ConfigNode.LoadObjectFromConfig(sTest3, cnToLoad);                                   //ERROR OCCURS HERE
                UnityEngine.Debug.Log("PersistingDataErrors3:Loaded!");
            }
            catch (Exception ex) { UnityEngine.Debug.Log("PersistingDataErrors3:Failed To Load-" + ex.Message); }
        }
    }

    public class Storage
    {
        [Persistent]
        String TestString = "New String";

        //[Persistent]
        //Vector3d vect3d = new Vector3d(1, 2, 3);
    }
    public class Storage2
    {
        [Persistent]
        String TestString = "New String2";

        [Persistent]
        Vector3d vect3d = new Vector3d(1, 2, 3);
    }
    public class Storage3
    {
        [Persistent]
        String TestString = "New String3";

        [Persistent]
        Vector2 vect3d = new Vector2(1, 2);
    }

}
