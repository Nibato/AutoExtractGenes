using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace AutoExtractGenes
{
    public class AutoExtractGenes : Mod
    {
        AutoExtractGenesSettings settings;

        public AutoExtractGenes(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<AutoExtractGenesSettings>();

            Harmony harmony = new Harmony("nibato.rimworld.autoextractgenes");

#if DEBUG
            Harmony.DEBUG = true;
#endif

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            
            listingStandard.CheckboxLabeled("nibato.AutoExtractGenes.defaultEnabledExplanation".Translate(), 
                ref settings.defaultEnabled, 
                "nibato.AutoExtractGenes.defaultEnabledTooltip".Translate());

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "nibato.AutoExtractGenes.AutoExtractGenes".Translate();
        }

        public static AutoExtractGenesSettings getSettings()
        {
            return LoadedModManager.GetMod<AutoExtractGenes>().GetSettings<AutoExtractGenesSettings>();
        }

    }
}
