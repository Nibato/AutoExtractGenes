using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AutoExtractGenes
{
    [HarmonyPatch(typeof(HealthCardUtility))]
    [HarmonyPatch("DrawOverviewTab")]
    public class HealthCardUtilityPatch_DrawOverViewTab
    {
        public static void Postfix(ref float __result, ref Rect rect, ref Pawn pawn, ref float curY)
        {
            var retCurY = __result;

            var autoExtractGenesComp = Utils.GetAutoExtractGenesComponent(pawn);

            if (autoExtractGenesComp == null)
                return;

            Text.Font = GameFont.Tiny;
            Text.Anchor = TextAnchor.UpperLeft;
            GUI.color = Color.white;

            //retCurY += 4f;

            var newRect = new Rect(0.0f, retCurY, rect.width, 24f);
            Widgets.CheckboxLabeled(newRect, (string)"nibato.AutoExtractGenes.AutoExtractGenes".Translate(), ref autoExtractGenesComp.isEnabled);

            if (Mouse.IsOver(newRect))
                TooltipHandler.TipRegion(newRect, "nibato.AutoExtractGenes.AutoExtractGenes.Tooltip".Translate());

            retCurY += 28f;
            __result = retCurY;
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
