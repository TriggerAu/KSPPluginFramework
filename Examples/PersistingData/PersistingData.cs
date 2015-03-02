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
    public class MBPersistingData: MonoBehaviourWindow
    {
        SettingsTest settings = new SettingsTest("PersistingData.cfg");
            
        internal override void Awake()
        {
            this.WindowCaption = "Persistent Settings";
            this.Visible = true;
            this.DragEnabled = true;
            this.WindowRect = new Rect(0, 0, 300, 300);

            LogFormatted_DebugOnly("Loading Settings...");
            settings.Load();

            LogFormatted_DebugOnly("Saving Settings...");
            settings.Save();
        }

        internal override void DrawWindow(int id)
        {
            GUILayout.BeginVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Enum:" + settings.TestEnumValue);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Toggle", GUILayout.Width(80)))
            {
                if (settings.TestEnumValue == SettingsTest.TestEnum.TestName1)
                    settings.TestEnumValue = SettingsTest.TestEnum.TestName2;
                else
                    settings.TestEnumValue = SettingsTest.TestEnum.TestName1;
                settings.Save();
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("String:" + settings.testString);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Double:" + settings.testDouble);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add 5", GUILayout.Width(80)))
            { 
                settings.testDouble += 5;
                settings.Save();
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("Quaternion:" + settings.testQuat.ToString());
            GUILayout.BeginHorizontal();
            GUILayout.Label("Bool:" + settings.testBool);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Toggle", GUILayout.Width(80)))
            {
                settings.testBool = !settings.testBool;
                settings.Save();
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("Vect3:" + settings.testVect3.ToString());

            GUILayout.Label("ClassStore.String:" + settings.StoreThis.TestString);

            GUILayout.BeginHorizontal();
            GUILayout.Label("ManNode.DeltaV:" + settings.ManNode.DeltaV.ToString());
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Increase",GUILayout.Width(80)))
            {
                settings.ManNode.DeltaV.x += 1;
                settings.ManNode.DeltaV.y += 2;
                settings.ManNode.DeltaV.z += 3;
                settings.Save();
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("ManNode.NodeRotation:" + settings.ManNode.nodeRotation.ToString());
            GUILayout.EndVertical();
        }
    }



    public class SettingsTest:ConfigNodeStorage
    {
        internal SettingsTest(String FilePath) : base(FilePath) { }

        internal enum TestEnum { TestName1, TestName2 }
        [Persistent] internal TestEnum TestEnumValue = TestEnum.TestName2;

        [Persistent] internal String testString = "Init";
        [Persistent] internal Double testDouble = 1.234;
        [Persistent] internal Quaternion testQuat = new Quaternion(90, 180, 270, 360);
        [Persistent] internal QuaternionD testQuatD = new QuaternionD(890, 8180, 8270, 8360);
        [Persistent] internal Boolean testBool = true;

        //TEST THESE TWO ERRORS IN KSP!!!!!!!
        //This throws a "Object of type 'UnityEngine.Vector3' cannot be converted to type 'UnityEngine.Vector2'." at LoadObjectFromConfig
        //[Persistent] internal Vector2 testVect2 = new Vector2(10, 11);
        [Persistent] internal Vector3 testVect3 = new Vector3d(1, 2, 3);

        //Throws "Specified cast is not valid." at CreateConfigFromObject
        //[Persistent] Vector3d testVect3d = new Vector3d(6, 7, 8);
        //yet this works fine
        [Persistent] internal Vector3 testVect3d = new Vector3d(6, 7, 8);

        [Persistent] internal Vector4 testVect4 = new Vector4(20, 21, 22, 23);

        [Persistent] internal Color testColor = Color.red;
        [Persistent] internal Color32 testColor32 = new Color32(255, 127, 127, 255);

        [Persistent] internal Matrix4x4 testMatrix = new Matrix4x4();

        //Class containing standard types
        [Persistent] internal ClassStore StoreThis = new ClassStore();


        //Custom Class Storage
        [Persistent] private ManeuverNodeStorage ManNodeStore = new ManeuverNodeStorage();
        internal ManeuverNode ManNode = new ManeuverNode();

        // Events to convert ManNode to Storable object
        public override void OnDecodeFromConfigNode()
        {
            ManNode = ManNodeStore.ToManeuverNode();
        }
        public override void OnEncodeToConfigNode()
        {
            ManNodeStore.FromManeuverNode(ManNode);
        }
    }

    public class ClassStore
    {
        [Persistent] internal String TestString = "blah blah blah";
        [Persistent] internal Color colstore = Color.red;

        String DontStoreThisString = "Hello";
    }

    public class ManeuverNodeStorage
    {
        [Persistent] Vector3 DeltaV;
        [Persistent] Quaternion NodeRotation;
        [Persistent] Double UT;

        public ManeuverNode ToManeuverNode()
        {
            ManeuverNode retManNode = new ManeuverNode();
            retManNode.DeltaV = DeltaV;
            retManNode.nodeRotation = NodeRotation;
            retManNode.UT = UT;
            return retManNode;
        }
        public ManeuverNodeStorage FromManeuverNode(ManeuverNode ManNodeToStore)
        {
            this.DeltaV = ManNodeToStore.DeltaV;
            this.NodeRotation = ManNodeToStore.nodeRotation;
            this.UT = ManNodeToStore.UT;
            return this;
        }
    }
}
