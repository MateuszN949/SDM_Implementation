using SportsCompetition.Core.Events;

namespace SportsCompetition.Core.States;

public sealed class TimeOverState : IMatchState
{
    public bool CanAddParticipant => false;
    public bool CanAddEvent(EventType eventType)
    {
        return eventType == EventType.OvertimeEvent;
    }
}