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
    public class Host : AbilityBehaviour
    {
        public override Ability Ability
        {
            get
            {
                return ability;
            }
        }
        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            return true;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return new WaitForSeconds(0.2f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
            yield return new WaitForSeconds(0.2f);
            if (Card.Info.iceCubeParams.creatureWithin != null)
            {
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(Card.Info.iceCubeParams.creatureWithin, new List<CardModificationInfo>(), new Vector3(0f, 0f, 0f), 0f, null);
            }
            else
            {
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(ScriptableObjectLoader<CardInfo>.AllData.Find((info) => info.name == "RingWorm"), new List<CardModificationInfo>(), new Vector3(0f, 0f, 0f), 0f, null);
            }
            yield break;
        }
        public static Ability ability;
    }
}
