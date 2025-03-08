using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.Roles;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    class KillButton_DoClick
    {
        static bool Prefix(KillButton __instance)
        {
            if (__instance == HudManager.Instance.KillButton && Main.Instance.GetLocalModdedPlayer()?.Role?.RoleName == "Bellatrix" && ((Bellatrix)Main.Instance.GetLocalModdedPlayer().Role).MindControlledPlayer != null)
            {
                PlayerControl killer = ((Bellatrix)Main.Instance.GetLocalModdedPlayer().Role).MindControlledPlayer._Object;
                if (HudManager.Instance.KillButton.currentTarget != null && !Main.Instance.ControlKillUsed)
                {
                    Main.Instance.ControlKillUsed = true;
                    Main.Instance.RpcKillPlayer(killer, HudManager.Instance.KillButton.currentTarget, true);
                }
                return false;
            }

            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (Main.Instance.GetLocalModdedPlayer()?.Role != null)
                return Main.Instance.GetLocalModdedPlayer().Role.DoClick(__instance);
            return true;
        }
    }
}