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
    public class Launcher : AbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return Card != null && Card.OpponentCard != playerTurnEnd && Card.OnBoard;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            List<CardSlot> cards = playerTurnEnd ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
            List<CardSlot> openspots = new List<CardSlot>();
            foreach (CardSlot slot in cards)
            {
                bool flag = slot.Card == null;
                if (flag)
                {
                    openspots.Add(slot);
                }
            }
            bool flag2 = openspots.Count != 0;
            if (flag2)
            {
                System.Random random = new System.Random();
                yield return new WaitForSeconds(0.3f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
                yield return new WaitForSeconds(0.3f);
                bool flag3 = Card.Info.iceCubeParams.creatureWithin != null;
                if (flag3)
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(Card.Info.iceCubeParams.creatureWithin, openspots[random.Next(openspots.Count)], 0.1f, true);
                }
                else
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(ScriptableObjectLoader<CardInfo>.AllData.Find((info) => info.name == "Squirrel"), openspots[random.Next(openspots.Count)], 0.1f, true);
                }
                yield return new WaitForSeconds(0.3f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
            }
        }
        public static Ability ability;
    }
}
