using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooManySigils.Classes
{
    internal class ClawStrike : ExtendedAbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        public static Ability ability;

        public override bool RespondsToGetOpposingSlots()
        {
            return true;
        }

        public override List<CardSlot> GetOpposingSlots(List<CardSlot> originalSlots, List<CardSlot> otherAddedSlots)
        {
            List<CardSlot> opposingSlots = [];
            opposingSlots.Remove(base.Card.OpposingSlot());

            CardSlot toLeftSlot = BoardManager.Instance.GetAdjacent(base.Card.Slot, true);
            if (toLeftSlot != null)
            {
                opposingSlots.Add(toLeftSlot.opposingSlot);
            }
            else
            {
                Console.Write("Cant Do");
            }

            CardSlot toRightSlot = BoardManager.Instance.GetAdjacent(base.Card.Slot, false);
            if (toRightSlot != null)
            {
                opposingSlots.Add(toRightSlot.opposingSlot);
            }
            else
            {
                Console.Write("Cant Do");
            }
            opposingSlots.Remove(base.Card.OpposingSlot());
            return opposingSlots;
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            CardSlot toLeftSlot = BoardManager.Instance.GetAdjacent(base.Card.Slot, true);
            CardSlot toRightSlot = BoardManager.Instance.GetAdjacent(base.Card.Slot, false);
            int AttackBonus = 1;
            CardSlot chosenSlot = null;

            var rand = new System.Random();
            int randomValue = rand.Next(0, 1);

            if (randomValue == 0 && toLeftSlot.opposingSlot != null)
            {
                Console.Write($"Attacking Left");
                chosenSlot = toLeftSlot.opposingSlot;
            }
            else if (randomValue == 1 && toRightSlot.opposingSlot != null)
            {
                Console.Write($"Attacking Right");
                chosenSlot = toRightSlot.opposingSlot;
            }
            else
            {
                Console.Write($"Err No Slots");
            }
            if (chosenSlot != null && chosenSlot.Card != null)
            {
                yield return PreSuccessfulTriggerSequence();
                yield return new WaitForSeconds(0.2f);
                yield return chosenSlot.Card.TakeDamage(AttackBonus, null);
            }
        }
    }
}

