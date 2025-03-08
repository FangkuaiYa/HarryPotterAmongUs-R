using HarmonyLib;
using HarryPotter.Classes;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Linq;

namespace HarryPotter.Patches;

[HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString), typeof(StringNames),
    typeof(Il2CppReferenceArray<Il2CppSystem.Object>))]
public static class TranslationController_GetString
{
    public static void Postfix(ref string __result, [HarmonyArgument(0)] StringNames name)
    {
        if (ExileController.Instance == null || ExileController.Instance.initData.networkedPlayer == null) return;

        switch (name)
        {
            case StringNames.ExileTextPN:
            case StringNames.ExileTextSN:
            case StringNames.ExileTextPP:
            case StringNames.ExileTextSP:
                {
                    var info = ExileController.Instance.initData.networkedPlayer;
                    var roleName = Main.Instance.AllPlayers.FindAll(player => player._Object == ExileController.Instance.initData.networkedPlayer).FirstOrDefault().Role.RoleName;
                    __result = $"{info.PlayerName} was {roleName}.";
                    return;
                }
        }
    }
}
