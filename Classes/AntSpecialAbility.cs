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
    // Token: 0x02000396 RID: 918
    public class AntMinus : VariableStatBehaviour
    {
        // Token: 0x170002EB RID: 747
        // (get) Token: 0x060015EC RID: 5612 RVA: 0x0000CA74 File Offset: 0x0000AC74
        public override SpecialStatIcon IconType
        {
            get
            {
                return SpecialStatIcon.Ants;
            }
        }

        // Token: 0x060015ED RID: 5613 RVA: 0x0004A8D0 File Offset: 0x00048AD0
        public override int[] GetStatValues()
        {
            List<CardSlot> list = PlayableCard.Slot.IsPlayerSlot ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
            int num = 0;
            foreach (CardSlot cardSlot in list)
            {
                if (cardSlot.Card != null && cardSlot.Card.Info.HasTrait(Trait.Ant))
                {
                    num++;
                }
            }
            num--;
            int[] array = new int[2];
            array[0] = num;
            return array;
        }
    }
}