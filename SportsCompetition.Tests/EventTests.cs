using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Events;

namespace SportsCompetition.Tests;

public class EventTests
{
    [Fact]
    public void Goal_Should_Increase_Team_Score()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();

        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();

        match.AddEvent(new GoalEvent(a));

        Assert.Equal(1, match.Scores[a].Goals);
        Assert.Equal(0, match.Scores[b].Goals);
    }

    [Fact]
    public void Goal_Cannot_Be_Added_After_Time_Over()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();

        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.TimeOver();

        Assert.Throws<InvalidOperationException>(
            () => match.AddEvent(new GoalEvent(a)));
    }
}