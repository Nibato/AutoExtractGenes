using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;
using UnityEngine;


namespace AutoExtractGenes
{
    internal static class Utils
    {
		public static AutoExtractGenesComp GetAutoExtractGenesComponent(Pawn pawn)
		{
			if (!(pawn.IsColonist || pawn.IsSlaveOfColony || pawn.IsPrisonerOfColony)
					|| pawn.Dead
					|| !pawn.RaceProps.Humanlike
					|| pawn.IsQuestLodger())
			{
				return null;
			}


			var autoExtractGenesComp = pawn.GetComp<AutoExtractGenesComp>();


			if (autoExtractGenesComp == null)
				return null;

			return autoExtractGenesComp;
		}
	}
}
