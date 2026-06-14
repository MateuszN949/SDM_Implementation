using SportsCompetition.Core.Parties;
using SportsCompetition.Core.Scores;
using SportsCompetition.Core.States;

namespace SportsCompetition.Core.Events;

public class GoalEvent(Team party) : FootballEvent
{
    public override EventType EType { get; } = EventType.InProgressEvent;

    private readonly Team _party = party;

    public override void Apply(Dictionary<Team, FootballScore> scores)
    {
        scores[_party].AddGoal();
    }
}