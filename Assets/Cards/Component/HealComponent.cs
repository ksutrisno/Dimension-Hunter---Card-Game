using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealComponent : CardComponent {


    public override bool Execute()
    {
        base.Execute();

        foreach (var target in Target)
        {
            target.Heal(Amount);
        }
        return true;
    }

    public override string GetDescription()
    {
        return "Heal " + Amount.ToString() + " health";
    }
}
