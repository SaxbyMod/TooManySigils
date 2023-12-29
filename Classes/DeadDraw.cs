using APIPlugin;
using DiskCardGame;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;

namespace TooManySigils.Classes
{
    internal class DeadDraw
    {
        // This is your ability class. This defines what your ability does.
        public class DeathDraw : AbilityBehaviour
        {
            public override Ability Ability
            {
                get
                {
                    return ability;
                }
            }

            //PreDeathAnimation is for when the card has died, but hasn't left the board yet
            //Sigils like Detonator use it
            public override bool RespondsToPreDeathAnimation(bool wasSacrifice)
            {
                //what you return determines if the sigil activates when the trigger happens
                //if this said 'return !wasSacrifice' instead, it would only trigger if it died and wasn't a sacrifice
                //the '!' symbol in coding means NOT btw, so you can read !wasSacrifice as NOTwasSacrifice
                return !wasSacrifice;
            }
            public override IEnumerator OnPreDeathAnimation(bool wasSacrifice)
            {
                yield return PreSuccessfulTriggerSequence();
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
                yield return new WaitForSeconds(0.25f);
                if (RunState.Run.consumables.Count < RunState.Run.MaxConsumables)
                {
                    RunState.Run.consumables.Add(ItemsUtil.GetRandomUnlockedConsumable(GetRandomSeed()).name);
                    Singleton<ItemsManager>.Instance.UpdateItems(false);
                }
                else
                {
                    Card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.2f);
                    Singleton<ItemsManager>.Instance.ShakeConsumableSlots(0f);
                }
                yield return new WaitForSeconds(0.2f);
                yield return LearnAbility(0f);
                yield break;
            }

            //this is just where you store the info
            public static Ability ability;
        }
    }
}
