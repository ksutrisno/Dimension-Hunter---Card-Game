using UnityEngine;



[System.Serializable]
public class DealDamageComponent : CardComponent
{
    [SerializeField]
    private float hitModifier = 0;


    public override string GetDescription()
    {
        description = description.Replace("[amount]", Amount.ToString());
        description = description.Replace("[execute_amount]", ExecuteAmount.ToString());

        return description;
    }

    public override bool Execute()
    {
        base.Execute();


        Card.Owner.PlayAttackAnim(0.4f);

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


}
