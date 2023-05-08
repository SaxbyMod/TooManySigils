using BepInEx;
using DiskCardGame;
using ExampleMod.Classes;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleMod
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {

        // Declare Harmony here for future Harmony patches. You'll use Harmony to patch the game's code outside of the scope of the API.
        Harmony harmony = new Harmony(PluginGuid);

        // These are variables that exist everywhere in the entire class.
        private const string PluginGuid = "creator.TooManySigils";
        private const string PluginName = "TooManySigils";
        private const string PluginVersion = "1.0.0";
        private const string PluginPrefix = "Too Many Sigils";

        // For some things, like challenge icons, we need to add the art now instead of later.
        // We initialize the list here, in Awake() we'll add the sprites themselves.
        public static List<Sprite> art_sprites;

        // This is where you would run all of your other methods.
        private void Awake()
        {
            Logger.LogInfo($"Loaded {PluginName}!");

            // Apply our harmony patches.
            harmony.PatchAll(typeof(Plugin));

            // Here we add the sprites to the list we created earlier.
            art_sprites = new List<Sprite>();

            // Add abilities before cards. Otherwise, the game will try to load cards before the abilities are created.

            // The example ability method.
            AddNewTestAbility();

        }

        // This method passes the ability and the ability information to the API.
        private void AddNewTestAbility()
        {
            // This builds our ability information.
            AbilityInfo deadpack = AbilityManager.New(
                PluginGuid,
                "DeadPack",
                "When this card perishes, it provides 1 Item to its owner.",
                typeof(DeadDraw.NewTestAbility),
                "Dead_Pack_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Dead_Pack_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            DeadDraw.NewTestAbility.ability = deadpack.ability;
        }
    }
}
