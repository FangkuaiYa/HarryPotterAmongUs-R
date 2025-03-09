using HarryPotter.Classes.WorldItems;
using Hazel;

namespace HarryPotter.Classes.Items
{
    public class PhiloStone : Item
    {
        public PhiloStone(ModdedPlayerClass owner)
        {
            this.Owner = owner;
            this.ParentInventory = owner.Inventory;
            this.Id = 9;
            this.Icon = Main.Instance.Assets.WorldItemIcons[Id];
            this.Name = "Philosopher's Stone";
            this.Tooltip = ModTranslation.getString("PhiloStoneTooltip");
            this.IsSpecial = true;
        }
    }
}