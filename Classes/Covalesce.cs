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
    internal class Covalesce : AbilityBehaviour
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
                Card.TemporaryMods.Add(new CardModificationInfo(0, 1)); //CHANGE NOW!!!!!!!!!!!!!!!!!!
                yield return LearnAbility(0f);
                yield return new WaitForSeconds(0.3f);
            }

            yield break;
        }
    }
}
