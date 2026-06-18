using System.Linq;
using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Strategies.Pairing;

namespace SportsCompetition.Tests;

public class StrategyTests
{
    [Fact]
    public void RoundRobinPairing_Creates_All_Pairs()
    {
        List<Team> participants =
        [
            new("A", []),
            new("B", []),
            new("C", []),
            new("D", [])
        ];

        RoundRobinPairingStrategy<Team> strategy = new();

        var pairs = strategy.CreatePairs(participants);

        Assert.Equal(6, pairs.Count);
        Assert.Contains((participants[0], participants[1]), pairs);
        Assert.Contains((participants[0], participants[2]), pairs);
        Assert.Contains((participants[0], participants[3]), pairs);
        Assert.Contains((participants[1], participants[2]), pairs);
        Assert.Contains((participants[1], participants[3]), pairs);
        Assert.Contains((participants[2], participants[3]), pairs);
    }

    [Fact]
    public void RandomPairing_Creates_Each_Participant_Exactly_Once()
    {
        List<Team> participants =
        [
            new("A", []),
            new("B", []),
            new("C", []),
            new("D", [])
        ];

        RandomPairingStrategy<Team> strategy = new();

        var pairs = strategy.CreatePairs(participants);

        Assert.Equal(2, pairs.Count);

        var allParticipants = pairs.SelectMany(pair => new[] { pair.Item1, pair.Item2 });
        Assert.Equal(4, allParticipants.Distinct().Count());
        Assert.All(participants, participant => Assert.Contains(participant, allParticipants));
    }
}
