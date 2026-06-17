using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Strategies.Pairing;

public class RandomPairingStrategy<TParty> : IPairingStrategy<TParty> where TParty : IParty
{
    private readonly Random _random = new();

    public IReadOnlyList<(TParty, TParty)> CreatePairs(IReadOnlyList<TParty> participants)
    {
        List<TParty> shuffled = [.. participants.OrderBy(_ => _random.Next())];

        List<(TParty, TParty)> pairs = [];

        for (int i = 0; i < shuffled.Count; i += 2)
            pairs.Add((shuffled[i], shuffled[i + 1]));

        return pairs;
    }
}