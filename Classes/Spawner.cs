using APIPlugin;
using DiskCardGame;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;

namespace ExampleMod.Classes
{
    public class Spawner : Strafe
    {
        public override Ability Ability
        {
            get
            {
                return Spawner.ability;
            }
        }
        public override IEnumerator PostSuccessfulMoveSequence(CardSlot oldSlot)
        {
            bool flag = oldSlot.Card == null;
            if (flag)
            {
                bool flag2 = base.Card.Info.iceCubeParams.creatureWithin != null;
                if (flag2)
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info.iceCubeParams.creatureWithin, oldSlot, 0.1f, true);
                }
                else
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(ScriptableObjectLoader<CardInfo>.AllData.Find((CardInfo info) => info.name == "Squirrel"), oldSlot, 0.1f, true);
                }
            }
            yield break;
        }
        public static Ability ability;
    }
}
