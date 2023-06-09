using BepInEx;
using DiskCardGame;
using ExampleMod.Classes;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
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
        private const string PluginVersion = "1.1.0";
        private const string PluginPrefix = "Too Many Sigils";

        // For some things, like challenge icons, we need to add the art now instead of later.
        // We initialize the list here, in Awake() we'll add the sprites themselves.
        public static List<Sprite> art_sprites;

        // This is where you would run all of your other methods.
        private void Awake()
        {
            // Apply our harmony patches.
            harmony.PatchAll(typeof(Plugin));

            // Here we add the sprites to the list we created earlier.
            art_sprites = new List<Sprite>();

            Logger.LogInfo($"Loaded {PluginName}!");

            /* Add abilities before cards. Otherwise, the game will try to load cards before the abilities are created.
            AddTestCardForTMS();
            AddTestCard();
             The example ability method. */
            AddDeadPack();
            AddHeartlocked();
            AddThickshell();
            AddSpawner();
            AddLauncher();
            AddHost();
            AddBastion();
            AddRotting();
            AddCovalesce();
            AddAntsMinus();
            AddLoseBattery();
            AddLoseBattery2();
            AddLoseBattery3();
            AddLoseBattery4();
            AddLoseBattery5();
            AddLoseBattery6();
            AddGainBattery();
            AddGainBattery2();
            AddGainBattery3();
            AddGainBattery4();
            AddGainBattery5();
            AddGainBattery6();
        }

        // This method passes the ability and the ability information to the API.
        private void AddDeadPack()
        {
            // This builds our ability information.
            AbilityInfo deadpack = AbilityManager.New(
                PluginGuid,
                "Dead Pack",
                "When a card bearing this sigil perishes, it provides 1 Item to its owner.",
                typeof(DeadDraw.DeathDraw),
                "Dead_Draw_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Test_Icon_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            DeadDraw.DeathDraw.ability = deadpack.ability;
        }
        private void AddHeartlocked()
        {
            // This builds our ability information.
            AbilityInfo Heartlocked = AbilityManager.New(
                PluginGuid,
                "Heart Locked",
                "When a card bearing this sigil is damaged, It only takes 1 Damage.",
                typeof(HeartLocked),
                "Resistant_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Resistant_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            HeartLocked.ability = Heartlocked.ability;
        }
        private void AddThickshell()
        {
            // This builds our ability information.
            AbilityInfo thickshell = AbilityManager.New(
                PluginGuid,
                "Thick Shell",
                "When a card bearing this sigil is damaged, It takes 1 less Damage.",
                typeof(Thickshell),
                "Thick_Shell_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Thick_Shell_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Thickshell.ability = thickshell.ability;
        }
        private void AddLauncher()
        {
            // This builds our ability information.
            AbilityInfo launcher = AbilityManager.New(
                PluginGuid,
                "Launcher",
                "If a card bearing this sigil is on the field during your endstep, a card is launched to one of your empty spaces.",
                typeof(Launcher),
                "Launcher_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Launcher_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Launcher.ability = launcher.ability;
        }
        private void AddSpawner()
        {
            // This builds our ability information.
            AbilityInfo spawner = AbilityManager.New(
                PluginGuid,
                "Spawner",
                "If a card bearing this sigil is on field during your endstep, this card moves in the direction inscribed in the sigil, and a card is played in its previous space.",
                typeof(Spawner),
                "Spawner_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Spawner_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Spawner.ability = spawner.ability;
        }
        private void AddHost()
        {
            // This builds our ability information.
            AbilityInfo host = AbilityManager.New(
                PluginGuid,
                "Host",
                "When a card bearing this sigil is damaged, a card is added to your hand.",
                typeof(Host),
                "Host_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Host_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Host.ability = host.ability;
        }
        private void AddBastion()
        {
            // This builds our ability information.
            AbilityInfo bastion = AbilityManager.New(
                PluginGuid,
                "Bastion",
                "When this card is damaged, It takes half the Damage it would normally.",
                typeof(Bastion),
                "Bastion_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Bastion_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Bastion.ability = bastion.ability;
        }
        private void AddRotting()
        {
            // This builds our ability information.
            AbilityInfo rotting = AbilityManager.New(
                PluginGuid,
                "Rotting",
                "If a card bearing this sigil is alive during your endstep, this card loses 1 power and 1 health.",
                typeof(Rotting),
                "Rotting_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Rotting_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Rotting.ability = rotting.ability;
        }
        private void AddCovalesce()
        {
            // This builds our ability information.
            AbilityInfo covalesce = AbilityManager.New(
                PluginGuid,
                "Covalesce",
                "If a card bearing this sigil is alive during your endstep, This card gains 1 health.",
                typeof(Covalesce),
                "Covalesce_A1.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Covalesce_A2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Covalesce.ability = covalesce.ability;
        }
        private void AddAntsMinus()
        {
            StatIconManager.New(
                 PluginGuid,
                 "Ants Minus",
                 "This cards power is equal to the amount of Ant's on field, minus this one.",
                 typeof(AntMinus)
                 )

                 .SetIcon("AntsMinus_A1.png")
     .SetDefaultPart1Ability()
             .SetDefaultPart3Ability()
             .appliesToHealth = false;
        }
        private void AddLoseBattery()
        {
            // This builds our ability information.
            AbilityInfo losebattery = AbilityManager.New(
                PluginGuid,
                "Lose One Battery",
                "When a card bearing this sigil is played, the owner loses one Max Energy.",
                typeof(Lose1Battery),
                "Lose1Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Lose1Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Lose1Battery.ability = losebattery.ability;
        }
        private void AddLoseBattery2()
        {
            // This builds our ability information.
            AbilityInfo lose2battery = AbilityManager.New(
                PluginGuid,
                "Lose Two Batteries",
                "When a card bearing this sigil is played, the owner loses two Max Energy.",
                typeof(Lose2Battery),
                "Lose2Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Lose2Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Lose2Battery.ability = lose2battery.ability;
        }
        private void AddLoseBattery3()
        {
            // This builds our ability information.
            AbilityInfo lose3battery = AbilityManager.New(
                PluginGuid,
                "Lose Three Batteries",
                "When a card bearing this sigil is played, the owner loses three Max Energy.",
                typeof(Lose3Battery),
                "Lose3Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Lose3Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Lose3Battery.ability = lose3battery.ability;
        }
        private void AddLoseBattery4()
        {
            // This builds our ability information.
            AbilityInfo lose4battery = AbilityManager.New(
                PluginGuid,
                "Lose Four Batteries",
                "When a card bearing this sigil is played, the owner loses four Max Energy.",
                typeof(Lose4Battery),
                "Lose4Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Lose4Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Lose4Battery.ability = lose4battery.ability;
        }
        private void AddLoseBattery5()
        {
            // This builds our ability information.
            AbilityInfo lose5battery = AbilityManager.New(
                PluginGuid,
                "Lose Five Batteries",
                "When a card bearing this sigil is played, the owner loses five Max Energy.",
                typeof(Lose5Battery),
                "Lose5Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Lose5Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Lose5Battery.ability = lose5battery.ability;
        }
        private void AddLoseBattery6()
        {
            // This builds our ability information.
            AbilityInfo lose6battery = AbilityManager.New(
                PluginGuid,
                "Lose Six Batteries",
                "When a card bearing this sigil is played, the owner loses six Max Energy.",
                typeof(Lose6Battery),
                "Lose6Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Lose6Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Lose6Battery.ability = lose6battery.ability;
        }
        private void AddGainBattery()
        {
            // This builds our ability information.
            AbilityInfo gain1battery = AbilityManager.New(
                PluginGuid,
                "Gain One Battery",
                "When a card bearing this sigil is played, the owner gains one Max Energy.",
                typeof(Gain1Battery),
                "Gain1Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Gain1Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Gain1Battery.ability = gain1battery.ability;
        }
        private void AddGainBattery2()
        {
            // This builds our ability information.
            AbilityInfo gain2battery = AbilityManager.New(
                PluginGuid,
                "Gain Two Batteries",
                "When a card bearing this sigil is played, the owner gains two Max Energy.",
                typeof(Gain2Battery),
                "Gain2Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Gain2Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Gain2Battery.ability = gain2battery.ability;
        }
        private void AddGainBattery3()
        {
            // This builds our ability information.
            AbilityInfo gain3battery = AbilityManager.New(
                PluginGuid,
                "Gain Three Batteries",
                "When a card bearing this sigil is played, the owner gains three Max Energy.",
                typeof(Gain3Battery),
                "Gain3Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Gain3Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Gain3Battery.ability = gain3battery.ability;
        }
        private void AddGainBattery4()
        {
            // This builds our ability information.
            AbilityInfo gain4battery = AbilityManager.New(
                PluginGuid,
                "Gain Four Batteries",
                "When a card bearing this sigil is played, the owner gains four Max Energy.",
                typeof(Gain4Battery),
                "Gain4Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Gain4Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Gain4Battery.ability = gain4battery.ability;
        }
        private void AddGainBattery5()
        {
            // This builds our ability information.
            AbilityInfo gain5battery = AbilityManager.New(
                PluginGuid,
                "Gain Five Batteries",
                "When a card bearing this sigil is played, the owner gains five Max Energy.",
                typeof(Gain5Battery),
                "Gain5Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Gain5Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Gain5Battery.ability = gain5battery.ability;
        }
        private void AddGainBattery6()
        {
            // This builds our ability information.
            AbilityInfo gain6battery = AbilityManager.New(
                PluginGuid,
                "Gain Six Batteries",
                "When a card bearing this sigil is played, the owner gains six Max Energy.",
                typeof(Gain6Battery),
                "Gain6Battery.png"
            )

            // This ability will show up in the Part 1 rulebook and can appear on cards in Part 1.
            .SetDefaultPart1Ability()

            // This specifies the icon for the ability if it exists in Part 2.
            .SetPixelAbilityIcon(TextureHelper.GetImageAsTexture("Gain6Battery_a2.png"), FilterMode.Point)

            //Adds all rulebook metacategories
            .AddMetaCategories(AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part3Rulebook, AbilityMetaCategory.GrimoraRulebook, AbilityMetaCategory.MagnificusRulebook);

            // Pass the ability to the API.
            Gain6Battery.ability = gain6battery.ability;
        }
        /*
        private void AddTestCard()
        {
            CardInfo Test = CardManager.New(

                // Card ID Prefix
                modPrefix: "Test",
                // Card internal name
                "Test",
                // Displayed Name
                "Test",
                // Attack.
                0,
                // Health.
                3
            )
            .SetCost(energyCost: 1)
            .AddAbilities(Lose1Battery.ability)
            .SetDefaultPart1Card();
            CardManager.Add("Test", Test);
        }

        private void AddTestCardForTMS()
        {
            CardInfo Test = CardManager.New(
                PluginGuid,
                // Card internal name
                "TestTMS",
                // Displayed Name
                "TestTMS",
                // Attack.
                2,
                // Health.
                3
            )
            .SetCost(energyCost: 1)
            .AddAbilities(Rotting.ability, Covalesce.ability)
            .SetDefaultPart1Card();
            CardManager.Add("Test", Test);
        }
        */
    }
}
