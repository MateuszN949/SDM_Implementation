using SportsCompetition.Core.Events;

namespace SportsCompetition.Core.States;

public sealed class CreatingState : IMatchState
{
    public bool CanAddParticipant => true;
    bool IMatchState.CanAddEvent(object ev) => false;
}