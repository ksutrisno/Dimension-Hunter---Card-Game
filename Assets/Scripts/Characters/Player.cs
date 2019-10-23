using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {


    [SerializeField]
    private Text m_energyText;

    [SerializeField]
    private Transform m_tempZone;

    public override int CurrentEnergy
    {
        get
        {
            return base.CurrentEnergy;
        }

        set
        {
            base.CurrentEnergy = value;

            m_energyText.text = m_currentEnergy + "/" + m_energy;
        }
    }

    public Transform TempZone { get => m_tempZone; set => m_tempZone = value; }

    public override IEnumerator StartTurnCoroutine()
    {
        yield return base.StartTurnCoroutine();
     
    }

    public override IEnumerator EndTurnCoroutine()
    {
        yield return base.EndTurnCoroutine();
    }



    public override IEnumerator Turn()
    {
        yield return null;
    }

    public override IEnumerator PlayCardEffect(Card card)
    {
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
