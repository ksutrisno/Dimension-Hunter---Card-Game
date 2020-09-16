using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Selector : CardComponent
{
     [SerializeField]
     List<CardComponent> m_cardComponents;

    public override string GetDescription()
    {
        return Select().GetDescription();
    }

    public CardComponent Select()
    {
        foreach (var cardComponent in m_cardComponents)
        {
            if (!cardComponent.ConditionFulfilled())
            {
                return cardComponent;
            }
        }
        return null;
    }


}
