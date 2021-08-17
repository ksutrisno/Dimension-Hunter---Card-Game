using UnityEngine;

public class BuffComponent : CardComponent
{
    [SerializeField]
    Buff buff;

    public override bool Execute()
    {
        base.Execute();


        Buff newBuff = Instantiate(buff);

        newBuff.Duration = Amount;
        foreach (var target in Target)
        {
            target.AddBuff(newBuff);
        }
        return true;

    }

    public override string GetDescription()
    {
        return "";
    }
}
