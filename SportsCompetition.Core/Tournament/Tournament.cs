using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Stages;

namespace SportsCompetition.Core.Tournament;

public class Tournament<TParty> where TParty : IParty
{
    private readonly TournamentController<TParty> _controller = new();

    public IReadOnlyList<IStage<TParty>> Stages => _controller.Stages;

    public void AddStage(IStage<TParty> stage)
    {
        _controller.AddStage(stage);
    }

    public IStage<TParty> CurrentStage => _controller.CurrentStage;

    public IReadOnlyList<TParty> GetRanking() => _controller.GetRanking();

    public bool Advance()
    {
        return _controller.Advance();
    }
}