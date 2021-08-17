using UnityEngine;

[CreateAssetMenu(fileName = "Corrosion", menuName = "ScriptableObjects/Buff/Wound", order = 2)]
[System.Serializable]
public class Wound : Buff
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