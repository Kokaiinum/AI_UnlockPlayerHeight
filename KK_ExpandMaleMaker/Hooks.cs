using UnityEngine;

namespace ExpandMaleMaker {
    internal static partial class Hooks {

        internal static void EnableUnderhair() {
            GameObject element = GameObject.Find("CustomScene/CustomRoot/FrontUIGroup/CustomUIGroup/CvsMenuTree/01_BodyTop/tglUnderhair");
            SetUnderhairDisplay(element);
            SetUnderhairOffset(element);
        }

        static void SetUnderhairOffset(GameObject element) {
            element.transform.GetChild(1).position = new Vector3(60.78f, 432.85f, 0);
            element.transform.GetChild(1).localPosition = new Vector3(132.65f, 630.0f, 0);
        }
    }
}
