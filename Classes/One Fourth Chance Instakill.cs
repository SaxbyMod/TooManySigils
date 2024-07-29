using DiskCardGame;
using System;
using System.Collections;
using UnityEngine;

namespace TooManySigils.Classes
{
    public class One_Fouth_Chance_Instakill : AbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        // i assume with this sigil, you're trying to avoid the base card taking any damage - this will take care of that
        public override bool RespondsToSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            return Card.OnBoard && !Card.Dead && slot == Card.Slot;
        }

        public override IEnumerator OnSlotTargetedForAttack(CardSlot slot, PlayableCard attacker)
        {
            // (0, 4) means the same as (1, 5), i just prefer 0-indexing; feel free to revert back
            // also i condensed the variable into the if-statement, since you don't use the value anywhere else
            if (UnityEngine.Random.Range(0, 4) == 0)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                yield return new WaitForSeconds(0.15f);
                if (attacker != null)
                {
                    yield return attacker.Die(false, null, false);
                    Card.Anim.LightNegationEffect();
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Unsuccessful: Card == null");
                }
                yield return new WaitForSeconds(0.3f);
            }
            else // don't need to check for the other values
            {
                Console.Write("Unsuccessful: Random == 1-3"); // also changed the message
            }
            yield return base.LearnAbility(0.1f);
        }

        public static Ability ability;
    }
}