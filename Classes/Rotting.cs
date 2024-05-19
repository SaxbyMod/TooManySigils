using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace TooManySigils.Classes
{
    internal class Rotting : AbilityBehaviour
    {
        public static Ability ability;

        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return Card.Slot.IsPlayerSlot == playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            yield return new WaitForSeconds(0.15f);

            if (!Card.Slot == Card.Dead)
            {
                if (Card.Slot == Card.Health.Equals(1))
                {
                    yield return Card.Die(false, null, false);
                }
                else
                {
                    Card.Anim.StrongNegationEffect();
                    Card.TemporaryMods.Add(new CardModificationInfo(-1, -1));
                    yield return LearnAbility(0f);
                    yield return new WaitForSeconds(0.3f);
                }
            }

            yield break;
        }
    }
}
