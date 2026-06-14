using SportsCompetition.Core.Events;

namespace SportsCompetition.Core.States;

public sealed class CompletedState : IMatchState
{
    public bool CanAddParticipant => false;

    bool IMatchState.CanAddEvent(object ev) => false;
}