using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;

namespace MBWindow
{
    [KSPAddon(KSPAddon.Startup.MainMenu,false)]
    public class MBWindow : MonoBehaviourWindow
    {
        internal override void Awake()
        {
            WindowCaption = "Test Window";
            WindowRect = new Rect(0, 0, 250, 50);
            Visible = true;
        }

        internal override void Update()
        {
            //toggle whether its visible or not
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.F11))
                Visible = !Visible;
        }

        internal override void DrawWindow(int id)
        {
            GUILayout.Label(new GUIContent("Window Contents", "Here is a reallly long tooltip to demonstrate the war and peace model of writing too much text in a tooltip\r\n\r\nIt even includes a couple of carriage returns to make stuff fun"));
            GUILayout.Label(String.Format("Drag Enabled:{0}",DragEnabled.ToString()));
            GUILayout.Label(String.Format("ClampToScreen:{0}",ClampToScreen.ToString()));
            GUILayout.Label(String.Format("Tooltips:{0}",TooltipsEnabled.ToString()));

            if (GUILayout.Button("Toggle Drag"))
                DragEnabled = !DragEnabled;
            if (GUILayout.Button("Toggle Screen Clamping"))
                ClampToScreen = !ClampToScreen;

            if (GUILayout.Button(new GUIContent("Toggle Tooltips","Can you see my Tooltip?")))
                TooltipsEnabled = !TooltipsEnabled;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Max Tooltip Width");
            TooltipMaxWidth=Convert.ToInt32(GUILayout.TextField(TooltipMaxWidth.ToString()));
            GUILayout.EndHorizontal();
            GUILayout.Label("Width of 0 means no limit");

            GUILayout.Label("Alt+F11 - shows/hides window");

        }
    }
}
