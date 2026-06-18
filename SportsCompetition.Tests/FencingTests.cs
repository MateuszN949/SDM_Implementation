using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Tests;

public class FencingTests
{
    [Fact]
    public void TouchEvent_Should_Increase_Player_Points()
    {
        Player a = new("A");
        Player b = new("B");

        FencingMatch match = new();
        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.AddEvent(new TouchEvent(a));
        match.AddEvent(new TouchEvent(a));

        Assert.Equal(2, match.Scores[a].Points);
        Assert.Equal(0, match.Scores[b].Points);
    }

    [Fact]
    public void FencingMatch_End_Selects_MaxScore_Winner()
    {
        Player a = new("A");
        Player b = new("B");

        FencingMatch match = new();
        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.AddEvent(new TouchEvent(a));
        match.AddEvent(new TouchEvent(a));
        match.AddEvent(new TouchEvent(b));

        match.End();

        Assert.Equal(a, match.Winner);
    }

    [Fact]
    public void FencingTiebreakerEvent_Chooses_Priority_Winner_When_Tie()
    {
        Player a = new("A");
        Player b = new("B");

        FencingMatch match = new();
        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.AddEvent(new TouchEvent(a));
        match.AddEvent(new TouchEvent(b));
        match.TimeOver();

        match.AddEvent(new FencingTiebreakerEvent(a));
        match.End();

        Assert.Equal(a, match.Winner);
    }

    [Fact]
    public void FencingMatch_Is_Draw_When_Scores_Equal_And_No_Priority_Winner()
    {
        Player a = new("A");
        Player b = new("B");

        FencingMatch match = new();
        match.AddParticipant(a);
        match.AddParticipant(b);

        match.Start();
        match.AddEvent(new TouchEvent(a));
        match.AddEvent(new TouchEvent(b));

        Assert.True(match.IsDraw());
    }
}
