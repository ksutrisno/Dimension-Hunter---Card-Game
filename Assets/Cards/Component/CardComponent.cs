using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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

    private Card card;

    [SerializeField]
    private int [] m_executeAmount = { 1, 1 };

    [SerializeField]
    private List<Condition> m_conditions;


    [SerializeField]
    protected GameObject m_animationEffect;

    public TargetEnum TargetType { get => m_targetType; set => m_targetType = value; }
    public List<Character> Target { get => m_target; set => m_target = value; }
    protected int Amount
    { get => m_amount[Card.Level]; }

    public virtual int ExecuteAmount
    {
        get
        {
            if(m_executeAmount.Length > 0)
            {
                return m_executeAmount[Card.Level];
            }
            else
            {
                return 1;
            }
        
        }
    }

    public Card Card { get => card; set => card = value; }

    public virtual void UpdateComponent()
    {

    }

    public bool ConditionFulfilled()
    {
        foreach (var condition in m_conditions)
        {
            if(!condition.ConditionFulfilled(m_target, Card.Owner))
            {
                return false;
            }
        }
        return true;
    }

    public virtual bool Execute()
    {

        if(Target.Count == 0)
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


    public abstract string GetDescription();


    public void AddCondition(ConditionEnum condition)
    {
        Condition asset = null;
        switch (condition)
        {
           
            case ConditionEnum.kBuff:
                asset = ScriptableObject.CreateInstance<Buff_Condition>();
                m_conditions.Add(asset);
                break;
            case ConditionEnum.kHP:
                asset = ScriptableObject.CreateInstance<HP_Condition>();
                m_conditions.Add(asset);
                break;
            default:
                break;


        }

        if(asset != null)
        {
            string path = "Assets/Cards/Conditional/";
           
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + asset + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);


            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
  
            
        }
      
    }

 

}
