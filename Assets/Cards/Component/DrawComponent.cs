using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrawComponent : CardComponent
{


    public override void Execute()
    {
        foreach (var target in Target)
        {
            target.Hand.Draw(Amount);
        }
  
    }


    public override string GetDescription()
    {
        return "Draw " + Amount.ToString() + " card";
    }

}
