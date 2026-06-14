using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;

namespace SportsCompetition.Core.Events;

public enum EventType
{
    InProgressEvent,
    OvertimeEvent
};

public interface IEvent<TScore, TParty>
    where TScore : IScore<TScore>
    where TParty : IParty
{
    EventType EType { get; }

    void Apply(Dictionary<TParty, TScore> scores);
}