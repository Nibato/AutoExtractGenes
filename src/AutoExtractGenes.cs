using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace AutoExtractGenes
{
    public class AutoExtractGenes : Mod
    {
        private static int reorderableGroupId = -1;

        public AutoExtractGenes(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony("nibato.rimworld.autoextractgenes");

#if DEBUG
            Harmony.DEBUG = true;
#endif

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
