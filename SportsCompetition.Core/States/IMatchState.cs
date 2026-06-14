using SportsCompetition.Core.Events;

namespace SportsCompetition.Core.States;

public interface IMatchState
{
    bool CanAddParticipant { get; }
    bool CanAddEvent(object ev);
}