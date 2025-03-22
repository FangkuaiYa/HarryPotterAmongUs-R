﻿using HarmonyLib;
using HarryPotter.Classes;

namespace HarryPotter.Patches;

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.StartMeeting))]
public class PlayerControl_RpcStartMeeting
{
    private static void Postfix()
    {
        Main.Instance.CurrentStage++;
    }
}