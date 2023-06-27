using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace ExampleMod.Classes
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
            return base.Card.Slot.IsPlayerSlot == playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            yield return new WaitForSeconds(0.15f);

            if (!base.Card.Slot == Card.Dead)
            {
                base.Card.Anim.StrongNegationEffect();
                base.Card.TemporaryMods.Add(new CardModificationInfo(-1, -1));
                yield return base.LearnAbility(0f);
                yield return new WaitForSeconds(0.3f);
            }

            yield break;
        }
    }
}
