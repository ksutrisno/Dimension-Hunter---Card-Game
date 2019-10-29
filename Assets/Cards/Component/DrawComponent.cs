using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrawComponent : CardComponent
{


    public override bool Execute()
    {
        base.Execute();

        foreach (var target in Target)
        {
            target.Hand.Draw(Amount);
        }
        return true;
    }


    public override string GetDescription()
    {
        return "Draw " + Amount.ToString() + " card";
    }

}
