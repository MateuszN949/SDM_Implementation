using SportsCompetition.Core.Events;
using SportsCompetition.Core.Matches;
using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;

namespace SportsCompetition.Core.Stages;

public abstract class PlayStage<TParty, TMatch, TEvent, TScore> : IStage<TParty>
where TMatch : IMatch<TEvent, TScore, TParty>
where TEvent : IEvent<TScore, TParty>
where TScore : class, IScore<TScore>
where TParty : IParty
{
    protected abstract List<TParty> _particpants { get; }
    public IReadOnlyList<TParty> Participants => _particpants;

    public abstract List<TMatch> Matches { get; }
    // public List<TMatch> Matches => _matches;

    public abstract IReadOnlyList<TParty> GetRanking();
}