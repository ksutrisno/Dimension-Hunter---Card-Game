using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Zone m_enemyPlayZone;

    [SerializeField]
    private GameObject selectBorder;

    public override IEnumerator StartTurnCoroutine()
    {
        yield return base.StartTurnCoroutine();

        yield return new WaitForSeconds(0.2f);
    }

    public override IEnumerator EndTurnCoroutine()
    {
        yield return base.EndTurnCoroutine();
    }

    public override IEnumerator Turn()
    {

        foreach(Card card in m_hand.Cards)
        {
            if(m_currentEnergy >= card.EnergyCost)
            {
                yield return new WaitForSeconds(1.0f);
                card.gameObject.SetActive(true);
                card.transform.localScale *= 1.4f;
                yield return card.Play();
            }       
        }
    }

 
    public override IEnumerator PlayCardEffect(Card card)
    {
        yield return CardPlayZone.instance.StartAddCoroutine(card, 0.45f);

        yield return new WaitForSeconds(0.6f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TargetArrow arrow = collision.GetComponent<TargetArrow>();

        if(arrow)
        {
            arrow.Target = this;

            selectBorder.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        TargetArrow arrow = collision.GetComponent<TargetArrow>();

        if (arrow)
        {
            arrow.Target = null;

            selectBorder.SetActive(false);
        }
    }

    protected override void Die()
    {
        CombatManager.instance.Enemies.Remove(this);
        Destroy(gameObject, 0.50f);
    }
}
