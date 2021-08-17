using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    [SerializeField]
    private Character m_owner;

    private int m_capacity = 30;

    [SerializeField]
    private List<Card> m_cards = new List<Card>();



    private void Awake()
    {
        m_owner = GetComponentInParent<Character>();


    }


    public List<Card> Cards
    {
        get
        {
            return m_cards;
        }

        set
        {
            m_cards = value;
        }
    }

    public Character Owner
    {
        get
        {
            return m_owner;
        }

        set
        {
            m_owner = value;
        }
    }

    public void RemoveCard()
    {

    }


    public void AddCard(Card card)
    {
        if (m_cards.Count == m_capacity)
        {

            return;
        }

        m_cards.Add(card);
    }
}
