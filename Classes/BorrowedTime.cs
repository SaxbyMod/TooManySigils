using DiskCardGame;
using System;
using System.Collections;
using UnityEngine;

namespace TooManySigils.Classes
{
    internal class BorrowedTime : AbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }
        public static Ability ability;
        public int Turn = 0;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            // this ensures Turn is only incremented during the base card's turn
            // returning playerUpkeep by itself will cause it to trigger whenever it's the player's turn, even if the card is an opponent
            return playerUpkeep != base.Card.OpponentCard;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            Turn++;
            yield break; // not sure what yield return Turn++ does, so unless you know then keep this as is
        }


        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            // base.RespondsToDie always returns false
            // since this sigil creates a new card in the current slot, we don't want it to react to being sacced
            // otherwise, you create the possibility of a softlock in the event you're playing a Blood card and there are no other empty slots
            return !wasSacrifice;
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            // evolveParams can be null so add null-coalescing to account for that
            int turnsToEvolve = base.Card.Info.evolveParams?.turnsToEvolve ?? 0;

            // move this here since it's shared between both parts of the if-statement
            if (Singleton<ViewManager>.Instance.CurrentView != View.BoardCentered)
            {
                yield return new WaitForSeconds(0.3f);
                Singleton<ViewManager>.Instance.SwitchToView(View.BoardCentered, false, false);
                yield return new WaitForSeconds(0.3f);
            }
            if (Turn < turnsToEvolve)
            {
                Console.Write("Turn < turnsToEvolve");
                // iceCubeParams can be null as well
                if (base.Card.Info.iceCubeParams?.creatureWithin != null)
                {
                    // the 'name' parameter refers to the name of the current Object, not the name of the CardInfo
                    // also, since creatureWithin is a CardInfo and not a string, we want to create a clone of it to account for any built-in CardModInfos that may be attached
                    Console.Write("Ice Cube");
                    yield return BoardManager.Instance.CreateCardInSlot(base.Card.Info.iceCubeParams.creatureWithin.Clone() as CardInfo, base.Card.Slot, 0.15f);
                    yield break;
                }
            }
            else  // if Turn is not less than turnsToEvolve then it MUST be equal or greater to it, so no need to check for that
            {
                Console.Write("Turn >= turnsToEvolve");
                if (base.Card.Info.evolveParams?.evolution != null)
                {
                    Console.Write("Evolution");
                    yield return BoardManager.Instance.CreateCardInSlot(base.Card.Info.evolveParams.evolution.Clone() as CardInfo, base.Card.Slot, 0.15f);
                    yield break;
                }
            }

            // moved this here since it's a shared possibility
            Console.Write("Ringworm");
            yield return BoardManager.Instance.CreateCardInSlot(CardLoader.GetCardByName("RingWorm"), base.Card.Slot, 0.15f);
        }
    }
}