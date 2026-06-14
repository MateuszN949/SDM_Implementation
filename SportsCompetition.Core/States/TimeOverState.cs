using SportsCompetition.Core.Events;

namespace SportsCompetition.Core.States;

public sealed class TimeOverState : IMatchState
{
    public bool CanAddParticipant => false;
    public bool CanAddEvent(object ev)
    {
        return ev is ITimeOverEvent;
    }
}