namespace AutoExtractGenes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using HarmonyLib;
    using JetBrains.Annotations;
    using RimWorld;
    using RimWorld.Planet;
    using UnityEngine;
    using Verse;

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
