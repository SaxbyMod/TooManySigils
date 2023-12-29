using APIPlugin;
using BepInEx;
using DiskCardGame;
using InscryptionAPI.Guid;
using System;
using System.Collections;
using UnityEngine;

namespace TooManySigils.Classes
{
    // Token: 0x020003A4 RID: 932
    public class Child13Trait : SpecialCardBehaviour
    {
        // Token: 0x06001625 RID: 5669 RVA: 0x0004AF04 File Offset: 0x00049104
        private void Start()
        {
            if (Card != null)
                mod = new CardModificationInfo();
            mod.nonCopyable = true;
            mod.abilities.Add(Ability.Flying);
            mod.attackAdjustment = -2;
        }

        // Token: 0x06001626 RID: 5670 RVA: 0x0000CA74 File Offset: 0x0000AC74
        public override bool RespondsToSacrifice()
        {
            return true;
        }

        // Token: 0x06001627 RID: 5671 RVA: 0x0004AF79 File Offset: 0x00049179
        public override IEnumerator OnSacrifice()
        {
            sacrificeCount++;
            if (sacrificeCount > MAX_SACRIFICES)
            {
                yield return PlayableCard.Die(true, null, true);
            }
            else if (sacrificeCount < MAX_SACRIFICES)
            {
                attackMode = !attackMode;
                if (attackMode)
                {
                    PlayableCard.SwitchToAlternatePortrait();
                    PlayableCard.AddTemporaryMod(mod);
                }
                else
                {
                    PlayableCard.SwitchToDefaultPortrait();
                    PlayableCard.RemoveTemporaryMod(mod, true);
                }
            }
            yield break;
        }

        // Token: 0x04000F7C RID: 3964
        private bool attackMode;

        // Token: 0x04000F7D RID: 3965
        private CardModificationInfo mod;

        // Token: 0x04000F7E RID: 3966
        private int sacrificeCount;

        // Token: 0x04000F7F RID: 3967
        private int MAX_SACRIFICES = 9999;
    }
}
