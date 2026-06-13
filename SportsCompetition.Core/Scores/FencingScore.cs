namespace SportsCompetition.Core.Scores;

public class FencingScore : IScore<FencingScore>
{
    private int points = 0;
    public int Points => points;
    public void AddPoint() { points++; }

    public FencingScore Aggregate(FencingScore score)
    {
        return new FencingScore { points = points + score.points };
    }

    public int CompareTo(FencingScore? other)
    {
        if (other is null) return 1;

        if (points > other.points) return 1;
        if (points < other.points) return -1;

        return 0;
    }
}