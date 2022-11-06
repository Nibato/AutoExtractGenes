using System;
using System.Reflection;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using HarmonyLib;

namespace AutoExtractGenes
{
    public class AutoExtractGenesAssignerComp : ThingComp
    {
        private static readonly MethodInfo m_selectPawn = AccessTools.Method(typeof(Building_GeneExtractor), "SelectPawn");
        private static readonly FastInvokeHandler selectPawn = MethodInvoker.GetHandler(m_selectPawn);

        private List<Pawn> others = new List<Pawn>();

        private bool isOthersUpdated = false;

        public override void CompTick()
        {
            if (parent.IsHashIntervalTick(250))
                CompTickRare();
        }
        private List<Pawn> getOthers(Building_GeneExtractor extractor)
        {
            if (isOthersUpdated)
                return others;

            others.Clear();
            foreach (var other in extractor.Map.listerBuildings.AllBuildingsColonistOfClass<Building_GeneExtractor>())
            {
                if (other.SelectedPawn != null && other != extractor)
                    others.Add(other.SelectedPawn);
            }

            isOthersUpdated = true;

            return others;
        }

        public override void CompTickRare()
        {
            var parent = this.parent;

            if (!(parent is Building_GeneExtractor extractor))
                return;

            if (!extractor.PowerOn || extractor.innerContainer.Count > 0)
                return;

            if (extractor.SelectedPawn != null)
                return;

            isOthersUpdated = false;

            foreach (var pawn in extractor.Map.mapPawns.AllPawnsSpawned)
            {
                // Make sure we want to auto-extract this pawn's genes
                var comp = pawn.GetComp<AutoExtractGenesComp>();
                if (comp == null || !comp.isEnabled)
                    continue;

                // Make sure genes have regrown and the pawn is not deathresting
                if (pawn.health.hediffSet.HasHediff(HediffDefOf.XenogermReplicating) || pawn.health.hediffSet.HasHediff(HediffDefOf.Deathrest))
                    continue;

                // Bail if this pawn is in a high priority duty
                if (pawn.mindState.duty?.def.hook == ThinkTreeDutyHook.HighPriority)
                    continue;

                var acceptanceReport = extractor.CanAcceptPawn(pawn);
                if (!acceptanceReport.Accepted)
                    continue;

                // Make sure no other gene extractors want this pawn
                if (getOthers(extractor).Contains(pawn))
                    continue;

                // Make sure the pawn can reach this 
                // if (!pawn.CanReach(extractor, PathEndMode.InteractionCell, Danger.Deadly, false, false, TraverseMode.ByPawn))
                //    return;

                
                //extractor.SelectedPawn = pawn;
                //if (!pawn.IsPrisonerOfColony && !pawn.Downed)
                //    pawn.jobs.TryTakeOrderedJob(JobMaker.MakeJob(JobDefOf.EnterBuilding, extractor), new JobTag?(JobTag.Misc), false);

                selectPawn(extractor, pawn);
            }

            // Don't hold on to references
            others.Clear();
        }


    }
}

