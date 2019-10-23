using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Corrosion", menuName = "ScriptableObjects/Buff/Corrosion", order = 2)]
[System.Serializable]
public class Corrosion : Buff
{
    public override void AddEffect(Character owner)
    {


      
    }

    public override void Execute(Character owner)
    {
        base.Execute(owner);
    }


    public override void RemoveEffect(Character owner)
    {


    }



}