using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Events;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;

namespace SportsCompetition.Core.Matches;

public interface IMatch<TEvent, TScore, TParty>
    where TEvent : IEvent<TScore, TParty>
    where TScore : class, IScore<TScore>
    where TParty : IParty
{
    IReadOnlyList<TParty> Participants { get; }

    IReadOnlyList<TEvent> Events { get; }

    IReadOnlyDictionary<TParty, TScore> Scores { get; }

    public IMatchState State { get; }

    void AddParticipant(TParty participant);

    void AddEvent(TEvent matchEvent);

    void Start();

    void TimeOver();

    void End();
}