using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using HarmonyLib;
using UnityEngine;


namespace AutoExtractGenes
{ 
		[HarmonyPatch(typeof(HealthCardUtility))]
		[HarmonyPatch("DrawOverviewTab")]
		public class HealthCardUtilityPatches
		{


			public static void Postfix(ref float __result, ref Rect leftRect, ref Pawn pawn) //pass the __result by ref to alter it.
			{
				var curY = __result;


				if (!(pawn.IsColonist || pawn.IsSlaveOfColony || pawn.IsPrisonerOfColony) 
						|| pawn.Dead 
						|| pawn.DevelopmentalStage.Baby()
						|| !pawn.RaceProps.Humanlike
						|| pawn.IsQuestLodger())
					return;

				Text.Font = GameFont.Tiny;
				Text.Anchor = TextAnchor.UpperLeft;
				GUI.color = Color.white;

				var autoExtractGenesComp = pawn.GetComp<AutoExtractGenesComp>();

				if (autoExtractGenesComp == null)
					return;

				//curY += 4f;

				var rect = new Rect(0.0f, curY, leftRect.width, 24f);
				Widgets.CheckboxLabeled(rect, (string)"nibato.AutoExtractGenes.AutoExtractGenes".Translate(), ref autoExtractGenesComp.isEnabled);

				if (Mouse.IsOver(rect))
					TooltipHandler.TipRegion(rect, "nibato.AutoExtractGenes.AutoExtractGenes.Tooltip".Translate());

				curY += 28f;
				__result = curY;
			}
		}
}
