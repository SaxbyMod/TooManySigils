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

        public override bool RespondsToCardGettingAttacked(PlayableCard source)
        {
            return base.Card.Health > 0 && !base.Card.Dead && this.Card.AttackedThisTurn;
        }

        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            int RandomInt = UnityEngine.Random.Range(1, 5);
            if (RandomInt == 1)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                yield return new WaitForSeconds(0.15f);
                if (source != null)
                {
                    yield return source.Die(false, null, false);
                    Console.WriteLine("Success");
                } else
                {
                    Console.WriteLine("Unsuccsessful: Source Null");
                }
                yield return new WaitForSeconds(0.3f);
            } else if (RandomInt == 2 || RandomInt == 3 || RandomInt == 4) {
                Console.WriteLine("Unsuccsessful: Random = 2-4");
            }
            else
            {
                Console.WriteLine("Unsuccessful: Not In Range");
            }
            yield return base.LearnAbility(0.1f);
            yield break;
        }
        public static Ability ability;
    }
}
