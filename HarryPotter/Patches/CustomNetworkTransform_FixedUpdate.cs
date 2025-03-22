using HarmonyLib;

namespace HarryPotter.Patches;

[HarmonyPatch(typeof(CustomNetworkTransform), nameof(CustomNetworkTransform.FixedUpdate))]
internal class CustomNetworkTransform_FixedUpdate
{
    /*static bool Prefix(CustomNetworkTransform __instance)
    {
        if (!__instance.AmOwner)
        {
            if (Main.Instance.AllPlayers.Any(x => x._Object.NetTransform == __instance && x.ControllerOverride == Main.Instance.GetLocalModdedPlayer())) return false;

            if (__instance.body.inertia != 0f)
            {
                Vector2 vector = __instance.lastPosition - __instance.body.position;
                if (vector.sqrMagnitude >= 0.0001f)
                {
                    float num = __instance.body.inertia / CustomNetworkTransform.SEND_MOVEMENT_THRESHOLD;
                    vector.x *= num;
                    vector.y *= num;
                    if (PlayerControl.LocalPlayer)
                    {
                        float multiplier = 1f;
                        var foundPlayers = Main.Instance.AllPlayers.FindAll(x => x._Object.NetTransform == __instance);
                        if (foundPlayers.Count > 0)
                            multiplier = foundPlayers.First().SpeedMultiplier;
                        vector = Vector2.ClampMagnitude(vector, PlayerControl.LocalPlayer.MyPhysics.TrueSpeed * multiplier);
                    }
                    __instance.body.velocity = vector;
                }
                else
                {
                    __instance.body.velocity = Vector2.zero;
                }
            }
            __instance.lastPosition += __instance.lastPosSent * Time.fixedDeltaTime * 0.1f;
            return false;
        }
        return true;
    }*/
}