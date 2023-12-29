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
    public class HeartLocked : AbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }

        public static Ability ability;

        [HarmonyPatch(typeof(PlayableCard), "TakeDamage")]
        public class TakeDamagePatch : PlayableCard
        {
            // Token: 0x06000228 RID: 552 RVA: 0x000090AC File Offset: 0x000072AC
            private static void Prefix(ref PlayableCard __instance, ref int damage)
            {
                bool flag = __instance.HasAbility(ability);
                if (flag)
                {
                    damage = 1;
                }
            }
        }
    }
}
