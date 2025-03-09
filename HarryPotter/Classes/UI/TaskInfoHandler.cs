using Reactor.Utilities.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace HarryPotter.Classes
{
    public class TaskInfoHandler
    {
        public static TaskInfoHandler Instance { get; set; }
        public List<ImportantTextTask> AllInfo { get; set; }

        public void Update()
        {
            if (HudManager.Instance.taskDirtyTimer != 0f)
                return;

            if (AllInfo.Count == 0 && PlayerControl.LocalPlayer.myTasks.Count > 0)
            {
                string roleName = Main.Instance.GetLocalModdedPlayer().Role == null
                    ? (PlayerControl.LocalPlayer.Data.Role.IsImpostor ? ModTranslation.getString("TeamImpostor") : ModTranslation.getString("TeamMuggle"))
                    : ModTranslation.getString(Main.Instance.GetLocalModdedPlayer().Role.RoleNameTranslation);
                AddNewItem(0, string.Format(ModTranslation.getString("taskTextRoleText"), GetRoleHexColor(PlayerControl.LocalPlayer), roleName));
                if (Main.Instance.GetLocalModdedPlayer().Role == null) return;
                if (!Main.Instance.Config.ShowPopups) return;
                AddNewItem(1, string.Format(ModTranslation.getString("taskTextRoleIntroText"), GetRoleHexColor(PlayerControl.LocalPlayer)));
            }
        }

        public ImportantTextTask AddNewItem(int index, string text)
        {
            GameObject roleTextObj = new GameObject();
            ImportantTextTask textTask = roleTextObj.AddComponent<ImportantTextTask>();
            textTask.transform.SetParent(PlayerControl.LocalPlayer.transform, false);
            textTask.Text = text;
            textTask.Index = 0;
            PlayerControl.LocalPlayer.myTasks.Insert(index, textTask);
            AllInfo.Add(textTask);
            return textTask;
        }

        public void RemoveItem(ImportantTextTask item)
        {
            item.Destroy();
            PlayerControl.LocalPlayer.myTasks.Remove(item);
        }

        public string GetRoleHexColor(PlayerControl player)
        {
            ModdedPlayerClass moddedPlayer = Main.Instance.ModdedPlayerById(player.PlayerId);
            if (moddedPlayer.Role == null)
                return "<#FFFFFF>";

            return Extensions.ToTextColor(moddedPlayer.Role.RoleColor);
        }
    }
}