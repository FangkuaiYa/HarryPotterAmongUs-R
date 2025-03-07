﻿using System;
using System.Collections.Generic;
using HarmonyLib;
using HarryPotter.Classes;
using HarryPotter.Classes.Roles;
using Hazel;
using UnityEngine;

namespace HarryPotter.Patches
{
    [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
    class PlayerPhysics_FixedUpdate
    {
        static bool Prefix(PlayerPhysics __instance)
        {
            ModdedPlayerClass moddedController = Main.Instance.ModdedPlayerById(__instance.myPlayer.PlayerId);
            if (__instance.myPlayer != PlayerControl.LocalPlayer || moddedController?.Role?.RoleName != "Bellatrix" || ((Bellatrix)moddedController.Role).MindControlledPlayer == null) return true;
            PlayerPhysics controlledPlayer = ((Bellatrix)moddedController.Role).MindControlledPlayer._Object.MyPhysics;
            Vector2 vel = HudManager.Instance.joystick.Delta * __instance.TrueSpeed;
            controlledPlayer.body.velocity = vel;
            MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)Packets.MoveControlledPlayer, SendOption.Reliable);
            writer.Write(controlledPlayer.myPlayer.PlayerId);
            writer.Write(vel.x);
            writer.Write(vel.y);
            writer.Write(controlledPlayer.body.position.x);
            writer.Write(controlledPlayer.body.position.y);
            writer.EndMessage();
            return false;
        }

        static void Postfix(PlayerPhysics __instance)
        {
            if (__instance.AmOwner && Main.Instance.GetLocalModdedPlayer() != null)
            {
                __instance.body.velocity *= Main.Instance.GetLocalModdedPlayer().ReverseDirectionalControls ? -1f : 1f;
                __instance.body.velocity *= Main.Instance.GetLocalModdedPlayer().SpeedMultiplier;
            }
        }
    }
}