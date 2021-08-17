using System.Collections;
using UnityEngine;

public class Player : Character
{


    protected override void Awake()
    {
        base.Awake();

        m_currentEnergy = m_energy;
    }


    public override IEnumerator Initialize()
    {
        return base.Initialize();
    }

    public override IEnumerator StartTurnCoroutine()
    {
        yield return base.StartTurnCoroutine();

        m_energyText.text = m_currentEnergy + "/" + m_energy;

    }





    public override IEnumerator EndTurnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);


        m_hand.Discard();
    }



    public override IEnumerator Turn()
    {

        yield return m_hand.DrawCoroutine(m_drawPower);


        yield return null;
    }


    void Update()
    {
        foreach (Card temp in m_hand.Cards)
        {
            if (m_currentEnergy < temp.EnergyCost)
            {
                temp.EnergyCostText.color = Color.red;
            }
            else
            {
                temp.EnergyCostText.color = Color.white;
            }
        }
    }
}
