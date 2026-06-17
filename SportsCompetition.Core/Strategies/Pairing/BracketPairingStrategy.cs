using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Strategies.Pairing;

public class BracketPairingStrategy<TParty> : IPairingStrategy<TParty> where TParty : IParty
{
    public IReadOnlyList<(TParty, TParty)> CreatePairs(IReadOnlyList<TParty> participants)
    {
        if (participants.Count % 2 != 0)
            throw new InvalidOperationException("Bracket requires an even number of participants.");

        List<(TParty, TParty)> pairs = [];

        for (int i = 0; i < participants.Count / 2; i++)
            pairs.Add((participants[i], participants[participants.Count - 1 - i]));

        return pairs;
    }
}