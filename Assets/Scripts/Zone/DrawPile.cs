using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPile : Pile {

    private Text drawText;

    protected override void Awake()
    {
        base.Awake();

        PreAdd += (MyObject obj) =>
        {
            obj.GetComponent<Card>().Back.transform.GetChild(0).gameObject.SetActive(true);
            obj.GetComponent<Card>().Back.transform.GetChild(1).gameObject.SetActive(false);
        };

        PostAdd += (MyObject obj) =>
        {
            Shuffle();
        };
    }

    public bool PopulateFromDeck(Deck deck)
    {
        foreach (Card card in deck.Cards)
        {
            Card temp = Instantiate(card);

            temp.Owner = deck.Owner;

            temp.gameObject.SetActive(false);

            Add(temp, 0);
            
        }

        return true;
    }

    public IEnumerator PopulateFromDiscardPile()
    {
        foreach (Card card in m_owner.DiscardPile.Cards)
        {
            yield return new WaitForSeconds(0.075f);
            Add(card, 0.35f);

        }
    }


}
