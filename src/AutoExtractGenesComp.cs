using Verse;
using System;

namespace AutoExtractGenes
{
    public class AutoExtractGenesComp : ThingComp
    {
        public bool isEnabled = false;



        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<bool>(ref this.isEnabled, "nibato.AutoExtractGenes.Enabled", false);
        }
    }
}
