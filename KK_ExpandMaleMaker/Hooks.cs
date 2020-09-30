using ChaCustom;
using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace KK_ExpandMaleMaker
{
    internal static class Hooks {
        internal static bool isDark;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CustomChangeBodyMenu), "Start")]
        public static void DisplayHeightPostfix() {
            if (KK_ExpandMaleMaker.hairEnabled.Value) {
                GameObject element = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/01_BodyTop/tglUnderhair");
                SetUnderhairDisplay(element);
                SetUnderhairOffset(element);
            }

            if (KK_ExpandMaleMaker.heightEnabled.Value) KK_ExpandMaleMaker.instance.StartCoroutine(IGuessThisHasToBeDelayed()); //luv 2 be forced into shitty singleton patterns by unity design choices

            IEnumerator IGuessThisHasToBeDelayed() {
                yield return null;
                yield return null;
                var element = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/01_BodyTop/tglAll/AllTop");
                element.transform.GetChild(0).gameObject.SetActive(true);
                yield return null;
            }
        }

        static void SetUnderhairDisplay(GameObject element) {
            element.SetActive(true);
        }

        static void SetUnderhairOffset(GameObject element) {
            element.transform.GetChild(1).position = new Vector3(60.78f, 432.85f, 0);
            element.transform.GetChild(1).localPosition = new Vector3(132.65f, 630.0f, 0);
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
