using System.Collections.Generic;
using UnityEngine;

public abstract class CardComponent : MonoBehaviour
{

    public enum TargetEnum
    {
        kSelf,
        kSingle,
        kAll
    }


    [SerializeField]
    private int m_amount;

    [SerializeField]
    private TargetEnum m_targetType;

    protected List<Character> m_target = new List<Character>();

    private Card card;

    [SerializeField]
    private int m_executeAmount = 1;


    [SerializeField]
    protected GameObject m_animationEffect;

    public TargetEnum TargetType { get => m_targetType; set => m_targetType = value; }
    public List<Character> Target { get => m_target; set => m_target = value; }
    protected int Amount
    { get => m_amount; }


    [SerializeField]
    protected string description;

    public virtual int ExecuteAmount
    {
        get
        {

            return m_executeAmount;
        }
    }

    public abstract string GetDescription();

    public Card Card { get => card; set => card = value; }

    [SerializeField]
    private ComponentUpgrade m_onUpgrade;

    public virtual void UpdateComponent()
    {

    }


    public virtual bool Execute()
    {

        if (Target.Count == 0)
        {
            Target.Clear();
            if (m_targetType == TargetEnum.kSelf)
            {
                m_target.Add(Card.Owner);
            }
            else if (m_targetType == TargetEnum.kAll)
            {
                Target.Clear();
                if (Card.Owner.type == Character.Type.kEnemy)
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
                Target.Clear();
                if (Card.Owner.type == Character.Type.kEnemy)
                {
                    m_target.Add(CombatManager.instance.Player);
                }
            }

        }
        return true;

    }

    public virtual void Init(Card card)
    {
        Card = card;



    }

    protected virtual void PostExecute()
    {

    }


    public void Upgrade()
    {
        m_amount += m_onUpgrade.Amount;
        m_executeAmount += m_onUpgrade.Execute_amount;

    }

    protected virtual void OnUpgrade()
    {

    }




}
