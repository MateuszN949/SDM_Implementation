namespace SportsCompetition.Core.Parties;

public abstract class Party : IParty
{
    public int ID { get; }

    protected Party()
    {
        ID = IDGenerator.Instance.GetNextId();
    }
}