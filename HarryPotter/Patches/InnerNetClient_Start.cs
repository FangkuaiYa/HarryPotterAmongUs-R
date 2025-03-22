using HarmonyLib;
using HarryPotter.Classes;
using InnerNet;

namespace HarryPotter.Patches;

[HarmonyPatch(typeof(InnerNetClient), nameof(InnerNetClient.Start))]
public static class InnerNetClient_Start
{
    private static void Postfix()
    {
        SoundManager.Instance.PlaySound(Main.Instance.Assets.HPTheme, false);
    }
}