using System.Collections;
using UnityEngine;

public class Enemy : Character
{

    [SerializeField]
    private GameObject selectBorder;

    [SerializeField]
    private Zone m_enemyPlayZone;

    public override IEnumerator Initialize()
    {
        return base.Initialize();
    }
    public override IEnumerator StartTurnCoroutine()
    {
        yield return base.StartTurnCoroutine();

        yield return new WaitForSeconds(0.2f);
    }

    public override IEnumerator EndTurnCoroutine()
    {
        yield return null;
    }

    public override IEnumerator Turn()
    {

        foreach (Card card in m_hand.Cards)
        {
            if (m_currentEnergy >= card.EnergyCost)
            {
                yield return new WaitForSeconds(0.50f);
                card.gameObject.SetActive(true);
                card.transform.localScale *= 1.4f;
                yield return card.Play();
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        TargetArrow arrow = collision.GetComponent<TargetArrow>();

        if (arrow)
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

    public override IEnumerator PlayCardEffect(Card card)
    {
        yield return CardPlayZone.instance.StartAddCoroutine(card, 0.35f);

        yield return new WaitForSeconds(0.35f);
    }
}
