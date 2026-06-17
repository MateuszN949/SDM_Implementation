using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Strategies.Pairing;

public interface IPairingStrategy<TParty> where TParty : IParty
{
    IReadOnlyList<(TParty, TParty)> CreatePairs(IReadOnlyList<TParty> participants);
}