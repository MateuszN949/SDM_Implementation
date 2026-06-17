using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Stages;

public class PodiumStage<TParty>(List<TParty> participants) : IStage<TParty> where TParty : IParty
{
    private readonly List<TParty> _particpants = participants;
    public IReadOnlyList<TParty> Participants => _particpants;

    public IReadOnlyList<TParty> GetRanking()
    {
        if (!Participants.Any())
            throw new InvalidOperationException("Cannot return ranking from an empty tournament.");
        
        if (Participants.Count >= 3)
            return [.. Participants.Take(3)];
        else
            return Participants;
    }
}