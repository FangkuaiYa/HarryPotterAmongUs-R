using System;
using System.Collections.Generic;
using System.Linq;
using AmongUs.Data;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HarryPotter.Classes.Hats;

[HarmonyPatch(typeof(HatsTab), nameof(HatsTab.OnEnable))]
public static class HatsTab_OnEnable
{
    public static bool Prefix(HatsTab __instance)
    {
        __instance.currentHat =
            DestroyableSingleton<HatManager>.Instance.GetHatById(DataManager.Player.Customization.Hat);
        var allHats = DestroyableSingleton<HatManager>.Instance.GetUnlockedHats();
        var hatGroups = new SortedList<string, List<HatData>>(
            new PaddedComparer<string>("Vanilla", "")
        );
        foreach (var hat in allHats)
        {
            if (!hatGroups.ContainsKey(hat.StoreName))
                hatGroups[hat.StoreName] = new List<HatData>();
            hatGroups[hat.StoreName].Add(hat);
        }

        foreach (var instanceColorChip in __instance.ColorChips)
            instanceColorChip.gameObject.Destroy();
        __instance.ColorChips.Clear();
        var groupNameText = __instance.GetComponentInChildren<TextMeshPro>(false);
        var hatIdx = 0;
        foreach (var (groupName, hats) in hatGroups)
        {
            var text = Object.Instantiate(groupNameText, __instance.scroller.Inner);
            text.gameObject.transform.localScale = Vector3.one;
            text.GetComponent<TextTranslatorTMP>().Destroy();
            text.text = groupName;
            text.alignment = TextAlignmentOptions.Center;
            text.fontSize = 3f;
            text.fontSizeMax = 3f;
            text.fontSizeMin = 0f;

            hatIdx = (hatIdx + 4) / 5 * 5;

            var xLerp = __instance.XRange.Lerp(0.5f);
            var yLerp = __instance.YStart - hatIdx / __instance.NumPerRow * __instance.YOffset;
            text.transform.localPosition = new Vector3(xLerp, yLerp, -1f);
            hatIdx += 5;
            foreach (var hat in hats.OrderBy(HatManager.Instance.allHats.IndexOf))
            {
                var num = __instance.XRange.Lerp(hatIdx % __instance.NumPerRow / (__instance.NumPerRow - 1f));
                var num2 = __instance.YStart - hatIdx / __instance.NumPerRow * __instance.YOffset;
                var colorChip = Object.Instantiate(__instance.ColorTabPrefab, __instance.scroller.Inner);
                colorChip.transform.localPosition = new Vector3(num, num2, -1f);
                colorChip.Button.OnClick.AddListener((Action)(() => __instance.SelectHat(hat)));
                colorChip.Inner.SetHat(hat,
                    __instance.HasLocalPlayer()
                        ? PlayerControl.LocalPlayer.Data.DefaultOutfit.ColorId
                        : DataManager.Player.Customization.Color);
                colorChip.Inner.transform.localPosition = hat.ChipOffset + new Vector2(0f, -0.3f);
                colorChip.Tag = hat;
                __instance.ColorChips.Add(colorChip);
                hatIdx += 1;
            }
        }

        __instance.scroller.ContentYBounds.max =
            -(__instance.YStart - (hatIdx + 1) / __instance.NumPerRow * __instance.YOffset) - 3f;
        __instance.currentHatIsEquipped = true;

        return false;
    }
}