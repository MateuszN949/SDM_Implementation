using SportsCompetition.Core.Scores;

namespace SportsCompetition.Tests;

public class ScoresTests
{
    [Fact]
    public void Goal_Should_Increase_Goal_Count()
    {
        FootballScore score = new();

        score.AddGoal();

        Assert.Equal(1, score.Goals);
    }

    [Fact]
    public void Aggregate_Should_Combine_Scores()
    {
        FootballScore first = new();
        first.AddGoal();

        FootballScore second = new();
        second.AddGoal();
        second.AddGoal();

        FootballScore total = first.Aggregate(second);

        Assert.Equal(3, total.Goals);
    }
}