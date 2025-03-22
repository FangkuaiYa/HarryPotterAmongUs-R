
using HarryPotter.Classes.Roles;
using HarryPotter.CustomOption;

namespace HarryPotter.Classes
{
    class Config
    {
        private static CustomHeaderOption roleSettings;
        private static CustomToggleOption BellatrixOn;
        private static CustomToggleOption HarryOn;
        private static CustomToggleOption HermioneOn;
        private static CustomToggleOption RonOn;
        private static CustomToggleOption VoldemortOn;

        private static CustomHeaderOption HPSettings;
        private static CustomToggleOption enableModRole;
        private static CustomToggleOption spellsInVents;
        private static CustomToggleOption randomGameStartPosition;

        private static CustomHeaderOption ItemSettings;
        private static CustomNumberOption beerDuration;
        private static CustomNumberOption mapDuration;

        private static CustomHeaderOption VoldemortSettings;
        private static CustomToggleOption orderOfTheImp;
        private static CustomToggleOption separateCooldowns;
        private static CustomNumberOption crucioCooldown;
        private static CustomNumberOption crucioDuration;
        private static CustomHeaderOption BellatrixSettings;
        private static CustomNumberOption imperioDuration;
        private static CustomHeaderOption RonSettings;
        private static CustomNumberOption defensiveDuelistCooldown;
        private static CustomNumberOption defensiveDuelistDuration;
        private static CustomHeaderOption HarrySettings;
        private static CustomNumberOption invisCloakCooldown;
        private static CustomNumberOption invisCloakDuration;
        private static CustomHeaderOption HermioneSettings;
        private static CustomNumberOption hourglassCooldown;
        private static CustomNumberOption hourglassTimer;


        public bool EnableVoldemort { get; private set; }
        public bool EnableBellatrix { get; private set; }
        public bool EnableHarry { get; private set; }
        public bool EnableHermione { get; private set; }
        public bool EnableRon { get; private set; }

        public bool OrderOfTheImp { get; private set; }
        public float MapDuration { get; private set; }
        public float DefensiveDuelistDuration { get; private set; }
        public float InvisCloakDuration { get; private set; }
        public float HourglassTimer { get; private set; }
        public float BeerDuration { get; private set; }
        public float CrucioDuration { get; private set; }
        public float DefensiveDuelistCooldown { get; private set; }
        public float InvisCloakCooldown { get; private set; }
        public float HourglassCooldown { get; private set; }
        public float CrucioCooldown { get; private set; }
        public bool SpellsInVents { get; private set; }
        public float ImperioDuration { get; private set; }
        public bool SeparateCooldowns { get; private set; }
        public bool EnableModRole { get; private set; }
        public bool RandomGameStartPosition { get; private set; }

        public static void LoadOption()
        {
            var num = 0;
            HPSettings = new CustomHeaderOption(num++, "HPSettings");
            enableModRole = new CustomToggleOption(num++, "enableModRole");
            orderOfTheImp = new CustomToggleOption(num++, "orderOfTheImp", false);
            randomGameStartPosition = new CustomToggleOption(num++, "randomGameStartPosition");

            roleSettings = new CustomHeaderOption(num++, "roleSettings");
            VoldemortOn = new CustomToggleOption(num++, ModHelpers.cs(Palette.ImpostorRed, "RoleNameVoldemort"), true);
            BellatrixOn = new CustomToggleOption(num++, ModHelpers.cs(Palette.ImpostorRed, "RoleNameBellatrix"), true);
            HarryOn = new CustomToggleOption(num++, ModHelpers.cs(Palette.Orange, "RoleNameHarry"), true);
            HermioneOn = new CustomToggleOption(num++, ModHelpers.cs(Palette.Orange, "RoleNameHermione"), true);
            RonOn = new CustomToggleOption(num++, ModHelpers.cs(Palette.Orange, "RoleNameRon"), true);

            VoldemortSettings = new CustomHeaderOption(num++, ModHelpers.cs(Palette.ImpostorRed, "RoleNameVoldemort"));
            spellsInVents = new CustomToggleOption(num++, "spellsInVents", false);
            separateCooldowns = new CustomToggleOption(num++, "separateCooldowns");
            crucioCooldown = new CustomNumberOption(num++, "crucioCooldown", 20f, 40f, 10f, 2.5f);
            crucioDuration = new CustomNumberOption(num++, "crucioDuration", 5f, 5f, 30f, 1f);

            BellatrixSettings = new CustomHeaderOption(num++, ModHelpers.cs(Palette.ImpostorRed, "RoleNameBellatrix"));
            imperioDuration = new CustomNumberOption(num++, "imperioDuration", 5f, 5f, 30f, 1f);

            RonSettings = new CustomHeaderOption(num++, ModHelpers.cs(Palette.Orange, "RoleNameRon"));
            defensiveDuelistCooldown = new CustomNumberOption(num++, "defensiveDuelistCooldown", 20f, 40f, 10, 2.5f);
            defensiveDuelistDuration = new CustomNumberOption(num++, "defensiveDuelistDuration", 5f, 5f, 30f, 1f);

            HarrySettings = new CustomHeaderOption(num++, ModHelpers.cs(Palette.Orange, "RoleNameHarry"));
            invisCloakCooldown = new CustomNumberOption(num++, "invisCloakCooldown", 20f, 40f, 10, 2.5f);
            invisCloakDuration = new CustomNumberOption(num++, "invisCloakDuration", 5f, 5f, 30f, 1f);

            HermioneSettings = new CustomHeaderOption(num++, ModHelpers.cs(Palette.Orange, "RoleNameHermione"));
            hourglassCooldown = new CustomNumberOption(num++, "hourglassCooldown", 20f, 40f, 10f, 2.5f);
            hourglassTimer = new CustomNumberOption(num++, "hourglassTimer", 5f, 5f, 30f, 1f);


            ItemSettings = new CustomHeaderOption(num++, "ItemSettings");
            
            beerDuration = new CustomNumberOption(num++, "beerDuration", 5f, 5f, 30f, 1f);
            mapDuration = new CustomNumberOption(num++, "mapDuration", 5f, 5f, 30f, 1f);
        }

        public void ReloadSettings()
        {
            EnableModRole = enableModRole.Get();

            EnableVoldemort = VoldemortOn.Get();
            EnableBellatrix = BellatrixOn.Get();
            EnableHarry = HarryOn.Get();
            EnableHermione = HermioneOn.Get();
            EnableRon = RonOn.Get();


            OrderOfTheImp = orderOfTheImp.Get();
            SpellsInVents = spellsInVents.Get();
            DefensiveDuelistCooldown = defensiveDuelistCooldown.Get();
            InvisCloakCooldown = invisCloakCooldown.Get();
            HourglassCooldown = hourglassCooldown.Get();
            CrucioCooldown = crucioCooldown.Get();
            SeparateCooldowns = !separateCooldowns.Get();
            ImperioDuration = imperioDuration.Get();
            RandomGameStartPosition = randomGameStartPosition.Get();
            MapDuration = mapDuration.Get();
            DefensiveDuelistDuration = defensiveDuelistDuration.Get();
            InvisCloakDuration = invisCloakDuration.Get();
            HourglassTimer = hourglassTimer.Get();
            BeerDuration = beerDuration.Get();
            CrucioDuration = crucioDuration.Get();
        }
    }
}
