﻿using APIPlugin;
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
    public class Spawner : Strafe
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }
        public override IEnumerator PostSuccessfulMoveSequence(CardSlot oldSlot)
        {
            if (oldSlot.Card == null)
            {
                if (Card.Info.iceCubeParams.creatureWithin != null)
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(Card.Info.iceCubeParams.creatureWithin, oldSlot, 0.1f, true);
                }
                else
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(ScriptableObjectLoader<CardInfo>.AllData.Find((info) => info.name == "Squirrel"), oldSlot, 0.1f, true);
                }
            }
            yield break;
        }
        public static Ability ability;
    }
}
