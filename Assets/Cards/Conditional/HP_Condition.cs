using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HP_Condition : Condition
{


    public enum Type
    {
        kSmaller,
        kEqual,
        kBigger,
        kBiggerEqual,
        kSmallerEqual
    }


    [SerializeField]
    private Type type;



    [SerializeField]
    private int percentage;

   


    public override bool ConditionFulfilled(List<Character> targetList, Character owner)
    {
        foreach (var target in GetTarget(targetList, owner))
        {
            switch (type)
            {
                case Type.kBigger:
                    if ((target.CurrentHealth / target.Health) * 100 > percentage)
                    {
                        return true;
                    }
                    break;
                case Type.kEqual:
                    if ((target.CurrentHealth / target.Health) * 100 == percentage)
                    {
                        return true;
                    }
                    break;
                case Type.kSmaller:
                    if ((target.CurrentHealth / target.Health) * 100 < percentage)
                    {
                        return true;
                    }
                    break;
                case Type.kBiggerEqual:
                    if ((target.CurrentHealth / target.Health) * 100 >= percentage)
                    {
                        return true;
                    }
                    break;
                case Type.kSmallerEqual:
                    if ((target.CurrentHealth / target.Health) * 100 <= percentage)
                    {
                        return true;
                    }
                    break;

            }
      
        }

        return false;


    }
}
