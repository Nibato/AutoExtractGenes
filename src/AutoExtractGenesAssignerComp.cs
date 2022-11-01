using System;
using System.Reflection;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using HarmonyLib;

namespace AutoExtractGenes
{
    public class AutoExtractGenesAssignerComp : ThingComp
    {
        private static readonly MethodInfo m_selectPawn = AccessTools.Method(typeof(Building_GeneExtractor), "SelectPawn");
        private static readonly FastInvokeHandler selectPawn = MethodInvoker.GetHandler(m_selectPawn);

        private List<Pawn> others = new List<Pawn>();

        public override void CompTick()
        {
            if (parent.IsHashIntervalTick(250))
                CompTickRare();
        }

        public override void CompTickRare()
        {
            var parent = this.parent;

            if (!(parent is Building_GeneExtractor))
                return;

            var extractor = (Building_GeneExtractor)parent;

            if (!extractor.PowerOn || extractor.innerContainer.Count > 0)
                return;

            if (extractor.SelectedPawn != null)
                return;

            // get a list of pawns that other extrractors currently want
            others.Clear();
            foreach (var other in extractor.Map.listerBuildings.AllBuildingsColonistOfClass<Building_GeneExtractor>())
            {
                if (other.SelectedPawn != null && other != extractor)
                    others.Add(other.SelectedPawn);
            }

            foreach (var pawn in extractor.Map.mapPawns.AllPawnsSpawned)
            {
                var acceptanceReport = extractor.CanAcceptPawn(pawn);

                if (!acceptanceReport.Accepted)
                    continue;

                // Make sure we want to auto-extract this pawn's genes
                var comp = pawn.GetComp<AutoExtractGenesComp>();
                if (comp == null || !comp.isEnabled)
                    continue;

                // Make sure genes have regrown
                if (pawn.health.hediffSet.HasHediff(HediffDefOf.XenogermReplicating, false))
                    continue;

                // Make sure no other gene extractors want this pawn
                if (others.Contains(pawn))
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
