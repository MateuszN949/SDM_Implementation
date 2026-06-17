using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Stages;

public interface IStage<TParty> where TParty : IParty
{
    IReadOnlyList<TParty> Participants { get; }

    IReadOnlyList<TParty> GetRanking();
}