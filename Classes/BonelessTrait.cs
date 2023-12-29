using DiskCardGame;
using ExampleMod;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Guid;

namespace TooManySigils.Classes
{
    internal class BonelessTrait
    {
        private static Trait Boneless = GuidManager.GetEnumValue<Trait>(PluginInfo.PLUGIN_GUID, "Boneless");
        [HarmonyPatch(typeof(ResourcesManager), nameof(ResourcesManager.AddBones))]
        public class Boneless_Patch
        {
            [HarmonyPrefix]
            public static bool Prefix(int amount, CardSlot slot)
            {
                if (slot != null
                && slot.Card != null
                && slot.Card.HasTrait(Boneless))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
