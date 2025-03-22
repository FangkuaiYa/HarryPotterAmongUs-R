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

namespace HarryPotter
{
    [BepInPlugin(Id, "Harry Potter", VersionString)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]

    public class Plugin : BasePlugin
    {
        public const string Id = "harry.potter.mod";
        public Harmony Harmony { get; } = new Harmony(Id);
        public const string VersionString = "2.1.0";
        public static Version Version = Version.Parse(VersionString);
        public static Plugin Instance;

        public override void Load()
        {
            Main.Instance = new Main();
            Instance = this;
            ModTranslation.Load();
            TaskInfoHandler.Instance = new TaskInfoHandler { AllInfo = new List<ImportantTextTask>() };
            PopupTMPHandler.Instance = new PopupTMPHandler { AllPopups = new List<TextMeshPro>() };
            Classes.Config.LoadOption();
            Harmony.PatchAll();
            CustomOption.Patches.ImportSlot();
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

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        private static void Postfix(PingTracker __instance)
        {
            AspectPosition position = __instance.GetComponent<AspectPosition>();
            var text2 = AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started ?
ModTranslation.getString("PingTracker_Update")
+ $"\n{__instance.text.text}" : "";
            __instance.text.text = $"<size=130%><color=#FFF319>Harry Potter</color> v{Plugin.Version.ToString()}</size>\n{text2}";
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