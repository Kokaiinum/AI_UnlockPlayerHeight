using ChaCustom;
using HarmonyLib;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace KK_ExpandMaleMaker {

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

            var il = instructions.ToList();

            var index = il.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 0.6f);
            if (index <= 0) return il;
            if (isDark) {
                il[index - 9].opcode = OpCodes.Nop;
                il[index - 8].opcode = OpCodes.Nop;
                il[index - 7].opcode = OpCodes.Nop;
            }
            il[index - 6].opcode = OpCodes.Nop;
            il[index - 5].opcode = OpCodes.Nop;
            il[index - 4].opcode = OpCodes.Nop;
            il[index - 3].opcode = OpCodes.Nop;
            il[index - 2].opcode = OpCodes.Nop;
            il[index - 1].opcode = OpCodes.Nop;
            il[index].opcode = OpCodes.Nop;
            il[index + 1].opcode = OpCodes.Nop;
            if (isDark) {
                il[index + 2].opcode = OpCodes.Nop;
                il[index + 3].opcode = OpCodes.Nop;
            }

            return il;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(ChaControl), "SetShapeBodyValue")]
        public static IEnumerable<CodeInstruction> ChaControl_SetShapeBodyValue_RemoveHeightLock(IEnumerable<CodeInstruction> instructions) {

            var il = instructions.ToList();

            var index = il.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 0.6f);
            if (index <= 0) return il;

            if (isDark) {
                il[index - 7].opcode = OpCodes.Nop;
                il[index - 6].opcode = OpCodes.Nop;
                il[index - 5].opcode = OpCodes.Nop;
            }
            il[index - 4].opcode = OpCodes.Nop;
            il[index - 3].opcode = OpCodes.Nop;
            il[index - 2].opcode = OpCodes.Nop;
            il[index - 1].opcode = OpCodes.Nop;
            il[index].opcode = OpCodes.Nop;
            il[index + 1].opcode = OpCodes.Nop;
            il[index + 2].opcode = OpCodes.Nop;
            if (isDark) {
                il[index + 3].opcode = OpCodes.Nop;
                il[index + 4].opcode = OpCodes.Nop;
            }

            return il;
        }

        [HarmonyTranspiler, HarmonyPatch(typeof(ChaControl), "UpdateShapeBodyValueFromCustomInfo")]
        public static IEnumerable<CodeInstruction> ChaControl_UpdateShapeBodyValueFromCustomInfo_RemoveHeightLock(IEnumerable<CodeInstruction> instructions) {

            var il = instructions.ToList();

            var index = il.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == 0.6f);
            if (index <= 0) return il;

            if (isDark) {
                il[index - 8].opcode = OpCodes.Nop;
                il[index - 7].opcode = OpCodes.Nop;
                il[index - 6].opcode = OpCodes.Nop;
            }
            il[index - 5].opcode = OpCodes.Nop;
            il[index - 4].opcode = OpCodes.Nop;
            il[index - 3].opcode = OpCodes.Nop;
            il[index - 2].opcode = OpCodes.Nop;
            il[index - 1].opcode = OpCodes.Nop;
            il[index].opcode = OpCodes.Nop;
            il[index + 1].opcode = OpCodes.Nop;
            il[index + 2].opcode = OpCodes.Nop;
            il[index + 3].opcode = OpCodes.Nop;
            if (isDark) {
                il[index + 4].opcode = OpCodes.Nop;
                il[index + 5].opcode = OpCodes.Nop;
            }

            return il;
        }

    }
}


