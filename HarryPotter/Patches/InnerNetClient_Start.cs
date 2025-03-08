using HarmonyLib;
using HarryPotter.Classes;
using InnerNet;
using HarryPotter.Classes.Helpers.UI;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(InnerNetClient), nameof(InnerNetClient.Start))]
    public static class InnerNetClient_Start
    { 
        static void Postfix()
        {
            SoundManager.Instance.PlaySound(Main.Instance.Assets.HPTheme, false, 1f);

            new GameObject().AddComponent<InventoryUI>();
            new GameObject().AddComponent<MindControlMenu>();
            new GameObject().AddComponent<HotbarUI>();

            //Main.Instance.ResetCustomOptions();
        }
    }
}