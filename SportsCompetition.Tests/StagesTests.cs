using System.Linq;
using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.Stages;
using SportsCompetition.Core.Strategies.Grouping;
using SportsCompetition.Core.Strategies.Pairing;
using SportsCompetition.Core.Factory;

namespace SportsCompetition.Tests;

public class StagesTests
{
    [Fact]
    public void RegisterStage_GetRanking_Throws_When_Empty()
    {
        RegisterStage<Team> stage = new();

        Assert.Throws<InvalidOperationException>(() => stage.GetRanking());
    }

    [Fact]
    public void RegisterStage_Adds_Participants()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        RegisterStage<Team> stage = new();
        stage.AddParticipant(a);
        stage.AddParticipant(b);

        Assert.Equal(2, stage.Participants.Count);
        Assert.Contains(a, stage.Participants);
        Assert.Contains(b, stage.Participants);
    }

    [Fact]
    public void RegisterStage_Adds_Participants_In_Bulk()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        RegisterStage<Team> stage = new();
        stage.AddParticipants(new[] { a, b });

        Assert.Equal(2, stage.Participants.Count);
        Assert.Contains(a, stage.Participants);
        Assert.Contains(b, stage.Participants);
    }

    [Fact]
    public void PodiumStage_Returns_Top_Three_Only()
    {
        Team a = new("A", []);
        Team b = new("B", []);
        Team c = new("C", []);
        Team d = new("D", []);

        PodiumStage<Team> stage = new(new List<Team> { a, b, c, d });

        var ranking = stage.GetRanking();

        Assert.Equal(3, ranking.Count);
        Assert.Equal(new[] { a, b, c }, ranking);
    }

    [Fact]
    public void PodiumStage_Returns_All_When_Less_Than_Three()
    {
        Team a = new("A", []);
        Team b = new("B", []);

        PodiumStage<Team> stage = new(new List<Team> { a, b });

        var ranking = stage.GetRanking();

        Assert.Equal(2, ranking.Count);
        Assert.Equal(new[] { a, b }, ranking);
    }

    [Fact]
    public void BracketStage_Should_Return_Winners()
    {
        Team a = new("A", []);
        Team b = new("B", []);
        Team c = new("C", []);
        Team d = new("D", []);

        BracketStage<Team, FootballMatch, FootballEvent, FootballScore> stage =
            new(
                new[] { a, b, c, d },
                new BracketPairingStrategy<Team>(),
                new FootballMatchFactory());

        Assert.Equal(2, stage.Matches.Count);

        stage.Matches[0].Start();
        stage.Matches[0].AddEvent(new GoalEvent(a));
        stage.Matches[0].AddEvent(new GoalEvent(a));
        stage.Matches[0].End();

        stage.Matches[1].Start();
        stage.Matches[1].AddEvent(new GoalEvent(b));
        stage.Matches[1].End();

        var ranking = stage.GetRanking();

        Assert.Contains(a, ranking);
        Assert.Contains(b, ranking);
        Assert.Equal(2, ranking.Count);
    }

    [Fact]
    public void GroupStage_Should_Return_Advancing_Teams_By_Score()
    {
        Team a = new("A", []);
        Team b = new("B", []);
        Team c = new("C", []);
        Team d = new("D", []);

        GroupStage<Team, FootballMatch, FootballEvent, FootballScore> stage =
            new(
                new[] { a, b, c, d },
                new SequentialGroupingStrategy<Team>(),
                new BracketPairingStrategy<Team>(),
                new FootballMatchFactory(),
                groupCount: 2,
                advancingPerGroup: 1);

        stage.Matches[0].Start();
        stage.Matches[0].AddEvent(new GoalEvent(a));
        stage.Matches[0].AddEvent(new GoalEvent(a));

        stage.Matches[1].Start();
        stage.Matches[1].AddEvent(new GoalEvent(d));

        var ranking = stage.GetRanking();

        Assert.Equal(2, ranking.Count);
        Assert.Equal(a, ranking[0]);
        Assert.Equal(d, ranking[1]);
    }

    [Fact]
    public void BracketPairing_Should_Create_Seeded_Pairs()
    {
        List<Team> participants =
        [
            new Team("1", []),
            new Team("2", []),
            new Team("3", []),
            new Team("4", []),
            new Team("5", []),
            new Team("6", []),
            new Team("7", []),
            new Team("8", [])
        ];

        BracketPairingStrategy<Team> strategy = new();

        var pairs = strategy.CreatePairs(participants);

        Assert.Contains((participants[0], participants[7]), pairs);
        Assert.Contains((participants[1], participants[6]), pairs);
        Assert.Contains((participants[2], participants[5]), pairs);
        Assert.Contains((participants[3], participants[4]), pairs);
    }
}
