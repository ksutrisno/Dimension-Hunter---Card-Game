using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Buff_Condition : Condition
{
    [SerializeField]
    Buff m_buff;




    public override bool ConditionFulfilled(List<Character> targetList, Character owner)
    {
        foreach (var target in GetTarget(targetList, owner))
        {
            foreach (var buff in target.BuffList)
            {
                if(buff.name == m_buff.name)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
