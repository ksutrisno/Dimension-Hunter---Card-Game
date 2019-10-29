using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mark", menuName = "ScriptableObjects/Buff/Mark", order = 3)]
[System.Serializable]
public class Mark : Buff
{

    public override void AddEffect(Character owner)
    {
        owner.DodgeChance -=  50;
    }

    public override void Execute(Character owner)
    {
        base.Execute(owner);
    }


    public override void RemoveEffect(Character owner)
    {
        owner.DodgeChance += 50;
    }

}