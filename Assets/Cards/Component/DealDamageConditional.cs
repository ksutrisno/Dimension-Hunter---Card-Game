using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageConditional : CardComponent
{
    [SerializeField]
    private float hitModifier = 0;

    [SerializeField]
    private int[] conditionalAmount;
    [SerializeField]
    private int[] conditionalExecuteAmount;

    int amount;

    public override int ExecuteAmount
    {
        get
        {
            return conditionalExecuteAmount[m_card.Level];
        }
    }

    public override void Init(Card card)
    {
        base.Init(card);

        amount = Amount;

    }


    public override void UpdateComponent()
    {
        if (ConditionFulfilled())
        {
            amount = conditionalAmount[m_card.Level];

        }
        else
        {
            amount = Amount;
        }
    }

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

            target.TakeDamage(amount, hitModifier);
        }
        return true;
    }

    public override string GetDescription()
    {
        string desc;
        if (TargetType == TargetEnum.kSingle)
        {
            desc = "Deal " + amount.ToString() + " Damage";
        }
        else if (TargetType == TargetEnum.kAll)
        {
            desc = "Deal " + amount.ToString() + " Damage";
        }
        else
        {
            desc = "Deal " + amount.ToString() + " Damage";
        }

        if (hitModifier < 0)
        {
            desc += ". \n" + hitModifier.ToString() + " hit chance";
        }

        return desc;

    }
}
