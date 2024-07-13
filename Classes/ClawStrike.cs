using DiskCardGame;
using HarmonyLib;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TooManySigils.Classes
{
    [HarmonyPatch]
    internal class ClawStrike : ExtendedAbilityBehaviour, IOnPostSlotAttackSequence
    {

        public static Ability ability;

        // instead of adding and modifying CardModInfos, we can use an internal bool to keep track of alternating strikes
        // and a second bool that can determines whether or not to give extra attack
        private bool DealBonusToLeft = true;
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        public override bool RemoveDefaultAttackSlot() => true; // truncated these to reduce visual noise, revert them back if you want
        public override bool RespondsToGetOpposingSlots() => true;

        public override List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            // sets currentTargets to the result, then returns the result
            return currentTargets = Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot.opposingSlot);
        }

        List<CardSlot> currentTargets = new();
        // the API adds custom trigger interfaces for OnPostSingularSlotAttackSlot, this is unnecessary (in most cases)
        /*[HarmonyPostfix]
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
        }*/

        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            return attacker == base.Card && currentTargets.Contains(slot);
        }

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            // since we did a .Contains check, we can assume there's at least one item in this list
            if (DealBonusToLeft)
            {
                if (currentTargets[0] == slot)
                {
                    base.Card.AddTemporaryMod(new() { singletonId = "ClawStrikePower", nonCopyable = true }); // nonCopyable because we don't want other cards to accidentally get this
                }
            }
            else if (currentTargets.Count > 1 && currentTargets[1] == slot) // make sure there's a second slot to check
            {
                base.Card.AddTemporaryMod(new() { singletonId = "ClawStrikePower", nonCopyable = true });
            }
            yield break;
        }

        public bool RespondsToPostSlotAttackSequence(CardSlot attackingSlot) => attackingSlot.Card == base.Card;

        // alternate between dealing more damage to the left or right slot
        // does the same as that SlotAttackSlot patch but triggers after the base card's entire attack is done (rather than on each individual strike)
        // SlotAttackSequence -> GetOpposingSlots -> foreach opposingSlot -> SlotAttackSlot
        public IEnumerator OnPostSlotAttackSequence(CardSlot attackingSlot)
        {
            DealBonusToLeft = !DealBonusToLeft;
            yield return new WaitForSeconds(0.1f); // brief delay so players can tell that an effect has occurred
            attackingSlot.Card.RemoveTemporaryMod(attackingSlot.Card.TemporaryMods.Find(x => x.singletonId == "ClawStrikePower"));
            base.Card.Anim.StrongNegationEffect(); // indicate that the bonus-damage target has changed
            yield return new WaitForSeconds(0.4f);
        }

        // i'm confused on the intent here - is the target of the extra damage dealt meant to be determined by the TurnNumber, or an independent thing?

        // since we're just adding an additional Power, we don't need to override the entire method
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayableCard), nameof(PlayableCard.Attack), MethodType.Getter)]
        public static void AttackPatch(ref PlayableCard __instance, ref int __result)
        {
            if (!__instance.HasAbility(ability)) // only affect cards with Claw Strike
                return;

            if (__instance.TemporaryMods.Find(x => x.singletonId == "ClawStrikePower") != null)
                __result++;
        }
    }
}