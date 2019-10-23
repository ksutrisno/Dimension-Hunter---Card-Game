using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Zone {

    [SerializeField]
    private Card m_choosenCard;
    [SerializeField]
    private List<Card> drawQueue = new List<Card>();

    [SerializeField]
    protected bool m_isInteractable = false;

    [SerializeField]
    private float m_drawSpeed = 1;

    protected override void Awake()
    {
        base.Awake();

        PostAdd += (MyObject obj) =>
        {
            Arrange();

            
            obj.transform.GetComponent<BoxCollider2D>().enabled = m_isInteractable;
        };
    }

    public void Discard()
    {
        StartCoroutine(DiscardCoroutine()); 
    }

    private IEnumerator DiscardCoroutine()
    {
        foreach(Card card in Cards)
        {
            yield return new WaitForSeconds(0.1f);
            card.Discard();
        }
    }


    public Card ChoosenCard
    {
        get
        {
            return m_choosenCard;
        }

        set
        {
            m_choosenCard = value;
        }
    }

    public void Draw(int amount)
    {
        StartCoroutine(DrawCoroutine(amount));

    }


    public IEnumerator DrawCoroutine(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(0.15f/ m_drawSpeed);

            Card card = null;


            if (m_owner.DrawPile.Content.childCount > 0)
            {
                card = m_owner.DrawPile.Content.GetChild(m_owner.DrawPile.Content.childCount -1).GetComponent<Card>();
                card.gameObject.SetActive(true);
                card.Ui.SetActive(m_isVisible);
            }
            else if(m_owner.DiscardPile.Content.childCount > 0)
            {
                yield return m_owner.DrawPile.PopulateFromDiscardPile();
                yield return new WaitUntil(() => m_owner.DrawPile.Content.childCount > 0);
                card = m_owner.DrawPile.Content.GetChild(m_owner.DrawPile.Content.childCount - 1).GetComponent<Card>();
                card.gameObject.SetActive(true);
                card.Ui.SetActive(m_isVisible);
            }

            if (Cards.Count + drawQueue.Count < m_capacity && card != null)
            {
                drawQueue.Add(card);
                card.gameObject.SetActive(true);
                card.Ui.SetActive(m_isVisible);
                Add(card, 0.35f);

                drawQueue.Remove(card);
            }
            else if (card != null)
            {
                m_owner.DiscardPile.Add(card, 0.35f);
            }
        }

    }


}
