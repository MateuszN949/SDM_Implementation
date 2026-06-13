namespace SportsCompetition.Core.Parties;

public class Player(string fullName) : Party
{
    public string FullName { get; } = fullName;
}