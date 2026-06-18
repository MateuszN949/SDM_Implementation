using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Stages;
using SportsCompetition.Core.Tournament;

namespace SportsCompetition.Tests;

public class TournamentControllerTests
{
    private class TestStage : IStage<Team>
    {
        private readonly List<Team> _participants = new();
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
        TournamentController<Team> controller = new();

        Team a = new("A", []);

        var stage = new TestStage([a], [a]);

        controller.AddStage(stage);

        Assert.Equal(0, controller.CurrentStageIndex);
        Assert.Same(stage, controller.CurrentStage);
    }

    [Fact]
    public void Advance_And_Rollback_Work()
    {
        TournamentController<Team> controller = new();

        Team a = new("A", []);
        Team b = new("B", []);
        Team c = new("C", []);

        var s1 = new TestStage([a], [a]);
        var s2 = new TestStage([b], [b]);
        var s3 = new TestStage([c], [c]);

        controller.AddStage(s1);
        controller.AddStage(s2);
        controller.AddStage(s3);

        Assert.Equal(0, controller.CurrentStageIndex);

        bool advanced = controller.Advance();
        Assert.True(advanced);
        Assert.Equal(1, controller.CurrentStageIndex);

        advanced = controller.Advance();
        Assert.True(advanced);
        Assert.Equal(2, controller.CurrentStageIndex);

        // cannot advance beyond last
        advanced = controller.Advance();
        Assert.False(advanced);
        Assert.Equal(2, controller.CurrentStageIndex);

        bool rolled = controller.Rollback();
        Assert.True(rolled);
        Assert.Equal(1, controller.CurrentStageIndex);

        rolled = controller.Rollback();
        Assert.True(rolled);
        Assert.Equal(0, controller.CurrentStageIndex);

        // cannot rollback beyond first
        rolled = controller.Rollback();
        Assert.False(rolled);
        Assert.Equal(0, controller.CurrentStageIndex);
    }

    [Fact]
    public void GetRanking_Delegates_To_CurrentStage()
    {
        TournamentController<Team> controller = new();

        Team a = new("A", []);
        Team b = new("B", []);

        var s1 = new TestStage([a], [a]);
        var s2 = new TestStage([b], [b]);

        controller.AddStage(s1);
        controller.AddStage(s2);

        // current is s1
        var rank1 = controller.GetRanking();
        Assert.Equal(new[] { a }, rank1);

        controller.Advance();

        var rank2 = controller.GetRanking();
        Assert.Equal(new[] { b }, rank2);
    }

    [Fact]
    public void StageChanged_Event_Fires()
    {
        TournamentController<Team> controller = new();

        Team a = new("A", []);
        Team b = new("B", []);

        var s1 = new TestStage([a], [a]);
        var s2 = new TestStage([b], [b]);

        List<int> notifications = [];

        controller.StageChanged += (idx, stage) => notifications.Add(idx);

        controller.AddStage(s1);
        controller.AddStage(s2);

        controller.Advance();

        Assert.Contains(0, notifications);
        Assert.Contains(1, notifications);
    }

    [Fact]
    public void CurrentStage_Throws_When_No_Stages()
    {
        TournamentController<Team> controller = new();

        Assert.Throws<InvalidOperationException>(() => _ = controller.CurrentStage);
    }
}
