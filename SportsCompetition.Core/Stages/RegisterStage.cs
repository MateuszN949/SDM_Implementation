using SportsCompetition.Core.Parties;

namespace SportsCompetition.Core.Stages;

public class RegisterStage<TParty>(List<TParty>? participants = null) : IStage<TParty> where TParty : IParty
{
    private readonly List<TParty> _particpants = (participants is null) ? [] : participants;
    public IReadOnlyList<TParty> Participants => _particpants;

    public IReadOnlyList<TParty> GetRanking()
    {
        if (!Participants.Any())
            throw new InvalidOperationException("Cannot return ranking from an empty tournament.");
        
        return Participants;
    }

    public void AddParticipant(TParty party)
    {
        _particpants.Add(party);
    }

    public void AddParticipants(IEnumerable<TParty> parties)
    {
        _particpants.AddRange(parties);
    }
}