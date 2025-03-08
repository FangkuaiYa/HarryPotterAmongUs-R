using System;
using HarryPotter.Classes.UI;
using UnityEngine;

namespace HarryPotter.Classes.Roles
{
    public class Harry : Role
    {
        public KillButton InvisCloakButton { get; set; }
        public DateTime LastCloak { get; set; }
        
        public Harry(ModdedPlayerClass owner)
        {
            RoleName = "Harry";
            RoleColor = Palette.Orange;
            RoleColor2 = Palette.Orange;
            IntroString = "I solemnly swear I am up to no good.";
            Owner = owner;
            
            if (!Owner._Object.AmOwner)
                return;
            
            InvisCloakButton = KillButton.Instantiate(HudManager.Instance.KillButton);
            InvisCloakButton.graphic.enabled = true;
            Tooltip tt = InvisCloakButton.gameObject.AddComponent<Tooltip>();
            tt.TooltipText = $"Cloak:\nWill make you invisible for {Main.Instance.Config.InvisCloakDuration}s";
        }

        public override void ResetCooldowns()
        {
            LastCloak = DateTime.UtcNow;
        }

        public override void Update()
        {
            if (!Owner._Object.AmOwner)
                return;
            
            if (!HudManager.Instance)
                return;
            
            DrawButtons();
        }
        
        public override bool DoClick(KillButton __instance)
        {
            if (__instance == InvisCloakButton)
                TryBecomeInvisible();
            else
                return true;
            
            return false;
        }

        public void TryBecomeInvisible()
        {
            if (InvisCloakButton.isCoolingDown)
                return;

            if (!InvisCloakButton.isActiveAndEnabled)
                return;

            if (Owner._Object.Data.IsDead)
                return;
            
            if (Owner.ControllerOverride != null)
                return;

            Main.Instance.RpcInvisPlayer(Owner._Object);
        }
        
        public void DrawButtons()
        {
            Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            
            InvisCloakButton.gameObject.SetActive(HudManager.Instance.ReportButton.isActiveAndEnabled);
            InvisCloakButton.graphic.sprite = Main.Instance.Assets.AbilityIcons[4];
            InvisCloakButton.buttonLabelText.text = "Cloak";
            InvisCloakButton.transform.position = new Vector2(bottomLeft.x + 0.75f, bottomLeft.y + 0.75f);
            InvisCloakButton.SetTarget(null);
            InvisCloakButton.SetCoolDown(Main.Instance.Config.InvisCloakCooldown - (float)(DateTime.UtcNow - LastCloak).TotalSeconds, Main.Instance.Config.InvisCloakCooldown);
            
            bool isDead = Owner._Object.Data.IsDead;
            if (isDead) InvisCloakButton.SetCoolDown(0, 1);
            if (!InvisCloakButton.isCoolingDown && !isDead)
            {
                InvisCloakButton.graphic.material.SetFloat("_Desat", 0f);
                InvisCloakButton.graphic.color = Palette.EnabledColor;
            }
        }
    }
}