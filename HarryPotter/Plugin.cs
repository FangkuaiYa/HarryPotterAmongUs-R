using BepInEx;
using HarmonyLib;
using HarryPotter.Classes;
using System.Collections.Generic;
using HarryPotter.Classes.UI;
using TMPro;
using UnityEngine;
using System;
using Reactor;
using BepInEx.Unity.IL2CPP;
using InnerNet;
using HarryPotter.Classes.CustomHats;

namespace HarryPotter
{
    [BepInPlugin(Id, "Harry Potter", VersionString)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]

    public class Plugin : BasePlugin
    {
        public const string Id = "harry.potter.mod";
        public Harmony Harmony { get; } = new Harmony(Id);
        public const string VersionString = "1.2.0";
        public static Version Version = Version.Parse(VersionString);
        public static Plugin Instance;

        public override void Load()
        {
            Main.Instance = new Main();
            Instance = this;
            TaskInfoHandler.Instance = new TaskInfoHandler { AllInfo = new List<ImportantTextTask>() };
            PopupTMPHandler.Instance = new PopupTMPHandler { AllPopups = new List<TextMeshPro>() };
            Classes.Config.LoadOption();
            CustomHatManager.LoadHats();
            Harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.AmBanned), MethodType.Getter)]
    public static class StatsManager_AmBanned
    { 
        static void Postfix(out bool __result)
        {
            __result = false;
        }
    }

    /*[HarmonyPatch(typeof(GameOptionsData), nameof(GameOptionsData.ToHudString))]
    public class GameOptionsDataPatch
    {
        public static void Postfix(GameOptionsData __instance, ref string __result)
        {
            List<string> resultLines = __result.Split("\n").ToList();
            resultLines.RemoveAt(0);
            resultLines.Insert(0, "Game Settings:");
            
            __result = string.Join("\n", resultLines);
            __result += "\n<#EEFFB3FF>Mod settings:";
            if (Main.Instance.Config.ShowPopups) __result += "\n(Hover over a setting for more info)";
        }
    }*/

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        private static void Postfix(PingTracker __instance)
        {
            AspectPosition position = __instance.GetComponent<AspectPosition>();
            __instance.text.text = $"<size=130%><color=#FFF319>Harry Potter</color> v{Plugin.Version.ToString()}</size>"
    + "\nCreated by: <color=#00ffff>FangkuaiYa</color>"
    + "\n<#FFFFFFFF>Original Design by: <#7289DAFF>Hunter101#1337"
    + "\n<#FFFFFFFF>Art by:<color=#5A5AAD>賣蟑螂</color><color=#D3A4FF>NotKomi</color> & <#E67E22FF>PhasmoFireGod</color>"
    + $"\n{__instance.text.text}";
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
            {
                __instance.text.alignment = TextAlignmentOptions.Top;
                position.Alignment = AspectPosition.EdgeAlignments.Top;
                position.DistanceFromEdge = new Vector3(1.5f, 0.11f, 0);
            }
            else
            {
                position.Alignment = AspectPosition.EdgeAlignments.LeftTop;
                __instance.text.alignment = TextAlignmentOptions.TopLeft;
                position.DistanceFromEdge = new Vector3(0.5f, 0.11f);
            }
        }
    }
}