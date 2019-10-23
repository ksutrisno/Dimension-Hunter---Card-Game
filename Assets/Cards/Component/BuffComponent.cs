using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffComponent : CardComponent
{


    [SerializeField]
    Buff buff;



    public override void Execute()
    {
        Buff newBuff = Instantiate(buff);

        newBuff.Duration = Amount;
        foreach (var target in Target)
        {
            target.AddBuff(newBuff);
        }
 
    }

    public override string GetDescription()
    {
        if(TargetType == TargetEnum.kSelf)
        {
            return "Gain " + buff.name + " " + Amount.ToString();
        }
        else if (TargetType == TargetEnum.kSingle)
        {
            return  "Inflicts " + buff.name + Amount.ToString() + " " ;
        }
        else
        {
            return "Inflicts " + buff.name + Amount.ToString() + " to All";
        }
      
    }
}
