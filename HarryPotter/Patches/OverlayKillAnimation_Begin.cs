using HarmonyLib;
using HarryPotter.Classes;
using Il2CppSystem;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(OverlayKillAnimation), nameof(OverlayKillAnimation.Initialize))]
    public class OverlayKillAnimation_Begin
    {
        static void Prefix(OverlayKillAnimation __instance, KillOverlayInitData initData)
        {
            if (initData.killerOutfit == null || initData.victimOutfit == null || __instance == null) return;
            if (initData.killerOutfit != initData.victimOutfit) return;
            ModdedPlayerClass harry = Main.Instance.FindPlayerOfRole("Harry");
            if (harry == null) return;

            initData.killerOutfit = harry._Object.Data.DefaultOutfit;
            __instance.killerParts.cosmetics.currentBodySprite.BodySprite.transform.localScale = new Vector3(0.4f, 0.4f);
            __instance.killerParts.cosmetics.currentBodySprite.BodySprite.transform.position -= new Vector3(0.3f, 0f, 0f);
        }
    }
}