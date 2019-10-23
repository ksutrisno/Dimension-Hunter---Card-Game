using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealComponent : CardComponent {

    public override void Execute()
    {
        foreach (var target in Target)
        {
            target.Heal(Amount);
        }
    }

    public override string GetDescription()
    {
        return "Heal " + Amount.ToString() + " health";
    }
}
