using System;
using System.Linq;
using HarryPotter.Classes.Roles;
using HarryPotter.Classes.UI;
using Reactor.Utilities.Attributes;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace HarryPotter.Classes.Helpers.UI
{
    [RegisterInIl2Cpp]
    class PlayerSlot : MonoBehaviour
    {
        public PlayerSlot(IntPtr ptr) : base(ptr)
        {
        }

        private void Awake()
        {
            GameObject itemButtonObj = gameObject.transform.GetChild(0).gameObject;
            PlayerButton = itemButtonObj.gameObject.AddComponent<CustomButton>();
            PlayerButton.OnClick += TryControlTargetedPlayer;
            PlayerTooltip = itemButtonObj.gameObject.AddComponent<Tooltip>();
        }

        public void TryControlTargetedPlayer()
        {
            ModdedPlayerClass localModdedPlayer = Main.Instance.GetLocalModdedPlayer();
            
            if (localModdedPlayer == null) return;
            if (((Bellatrix) localModdedPlayer.Role).MindControlledPlayer != null) return;
            if (((Bellatrix) localModdedPlayer.Role).MindControlButton.isCoolingDown) return;
            if (Main.Instance.ModdedPlayerById(TargetedPlayer.PlayerId).Immortal) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (TargetedPlayer.Data.IsDead) return;
            if (TargetedPlayer.Data.Disconnected) return;
            if (Main.Instance.GetPlayerRoleName(localModdedPlayer) == "Bellatrix")
                if (((Bellatrix) localModdedPlayer.Role).MarkedPlayers.All(x => x.PlayerId != TargetedPlayer.PlayerId)) return;
            if (PlayerControl.LocalPlayer.inVent) return;
            if (MeetingHud.Instance) return;
            if (ExileController.Instance) return;
            if (!PlayerButton.Enabled) return;
            if (PlayerButton.HoverColor != Color.yellow) return;

            MindControlMenu.Instance.CloseMenu();
            Main.Instance.RpcControlPlayer(PlayerControl.LocalPlayer, TargetedPlayer);
        }

        public void ResetIcon()
        {
            PlayerButton.Enabled = false;
            PlayerTooltip.Enabled = false;
            
            ModdedPlayerClass localModdedPlayer = Main.Instance.GetLocalModdedPlayer();
            if (Main.Instance.GetPlayerRoleName(localModdedPlayer) != "Bellatrix")
            {
                if (Icon != null)
                {
                    Icon.cosmetics.hat.Destroy();
                    Icon.cosmetics.skin.layer.Destroy();
                    Icon.cosmetics.currentBodySprite.BodySprite.Destroy();
                    Icon.gameObject.Destroy();
                    Icon.Destroy();
                }
                Icon = null;
                return;
            }
            
            if (((Bellatrix) localModdedPlayer.Role).MarkedPlayers.Count < PlayerIndex + 1)
            {
                if (Icon != null)
                {
                    Icon.cosmetics.hat.Destroy();
                    Icon.cosmetics.skin.layer.Destroy();
                    Icon.cosmetics.currentBodySprite.BodySprite.Destroy();
                    Icon.gameObject.Destroy();
                    Icon.Destroy();
                }
                Icon = null;
                return;
            }

            TargetedPlayer = ((Bellatrix) localModdedPlayer.Role).MarkedPlayers.ToArray()[PlayerIndex];
            NetworkedPlayerInfo data = TargetedPlayer.Data;

            PlayerButton.Enabled = true;
            PlayerButton.SetColor(Color.yellow);

            PlayerTooltip.Enabled = true;
            PlayerTooltip.TooltipText = data.PlayerName;

            if (Icon == null)
            {
                Icon = Instantiate(HudManager.Instance.IntroPrefab.PlayerPrefab, gameObject.transform).DontDestroy();
                Icon.gameObject.layer = 5;
                Icon.cosmetics.currentBodySprite.BodySprite.sortingOrder = 5;
                Icon.cosmetics.skin.layer.sortingOrder = 6;
                Icon.cosmetics.hat.BackLayer.sortingOrder = 4;
                Icon.cosmetics.hat.FrontLayer.sortingOrder = 6;
                Icon.name = data.PlayerName;
                Icon.SetFlipX(true);
                Icon.transform.localScale = Vector3.one * 2f;
            }

            TargetedPlayer.SetPlayerMaterialColors(Icon.cosmetics.currentBodySprite.BodySprite);
            Icon.SetSkin(data.DefaultOutfit.SkinId, data.DefaultOutfit.ColorId);
            Icon.cosmetics.hat.SetHat(data.DefaultOutfit.HatId, data.DefaultOutfit.ColorId);
            Icon.cosmetics.nameText.gameObject.SetActive(false);
        }

        public void LateUpdate()
        {
            if (MindControlMenu.Instance.IsOpen) ResetIcon();
        }
        
        public Tooltip PlayerTooltip { get; set; }
        public CustomButton PlayerButton { get; set; }
        public int PlayerIndex { get; set; }
        public PlayerControl TargetedPlayer { get; set; }
        public PoolablePlayer Icon { get; set; }
    }
}