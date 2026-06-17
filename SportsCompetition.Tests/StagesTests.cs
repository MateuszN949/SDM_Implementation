using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Stages;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.Strategies.Pairing;

namespace SportsCompetition.Tests;

public class StagesTests
{
    // Not yet updated
    // [Fact]
    // public void BracketStage_Should_Rank_Winners()
    // {
    //     Team a = new("A", []);
    //     Team b = new("B", []);
    //     Team c = new("C", []);
    //     Team d = new("D", []);

    //     FootballMatch m1 = new();
    //     m1.AddParticipant(a);
    //     m1.AddParticipant(b);
    //     m1.Start();

    //     m1.AddEvent(new GoalEvent(a));
    //     m1.End();

    //     FootballMatch m2 = new();
    //     m2.AddParticipant(c);
    //     m2.AddParticipant(d);
    //     m2.Start();

    //     m2.AddEvent(new GoalEvent(d));
    //     m2.End();

    //     BracketStage<Team, FootballMatch, FootballEvent, FootballScore> stage = new([a, b, c, d], [m1, m2]);

    //     IReadOnlyList<Team> ranking =
    //         stage.GetRanking();

    //     Assert.Contains(a, ranking);
    //     Assert.Contains(d, ranking);
    // }

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

        var pairs =
            strategy.CreatePairs(participants);

        Assert.Contains((participants[0], participants[7]), pairs);
        Assert.Contains((participants[1], participants[6]), pairs);
        Assert.Contains((participants[2], participants[5]), pairs);
        Assert.Contains((participants[3], participants[4]), pairs);
    }
}
