using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;

namespace TooManySigils.Classes
{
    internal class One_Fourth_Chance_Instakill
    {
        internal static Ability ability;

        public class OneFouthChanceInstakill : AbilityBehaviour
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
                return base.RespondsToCardGettingAttacked(source);
            }
            public override IEnumerator OnTakeDamage(PlayableCard source)
            {
                if (UnityEngine.Random.value <= 0.25f)
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                    yield return new WaitForSeconds(0.15f);
                    if (source != null)
                    {
                        yield return source.Die(false, null, false);
                    }
                    yield return new WaitForSeconds(0.3f);
                    yield return base.LearnAbility(0.1f);
                }
                yield break;
            }
            public static Ability ability;
        }
    }
}
