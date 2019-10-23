using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[System.Serializable]
public class DealDamageComponent : CardComponent
{
 

    public override void Execute()
    {
        m_card.Owner.PlayAttackAnim(0.4f);

        foreach (var target in Target)
        {
            if (m_animationEffect)
            {
                GameObject obj = Instantiate(m_animationEffect, target.transform.position, target.transform.rotation);

                Destroy(obj, 0.5f);
            }

            target.TakeDamage(Amount);
        }
  
              
    }

    public override string GetDescription()
    {
        string desc;
        if (TargetType == TargetEnum.kSingle)
        {
            desc = "Deal " + Amount.ToString() + " Damage";
        }
        else if(TargetType == TargetEnum.kAll)
        {
            desc = "Deal " + Amount.ToString() + " Damage";
        }
        else
        {
            desc = "Deal " + Amount.ToString() + " Damage";
        }

        if(ExecuteAmount > 1)
        {
            desc += ". \n" + ExecuteAmount.ToString() + " times";
        }

        return desc;

    }
}
