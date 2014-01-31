using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;

namespace PersistingData
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    class Number_over_time:MonoBehaviourWindow
    {
        internal override void Awake()
        {
            //this just shows the window
            WindowRect = new Rect(300, 0, 300, 200);
            Visible = true;

            //this is a wrapper for invokerepeating - once a second
            StartRepeatingWorker(1);
        }

        internal override void RepeatingWorker()
        {
            //hsa the smoothstep run past its duration
            if ((Time.time - SmoothStart)>SmoothDuration)
            {
                //record the start of the smoothing
                SmoothStart = Time.time;
                //get a new destination and start stepping
                windMinimum = 0.0f;
                windMax = 6.0f;

                //store the final value as the new start
                windInitial = windFinal;
                //generate a new final value
                windFinal = UnityEngine.Random.Range(windMinimum, windMax);
            }

            //step closer
            //How far through the smooth duration are we
            float StepFraction = (Time.time - SmoothStart) / SmoothDuration;
            //now change the force
            windForce = UnityEngine.Mathf.SmoothStep(windInitial, windFinal, StepFraction);

        }

        internal float SmoothStart = 0f;
        internal float SmoothDuration = 5f;

        internal float windMinimum = 0f;
        internal float windMax = 0f;

        internal float windInitial = 0f;
        internal float windFinal = 0f;
        internal float windForce = 0f;

        internal override void DrawWindow(int id)
        {
            GUILayout.BeginVertical();
            GUILayout.Label(String.Format("{0}", SmoothStart));
            GUILayout.Label(String.Format("{0}", SmoothDuration));
            GUILayout.Label(String.Format("{0}", (Time.time - SmoothStart)));
            GUILayout.Label(String.Format("{0}", windInitial));
            GUILayout.Label(String.Format("{0}", windForce));
            GUILayout.Label(String.Format("{0}", windFinal));
            GUILayout.EndVertical();
        }
    }
}
