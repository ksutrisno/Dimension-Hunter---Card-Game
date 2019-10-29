using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[System.Serializable]
public class DealDamageComponent : CardComponent
{
    [SerializeField]
    private float hitModifier = 0;
    public override bool Execute()
    {
        base.Execute();


        m_card.Owner.PlayAttackAnim(0.4f);

        foreach (var target in Target)
        {
            if (m_animationEffect)
            {
                GameObject obj = Instantiate(m_animationEffect, target.transform.position, target.transform.rotation);

                Destroy(obj, 0.5f);
            }

            target.TakeDamage(Amount, hitModifier);
        }

        return true;
              
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


        if(hitModifier < 0)
        {
            desc += ". \n" + hitModifier.ToString() + " hit chance";
        }

        return desc;

    }
}
