using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Stages;
using SportsCompetition.Core.Tournament;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Strategies.Pairing;
using SportsCompetition.Core.Factory;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Tests;

public class TournamentTests
{
    private class TestStage : IStage<Team>
    {
        private readonly List<Team> _participants = [];
        private readonly List<Team> _ranking;

        public TestStage(IEnumerable<Team> participants, IEnumerable<Team> ranking)
        {
            _participants.AddRange(participants);
            _ranking = new List<Team>(ranking);
        }

        public IReadOnlyList<Team> Participants => _participants;

        public IReadOnlyList<Team> GetRanking() => _ranking;
    }

    [Fact]
    public void AddStage_Sets_CurrentStage()
    {
        Tournament<Team> tournament = new();
        Team a = new("A", []);
        var stage = new TestStage([a], [a]);

        tournament.AddStage(stage);

        Assert.Same(stage, tournament.CurrentStage);
        Assert.Single(tournament.Stages);
    }

    [Fact]
    public void GetRanking_Delegates_To_CurrentStage()
    {
        Tournament<Team> tournament = new();
        Team a = new("A", []);
        Team b = new("B", []);

        var stage1 = new TestStage([a], [a]);
        var stage2 = new TestStage([b], [b]);

        tournament.AddStage(stage1);
        tournament.AddStage(stage2);

        var ranking1 = tournament.GetRanking();
        Assert.Equal(new[] { a }, ranking1);
        Assert.Same(stage1, tournament.CurrentStage);
        Assert.Equal(2, tournament.Stages.Count);
    }
}
