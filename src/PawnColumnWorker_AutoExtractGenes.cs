using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;

namespace AutoExtractGenes
{
    public class PawnColumnWorker_AutoExtractGenes : PawnColumnWorker_Checkbox
    {
        public static float IconPositionVertical = 35f;
        public static float IconPositionHorizontal = 5f;

        protected override bool GetValue(Pawn pawn)
        {
            var comp = Utils.GetAutoExtractGenesComponent(pawn);

            if (comp == null)
            {
                return false;
            }

            return comp.isEnabled;
        }

        protected override bool HasCheckbox(Pawn pawn) => Utils.GetAutoExtractGenesComponent(pawn) != null;

        protected override void SetValue(Pawn pawn, bool value, PawnTable table)
        {
            var comp = Utils.GetAutoExtractGenesComponent(pawn);

            if (comp == null)
            {
                return;
            }

            comp.isEnabled = value;
        }

        protected override string GetHeaderTip(PawnTable table) => "nibato.AutoExtractGenes.AutoExtractGenes".Translate() + "\n\n" + "Numbers_ColumnHeader_Tooltip".Translate();

        protected override string GetTip(Pawn pawn) => "nibato.AutoExtractGenes.AutoExtractGenes.Tooltip".Translate();
    }
}
