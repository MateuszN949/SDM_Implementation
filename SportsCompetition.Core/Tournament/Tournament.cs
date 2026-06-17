using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Stages;

namespace SportsCompetition.Core.Tournament;

public class Tournament<TParty> where TParty : IParty
{
    private readonly List<IStage<TParty>> _stages = [];

    public IReadOnlyList<IStage<TParty>> Stages => _stages;

    public void AddStage(IStage<TParty> stage)
    {
        _stages.Add(stage);
    }

    public IStage<TParty> CurrentStage
    {
        get
        {
            if (_stages.Count == 0)
                throw new InvalidOperationException("Tournament contains no stages.");

            return _stages.Last();
        }
    }

    public IReadOnlyList<TParty> GetRanking()
    {
        return CurrentStage.GetRanking();
    }
}