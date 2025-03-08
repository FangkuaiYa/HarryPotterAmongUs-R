
using HarryPotter.CustomOption;

namespace HarryPotter.Classes
{
    class Config
    {
        private static CustomHeaderOption HPSettings;
        private static CustomToggleOption Option1;
        private static CustomToggleOption Option3;
        private static CustomToggleOption Option4;
        private static CustomToggleOption Option5;
        private static CustomNumberOption Option9;
        private static CustomNumberOption Option10;
        private static CustomNumberOption Option11;
        private static CustomNumberOption Option12;
        private static CustomToggleOption Option13;
        public bool OrderOfTheImp { get; private set; }
        public float MapDuration { get { return 10; } }
        public float DefensiveDuelistDuration { get { return 10; } }
        public float InvisCloakDuration { get { return 10; } }
        public float HourglassTimer { get { return 10; } }
        public float BeerDuration { get { return 10; } }
        public float CrucioDuration { get { return 10; } }
        public float DefensiveDuelistCooldown { get; private set; }
        public float InvisCloakCooldown { get; private set; }
        public float HourglassCooldown { get; private set; }
        public float CrucioCooldown { get; private set; }
        public bool SpellsInVents { get; private set; }
        public float ImperioDuration { get { return 10; } }
        public bool ShowPopups { get; private set; }
        public bool SeparateCooldowns { get; private set; }
        public bool SimplerWatermark { get { return false; } }
        public bool UseCustomRegion { get { return false; } }
        public bool EnableModRole { get; private set; }

        public static void LoadOption()
        {
            var num = 0;

            HPSettings = new CustomHeaderOption(num++, "<color=#FFF319>Harry Potter Settings</color>");
            Option13 = new CustomToggleOption(num++, "<color=#ffff00>Enable Mod Role</color>");
            Option1 = new CustomToggleOption(num++, "Order of the Impostors", false);
            Option3 = new CustomToggleOption(num++, "Can Spells be Used In Vents", false);
            Option4 = new CustomToggleOption(num++, "Show Info Popups/Tooltips");
            Option5 = new CustomToggleOption(num++, "Shared Voldemort Cooldowns");
            Option9 = new CustomNumberOption(num++, "Defensive Duelist Cooldown", 20f, 40f, 10, 2.5f);
            Option10 = new CustomNumberOption(num++, "Invisibility Cloak Cooldown", 20f, 40f, 10, 2.5f);
            Option11 = new CustomNumberOption(num++, "Time Turner Cooldown", 20f, 40f, 10f, 2.5f);
            Option12 = new CustomNumberOption(num++, "Crucio Cooldown", 20f, 40f, 10f, 2.5f);
        }

        public void ReloadSettings()
        {
            OrderOfTheImp = Option1.Get();
            SpellsInVents = Option3.Get();
            DefensiveDuelistCooldown = Option9.Get();
            InvisCloakCooldown = Option10.Get();
            HourglassCooldown = Option11.Get();
            CrucioCooldown = Option12.Get();
            ShowPopups = Option4.Get();
            SeparateCooldowns = !Option5.Get();
            EnableModRole = Option13.Get();
        }
    }
}
