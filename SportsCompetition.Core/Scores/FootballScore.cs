namespace SportsCompetition.Core.Scores;

public class FootballScore : IScore<FootballScore>
{
    private int goals = 0;
    private int redCards = 0;
    private int yelCards = 0;

    public int Goals => goals;
    public int RedCards => redCards;
    public int YellowCards => yelCards;

    public void AddGoal() { goals++; }
    public void AddRedCard() { redCards++; }
    public void AddYellowCard() { yelCards++; }

    public FootballScore Aggregate(FootballScore score)
    {
        return new FootballScore {
            goals = goals + score.goals,
            redCards = redCards + score.redCards,
            yelCards = yelCards + score.yelCards
        };
    }

    public int CompareTo(FootballScore? other)
    {
        if (other is null) return 1;

        foreach ((int, int) i in new List<(int, int)> {
            (goals, other.goals),
            (-redCards, -other.redCards),
            (-yelCards, -other.yelCards)})
        {
            if (i.Item1 > i.Item2) return 1;
            if (i.Item1 < i.Item2) return -1;
        }

        return 0;
    }
}