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
        public CardModificationInfo mod = new();

        public override Ability Ability
        {
            get
            {
                return Ability;
            }
        }

        public static Ability ability;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep;
        }

        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (base.Card.HasAbility(Ability.Flying))
            {
                this.mod.abilities.Add(Ability.Submerge);
                this.mod.abilities.Remove(Ability.Flying);
            }
            else if (base.Card.HasAbility(Ability.Submerge))
            {
                this.mod.abilities.Add(Ability.Flying);
                this.mod.abilities.Remove(Ability.Submerge);
            }
            else
            {
                this.mod.abilities.Add(Ability.Submerge);
            }
            yield break;
        }
    }
}
