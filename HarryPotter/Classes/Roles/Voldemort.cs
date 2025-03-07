﻿using System;
using HarryPotter.Classes.Helpers.UI;
using HarryPotter.Classes.UI;
using UnityEngine;

namespace HarryPotter.Classes.Roles
{
    public class Voldemort : Role
    {
        public KillButtonManager CurseButton { get; set; }
        public DateTime LastCurse { get; set; }

        public Voldemort(ModdedPlayerClass owner)
        {
            RoleName = "Voldemort";
            RoleColor = Palette.ImpostorRed;
            RoleColor2 = Palette.ImpostorRed;
            IntroString = "There is no good and evil. There is only\npower, and those too weak to seek it.";
            Owner = owner;

            if (!Owner._Object.AmOwner)
                return;
            
            CurseButton = UnityEngine.Object.Instantiate(HudManager.Instance.KillButton);
            CurseButton.renderer.enabled = true;
            
            Tooltip tt = CurseButton.gameObject.AddComponent<Tooltip>();
            tt.TooltipText = "The Killing Curse:\nA spell which will kill any target it hits, except Harry\nIf the spell hits Harry, you will die instead\n<#FF0000FF>Right click to shoot this spell in the direction of your cursor";
        }

        public override void Update()
        {
            if (!Owner._Object.AmOwner)
                return;
            
            if (!HudManager.Instance)
                return;
            
            Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));

            CurseButton.gameObject.SetActive(HudManager.Instance.KillButton.isActiveAndEnabled);
            CurseButton.renderer.sprite = Main.Instance.Assets.AbilityIcons[0];
            CurseButton.transform.position = new Vector2(bottomLeft.x + 0.75f, bottomLeft.y + 0.75f);
            CurseButton.SetTarget(null);
            if (Main.Instance.Config.SeparateCooldowns)
                CurseButton.SetCoolDown(PlayerControl.GameOptions.KillCooldown - (float)(DateTime.UtcNow - LastCurse).TotalSeconds, PlayerControl.GameOptions.KillCooldown);
            else
                CurseButton.SetCoolDown(Owner._Object.killTimer, PlayerControl.GameOptions.KillCooldown);
            
            bool isDead = Owner._Object.Data.IsDead;
            if (isDead)
                CurseButton.SetCoolDown(0, 1);
            
            if (!CurseButton.isCoolingDown && !isDead)
            {
                CurseButton.renderer.material.SetFloat("_Desat", 0f);
                CurseButton.renderer.color = Palette.EnabledColor;
            }
            
            if (Input.GetMouseButtonDown(1))
                ShootCurse();
        }
        
        public override void RemoveCooldowns()
        {
            //LastCurse = DateTime.UtcNow.AddSeconds(PlayerControl.GameOptions.KillCooldown * -1);
            //Owner._Object.SetKillTimer(0);
            
            if (Main.Instance.Config.SeparateCooldowns)
                LastCurse = DateTime.UtcNow.AddSeconds(PlayerControl.GameOptions.KillCooldown * -1);
            else
                Owner._Object.SetKillTimer(0);
        }

        public override void ResetCooldowns()
        {
            if (Main.Instance.Config.SeparateCooldowns)
                LastCurse = DateTime.UtcNow;
            else
                Owner._Object.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
        }

        public void ShootCurse()
        {
            if (CurseButton.isCoolingDown)
                return;

            if (!CurseButton.isActiveAndEnabled)
                return;
            
            if (Owner._Object.Data.IsDead)
                return;
            
            if (Owner._Object.inVent && !Main.Instance.Config.SpellsInVents)
                return;
            
            if (InventoryUI.Instance.IsOpen || InventoryUI.Instance.IsOpeningOrClosing)
                return;

            ResetCooldowns();
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Main.Instance.RpcCreateCurse(mouseWorld, Owner);
        }

        public override bool PerformKill(KillButtonManager __instance)
        {
            return __instance != CurseButton;
        }
    }
}
