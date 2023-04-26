using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;

namespace AutoExtractGenes
{
    public class AutoExtractGenesSettings : ModSettings
    {
        public bool defaultEnabled = false;


        public override void ExposeData()
        {
            Scribe_Values.Look(ref defaultEnabled, "defaultEnabled");
            base.ExposeData();
        }
    }
}
