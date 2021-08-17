public class HealComponent : CardComponent
{


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
        return "";
    }
}
