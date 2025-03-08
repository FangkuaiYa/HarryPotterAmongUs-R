using System;
using System.Collections.Generic;
using AmongUs.Data;
using Assets.InnerNet;
using HarmonyLib;
using HarryPotter.Classes;
using Reactor.Patches;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace HarryPotter.Patches
{

    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public class MainMenuPatch
    {
        private static AnnouncementPopUp popUp;
        private static GameObject CreditsButton;
        private static GameObject DiscordButton;
        public static MainMenuManager Instance { get; private set; }

        [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start)), HarmonyPostfix]
        public static void Start_Postfix(MainMenuManager __instance)
        {
            Instance = __instance;

            SimpleButton.SetBase(__instance.quitButton);

            int row = 1; int col = 0;
            GameObject CreatButton(string text, Action action)
            {
                col++; if (col > 2) { col = 1; row++; }
                var template = col == 1 ? __instance.creditsButton.gameObject : __instance.quitButton.gameObject;
                var button = Object.Instantiate(template, template.transform.parent);
                button.transform.transform.FindChild("FontPlacer").GetChild(0).gameObject.DestroyTranslator();
                var buttonText = button.transform.FindChild("FontPlacer").GetChild(0).GetComponent<TextMeshPro>();
                buttonText.text = text;
                PassiveButton passiveButton = button.GetComponent<PassiveButton>();
                passiveButton.OnClick = new();
                passiveButton.OnClick.AddListener(action);
                AspectPosition aspectPosition = button.GetComponent<AspectPosition>();
                aspectPosition.anchorPoint = new Vector2(col == 1 ? 0.415f : 0.583f, 0.5f - 0.08f * row);
                return button;
            }
            if (CreditsButton == null) CreditsButton = CreatButton("Credits", null);
            CreditsButton.gameObject.SetActive(true);
            CreditsButton.name = "Credits Button";
            PassiveButton passiveButtoncredits = CreditsButton.GetComponent<PassiveButton>();
            SpriteRenderer buttonSpritecredits = CreditsButton.transform.FindChild("Inactive").GetComponent<SpriteRenderer>();

            passiveButtoncredits.OnClick = new Button.ButtonClickedEvent();
            Color creditsColor = new Color(0, 1, 1, 0.8f);
            buttonSpritecredits.color = creditsColor;
            passiveButtoncredits.OnMouseOut.AddListener((System.Action)delegate
            {
                buttonSpritecredits.color = creditsColor;
            });
            //if (CreditsButton == null) CreditsButton = CreatButton("Credits", delegate
            passiveButtoncredits.OnClick.AddListener((System.Action)delegate
            {
                // do stuff
                if (popUp != null) Object.Destroy(popUp);
                var popUpTemplate = Object.FindObjectOfType<AnnouncementPopUp>(true);
                popUp = Object.Instantiate(popUpTemplate);

                popUp.gameObject.SetActive(true);
                string creditsString = @$"<align=""center"">Contributors:
Modded by FangkuaiYa
Original Design by Hunter101#1337
Art by 賣蟑螂NotKomi & PhasmoFireGod

";
                creditsString += $@"<size=70%> Other Credits & Resources:
Reactor - The framework used for all versions
BepInEx - Used to hook game functions
TownOfUs-R - Custom game options by eDonnes124
TheOtherRoles - Harry Potter Hats is based on the code from TheOtherRoles
</size>";
                creditsString += "</align>";
                Assets.InnerNet.Announcement creditsAnnouncement = new()
                {
                    Id = "hpCredits",
                    Language = 0,
                    Number = 500,
                    Title = "Credits and Contributors",
                    ShortTitle = "Harry Potter Credits",
                    SubTitle = "",
                    PinState = false,
                    Date = "03.08.2025",
                    Text = creditsString,
                };
                __instance.StartCoroutine(Effects.Lerp(0.1f, new Action<float>((p) =>
                {
                    if (p == 1)
                    {
                        var backup = DataManager.Player.Announcements.allAnnouncements;
                        DataManager.Player.Announcements.allAnnouncements = new();
                        popUp.Init(false);
                        DataManager.Player.Announcements.SetAnnouncements(new Announcement[] { creditsAnnouncement });
                        popUp.CreateAnnouncementList();
                        popUp.UpdateAnnouncementText(creditsAnnouncement.Number);
                        popUp.visibleAnnouncements._items[0].PassiveButton.OnClick.RemoveAllListeners();
                        DataManager.Player.Announcements.allAnnouncements = backup;
                    }
                })));
            });


            if (DiscordButton == null) DiscordButton = CreatButton("GitHub", () => Application.OpenURL("https://github.com/FangkuaiYa/HarryPotterAmongUs-R/"));
            DiscordButton.gameObject.SetActive(true);
            DiscordButton.name = "GitHub";
            PassiveButton passiveDiscordButton = DiscordButton.GetComponent<PassiveButton>();
            SpriteRenderer SpriteDiscordButton = DiscordButton.transform.FindChild("Inactive").GetComponent<SpriteRenderer>();
            Color DiscordColor = new Color(0.317f, 0, 1, 0.8f);
            SpriteDiscordButton.color = DiscordColor;
            passiveDiscordButton.OnMouseOut.AddListener((System.Action)delegate
            {
                SpriteDiscordButton.color = DiscordColor;
            });
        }
    }
    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    internal class VersionShowerStartPatch
    {
        private static GameObject VersionShower1;
        public static GameObject OVersionShower;
        private static TextMeshPro VisitText;
        private static void Postfix(VersionShower __instance)
        {
            string credentialsText = "<color=#00ffff>FANGKUAI</color> © 2025";
            credentialsText += "\t\t\t";
            string versionText = $"<color=#FFF319>Harry Potter</color> - {Plugin.Version.ToString()}";
            credentialsText += versionText;
            var friendCode = GameObject.Find("FriendCode");
            if (friendCode != null && VersionShower1 == null)
            {
                VersionShower1 = Object.Instantiate(friendCode, friendCode.transform.parent);
                VersionShower1.name = "HP Version Shower";
                VersionShower1.transform.localPosition = friendCode.transform.localPosition + new Vector3(3.2f, 0f, 0f);
                VersionShower1.transform.localScale *= 1.7f;
                var TMP = VersionShower1.GetComponent<TextMeshPro>();
                TMP.alignment = TextAlignmentOptions.Right;
                TMP.fontSize = 30f;
                TMP.SetText(credentialsText);
            }
            if ((OVersionShower = GameObject.Find("VersionShower")) != null && VisitText == null)
            {
                VisitText = Object.Instantiate(__instance.text);
                VisitText.name = "HP User Counter";
                VisitText.alignment = TextAlignmentOptions.Left;
                VisitText.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                VisitText.transform.localPosition = new Vector3(-3.92f, -2.9f, 0f);
                VisitText.enabled = GameObject.Find("TOR Background") != null;

                __instance.text.text = $"<color=#FFF319>Harry Potter</color> v{Plugin.Version.ToString()}";
                __instance.text.alignment = TextAlignmentOptions.Left;
                OVersionShower.transform.localPosition = new Vector3(-4.92f, -3.3f, 0f);

                var ap1 = OVersionShower.GetComponent<AspectPosition>();
                if (ap1 != null) Object.Destroy(ap1);
                var ap2 = VisitText.GetComponent<AspectPosition>();
                if (ap2 != null) Object.Destroy(ap2);
            };
        }
    }

    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start)), HarmonyPriority(Priority.First)]
    internal class TitleLogoPatch
    {
        public static GameObject TOR_Background;
        public static GameObject Ambience;
        public static GameObject Starfield;
        public static GameObject LeftPanel;
        public static GameObject RightPanel;
        public static GameObject CloseRightButton;
        public static GameObject Tint;
        public static GameObject Sizer;
        public static GameObject AULogo;
        public static GameObject BottomButtonBounds;

        public static Vector3 RightPanelOp;

        private static void Postfix(MainMenuManager __instance)
        {
            ReactorVersionShower.Text.Destroy();

            GameObject.Find("BackgroundTexture")?.SetActive(false);

            TOR_Background = new GameObject("TOR Background");
            TOR_Background.transform.position = new Vector3(0, 0, 520f);
            var bgRenderer = TOR_Background.AddComponent<SpriteRenderer>();
            bgRenderer.sprite = ModHelpers.loadSpriteFromResources("HP_BG.png", 179f);

            if (!(Ambience = GameObject.Find("Ambience"))) return;
            if (!(Starfield = Ambience.transform.FindChild("starfield").gameObject)) return;
            StarGen starGen = Starfield.GetComponent<StarGen>();
            starGen.SetDirection(new Vector2(0, -2));
            Starfield.transform.SetParent(TOR_Background.transform);
            UnityEngine.Object.Destroy(Ambience);

            if (!(LeftPanel = GameObject.Find("LeftPanel"))) return;
            LeftPanel.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            static void ResetParent(GameObject obj) => obj.transform.SetParent(LeftPanel.transform.parent);
            LeftPanel.ForEachChild((Il2CppSystem.Action<GameObject>)ResetParent);
            LeftPanel.SetActive(false);

            Color shade = new(0f, 0f, 0f, 0f);
            var standardActiveSprite = __instance.newsButton.activeSprites.GetComponent<SpriteRenderer>().sprite;
            var minorActiveSprite = __instance.quitButton.activeSprites.GetComponent<SpriteRenderer>().sprite;
            Dictionary<List<PassiveButton>, (Sprite, Color, Color, Color, Color)> mainButtons = new()
            {
                {new List<PassiveButton>() {__instance.playButton, __instance.inventoryButton, __instance.shopButton},
                    (standardActiveSprite, new(0.06f, 0.5f, 1f, 0.8f), shade, Color.white, Color.white) },
                {new List<PassiveButton>() {__instance.newsButton, __instance.myAccountButton, __instance.settingsButton},
                    (minorActiveSprite, new(0.06f, 0.28f, 1f, 0.8f), shade, Color.white, Color.white) },
                {new List<PassiveButton>() {__instance.creditsButton, __instance.quitButton},
                    (minorActiveSprite, new(0.06f, 0.56f, 1f, 0.8f), shade, Color.white, Color.white) },
            };
            try
            {
                mainButtons.Keys.Flatten().DoIf(x => x != null, x => x.buttonText.color = Color.white);
            }
            catch { }

            void FormatButtonColor(PassiveButton button, Sprite borderType, Color inActiveColor, Color activeColor, Color inActiveTextColor, Color activeTextColor)
            {
                button.activeSprites.transform.FindChild("Shine")?.gameObject?.SetActive(false);
                button.inactiveSprites.transform.FindChild("Shine")?.gameObject?.SetActive(false);
                var activeRenderer = button.activeSprites.GetComponent<SpriteRenderer>();
                var inActiveRenderer = button.inactiveSprites.GetComponent<SpriteRenderer>();
                activeRenderer.sprite = minorActiveSprite;
                inActiveRenderer.sprite = minorActiveSprite;
                activeRenderer.color = activeColor.a == 0f ? new Color(inActiveColor.r, inActiveColor.g, inActiveColor.b, 1f) : activeColor;
                inActiveRenderer.color = inActiveColor;
                button.activeTextColor = activeTextColor;
                button.inactiveTextColor = inActiveTextColor;
            }

            foreach (var kvp in mainButtons)
                kvp.Key.Do(button => FormatButtonColor(button, kvp.Value.Item1, kvp.Value.Item2, kvp.Value.Item3, kvp.Value.Item4, kvp.Value.Item5));

            GameObject.Find("Divider")?.SetActive(false);

            if (!(RightPanel = GameObject.Find("RightPanel"))) return;
            var rpap = RightPanel.GetComponent<AspectPosition>();
            if (rpap) UnityEngine.Object.Destroy(rpap);
            RightPanelOp = RightPanel.transform.localPosition;
            RightPanel.transform.localPosition = RightPanelOp + new Vector3(10f, 0f, 0f);
            RightPanel.GetComponent<SpriteRenderer>().color = new(0.06f, 0.36f, 1f, 1f);

            CloseRightButton = new GameObject("CloseRightPanelButton");
            CloseRightButton.transform.SetParent(RightPanel.transform);
            CloseRightButton.transform.localPosition = new Vector3(-4.78f, 1.3f, 1f);
            CloseRightButton.transform.localScale = new(1f, 1f, 1f);
            CloseRightButton.AddComponent<BoxCollider2D>().size = new(0.6f, 1.5f);
            var closeRightSpriteRenderer = CloseRightButton.AddComponent<SpriteRenderer>();
            closeRightSpriteRenderer.sprite = ModHelpers.loadSpriteFromResources("RightPanelCloseButton.png", 100f);
            closeRightSpriteRenderer.color = new(0.06f, 0.36f, 1f, 1f);
            var closeRightPassiveButton = CloseRightButton.AddComponent<PassiveButton>();
            closeRightPassiveButton.OnClick = new();
            closeRightPassiveButton.OnClick.AddListener((System.Action)MainMenuManagerPatch.HideRightPanel);
            closeRightPassiveButton.OnMouseOut = new();
            closeRightPassiveButton.OnMouseOut.AddListener((System.Action)(() => closeRightSpriteRenderer.color = new(0.06f, 0.36f, 1f, 1f)));
            closeRightPassiveButton.OnMouseOver = new();
            closeRightPassiveButton.OnMouseOver.AddListener((System.Action)(() => closeRightSpriteRenderer.color = new(0.06f, 0.36f, 1f, 1f)));

            Tint = __instance.screenTint.gameObject;
            var ttap = Tint.GetComponent<AspectPosition>();
            if (ttap) UnityEngine.Object.Destroy(ttap);
            Tint.transform.SetParent(RightPanel.transform);
            Tint.transform.localPosition = new Vector3(-0.0824f, 0.0513f, Tint.transform.localPosition.z);
            Tint.transform.localScale = new Vector3(1f, 1f, 1f);
            __instance.howToPlayButton.gameObject.SetActive(true);
            __instance.howToPlayButton.transform.parent.Find("FreePlayButton").gameObject.SetActive(true);

            var creditsScreen = __instance.creditsScreen;
            if (creditsScreen)
            {
                var csto = creditsScreen.GetComponent<TransitionOpen>();
                if (csto) UnityEngine.Object.Destroy(csto);
                var closeButton = creditsScreen.transform.FindChild("CloseButton");
                closeButton?.gameObject.SetActive(false);
            }

            if (!(Sizer = GameObject.Find("Sizer"))) return;
            if (!(AULogo = GameObject.Find("LOGO-AU"))) return;
            Sizer.transform.localPosition += new Vector3(0f, 0.12f, 0f);
            AULogo.transform.localScale = new Vector3(0.66f, 0.67f, 1f);
            AULogo.transform.position += new Vector3(0f, 0.1f, 0f);
            var logoRenderer = AULogo.GetComponent<SpriteRenderer>();
            logoRenderer.sprite = ModHelpers.loadSpriteFromResources("Banner.png", 300f);

            if (!(BottomButtonBounds = GameObject.Find("BottomButtonBounds"))) return;
            BottomButtonBounds.transform.localPosition -= new Vector3(0f, 0.1f, 0f);
        }
    }
    [HarmonyPatch(typeof(CreditsScreenPopUp))]
    internal class CreditsScreenPopUpPatch
    {
        [HarmonyPatch(nameof(CreditsScreenPopUp.OnEnable))]
        public static void Postfix(CreditsScreenPopUp __instance)
        {
            __instance.BackButton.transform.parent.FindChild("Background").gameObject.SetActive(false);
        }
    }
}
