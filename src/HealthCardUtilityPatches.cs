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
	public class HealthCardUtilityPatch_DrawOverViewTab
	{


		public static void Postfix(ref float __result, ref Rect leftRect, ref Pawn pawn)
		{
			var curY = __result;


			if (!(pawn.IsColonist || pawn.IsSlaveOfColony || pawn.IsPrisonerOfColony)
					|| pawn.Dead
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



	[HarmonyPatch(typeof(ITab_Pawn_Health), MethodType.Constructor)]
	public class HealthCardUtilityPatch_ITab_Pawn_Health
	{
		private static AccessTools.FieldRef<ITab_Pawn_Health, Vector2> size = AccessTools.FieldRefAccess<ITab_Pawn_Health, Vector2>("size");


		public static void Postfix(ITab_Pawn_Health __instance)
		{
			size(__instance).y += 28f;
		}
	}
}
