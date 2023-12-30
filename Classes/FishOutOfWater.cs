using DiskCardGame;
using InscryptionAPI.Card;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooManySigils.Classes
{
    public class FishOutOfWater : AbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return Ability;
            }
        }

        public static Ability ability;

        public CardModificationInfo SubmergeMod = new();
        public CardModificationInfo SwapToSubmergeMod = new();
        public CardModificationInfo SwapToAirborneMod = new();

        public void Start()
        {
            SwapToSubmergeMod.abilities.Add(Ability.Submerge);
            SwapToSubmergeMod.abilities.Remove(Ability.Flying);
            SwapToAirborneMod.abilities.Add(Ability.Flying);
            SwapToAirborneMod.abilities.Remove(Ability.Submerge);
            SubmergeMod.abilities.Add(Ability.Submerge);
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (base.Card.HasAbility(Ability.Flying))
            {
                base.Card.AddTemporaryMod(SwapToSubmergeMod);
            }
            else if (base.Card.HasAbility(Ability.Submerge))
            {
                base.Card.AddTemporaryMod(SwapToAirborneMod);
            }
            else
            {
                base.Card.AddTemporaryMod(SubmergeMod);
            }
            yield break;
        }
    }
}
