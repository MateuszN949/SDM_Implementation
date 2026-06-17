using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Strategies.Pairing;

public class RoundRobinPairingStrategy<TParty> : IPairingStrategy<TParty> where TParty : IParty
{
    public IReadOnlyList<(TParty, TParty)> CreatePairs(IReadOnlyList<TParty> participants)
    {
        List<(TParty, TParty)> pairs = [];

        for (int i = 0; i < participants.Count; i++)
        {
            for (int j = i + 1; j < participants.Count; j++)
                pairs.Add((participants[i], participants[j]));
        }

        return pairs;
    }
}