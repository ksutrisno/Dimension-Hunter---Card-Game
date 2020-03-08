using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayZone : Zone {

    public static CardPlayZone instance;

    protected override void Awake()
    {
        base.Awake();

        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        

    }
}
