using ChaCustom;
using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace ExpandMaleMaker {
    internal static partial class Hooks {
        internal static bool isDark;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CustomChangeBodyMenu), "Start")]
        public static void StartPostfix() {

            if (ExpandMaleMaker.heightEnabled.Value) ExpandMaleMaker.instance.StartCoroutine(IGuessThisHasToBeDelayed()); //luv 2 be forced into shitty singleton patterns by unity design choices

            IEnumerator IGuessThisHasToBeDelayed() {
                yield return null;
                yield return null;
                var element = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/01_BodyTop/tglAll/AllTop");
                element.transform.GetChild(0).gameObject.SetActive(true);
                yield return null;
            }

            if (ExpandMaleMaker.hairEnabled.Value) {
                EnableUnderhair();
            }
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(ChaControl), "Initialize")]
        public static IEnumerable<CodeInstruction> ChaControl_Initialize_RemoveHeightLock(IEnumerable<CodeInstruction> instructions) {
            var cm = new CodeMatcher(instructions);
            // find branch that overrides the male height
            cm.MatchForward(false, new CodeMatch(OpCodes.Ldc_R4, 0.6f))
                .MatchBack(false, new CodeMatch(OpCodes.Call))
                .MatchBack(false, new CodeMatch(c => c.Branches(out _)));
            // turn it into an unconditional jump
            cm.InsertAndAdvance(new CodeInstruction(OpCodes.Pop))
                .SetOpcodeAndAdvance(OpCodes.Br);
            return cm.Instructions();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(ChaControl), "SetShapeBodyValue")]
        [HarmonyPatch(typeof(ChaControl), "UpdateShapeBodyValueFromCustomInfo")]
        public static IEnumerable<CodeInstruction> ChaControl_SetShapeBodyValue_RemoveHeightLock(IEnumerable<CodeInstruction> instructions) {
            var cm = new CodeMatcher(instructions);
            // find branch that overrides the male height
            cm.MatchForward(false, new CodeMatch(OpCodes.Call, AccessTools.PropertyGetter(typeof(ChaInfo), nameof(ChaInfo.sex))))
                .MatchForward(false, new CodeMatch(c => c.Branches(out _)));
            // turn it into an unconditional jump
            cm.InsertAndAdvance(new CodeInstruction(OpCodes.Pop))
                .SetOpcodeAndAdvance(OpCodes.Br);
            return cm.Instructions();
        }
    }
}
