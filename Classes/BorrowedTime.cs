using DiskCardGame;
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
            return playerUpkeep;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return Turn++;
        }


        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return base.RespondsToDie(!wasSacrifice, killer);
        }

        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            if (Turn < base.Card.Info.evolveParams.turnsToEvolve)
            {
                if (Singleton<ViewManager>.Instance.CurrentView != View.BoardCentered)
                {
                    yield return new WaitForSeconds(0.3f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.BoardCentered, false, false);
                    yield return new WaitForSeconds(0.3f);
                }
                if (base.Card.Info.iceCubeParams.creatureWithin != null)
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName(name), base.Card.Slot, 0.15f, true);
                }
                else
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("Ringworm"), base.Card.Slot, 0.15f, true);
                }
            }
            else if (Turn >= base.Card.Info.evolveParams.turnsToEvolve)
            {
                if (Singleton<ViewManager>.Instance.CurrentView != View.BoardCentered)
                {
                    yield return new WaitForSeconds(0.3f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.BoardCentered, false, false);
                    yield return new WaitForSeconds(0.3f);
                }
                if (base.Card.Info.evolveParams.evolution != null)
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName(name), base.Card.Slot, 0.15f, true);
                }
                else
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(CardLoader.GetCardByName("Ringworm"), base.Card.Slot, 0.15f, true);
                }
            }
        }
    }
}
