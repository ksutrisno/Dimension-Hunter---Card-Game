using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Evade", menuName = "ScriptableObjects/Buff/Evade", order = 1)]
[System.Serializable]
public class Evade : Buff
{

    public override void AddEffect(Character owner)
    {


        owner.DodgeChance +=  50;
    }

    public override void Execute(Character owner)
    {
        base.Execute(owner);
    }


    public override void RemoveEffect(Character owner)
    {


        owner.DodgeChance -= 50;
    }

}