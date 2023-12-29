using BepInEx.Bootstrap;
using DiskCardGame;
using GBC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TooManySigils.Classes
{
    public class TreasureTracker : AbilityBehaviour
    {
        // Token: 0x1700005D RID: 93
        // (get) Token: 0x06000231 RID: 561 RVA: 0x0000B940 File Offset: 0x00009B40
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        // Token: 0x06000232 RID: 562 RVA: 0x0000B948 File Offset: 0x00009B48
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return Card != null && Card.OpponentCard != playerTurnEnd;
        }

        // Token: 0x06000233 RID: 563 RVA: 0x0000B97C File Offset: 0x00009B7C
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.15f);
            bool flag = !SaveManager.SaveFile.IsPart2;
            bool flag2 = flag;
            if (flag2)
            {
                bool flag3 = Chainloader.PluginInfos.ContainsKey("extraVoid.inscryption.LifeCost");
                if (flag3)
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Scales, false, true);
                    yield return new WaitForSeconds(0.25f);
                    RunState.Run.currency++;
                    yield return Singleton<CurrencyBowl>.Instance.DropWeightsIn(1);
                    yield return new WaitForSeconds(0.75f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, true);
                    Singleton<ViewManager>.Instance.Controller.LockState = 0;
                }
                else
                {
                    Singleton<ViewManager>.Instance.SwitchToView(View.Scales, false, true);
                    yield return new WaitForSeconds(0.25f);
                    RunState.Run.currency++;
                    yield return Singleton<CurrencyBowl>.Instance.ShowGain(1, true, false);
                    yield return new WaitForSeconds(0.25f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, true);
                    Singleton<ViewManager>.Instance.Controller.LockState = 0;
                }
            }
            else
            {
                SaveData.Data.currency++;
                Card.Anim.LightNegationEffect();
            }
            yield return LearnAbility(0.25f);
            yield return new WaitForSeconds(0.1f);
            yield break;
        }

        // Token: 0x0400008F RID: 143
        public static Ability ability;
    }
}
