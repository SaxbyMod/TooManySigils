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

        // use a single CardMod to control behaviour
        // i will note that SubmergeSquid overrides this sigil's effect, if that's something you care about
        // Submerge can't stack, so we just add it regardless of it's already possessed by a card or not
        private readonly CardModificationInfo fishMod = new(Ability.Submerge) { singletonId = "FishOutOfWater", nonCopyable = true };
        public void Start()
        {
            if (base.Card == null)
                return;

            base.Card.AddTemporaryMod(fishMod);
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep != base.Card.OpponentCard;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (base.Card.HasAbility(Ability.Submerge))
            {
                fishMod.abilities = new() { Ability.Flying };
                fishMod.negateAbilities = new() { Ability.Submerge };
            }
            else
            {
                fishMod.abilities = new() { Ability.Submerge };
                fishMod.negateAbilities = new() { Ability.Flying };
            }

            base.Card.Anim.PlayTransformAnimation();
            base.Card.OnStatsChanged();
            yield return new WaitForSeconds(0.4f);
        }
    }
}