using KKAPI;
using HarmonyLib;
using KKAPI.Chara;
using System.Collections;
using ExtensibleSaveFormat;

namespace KK_ExpandMaleMaker {
    public class MaleHeightCompatabilityController : CharaCustomFunctionController {

        protected override void OnCardBeingSaved(GameMode currentGameMode) {
            if (ChaControl.sex != 0) return;
            bool heightEnabled = KK_ExpandMaleMaker.heightEnabled.Value;
            var data = new PluginData();
            data.data.Add("MaleHeightEnabled", heightEnabled);
            data.version = 1;
            SetExtendedData(data);
        }

        protected override void OnReload(GameMode currentGameMode) {
            base.OnReload(currentGameMode);
            if (KK_ExpandMaleMaker.compatabilityMode.Value || !KK_ExpandMaleMaker.heightEnabled.Value)
                KK_ExpandMaleMaker.instance.StartCoroutine(DelayCheck());

            //Attempting to apply the check on the same frame doesn't seem to work for cards loaded with DragAndDrop? So, here we are
            IEnumerator DelayCheck() {
                yield return null;

                if (ChaControl.fileParam.sex != 0) yield break;

                bool heightEnabled = false;

                bool isJanitor = false;
                if (Hooks.isDark) {
                    isJanitor = GetExType() != 0;
                }

                var data = GetExtendedData();
                if (data != null && data.data.TryGetValue("MaleHeightEnabled", out var val) && val is bool bVal) {
                    heightEnabled = bVal;
                }

                if ((!heightEnabled) || (!KK_ExpandMaleMaker.heightEnabled.Value)) {
                    var sib = Traverse.Create(ChaControl).Property("sibBody").GetValue<ShapeInfoBase>(); //Public in JP but private in Party, thanks Illusion!
                    if (isJanitor) {
                        ChaControl.chaFile.custom.body.shapeValueBody[0] = 1;
                        sib.ChangeValue(0, 1);
                    }
                    else {
                        ChaControl.chaFile.custom.body.shapeValueBody[0] = 0.6f;
                        sib.ChangeValue(0, 0.6f);
                    }
                }
            }
        }

        int GetExType() => ChaControl.exType;

    }
}

