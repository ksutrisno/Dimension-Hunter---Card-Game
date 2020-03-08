using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Condition:ScriptableObject
{
    public enum ConditionTarget
    {
        kSelf,
        kTarget
    }



   
    [SerializeField]
    protected ConditionTarget m_conditionTarget;

    public virtual bool ConditionFulfilled(List<Character> targetList, Character owner)
    {

        return true;
    }


    protected List<Character> GetTarget(List<Character> targetList, Character owner)
    {
        List<Character> temp = new List<Character>();
        if (m_conditionTarget == ConditionTarget.kSelf)
        {
            temp.Add(owner);
        }
        else
        {
            temp = targetList;
        }

        return temp;

    }

}

