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
    internal class BellRingerStat : AbilityBehaviour
    {
        public static Ability ability;

        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        public bool RespondsToBellRung(bool playerCombatPhase)
        {
            return Card.OnBoard && playerCombatPhase;
        }

        public IEnumerator OnBellRung(bool playerCombatPhase)
        {
            yield return PreSuccessfulTriggerSequence();
            Card.Anim.LightNegationEffect();
            Card.AddTemporaryMod(new CardModificationInfo(1, 0));
            yield break;
        }
        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            return target.Info.name.ToLowerInvariant().Contains("bell") || target.Info.DisplayedNameEnglish.ToLowerInvariant().Contains("bell");
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            yield return PreSuccessfulTriggerSequence();
            Card.Anim.LightNegationEffect();
            Card.AddTemporaryMod(new CardModificationInfo(1, 0));
            yield break;
        }
    }
}
