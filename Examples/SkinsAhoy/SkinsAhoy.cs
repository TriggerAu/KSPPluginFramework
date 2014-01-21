using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;

namespace SkinsAhoy
{
    [KSPAddon( KSPAddon.Startup.MainMenu,false)]
    public class SkinsAhoy : MonoBehaviourExtended
    {
        SkinsMainWindow Main;
        internal override void Awake()
        {
            Main = gameObject.AddComponent<SkinsMainWindow>();
        }

        internal override void OnGUIOnceOnly()
        {
            //Lets get some skins together    
            GUISkin skinCustom = SkinsLibrary.CopySkin(SkinsLibrary.DefSkinType.Unity);
            skinCustom.button = SkinsLibrary.DefKSPSkin.button;
            SkinsLibrary.AddSkin("CustomSkin",skinCustom);

            GUIStyle redButton = new GUIStyle(SkinsLibrary.DefKSPSkin.button);
            redButton.name = "RedButton";
            redButton.normal.textColor = Color.red;
            redButton.hover.textColor = Color.red;
            SkinsLibrary.AddStyle(SkinsLibrary.DefSkinType.KSP, redButton);
            SkinsLibrary.AddStyle("CustomSkin", redButton);

            redButton = new GUIStyle(SkinsLibrary.DefUnitySkin.button);
            redButton.name = "RedButton";
            redButton.normal.textColor = Color.red;
            redButton.hover.textColor = Color.red;
            SkinsLibrary.AddStyle(SkinsLibrary.DefSkinType.Unity, redButton);

            GUIStyle CustomTooltip = new GUIStyle();
            CustomTooltip.name = "Tooltip";
            CustomTooltip.normal.textColor = Color.yellow;
            SkinsLibrary.AddStyle("CustomSkin", CustomTooltip);
        }
    }

    internal class SkinsMainWindow : MonoBehaviourWindow
    {
        internal override void Awake()
        {
            Visible = true;
            WindowCaption = "Main Window";
            WindowRect = new Rect(0, 0, 300, 400);
            TooltipsEnabled = true;

            skinsWindows = new List<SkinsWindow>();
        }

        private List<SkinsWindow> skinsWindows;

        internal override void DrawWindow(int id)
        {
            GUILayout.Label("Choose a Skin");

            if (GUILayout.Button(new GUIContent("KSP Style", "Sets the style to be the default KSPStyle")))
                SkinsLibrary.SetCurrent(SkinsLibrary.DefSkinType.KSP);
            if (GUILayout.Button(new GUIContent("Unity Style", "Sets the style to be the default Unity Style")))
                SkinsLibrary.SetCurrent(SkinsLibrary.DefSkinType.Unity);
            if (GUILayout.Button(new GUIContent("Custom Style", "Sets the style to be a custom style I just made up")))
                SkinsLibrary.SetCurrent("CustomSkin");

            GUILayout.Space(20);
            if (GUILayout.Button("Open New Window"))
            {
                SkinsWindow winTemp = gameObject.AddComponent<SkinsWindow>();
                skinsWindows.Add(winTemp);
            }
            if (GUILayout.Button("Destroy a Window"))
            {
                if (skinsWindows.Count>0)
                {
                    skinsWindows[0].Visible=false;
                    skinsWindows[0]=null;
                    skinsWindows.RemoveAt(0);
                }
            }
            if (GUILayout.Button("Toggle Drag for all TestWindows"))
            {
                foreach (SkinsWindow sw in skinsWindows)
                {
                    sw.DragEnabled = !sw.DragEnabled;
                }
            }

            GUILayout.Label("Tooltipwidth. 0 means nowrap");
            TooltipMaxWidth = Convert.ToInt32(GUILayout.TextField(TooltipMaxWidth.ToString()));
        }
    }

    internal class SkinsWindow : MonoBehaviourWindow
    {
        internal override void Awake()
        {
            WindowCaption = "Test Window";
            Visible = true;
            WindowRect = new Rect(UnityEngine.Random.Range(100,800),UnityEngine.Random.Range(100,800),300,300);
            ClampToScreen=true;
            DragEnabled=true;
            TooltipsEnabled = true;
        }

        Single horizvalue = 50;
        Boolean togglevalue = false;
        internal override void DrawWindow(int id)
        {
            GUILayout.Button("This is a button");
            togglevalue  =GUILayout.Toggle(togglevalue ,"This is a Toggle");
            GUILayout.Label("This is a Label");
            horizvalue = GUILayout.HorizontalScrollbar(horizvalue,20,0,100);
            GUILayout.Label("Scrollbar Value=" + horizvalue.ToString("0"));
            GUILayout.Button("red text button", "RedButton");
            GUILayout.Button("Unity button", SkinsLibrary.DefUnitySkin.button);
            GUILayout.Label("DragEnabled:" + DragEnabled.ToString());

        }
    }
    
}
