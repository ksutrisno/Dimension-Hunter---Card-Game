using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : Pile
{

    protected override void Awake()
    {
        base.Awake();

        PreAdd += (MyObject obj) =>
        {
            obj.GetComponent<Card>().Back.transform.GetChild(0).gameObject.SetActive(false);
            obj.GetComponent<Card>().Back.transform.GetChild(1).gameObject.SetActive(true);
        };

        PostAdd += (MyObject obj) =>
        {
            Shuffle();
        };
    }
}
