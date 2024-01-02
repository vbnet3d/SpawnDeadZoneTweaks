using HarmonyLib;
using SpawnDeadZoneTweaks.Common;
using System.Reflection;
using UnityEngine;

namespace SpawnDeadZoneTweaks.Harmony
{
    [HarmonyPatch()]
    public class World_isPositionInRangeOfBedrolls
    {
        static MethodBase TargetMethod()
        {
            return typeof(World).GetMethod("isPositionInRangeOfBedrolls", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static bool Prefix(World __instance, Vector3 _position, ref bool __result)
        {
            if (Config.Instance.UseLandClaimDeadZone)
            {
                Logging.LogOnce("Checking land claim dead zone for spawn");
                Vector3i blockPos = World.worldToBlockPos(_position);

                EnumLandClaimOwner spawnPoints = __instance.GetLandClaimOwner(blockPos, __instance.gameManager.GetPersistentLocalPlayer());

                if (spawnPoints != EnumLandClaimOwner.None)
                {
                    __result = true;
                }
            }

            return true;
        }
    }
}
