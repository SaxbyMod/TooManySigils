using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TooManySigils.Classes
{
    internal class ClawStrike : ExtendedAbilityBehaviour
    {
        public CardModificationInfo mod = new CardModificationInfo() { singletonId = "ClawStrikeLeft" };

        public static Ability ability;

        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        public void Start()
        {
            base.Card.AddTemporaryMod(mod);
        }

        public override bool RemoveDefaultAttackSlot()
        {
            return true;
        }

        public override bool RespondsToGetOpposingSlots()
        {
            return true;
        }

        public override List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            return Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot.opposingSlot);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CombatPhaseManager), nameof(CombatPhaseManager.SlotAttackSlot))]
        public static void SlotAttackSlotPatch(CardSlot attackingSlot)
        {
            if (attackingSlot.Card.HasAbility(ClawStrike.ability))
            {
                CardModificationInfo LeftMod = attackingSlot.Card.TemporaryMods.FirstOrDefault(x => x.singletonId == "ClawStrikeLeft");
                CardModificationInfo RightMod = attackingSlot.Card.TemporaryMods.FirstOrDefault(x => x.singletonId == "ClawStrikeRight");
                if (LeftMod != null)
                {
                    LeftMod.singletonId = "ClawStrikeRight";
                }
                else if (RightMod != null)
                {
                    RightMod.singletonId = "ClawStrikeLeft";
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Attack), MethodType.Getter)]
        public static bool AttackPatch(ref PlayableCard __instance, ref int __result)
        {
            CardModificationInfo LeftMod = __instance.TemporaryMods.FirstOrDefault(x => x.singletonId == "ClawStrikeLeft");
            bool isEvenTurn = Singleton<TurnManager>.Instance.TurnNumber % 2 == 0;
            if ((LeftMod != null) == isEvenTurn && __instance.HasAbility(ClawStrike.ability)) { return true; }

            __result = Mathf.Max(0, __instance.Info.Attack + __instance.GetAttackModifications() + __instance.GetPassiveAttackBuffs()) + 1;
            return false;
        }
    }
}