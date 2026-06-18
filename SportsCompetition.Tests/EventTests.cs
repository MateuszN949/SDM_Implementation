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

    [Fact]
    public void RedCardEvent_Adds_Red_Card_After_Time_Over()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();

        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.TimeOver();

        match.AddEvent(new RedCardEvent(a));

        Assert.Equal(1, match.Scores[a].RedCards);
    }

    [Fact]
    public void RedCardEvent_Adds_Red_Card_During_Match()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();

        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();

        match.AddEvent(new RedCardEvent(a));

        Assert.Equal(1, match.Scores[a].RedCards);
    }

    [Fact]
    public void Canceling_Winning_Event_Resets_Winner()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();

        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.TimeOver();

        CoinTossEvent ev = new(a);

        match.AddEvent(ev);
        match.CancelEvent(ev);

        Assert.Null(match.Winner);
    }

    [Fact]
    public void Canceling_Goal_Event_Removes_One_Goal()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();

        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();

        GoalEvent ev1 = new(a);
        match.AddEvent(ev1);

        GoalEvent ev2 = new(a);
        match.AddEvent(ev2);

        GoalEvent ev3 = new(b);
        match.AddEvent(ev3);
        
        int goalsBefore = match.Scores[a].Goals;

        match.CancelEvent(ev1);

        int goalsAfter = match.Scores[a].Goals;

        Assert.Equal(goalsBefore - 1, goalsAfter);
    }

    [Fact]
    public void GoldenGoalEvent_Sets_Winner()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();
        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.TimeOver();
        match.AddEvent(new GoldenGoalEvent(a));
        match.End();

        Assert.Equal(a, match.Winner);
    }

    [Fact]
    public void PenaltyShootoutEvent_Sets_Winner()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        FootballMatch match = new();
        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.TimeOver();
        match.AddEvent(new PenaltyShootoutEvent(a, 5, b, 3));
        match.End();

        Assert.Equal(a, match.Winner);
    }
}