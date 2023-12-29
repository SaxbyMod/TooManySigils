using BepInEx.Bootstrap;
using DiskCardGame;
using GBC;
using System.Collections;
using UnityEngine;

namespace TooManySigils.Classes
{
    public class CoinsWithin : AbilityBehaviour
    {
        // Token: 0x170000BC RID: 188
        // (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000F990 File Offset: 0x0000DB90
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        // Token: 0x060003F4 RID: 1012 RVA: 0x0000F998 File Offset: 0x0000DB98
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            return source != null && source.Health > 0;
        }

        // Token: 0x060003F5 RID: 1013 RVA: 0x0000F9BF File Offset: 0x0000DBBF
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, true);
            yield return new WaitForSeconds(0.1f);
            Card.Anim.LightNegationEffect();
            yield return PreSuccessfulTriggerSequence();
            bool flag2 = !SaveManager.SaveFile.IsPart2;
            bool flag3 = flag2;
            if (flag3)
            {
                bool flag4 = Chainloader.PluginInfos.ContainsKey("extraVoid.inscryption.LifeCost");
                if (flag4)
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Scales, false, true);
                    yield return new WaitForSeconds(0.25f);
                    RunState.Run.currency++;
                    yield return Singleton<CurrencyBowl>.Instance.DropWeightsIn(1);
                    yield return new WaitForSeconds(0.75f);
                }
                else
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Scales, false, true);
                    yield return new WaitForSeconds(0.25f);
                    RunState.Run.currency++;
                    yield return Singleton<CurrencyBowl>.Instance.ShowGain(1, true, false);
                    yield return new WaitForSeconds(0.25f);
                }
            }
            else
            {
                SaveData.Data.currency++;
                Card.Anim.StrongNegationEffect();
                Card.Anim.StrongNegationEffect();
            }
            yield return new WaitForSeconds(0.1f);
            yield return LearnAbility(0.1f);
            Singleton<ViewManager>.Instance.Controller.LockState = 0;
            yield break;
        }

        // Token: 0x040000FB RID: 251
        public static Ability ability;
    }
}
