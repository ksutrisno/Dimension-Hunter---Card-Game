using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public abstract class CardComponent : MonoBehaviour
{
 
    public enum TargetEnum
    {
        kSelf,
        kSingle,
        kAll
    }


    [SerializeField]
    private int [] m_amount = new int[2];

    [SerializeField]
    private TargetEnum m_targetType;

    [SerializeField]
    protected List<Character> m_target = new List<Character>();

    protected Card m_card;

    [SerializeField]
    protected int [] m_executeAmount = { 1, 1 };



    [SerializeField]
    protected GameObject m_animationEffect;

    public TargetEnum TargetType { get => m_targetType; set => m_targetType = value; }
    public List<Character> Target { get => m_target; set => m_target = value; }
    protected int Amount
    { get => m_amount[m_card.Level]; }
    public int ExecuteAmount
    {
        get
        {
            if(m_executeAmount.Length > 0)
            {
                return m_executeAmount[m_card.Level];
            }
            else
            {
                return 1;
            }
        
        }
    }

    public abstract void Execute();

    public virtual void Init(Card card)
    {
        m_card = card;

        if (m_targetType == TargetEnum.kSelf)
        {

            m_target.Add(card.Owner);
        }
        else if (m_targetType == TargetEnum.kAll)
        {
            if(card.Owner.type == Character.Type.kEnemy)
            {
                m_target.Add(CombatManager.instance.Player);
            }
           else
            {
                foreach (Character enemy in CombatManager.instance.Enemies)
                {
                    m_target.Add(enemy);
                }
            } 
        }
        else
        {
            if (card.Owner.type == Character.Type.kEnemy)
            {
                m_target.Add(CombatManager.instance.Player);
            }
        }
    }


    public abstract string GetDescription();
  
}
