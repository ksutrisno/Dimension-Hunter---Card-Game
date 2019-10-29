using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition :ScriptableObject
{

    public enum ConditionTarget
    {
        kSelf,
        kTarget
    }

    [SerializeField]
    protected ConditionTarget conditionTarget;

    public abstract bool ConditionFulfilled(List<Character> targetList, Character owner);


    protected List<Character> GetTarget(List<Character> targetList, Character owner)
    {
        List<Character> temp = new List<Character>();
        if (conditionTarget == ConditionTarget.kSelf)
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

