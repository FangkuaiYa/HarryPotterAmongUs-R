﻿using System.Linq;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using hunterlib.Classes;

namespace HarryPotter.Classes.WorldItems
{
    public class DeluminatorWorld : WorldItem
    {
        public static System.Random ItemRandom { get; set; } = new System.Random();
        public static float ItemSpawnChance { get; set; } = 30;
        public static bool HasSpawned { get; set; }
        
        public DeluminatorWorld(Vector2 position)
        {
            Position = position;
            Id = 0;
            Icon = Main.Instance.Assets.WorldItemIcons[Id];
            Name = "Deluminator";
        }

        public static void WorldSpawn()
        {
            if (!CanSpawn())
                return;

            if (!ShipStatus.Instance)
                return;
            
            Vector2 pos = Main.Instance.GetAllApplicableItemPositions().Random();
            Main.Instance.RpcSpawnItem(0, pos);
            HasSpawned = true;
        }
        
        public static bool CanSpawn()
        {
            if (Main.Instance.AllItems.Where(x => x.Id == 0).ToList().Count > 0) return false;
            if (MeetingHud.Instance) return false;
            if (!AmongUsClient.Instance.IsGameStarted) return false;
            if (ItemRandom.Next(0, 100000) > ItemSpawnChance) return false;
            if (HasSpawned) return false;

            return true;
        }
    }
}