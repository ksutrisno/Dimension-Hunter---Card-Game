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

        description = description.Replace("[amount]", Amount.ToString());
        description = description.Replace("[execute_amount]", ExecuteAmount.ToString());

        return description;
    }
}
