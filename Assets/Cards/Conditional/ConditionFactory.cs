using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    kBuff,
    kHP
}

[System.Serializable]
public class ConditionFactory 
{

    [SerializeField]
    private ConditionType m_conditionType;

 
    public ConditionType ConditionType
    {
        get
        {
            return m_conditionType;
        }

        set
        {
            m_conditionType = value;

            SetCondition(value);

            
        }
    }

    [SerializeField]
    private Condition m_condition;
    
 
    public Condition SetCondition(ConditionType conditionType)
    {
        switch(conditionType)
        {
            case ConditionType.kBuff:
                m_condition = new Buff_Condition();
                return m_condition;

            case ConditionType.kHP:
                m_condition = new HP_Condition();
                return m_condition;
            default:
                return new Condition();
        }
    }
        
}

    