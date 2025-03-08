using HarmonyLib;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public class ExileController_WrapUp
    {
        static bool Prefix(ExileController __instance)
        {
            if (__instance.initData.networkedPlayer != null)
            {
                PlayerControl @object = __instance.initData.networkedPlayer.Object;
                if (@object) @object.Exiled();
            }
            if (DestroyableSingleton<TutorialManager>.InstanceExists || !GameManager.Instance.LogicFlow.IsGameOverDueToDeath())
            {
                DestroyableSingleton<HudManager>.Instance.StartCoroutine(DestroyableSingleton<HudManager>.Instance.CoFadeFullScreen(Color.black, Color.clear, 0.2f));
                PlayerControl.LocalPlayer.SetKillTimer(GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
                ShipStatus.Instance.EmergencyCooldown = (float)GameOptionsManager.Instance.currentNormalGameOptions.EmergencyCooldown;
                Camera.main.GetComponent<FollowerCamera>().Locked = false;
                DestroyableSingleton<HudManager>.Instance.SetHudActive(true);
                ControllerManager.Instance.ResetAll();
            }
            Object.Destroy(__instance.gameObject);

            return false;
        }
    }
}