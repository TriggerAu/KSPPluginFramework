using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KSP;
using UnityEngine;

namespace MBExtended
{
    [KSPAddon(KSPAddon.Startup.MainMenu,false)]
    public class MBExtended : MonoBehaviourExtended
    {
        internal override void Awake()
        {
            LogFormatted("Parent is awake");

            //Create a Child Object
            MBExtendedChild Child = gameObject.AddComponent<MBExtendedChild>();

            //Start the repeating worker to fire once each second
            StartRepeatingWorker(1);
        }

        private Int32 intCounter=0;
        internal override void RepeatingWorker()
        {
            System.Threading.Thread.Sleep(intCounter);
            //increment the counter
            intCounter += 1;
            if (intCounter>9)
            {
                //Stop the function after ten seconds
                StopRepeatingWorker();
            }
            LogFormatted("Last RepeatFunction Ran for: {0}ms",RepeatingWorkerDuration.TotalMilliseconds);
            LogFormatted("UT Since Last RepeatFunction: {0}",RepeatingWorkerUTPeriod);
        }
    }

    public class MBExtendedChild : MonoBehaviourExtended
    {
        internal override void Awake()
        {
            LogFormatted("Child is awake");
        }
    }
}
